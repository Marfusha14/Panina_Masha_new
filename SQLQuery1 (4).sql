--создайте базу данных EncryptedDB
USE [master] 
GO
CREATE DATABASE [EncryptedDB23]
GO
--создания таблицы с именем CreditCardInformation в базе данных EncryptedDB
USE [EncryptedDB23]
GO
CREATE TABLE [dbo].[CreditCardInformation]
([PersonID] [int] PRIMARY KEY,
[CreditCardNumber] [varbinary](max))
GO
--таблица будет содержать ложную информацию о кредитных картах
--Нужно удалить ключ который сущ.
DROP MASTER KEY
--создания главного ключа DMK базы данных EncryptedDB
USE [EncryptedDB23]
GO
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '$tr0nGPa$$w0rd'
GO
--Данные шифруются с использованием симметричного ключа, который будет зашифрован с помощью асимметричного ключа. 
USE [EncryptedDB23]
GO
--Создание симметричного ключа, зашифрованного парольной фразой StrongPa$$w0rd!
CREATE ASYMMETRIC KEY MyAsymmetricKey
WITH ALGORITHM = RSA_2048
ENCRYPTION BY PASSWORD = 'StrongPa$$w0rd!'
GO
--Создание симметрического ключа, зашифрованного асимметричным ключом
CREATE SYMMETRIC KEY MySymmetricKey
WITH ALGORITHM = AES_256
ENCRYPTION BY ASYMMETRIC KEY MyAsymmetricKey
--расшифровать симметричный ключ c использованием заданного асимметричного ключа.
USE [EncryptedDB23]
GO
OPEN SYMMETRIC KEY MySymmetricKey
DECRYPTION BY ASYMMETRIC KEY MyAsymmetricKey
WITH PASSWORD = 'StrongPa$$w0rd!'
GO
--убедиться, что ключ открыт
USE [EncryptedDB23]
GO
SELECT * FROM [sys].[openkeys]
--Необходимо ввести несколько номеров кредитных карт в таблицу CreditCardInformation
USE [EncryptedDB23]
GO
DECLARE @SymmetricKeyGUID AS [uniqueidentifier]
SET @SymmetricKeyGUID = KEY_GUID('MySymmetricKey')
IF (@SymmetricKeyGUID IS NOT NULL)
BEGIN
INSERT INTO [dbo].[CreditCardInformation]
VALUES (07, ENCRYPTBYKEY(@SymmetricKeyGUID,
N'9876-1234-8765-4321'))
INSERT INTO [dbo].[CreditCardInformation]
VALUES (08, ENCRYPTBYKEY(@SymmetricKeyGUID,
N'9876-8765-8765-1234'))
INSERT INTO [dbo].[CreditCardInformation]
VALUES (09, ENCRYPTBYKEY(@SymmetricKeyGUID,
N'9876-1234-1111-2222'))
END
TRUNCATE TABLE [dbo].[CreditCardInformation]
--направьте запрос к таблице CreditCardInformation
USE [EncryptedDB23]
GO
SELECT * FROM [dbo].[CreditCardInformation]
--С помощью функции DECRYPTBYKEY можно просмотреть зашифрованные данные
USE [EncryptedDB23]
GO
SELECT [PersonID],
CONVERT([nvarchar](32), DECRYPTBYKEY(CreditCardNumber))
AS [CreditCardNumber]
FROM [dbo].[CreditCardInformation]



--Подготовка к применению прозрачного шифрования данных
--Нужно удалить ключ который сущ.
DROP MASTER KEY
--создать DMK и сертификат сервера в базе данных master.
USE [master]
GO

CREATE MASTER KEY ENCRYPTION BY
PASSWORD = '$tr0ngPa$$w0rd1'

CREATE CERTIFICATE EncryptedDBCert3
WITH SUBJECT = 'Certificate to encrypt EncyptedDB';
GO

--Cоздать ключ шифрования базы данных и включить шифрование для базы данных
USE [master]
GO
CREATE DATABASE ENCRYPTION KEY 
WITH ALGORITHM = AES_256
ENCRYPTION BY SERVER CERTIFICATE [EncryptedDBCert]

--Cостояние шифрование всех баз данных на сервере
USE [master]
GO
SELECT db.[name]
, db.[is_encrypted]
, dm.[encryption_state]
, dm.[percent_complete]
, dm.[key_algorithm]
, db.[is_encrypted]
FROM [sys].[databases] db
LEFT OUTER JOIN [sys].[dm_database_encryption_keys] dm
ON db.[database_id] = dm.[database_id];
GO
