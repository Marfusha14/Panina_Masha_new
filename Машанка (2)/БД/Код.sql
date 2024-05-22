use master
go
drop database if exists [название бд]
go
create database [название бд]
go
use [название бд]
go
/*
Created: 14.11.2023
Modified: 18.11.2023
Model: Full model
Database: MS SQL Server 2019
*/


-- Create tables section -------------------------------------------------

-- Table Сеанс

CREATE TABLE [Сеанс]
(
 [Номер_кинотеатра] Int NOT NULL,
 [Код_фильма] Int NOT NULL,
 [Дата_проведения] Datetime NOT NULL,
 [Базовая_стоимость] Float NOT NULL
)
go

-- Add keys for table Сеанс

ALTER TABLE [Сеанс] ADD CONSTRAINT [Unique_Identifier1] PRIMARY KEY ([Номер_кинотеатра],[Код_фильма],[Дата_проведения])
go

-- Table Клиент

CREATE TABLE [Клиент]
(
 [Код_клиента] Int NOT NULL,
 [Фамилия] Nvarchar(max) NOT NULL,
 [Имя] Nvarchar(max) NOT NULL,
 [Отчество] Nvarchar(max) NULL,
 [Дата_рождения] Nvarchar(max) NOT NULL,
 [Пол] Nvarchar(max) NOT NULL,
 [Роль_в_системе] Nvarchar(max) NOT NULL,
 [Логин] Nvarchar(max) NOT NULL,
 [Пароль] Nvarchar(max) NOT NULL,
 [Город] Nvarchar(max) NOT NULL,
 [Количество_баллов] Int NOT NULL
)
go

-- Add keys for table Клиент

ALTER TABLE [Клиент] ADD CONSTRAINT [Unique_Identifier2] PRIMARY KEY ([Код_клиента])
go

-- Table Кинотеатр

CREATE TABLE [Кинотеатр]
(
 [Номер_кинотеатра] Int NOT NULL,
 [Название] Nvarchar(max) NOT NULL,
 [Город] Nvarchar(max) NOT NULL,
 [Улица] Nvarchar(max) NOT NULL,
 [Номер_дома] Int NOT NULL,
 [Строение] Int NULL
)
go

-- Add keys for table Кинотеатр

ALTER TABLE [Кинотеатр] ADD CONSTRAINT [Unique_Identifier3] PRIMARY KEY ([Номер_кинотеатра])
go

-- Table Фильм

CREATE TABLE [Фильм]
(
 [Код_фильма] Int NOT NULL,
 [Название] Nvarchar(max) NOT NULL,
 [Год_выпуска] Nvarchar(max) NOT NULL,
 [Режиссёры] Nvarchar(max) NOT NULL,
 [Актёры] Nvarchar(max) NOT NULL,
 [Обложка] Nvarchar(max) NULL
)
go

-- Add keys for table Фильм

ALTER TABLE [Фильм] ADD CONSTRAINT [Unique_Identifier4] PRIMARY KEY ([Код_фильма])
go

-- Table Услуга

CREATE TABLE [Услуга]
(
 [Название_услуги] Nvarchar(50) NOT NULL,
 [Описание] Nvarchar(max) NULL
)
go

-- Add keys for table Услуга

ALTER TABLE [Услуга] ADD CONSTRAINT [Unique_Identifier5] PRIMARY KEY ([Название_услуги])
go

-- Table Дополнительная услуга

CREATE TABLE [Дополнительная услуга]
(
 [Номер_кинотеатра] Int NOT NULL,
 [Название_услуги] Nvarchar(50) NOT NULL,
 [Стоимость] Float NOT NULL,
 [Доп_информация] Nvarchar(max) NOT NULL
)
go

-- Add keys for table Дополнительная услуга

ALTER TABLE [Дополнительная услуга] ADD CONSTRAINT [PK_Дополнительная услуга] PRIMARY KEY ([Номер_кинотеатра],[Название_услуги],[Стоимость])
go

-- Table Билет

CREATE TABLE [Билет]
(
 [Номер_кинотеатра] Int NOT NULL,
 [ID_клиента] Int NOT NULL,
 [Код_фильма] Int NOT NULL,
 [Дата_проведения] Datetime NOT NULL,
 [Количество] Int NOT NULL
)
go

-- Add keys for table Билет

ALTER TABLE [Билет] ADD CONSTRAINT [PK_Билет] PRIMARY KEY ([Номер_кинотеатра],[ID_клиента],[Код_фильма],[Дата_проведения])
go

-- Table Жанр

CREATE TABLE [Жанр]
(
 [Название_жанра] Nvarchar(50) NOT NULL,
 [Описание] Nvarchar(max) NULL
)
go

-- Add keys for table Жанр

ALTER TABLE [Жанр] ADD CONSTRAINT [PK_Жанр] PRIMARY KEY ([Название_жанра])
go

-- Table Жанры_кино

CREATE TABLE [Жанры_кино]
(
 [Код_фильма] Int NOT NULL,
 [Название_жанра] Nvarchar(50) NOT NULL
)
go

-- Add keys for table Жанры_кино

