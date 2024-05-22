use master
go
drop database if exists [�������� ��]
go
create database [�������� ��]
go
use [�������� ��]
go
/*
Created: 14.11.2023
Modified: 18.11.2023
Model: Full model
Database: MS SQL Server 2019
*/


-- Create tables section -------------------------------------------------

-- Table �����

CREATE TABLE [�����]
(
 [�����_����������] Int NOT NULL,
 [���_������] Int NOT NULL,
 [����_����������] Datetime NOT NULL,
 [�������_���������] Float NOT NULL
)
go

-- Add keys for table �����

ALTER TABLE [�����] ADD CONSTRAINT [Unique_Identifier1] PRIMARY KEY ([�����_����������],[���_������],[����_����������])
go

-- Table ������

CREATE TABLE [������]
(
 [���_�������] Int NOT NULL,
 [�������] Nvarchar(max) NOT NULL,
 [���] Nvarchar(max) NOT NULL,
 [��������] Nvarchar(max) NULL,
 [����_��������] Nvarchar(max) NOT NULL,
 [���] Nvarchar(max) NOT NULL,
 [����_�_�������] Nvarchar(max) NOT NULL,
 [�����] Nvarchar(max) NOT NULL,
 [������] Nvarchar(max) NOT NULL,
 [�����] Nvarchar(max) NOT NULL,
 [����������_������] Int NOT NULL
)
go

-- Add keys for table ������

ALTER TABLE [������] ADD CONSTRAINT [Unique_Identifier2] PRIMARY KEY ([���_�������])
go

-- Table ���������

CREATE TABLE [���������]
(
 [�����_����������] Int NOT NULL,
 [��������] Nvarchar(max) NOT NULL,
 [�����] Nvarchar(max) NOT NULL,
 [�����] Nvarchar(max) NOT NULL,
 [�����_����] Int NOT NULL,
 [��������] Int NULL
)
go

-- Add keys for table ���������

ALTER TABLE [���������] ADD CONSTRAINT [Unique_Identifier3] PRIMARY KEY ([�����_����������])
go

-- Table �����

CREATE TABLE [�����]
(
 [���_������] Int NOT NULL,
 [��������] Nvarchar(max) NOT NULL,
 [���_�������] Nvarchar(max) NOT NULL,
 [��������] Nvarchar(max) NOT NULL,
 [�����] Nvarchar(max) NOT NULL,
 [�������] Nvarchar(max) NULL
)
go

-- Add keys for table �����

ALTER TABLE [�����] ADD CONSTRAINT [Unique_Identifier4] PRIMARY KEY ([���_������])
go

-- Table ������

CREATE TABLE [������]
(
 [��������_������] Nvarchar(50) NOT NULL,
 [��������] Nvarchar(max) NULL
)
go

-- Add keys for table ������

ALTER TABLE [������] ADD CONSTRAINT [Unique_Identifier5] PRIMARY KEY ([��������_������])
go

-- Table �������������� ������

CREATE TABLE [�������������� ������]
(
 [�����_����������] Int NOT NULL,
 [��������_������] Nvarchar(50) NOT NULL,
 [���������] Float NOT NULL,
 [���_����������] Nvarchar(max) NOT NULL
)
go

-- Add keys for table �������������� ������

ALTER TABLE [�������������� ������] ADD CONSTRAINT [PK_�������������� ������] PRIMARY KEY ([�����_����������],[��������_������],[���������])
go

-- Table �����

CREATE TABLE [�����]
(
 [�����_����������] Int NOT NULL,
 [ID_�������] Int NOT NULL,
 [���_������] Int NOT NULL,
 [����_����������] Datetime NOT NULL,
 [����������] Int NOT NULL
)
go

-- Add keys for table �����

ALTER TABLE [�����] ADD CONSTRAINT [PK_�����] PRIMARY KEY ([�����_����������],[ID_�������],[���_������],[����_����������])
go

-- Table ����

CREATE TABLE [����]
(
 [��������_�����] Nvarchar(50) NOT NULL,
 [��������] Nvarchar(max) NULL
)
go

-- Add keys for table ����

ALTER TABLE [����] ADD CONSTRAINT [PK_����] PRIMARY KEY ([��������_�����])
go

-- Table �����_����

CREATE TABLE [�����_����]
(
 [���_������] Int NOT NULL,
 [��������_�����] Nvarchar(50) NOT NULL
)
go

