--стоимость жилья
create function LivingValues(@tour int)
returns table
as
return
(
	select @tour as tour,max(Стоимость) as max,Min(Стоимость) as min from Номер_в_туре 
);
go

--цены на услуги
create function ServicesValues(@tour int)
returns table
as
return
(
select  @tour as tour,(select count(*) from Услуга_тура where @tour = Код_тура) as Общее_число_услуг,
(select count(*) from Услуга_тура where @tour = Код_тура and Входит_в_стоимость = 0) as Число_доп_услуг,
sum(Стоимость) as Стоимость_доп_услуг from Услуга_тура
where @tour=1);
go

--сведение цен в одну таблицу
go
create function TourFullData(@tour int)
returns table as
return(
select Код_тура,Название,(select max(День_начала) from Маршрут where Код_тура = @tour) as Длительность,
(select case when Активный = 1 then 'Активный' else 'Пассивный' end from Тур where Тур.Код_тура = @tour) as Активность,
Базовая_стоимость,case when min = 0 then 'Жильё включено в стоимость' else cast(min as nvarchar(100)) end as Минимальная_стоимость_проживания,
case when max = 0 then 'Жильё включено в стоимость и выбору не подлежит' else cast(max as nvarchar(100)) end as Максимальная_стоимость_проживания,
case when Включён_перелёт = 1 then 'Перелёт оплачен' else 'Требуется доплата за перелёт' end as Стоимость_перелёта,
Стоимость_доп_услуг,
min+Базовая_стоимость as Итоговая_минимальная_стоимость,
max+Базовая_стоимость+Стоимость_доп_услуг as Итоговая_Максимальная_стоимость
from Тур 
inner join (select * from LivingValues(@tour))t1 on t1.tour = Тур.Код_тура
inner join (select * from ServicesValues(@tour))t2 on t2.tour = Тур.Код_тура);
go

go
create procedure CountParameters @date date, @main_parameter int
as
if @main_parameter = 0
begin
	select t.* from Даты_начала
	cross apply TourFullData(Даты_начала.Код_тура) t
	where Дата_начала = @date
	order by newid()
end
else if 
@main_parameter = 1
begin
	select t.* from Даты_начала
	cross apply TourFullData(Даты_начала.Код_тура) t
	where Дата_начала = @date
	order by Итоговая_минимальная_стоимость,Стоимость_перелёта, newid()
end
else if 
@main_parameter = 2
begin
	select t.* from Даты_начала
	cross apply TourFullData(Даты_начала.Код_тура) t
	where Дата_начала = @date
	order by Длительность,newid()
end
else if
@main_parameter = 3
begin
	select t.* from Даты_начала
	cross apply TourFullData(Даты_начала.Код_тура) t
	where Дата_начала = @date
	order by Активность,newid()
end
else
	RAISERROR (15600,-1,-1,'Неверно задан ключевой параметр');
go


--далее в приложении выбрали тур, надо добавить услуги и номера в отеле
--если в отеле все номера по 0, то скипаем, иначе:
--есть случай, когда выбрать жильё не дают, это будет поле в таблице Тур
--показывает отели, если тур позволяет их выбирать
create function ShowHotels(@tour int)
returns table
return
(select * from Номер_в_туре where Код_тура = @tour and (select Выбор_жилья from Тур where Код_тура = @tour) = 1);
go

--показывает сервисы, которые пользователь может добавить, остальные добавляются автоматически
create function ShowServices(@tour int)
returns table
return
(select * from Услуга_тура where Код_тура = @tour and Входит_в_стоимость = 0);
go

create procedure AddTicket @document nvarchar(20), @tour int
as
	insert into Билет values(@document,@tour)

	insert into Услуга_туриста
	select Организация,Код_тура,(select max(Номер_билета) from Билет),Название_услуги from Услуга_тура 
	where 1 = Код_тура and Входит_в_стоимость = 1

	if(select Выбор_жилья from Тур where Код_тура = @tour) = 0
	begin
		insert into Номер_туриста

		select (select max(Номер_билета) from Билет), Код_отеля,Код_тура,Тип_номера from Номер_в_туре where Код_тура = @tour
	end
go


create procedure AddHotelToTicket @hotel int, @tour int, @type nvarchar(50)
as
	insert into Номер_туриста values((select max(Номер_билета) from Билет),@hotel, @tour,@type)
go


alter procedure AddServiceToTicket @org nvarchar(50),@tour int, @service nvarchar(200)
as
	insert into Услуга_туриста values(@org,@tour,(select max(Номер_билета) from Билет),@service)
go

--подсчёт итоговой суммы за билет
create function CountPrice(@ticket int)
returns table
return(
	select
	(select sum(Стоимость) from  Номер_в_туре inner join Номер_туриста
	on Номер_туриста.Код_отеля = Номер_в_туре.Код_отеля
	and Номер_туриста.Код_тура = Номер_в_туре.Код_тура
	and Номер_туриста.Тип_номера = Номер_в_туре.Тип_номера
	where Номер_билета = @ticket)
	+
	(select sum(Стоимость) from  Услуга_тура inner join Услуга_туриста
	on Услуга_тура.Организация = Услуга_туриста.Организация
	and Услуга_тура.Код_тура = Услуга_туриста.Код_тура
	and Услуга_тура.Название_услуги = Услуга_туриста.Название_услуги
	where Номер_билета = @ticket and Входит_в_стоимость = 0)
	+
	(select Базовая_стоимость from Тур where Код_тура = (select Код_тура from Билет where Номер_билета = @ticket)) as Итоговая_сумма
)
go