ALTER TABLE [Жанры_кино] ADD CONSTRAINT [PK_Жанры_кино] PRIMARY KEY ([Код_фильма],[Название_жанра])
go

-- Table Круг_интересов

CREATE TABLE [Круг_интересов]
(
 [Код_клиента] Int NOT NULL,
 [Название_жанра] Nvarchar(50) NOT NULL
)
go

-- Add keys for table Круг_интересов

ALTER TABLE [Круг_интересов] ADD CONSTRAINT [PK_Круг_интересов] PRIMARY KEY ([Код_клиента],[Название_жанра])
go

-- Table Работник

CREATE TABLE [Работник]
(
 [Код_клиента] Int NOT NULL,
 [Номер_кинотеатра] Int NOT NULL,
 [Номер_договора] Nvarchar(15) NOT NULL,
 [Должность] Nvarchar(15) NOT NULL
)
go

-- Add keys for table Работник

ALTER TABLE [Работник] ADD CONSTRAINT [PK_Работник] PRIMARY KEY ([Код_клиента],[Номер_кинотеатра])
go

-- Table Услуга_билета

CREATE TABLE [Услуга_билета]
(
 [Номер_кинотеатра] Int NOT NULL,
 [ID_клиента] Int NOT NULL,
 [Код_фильма] Int NOT NULL,
 [Дата_проведения] Datetime NOT NULL,
 [Название_услуги] Nvarchar(50) NOT NULL,
 [Стоимость] Float NOT NULL,
 [Количество] Int NOT NULL
)
go

-- Add keys for table Услуга_билета

ALTER TABLE [Услуга_билета] ADD CONSTRAINT [PK_Услуга_билета] PRIMARY KEY ([Номер_кинотеатра],[ID_клиента],[Код_фильма],[Дата_проведения],[Название_услуги],[Стоимость])
go

-- Create foreign keys (relationships) section ------------------------------------------------- 


ALTER TABLE [Сеанс] ADD CONSTRAINT [Показывается] FOREIGN KEY ([Код_фильма]) REFERENCES [Фильм] ([Код_фильма]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Сеанс] ADD CONSTRAINT [Проводит] FOREIGN KEY ([Номер_кинотеатра]) REFERENCES [Кинотеатр] ([Номер_кинотеатра]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Дополнительная услуга] ADD CONSTRAINT [Предоставляет] FOREIGN KEY ([Номер_кинотеатра]) REFERENCES [Кинотеатр] ([Номер_кинотеатра]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Дополнительная услуга] ADD CONSTRAINT [Предоставляется] FOREIGN KEY ([Название_услуги]) REFERENCES [Услуга] ([Название_услуги]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Билет] ADD CONSTRAINT [Посещает] FOREIGN KEY ([ID_клиента]) REFERENCES [Клиент] ([Код_клиента]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Билет] ADD CONSTRAINT [Посещается] FOREIGN KEY ([Номер_кинотеатра], [Код_фильма], [Дата_проведения]) REFERENCES [Сеанс] ([Номер_кинотеатра], [Код_фильма], [Дата_проведения]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Жанры_кино] ADD CONSTRAINT [Принадлежит] FOREIGN KEY ([Код_фильма]) REFERENCES [Фильм] ([Код_фильма]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Жанры_кино] ADD CONSTRAINT [Определяет] FOREIGN KEY ([Название_жанра]) REFERENCES [Жанр] ([Название_жанра]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Круг_интересов] ADD CONSTRAINT [Имеет] FOREIGN KEY ([Код_клиента]) REFERENCES [Клиент] ([Код_клиента]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Круг_интересов] ADD CONSTRAINT [Относится] FOREIGN KEY ([Название_жанра]) REFERENCES [Жанр] ([Название_жанра]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Работник] ADD CONSTRAINT [Работает] FOREIGN KEY ([Код_клиента]) REFERENCES [Клиент] ([Код_клиента]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Работник] ADD CONSTRAINT [Устраивает] FOREIGN KEY ([Номер_кинотеатра]) REFERENCES [Кинотеатр] ([Номер_кинотеатра]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Услуга_билета] ADD CONSTRAINT [Включает] FOREIGN KEY ([Номер_кинотеатра], [ID_клиента], [Код_фильма], [Дата_проведения]) REFERENCES [Билет] ([Номер_кинотеатра], [ID_клиента], [Код_фильма], [Дата_проведения]) ON UPDATE CASCADE ON DELETE CASCADE
go



ALTER TABLE [Услуга_билета] ADD CONSTRAINT [Входит] FOREIGN KEY ([Номер_кинотеатра], [Название_услуги], [Стоимость]) REFERENCES [Дополнительная услуга] ([Номер_кинотеатра], [Название_услуги], [Стоимость]) ON UPDATE NO ACTION ON DELETE NO ACTION
go





create trigger NoMsTrigger on Сеанс after Insert,Update,Delete 
as
	update Сеанс set Дата_проведения=dateadd(ms, -datepart(ms,Дата_проведения),Дата_проведения)
go
Enable trigger NoMsTrigger on Сеанс