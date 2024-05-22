--��������� �����
create function LivingValues(@tour int)
returns table
as
return
(
	select @tour as tour,max(���������) as max,Min(���������) as min from �����_�_���� 
);
go

--���� �� ������
create function ServicesValues(@tour int)
returns table
as
return
(
select  @tour as tour,(select count(*) from ������_���� where @tour = ���_����) as �����_�����_�����,
(select count(*) from ������_���� where @tour = ���_���� and ������_�_��������� = 0) as �����_���_�����,
sum(���������) as ���������_���_����� from ������_����
where @tour=1);
go

--�������� ��� � ���� �������
go
create function TourFullData(@tour int)
returns table as
return(
select ���_����,��������,(select max(����_������) from ������� where ���_���� = @tour) as ������������,
(select case when �������� = 1 then '��������' else '���������' end from ��� where ���.���_���� = @tour) as ����������,
�������_���������,case when min = 0 then '����� �������� � ���������' else cast(min as nvarchar(100)) end as �����������_���������_����������,
case when max = 0 then '����� �������� � ��������� � ������ �� ��������' else cast(max as nvarchar(100)) end as ������������_���������_����������,
case when �������_������ = 1 then '������ �������' else '��������� ������� �� ������' end as ���������_�������,
���������_���_�����,
min+�������_��������� as ��������_�����������_���������,
max+�������_���������+���������_���_����� as ��������_������������_���������
from ��� 
inner join (select * from LivingValues(@tour))t1 on t1.tour = ���.���_����
inner join (select * from ServicesValues(@tour))t2 on t2.tour = ���.���_����);
go

go
create procedure CountParameters @date date, @main_parameter int
as
if @main_parameter = 0
begin
	select t.* from ����_������
	cross apply TourFullData(����_������.���_����) t
	where ����_������ = @date
	order by newid()
end
else if 
@main_parameter = 1
begin
	select t.* from ����_������
	cross apply TourFullData(����_������.���_����) t
	where ����_������ = @date
	order by ��������_�����������_���������,���������_�������, newid()
end
else if 
@main_parameter = 2
begin
	select t.* from ����_������
	cross apply TourFullData(����_������.���_����) t
	where ����_������ = @date
	order by ������������,newid()
end
else if
@main_parameter = 3
begin
	select t.* from ����_������
	cross apply TourFullData(����_������.���_����) t
	where ����_������ = @date
	order by ����������,newid()
end
else
	RAISERROR (15600,-1,-1,'������� ����� �������� ��������');
go


--����� � ���������� ������� ���, ���� �������� ������ � ������ � �����
--���� � ����� ��� ������ �� 0, �� �������, �����:
--���� ������, ����� ������� ����� �� ����, ��� ����� ���� � ������� ���
--���������� �����, ���� ��� ��������� �� ��������
create function ShowHotels(@tour int)
returns table
return
(select * from �����_�_���� where ���_���� = @tour and (select �����_����� from ��� where ���_���� = @tour) = 1);
go

--���������� �������, ������� ������������ ����� ��������, ��������� ����������� �������������
create function ShowServices(@tour int)
returns table
return
(select * from ������_���� where ���_���� = @tour and ������_�_��������� = 0);
go

create procedure AddTicket @document nvarchar(20), @tour int
as
	insert into ����� values(@document,@tour)

	insert into ������_�������
	select �����������,���_����,(select max(�����_������) from �����),��������_������ from ������_���� 
	where 1 = ���_���� and ������_�_��������� = 1

	if(select �����_����� from ��� where ���_���� = @tour) = 0
	begin
		insert into �����_�������

		select (select max(�����_������) from �����), ���_�����,���_����,���_������ from �����_�_���� where ���_���� = @tour
	end
go


create procedure AddHotelToTicket @hotel int, @tour int, @type nvarchar(50)
as
	insert into �����_������� values((select max(�����_������) from �����),@hotel, @tour,@type)
go


alter procedure AddServiceToTicket @org nvarchar(50),@tour int, @service nvarchar(200)
as
	insert into ������_������� values(@org,@tour,(select max(�����_������) from �����),@service)
go

--������� �������� ����� �� �����
create function CountPrice(@ticket int)
returns table
return(
	select
	(select sum(���������) from  �����_�_���� inner join �����_�������
	on �����_�������.���_����� = �����_�_����.���_�����
	and �����_�������.���_���� = �����_�_����.���_����
	and �����_�������.���_������ = �����_�_����.���_������
	where �����_������ = @ticket)
	+
	(select sum(���������) from  ������_���� inner join ������_�������
	on ������_����.����������� = ������_�������.�����������
	and ������_����.���_���� = ������_�������.���_����
	and ������_����.��������_������ = ������_�������.��������_������
	where �����_������ = @ticket and ������_�_��������� = 0)
	+
	(select �������_��������� from ��� where ���_���� = (select ���_���� from ����� where �����_������ = @ticket)) as ��������_�����
)
go