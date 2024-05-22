/*
Created: 11.12.2023
Modified: 12.12.2023
Model: Model2
Database: MS SQL Server 2019
*/



-- Create tables section -------------------------------------------------

-- Table Туристическое агентство

CREATE TABLE [Туристическое агентство]
(
 [Организация_туроператора] Nvarchar(50) NOT NULL,
 [Название] Nvarchar(50) NOT NULL,
 [Описание] Nvarchar(500) NOT NULL
)
go

-- Add keys for table Туристическое агентство

ALTER TABLE [Туристическое агентство] ADD CONSTRAINT [Unique_Identifier1] PRIMARY KEY ([Организация_туроператора])
go

-- Table Достопримечательность

CREATE TABLE [Достопримечательность]
(
 [Код_достопримечательности] Int NOT NULL,
 [Название] Nvarchar(100) NOT NULL,
 [Описание] Nvarchar(500) NOT NULL,
 [Широта] Float NOT NULL,
 [Долгота] Float NOT NULL
)
go

-- Add keys for table Достопримечательность

ALTER TABLE [Достопримечательность] ADD CONSTRAINT [Unique_Identifier2] PRIMARY KEY ([Код_достопримечательности])
go

-- Table Транспорт

CREATE TABLE [Транспорт]
(
 [Код_ТС] Int NOT NULL,
 [Название_ТС] Nvarchar(50) NOT NULL
)
go

-- Add keys for table Транспорт

ALTER TABLE [Транспорт] ADD CONSTRAINT [Unique_Identifier3] PRIMARY KEY ([Код_ТС])
go

-- Table Место_жительства

CREATE TABLE [Место_жительства]
(
 [Код_отеля] Int NOT NULL,
 [Название] Nvarchar(500) NOT NULL,
 [Количество_звёзд] Int NOT NULL,
 [Страна] Nvarchar(50) NOT NULL,
 [Город] Nvarchar(50) NOT NULL,
 [Улица] Nvarchar(100) NOT NULL
)
go

-- Add keys for table Место_жительства

ALTER TABLE [Место_жительства] ADD CONSTRAINT [Unique_Identifier4] PRIMARY KEY ([Код_отеля])
go

-- Table Билет

CREATE TABLE [Билет]
(
 [Номер_билета] Int IDENTITY,
 [Номер_документа] Nvarchar(20) NOT NULL,
 [Код_тура] Int NOT NULL
)
go

-- Create indexes for table Билет

CREATE INDEX [IX_Содержится] ON [Билет] ([Код_тура])
go

CREATE INDEX [IX_Приобретает] ON [Билет] ([Номер_документа])
go

-- Add keys for table Билет

ALTER TABLE [Билет] ADD CONSTRAINT [Unique_Identifier5] PRIMARY KEY ([Номер_билета])
go

-- Table Ценообразование

CREATE TABLE [Ценообразование]
(
 [Код_тура] Int NOT NULL,
 [Тип_билета] Nvarchar(50) NOT NULL,
 [Стоимость] Nvarchar(50) NOT NULL
)
go

-- Add keys for table Ценообразование

ALTER TABLE [Ценообразование] ADD CONSTRAINT [Unique_Identifier6] PRIMARY KEY ([Код_тура],[Тип_билета])
go

-- Table Турист

CREATE TABLE [Турист]
(
 [Номер_документа] Nvarchar(20) NOT NULL,
 [Тип_документа] Nvarchar(40) NOT NULL,
 [Фамилия] Nvarchar(40) NOT NULL,
 [Имя] Nvarchar(40) NOT NULL,
 [Отчество] Nvarchar(40) NULL,
 [Дата_рождения] Date NOT NULL
)
go

-- Add keys for table Турист

ALTER TABLE [Турист] ADD CONSTRAINT [Unique_Identifier7] PRIMARY KEY ([Номер_документа])
go

-- Table Маршрут