-- Add keys for table �����_����

ALTER TABLE [�����_����] ADD CONSTRAINT [PK_�����_����] PRIMARY KEY ([���_������],[��������_�����])
go

-- Table ����_���������

CREATE TABLE [����_���������]
(
 [���_�������] Int NOT NULL,
 [��������_�����] Nvarchar(50) NOT NULL
)
go

-- Add keys for table ����_���������

ALTER TABLE [����_���������] ADD CONSTRAINT [PK_����_���������] PRIMARY KEY ([���_�������],[��������_�����])
go

-- Table ��������

CREATE TABLE [��������]
(
 [���_�������] Int NOT NULL,
 [�����_����������] Int NOT NULL,
 [�����_��������] Nvarchar(15) NOT NULL,
 [���������] Nvarchar(15) NOT NULL
)
go

-- Add keys for table ��������

ALTER TABLE [��������] ADD CONSTRAINT [PK_��������] PRIMARY KEY ([���_�������],[�����_����������])
go

-- Table ������_������

CREATE TABLE [������_������]
(
 [�����_����������] Int NOT NULL,
 [ID_�������] Int NOT NULL,
 [���_������] Int NOT NULL,
 [����_����������] Datetime NOT NULL,
 [��������_������] Nvarchar(50) NOT NULL,
 [���������] Float NOT NULL,
 [����������] Int NOT NULL
)
go

-- Add keys for table ������_������

ALTER TABLE [������_������] ADD CONSTRAINT [PK_������_������] PRIMARY KEY ([�����_����������],[ID_�������],[���_������],[����_����������],[��������_������],[���������])
go

-- Create foreign keys (relationships) section ------------------------------------------------- 


ALTER TABLE [�����] ADD CONSTRAINT [������������] FOREIGN KEY ([���_������]) REFERENCES [�����] ([���_������]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [�����] ADD CONSTRAINT [��������] FOREIGN KEY ([�����_����������]) REFERENCES [���������] ([�����_����������]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [�������������� ������] ADD CONSTRAINT [�������������] FOREIGN KEY ([�����_����������]) REFERENCES [���������] ([�����_����������]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [�������������� ������] ADD CONSTRAINT [���������������] FOREIGN KEY ([��������_������]) REFERENCES [������] ([��������_������]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [�����] ADD CONSTRAINT [��������] FOREIGN KEY ([ID_�������]) REFERENCES [������] ([���_�������]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [�����] ADD CONSTRAINT [����������] FOREIGN KEY ([�����_����������], [���_������], [����_����������]) REFERENCES [�����] ([�����_����������], [���_������], [����_����������]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [�����_����] ADD CONSTRAINT [�����������] FOREIGN KEY ([���_������]) REFERENCES [�����] ([���_������]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [�����_����] ADD CONSTRAINT [����������] FOREIGN KEY ([��������_�����]) REFERENCES [����] ([��������_�����]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [����_���������] ADD CONSTRAINT [�����] FOREIGN KEY ([���_�������]) REFERENCES [������] ([���_�������]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [����_���������] ADD CONSTRAINT [���������] FOREIGN KEY ([��������_�����]) REFERENCES [����] ([��������_�����]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [��������] ADD CONSTRAINT [��������] FOREIGN KEY ([���_�������]) REFERENCES [������] ([���_�������]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [��������] ADD CONSTRAINT [����������] FOREIGN KEY ([�����_����������]) REFERENCES [���������] ([�����_����������]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [������_������] ADD CONSTRAINT [��������] FOREIGN KEY ([�����_����������], [ID_�������], [���_������], [����_����������]) REFERENCES [�����] ([�����_����������], [ID_�������], [���_������], [����_����������]) ON UPDATE CASCADE ON DELETE CASCADE
go



ALTER TABLE [������_������] ADD CONSTRAINT [������] FOREIGN KEY ([�����_����������], [��������_������], [���������]) REFERENCES [�������������� ������] ([�����_����������], [��������_������], [���������]) ON UPDATE NO ACTION ON DELETE NO ACTION
go





create trigger NoMsTrigger on ����� after Insert,Update,Delete 
as
	update ����� set ����_����������=dateadd(ms, -datepart(ms,����_����������),����_����������)
go
Enable trigger NoMsTrigger on �����