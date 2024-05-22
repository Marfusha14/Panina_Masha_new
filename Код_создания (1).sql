/*
Created: 11.12.2023
Modified: 12.12.2023
Model: Model2
Database: MS SQL Server 2019
*/



-- Create tables section -------------------------------------------------

-- Table ������������� ���������

CREATE TABLE [������������� ���������]
(
 [�����������_������������] Nvarchar(50) NOT NULL,
 [��������] Nvarchar(50) NOT NULL,
 [��������] Nvarchar(500) NOT NULL
)
go

-- Add keys for table ������������� ���������

ALTER TABLE [������������� ���������] ADD CONSTRAINT [Unique_Identifier1] PRIMARY KEY ([�����������_������������])
go

-- Table ���������������������

CREATE TABLE [���������������������]
(
 [���_���������������������] Int NOT NULL,
 [��������] Nvarchar(100) NOT NULL,
 [��������] Nvarchar(500) NOT NULL,
 [������] Float NOT NULL,
 [�������] Float NOT NULL
)
go

-- Add keys for table ���������������������

ALTER TABLE [���������������������] ADD CONSTRAINT [Unique_Identifier2] PRIMARY KEY ([���_���������������������])
go

-- Table ���������

CREATE TABLE [���������]
(
 [���_��] Int NOT NULL,
 [��������_��] Nvarchar(50) NOT NULL
)
go

-- Add keys for table ���������

ALTER TABLE [���������] ADD CONSTRAINT [Unique_Identifier3] PRIMARY KEY ([���_��])
go

-- Table �����_����������

CREATE TABLE [�����_����������]
(
 [���_�����] Int NOT NULL,
 [��������] Nvarchar(500) NOT NULL,
 [����������_����] Int NOT NULL,
 [������] Nvarchar(50) NOT NULL,
 [�����] Nvarchar(50) NOT NULL,
 [�����] Nvarchar(100) NOT NULL
)
go

-- Add keys for table �����_����������

ALTER TABLE [�����_����������] ADD CONSTRAINT [Unique_Identifier4] PRIMARY KEY ([���_�����])
go

-- Table �����

CREATE TABLE [�����]
(
 [�����_������] Int IDENTITY,
 [�����_���������] Nvarchar(20) NOT NULL,
 [���_����] Int NOT NULL
)
go

-- Create indexes for table �����

CREATE INDEX [IX_����������] ON [�����] ([���_����])
go

CREATE INDEX [IX_�����������] ON [�����] ([�����_���������])
go

-- Add keys for table �����

ALTER TABLE [�����] ADD CONSTRAINT [Unique_Identifier5] PRIMARY KEY ([�����_������])
go

-- Table ���������������

CREATE TABLE [���������������]
(
 [���_����] Int NOT NULL,
 [���_������] Nvarchar(50) NOT NULL,
 [���������] Nvarchar(50) NOT NULL
)
go

-- Add keys for table ���������������

ALTER TABLE [���������������] ADD CONSTRAINT [Unique_Identifier6] PRIMARY KEY ([���_����],[���_������])
go

-- Table ������

CREATE TABLE [������]
(
 [�����_���������] Nvarchar(20) NOT NULL,
 [���_���������] Nvarchar(40) NOT NULL,
 [�������] Nvarchar(40) NOT NULL,
 [���] Nvarchar(40) NOT NULL,
 [��������] Nvarchar(40) NULL,
 [����_��������] Date NOT NULL
)
go

-- Add keys for table ������

ALTER TABLE [������] ADD CONSTRAINT [Unique_Identifier7] PRIMARY KEY ([�����_���������])
go

-- Table �������

CREATE TABLE [�������]
(
 [���_����] Int NOT NULL,
 [���������� �����] Int NOT NULL,
 [��������_����������] Nvarchar(50) NOT NULL,
 [����_������] Int NOT NULL,
 [�����_������] Time NOT NULL,
 [�����_���������] Time NOT NULL,
 [����������] Nvarchar(1000) NOT NULL,
 [���_��] Int NULL
)
go

-- Create indexes for table �������

CREATE INDEX [IX_Relationship8] ON [�������] ([���_��])
go

-- Add keys for table �������

ALTER TABLE [�������] ADD CONSTRAINT [Unique_Identifier8] PRIMARY KEY ([���������� �����],[���_����])
go

-- Table ���������

CREATE TABLE [���������]
(
 [�����������] Nvarchar(50) NOT NULL,
 [��������] Nvarchar(500) NOT NULL
)
go

-- Add keys for table ���������

ALTER TABLE [���������] ADD CONSTRAINT [Unique_Identifier9] PRIMARY KEY ([�����������])
go

-- Table ���

CREATE TABLE [���]
(
 [���_����] Int NOT NULL,
 [��������] Nvarchar(50) NOT NULL,
 [��������] Nvarchar(500) NOT NULL,
 [��������] Bit NOT NULL,
 [�������_���������] Float NOT NULL,
 [�������_������] Bit NOT NULL,
 [�����_�����] Bit NOT NULL,
 [�����������_������������] Nvarchar(50) NOT NULL,
 [������_��_���] Nvarchar(500) NOT NULL
)
go

-- Create indexes for table ���

CREATE INDEX [IX_����������] ON [���] ([�����������_������������])
go

-- Add keys for table ���

ALTER TABLE [���] ADD CONSTRAINT [Unique_Identifier10] PRIMARY KEY ([���_����])
go