CREATE TABLE [Маршрут]
(
 [Код_тура] Int NOT NULL,
 [Порядковый номер] Int NOT NULL,
 [Название_активности] Nvarchar(50) NOT NULL,
 [День_начала] Int NOT NULL,
 [Время_начала] Time NOT NULL,
 [Время_окончания] Time NOT NULL,
 [Инструкция] Nvarchar(1000) NOT NULL,
 [Код_ТС] Int NULL
)
go

-- Create indexes for table Маршрут

CREATE INDEX [IX_Relationship8] ON [Маршрут] ([Код_ТС])
go

-- Add keys for table Маршрут

ALTER TABLE [Маршрут] ADD CONSTRAINT [Unique_Identifier8] PRIMARY KEY ([Порядковый номер],[Код_тура])
go

-- Table Поставщик

CREATE TABLE [Поставщик]
(
 [Организация] Nvarchar(50) NOT NULL,
 [Описание] Nvarchar(500) NOT NULL
)
go

-- Add keys for table Поставщик

ALTER TABLE [Поставщик] ADD CONSTRAINT [Unique_Identifier9] PRIMARY KEY ([Организация])
go

-- Table Тур

CREATE TABLE [Тур]
(
 [Код_тура] Int NOT NULL,
 [Название] Nvarchar(50) NOT NULL,
 [Описание] Nvarchar(500) NOT NULL,
 [Активный] Bit NOT NULL,
 [Базовая_стоимость] Float NOT NULL,
 [Включён_перелёт] Bit NOT NULL,
 [Выбор_жилья] Bit NOT NULL,
 [Организация_туроператора] Nvarchar(50) NOT NULL,
 [Ссылка_на_тур] Nvarchar(500) NOT NULL
)
go

-- Create indexes for table Тур

CREATE INDEX [IX_Предлагает] ON [Тур] ([Организация_туроператора])
go

-- Add keys for table Тур

ALTER TABLE [Тур] ADD CONSTRAINT [Unique_Identifier10] PRIMARY KEY ([Код_тура])
go

-- Table Номер_туриста

CREATE TABLE [Номер_туриста]
(
 [Номер_билета] Int NOT NULL,
 [Код_отеля] Int NOT NULL,
 [Код_тура] Int NOT NULL,
 [Тип_номера] Nvarchar(50) NOT NULL
)
go

-- Add keys for table Номер_туриста

ALTER TABLE [Номер_туриста] ADD CONSTRAINT [PK_Номер_туриста] PRIMARY KEY ([Номер_билета],[Код_отеля],[Код_тура],[Тип_номера])
go

-- Table Номер_в_туре

CREATE TABLE [Номер_в_туре]
(
 [Код_отеля] Int NOT NULL,
 [Код_тура] Int NOT NULL,
 [Тип_номера] Nvarchar(50) NOT NULL,
 [Стоимость] Float NOT NULL
)
go

-- Add keys for table Номер_в_туре

ALTER TABLE [Номер_в_туре] ADD CONSTRAINT [PK_Номер_в_туре] PRIMARY KEY ([Код_отеля],[Код_тура],[Тип_номера])
go

-- Table Достопримечательность_в_туре

CREATE TABLE [Достопримечательность_в_туре]
(
 [Код_тура] Int NOT NULL,
 [Код_достопримечательности] Int NOT NULL
)
go

-- Add keys for table Достопримечательность_в_туре

ALTER TABLE [Достопримечательность_в_туре] ADD CONSTRAINT [PK_Достопримечательность_в_туре] PRIMARY KEY ([Код_тура],[Код_достопримечательности])
go

-- Table Услуга_тура

CREATE TABLE [Услуга_тура]
(
 [Организация] Nvarchar(50) NOT NULL,
 [Код_тура] Int NOT NULL,
 [Название_услуги] Nvarchar(200) NOT NULL,
 [Входит_в_стоимость] Bit NOT NULL,
 [Стоимость] Float NULL
)
go

-- Add keys for table Услуга_тура

ALTER TABLE [Услуга_тура] ADD CONSTRAINT [PK_Услуга_тура] PRIMARY KEY ([Организация],[Код_тура],[Название_услуги])
go

-- Table Даты_начала

CREATE TABLE [Даты_начала]
(
 [Дата_начала] Date NOT NULL,
 [Код_тура] Int NOT NULL
)
go

-- Add keys for table Даты_начала

ALTER TABLE [Даты_начала] ADD CONSTRAINT [PK_Даты_начала] PRIMARY KEY ([Дата_начала],[Код_тура])
go

-- Table Услуга_туриста

CREATE TABLE [Услуга_туриста]
(
 [Организация] Nvarchar(50) NOT NULL,
 [Код_тура] Int NOT NULL,
 [Номер_билета] Int NOT NULL,
 [Название_услуги] Nvarchar(200) NOT NULL
)
go

-- Add keys for table Услуга_туриста

ALTER TABLE [Услуга_туриста] ADD CONSTRAINT [PK_Услуга_туриста] PRIMARY KEY ([Организация],[Код_тура],[Номер_билета],[Название_услуги])
go

-- Create foreign keys (relationships) section ------------------------------------------------- 


ALTER TABLE [Тур] ADD CONSTRAINT [Предлагает] FOREIGN KEY ([Организация_туроператора]) REFERENCES [Туристическое агентство] ([Организация_туроператора]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Билет] ADD CONSTRAINT [Содержится] FOREIGN KEY ([Код_тура]) REFERENCES [Тур] ([Код_тура]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Ценообразование] ADD CONSTRAINT [Опирается] FOREIGN KEY ([Код_тура]) REFERENCES [Тур] ([Код_тура]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Маршрут] ADD CONSTRAINT [Определяет] FOREIGN KEY ([Код_тура]) REFERENCES [Тур] ([Код_тура]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Билет] ADD CONSTRAINT [Приобретает] FOREIGN KEY ([Номер_документа]) REFERENCES [Турист] ([Номер_документа]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Номер_туриста] ADD CONSTRAINT [Содержит4] FOREIGN KEY ([Номер_билета]) REFERENCES [Билет] ([Номер_билета]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Номер_в_туре] ADD CONSTRAINT [Содержится1] FOREIGN KEY ([Код_отеля]) REFERENCES [Место_жительства] ([Код_отеля]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Номер_в_туре] ADD CONSTRAINT [Содержит1] FOREIGN KEY ([Код_тура]) REFERENCES [Тур] ([Код_тура]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Номер_туриста] ADD CONSTRAINT [Принадлежит] FOREIGN KEY ([Код_отеля], [Код_тура], [Тип_номера]) REFERENCES [Номер_в_туре] ([Код_отеля], [Код_тура], [Тип_номера]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Достопримечательность_в_туре] ADD CONSTRAINT [Содержит3] FOREIGN KEY ([Код_тура]) REFERENCES [Тур] ([Код_тура]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Достопримечательность_в_туре] ADD CONSTRAINT [Содержится2] FOREIGN KEY ([Код_достопримечательности]) REFERENCES [Достопримечательность] ([Код_достопримечательности]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Маршрут] ADD CONSTRAINT [Используется] FOREIGN KEY ([Код_ТС]) REFERENCES [Транспорт] ([Код_ТС]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Услуга_тура] ADD CONSTRAINT [Предоставляется] FOREIGN KEY ([Организация]) REFERENCES [Поставщик] ([Организация]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Даты_начала] ADD CONSTRAINT [Определяется] FOREIGN KEY ([Код_тура]) REFERENCES [Тур] ([Код_тура]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Услуга_тура] ADD CONSTRAINT [Предоставляет] FOREIGN KEY ([Код_тура]) REFERENCES [Тур] ([Код_тура]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Услуга_туриста] ADD CONSTRAINT [Предоставляется1] FOREIGN KEY ([Организация], [Код_тура], [Название_услуги]) REFERENCES [Услуга_тура] ([Организация], [Код_тура], [Название_услуги]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Услуга_туриста] ADD CONSTRAINT [Содержит5] FOREIGN KEY ([Номер_билета]) REFERENCES [Билет] ([Номер_билета]) ON UPDATE NO ACTION ON DELETE NO ACTION
go