-- Table �����_�������

CREATE TABLE [�����_�������]
(
 [�����_������] Int NOT NULL,
 [���_�����] Int NOT NULL,
 [���_����] Int NOT NULL,
 [���_������] Nvarchar(50) NOT NULL
)
go

-- Add keys for table �����_�������

ALTER TABLE [�����_�������] ADD CONSTRAINT [PK_�����_�������] PRIMARY KEY ([�����_������],[���_�����],[���_����],[���_������])
go

-- Table �����_�_����

CREATE TABLE [�����_�_����]
(
 [���_�����] Int NOT NULL,
 [���_����] Int NOT NULL,
 [���_������] Nvarchar(50) NOT NULL,
 [���������] Float NOT NULL
)
go

-- Add keys for table �����_�_����

ALTER TABLE [�����_�_����] ADD CONSTRAINT [PK_�����_�_����] PRIMARY KEY ([���_�����],[���_����],[���_������])
go

-- Table ���������������������_�_����

CREATE TABLE [���������������������_�_����]
(
 [���_����] Int NOT NULL,
 [���_���������������������] Int NOT NULL
)
go

-- Add keys for table ���������������������_�_����

ALTER TABLE [���������������������_�_����] ADD CONSTRAINT [PK_���������������������_�_����] PRIMARY KEY ([���_����],[���_���������������������])
go

-- Table ������_����

CREATE TABLE [������_����]
(
 [�����������] Nvarchar(50) NOT NULL,
 [���_����] Int NOT NULL,
 [��������_������] Nvarchar(200) NOT NULL,
 [������_�_���������] Bit NOT NULL,
 [���������] Float NULL
)
go

-- Add keys for table ������_����

ALTER TABLE [������_����] ADD CONSTRAINT [PK_������_����] PRIMARY KEY ([�����������],[���_����],[��������_������])
go

-- Table ����_������

CREATE TABLE [����_������]
(
 [����_������] Date NOT NULL,
 [���_����] Int NOT NULL
)
go

-- Add keys for table ����_������

ALTER TABLE [����_������] ADD CONSTRAINT [PK_����_������] PRIMARY KEY ([����_������],[���_����])
go

-- Table ������_�������

CREATE TABLE [������_�������]
(
 [�����������] Nvarchar(50) NOT NULL,
 [���_����] Int NOT NULL,
 [�����_������] Int NOT NULL,
 [��������_������] Nvarchar(200) NOT NULL
)
go

-- Add keys for table ������_�������

ALTER TABLE [������_�������] ADD CONSTRAINT [PK_������_�������] PRIMARY KEY ([�����������],[���_����],[�����_������],[��������_������])
go

-- Create foreign keys (relationships) section ------------------------------------------------- 


ALTER TABLE [���] ADD CONSTRAINT [����������] FOREIGN KEY ([�����������_������������]) REFERENCES [������������� ���������] ([�����������_������������]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [�����] ADD CONSTRAINT [����������] FOREIGN KEY ([���_����]) REFERENCES [���] ([���_����]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [���������������] ADD CONSTRAINT [���������] FOREIGN KEY ([���_����]) REFERENCES [���] ([���_����]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [�������] ADD CONSTRAINT [����������] FOREIGN KEY ([���_����]) REFERENCES [���] ([���_����]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [�����] ADD CONSTRAINT [�����������] FOREIGN KEY ([�����_���������]) REFERENCES [������] ([�����_���������]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [�����_�������] ADD CONSTRAINT [��������4] FOREIGN KEY ([�����_������]) REFERENCES [�����] ([�����_������]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [�����_�_����] ADD CONSTRAINT [����������1] FOREIGN KEY ([���_�����]) REFERENCES [�����_����������] ([���_�����]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [�����_�_����] ADD CONSTRAINT [��������1] FOREIGN KEY ([���_����]) REFERENCES [���] ([���_����]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [�����_�������] ADD CONSTRAINT [�����������] FOREIGN KEY ([���_�����], [���_����], [���_������]) REFERENCES [�����_�_����] ([���_�����], [���_����], [���_������]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [���������������������_�_����] ADD CONSTRAINT [��������3] FOREIGN KEY ([���_����]) REFERENCES [���] ([���_����]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [���������������������_�_����] ADD CONSTRAINT [����������2] FOREIGN KEY ([���_���������������������]) REFERENCES [���������������������] ([���_���������������������]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [�������] ADD CONSTRAINT [������������] FOREIGN KEY ([���_��]) REFERENCES [���������] ([���_��]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [������_����] ADD CONSTRAINT [���������������] FOREIGN KEY ([�����������]) REFERENCES [���������] ([�����������]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [����_������] ADD CONSTRAINT [������������] FOREIGN KEY ([���_����]) REFERENCES [���] ([���_����]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [������_����] ADD CONSTRAINT [�������������] FOREIGN KEY ([���_����]) REFERENCES [���] ([���_����]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [������_�������] ADD CONSTRAINT [���������������1] FOREIGN KEY ([�����������], [���_����], [��������_������]) REFERENCES [������_����] ([�����������], [���_����], [��������_������]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [������_�������] ADD CONSTRAINT [��������5] FOREIGN KEY ([�����_������]) REFERENCES [�����] ([�����_������]) ON UPDATE NO ACTION ON DELETE NO ACTION
go






