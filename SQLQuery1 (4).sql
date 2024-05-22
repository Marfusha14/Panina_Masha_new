--�������� ���� ������ EncryptedDB
USE [master] 
GO
CREATE DATABASE [EncryptedDB23]
GO
--�������� ������� � ������ CreditCardInformation � ���� ������ EncryptedDB
USE [EncryptedDB23]
GO
CREATE TABLE [dbo].[CreditCardInformation]
([PersonID] [int] PRIMARY KEY,
[CreditCardNumber] [varbinary](max))
GO
--������� ����� ��������� ������ ���������� � ��������� ������
--����� ������� ���� ������� ���.
DROP MASTER KEY
--�������� �������� ����� DMK ���� ������ EncryptedDB
USE [EncryptedDB23]
GO
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '$tr0nGPa$$w0rd'
GO
--������ ��������� � �������������� ������������� �����, ������� ����� ���������� � ������� �������������� �����. 
USE [EncryptedDB23]
GO
--�������� ������������� �����, �������������� ��������� ������ StrongPa$$w0rd!
CREATE ASYMMETRIC KEY MyAsymmetricKey
WITH ALGORITHM = RSA_2048
ENCRYPTION BY PASSWORD = 'StrongPa$$w0rd!'
GO
--�������� ��������������� �����, �������������� ������������� ������
CREATE SYMMETRIC KEY MySymmetricKey
WITH ALGORITHM = AES_256
ENCRYPTION BY ASYMMETRIC KEY MyAsymmetricKey
--������������ ������������ ���� c �������������� ��������� �������������� �����.
USE [EncryptedDB23]
GO
OPEN SYMMETRIC KEY MySymmetricKey
DECRYPTION BY ASYMMETRIC KEY MyAsymmetricKey
WITH PASSWORD = 'StrongPa$$w0rd!'
GO
--���������, ��� ���� ������
USE [EncryptedDB23]
GO
SELECT * FROM [sys].[openkeys]
--���������� ������ ��������� ������� ��������� ���� � ������� CreditCardInformation
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
--��������� ������ � ������� CreditCardInformation
USE [EncryptedDB23]
GO
SELECT * FROM [dbo].[CreditCardInformation]
--� ������� ������� DECRYPTBYKEY ����� ����������� ������������� ������
USE [EncryptedDB23]
GO
SELECT [PersonID],
CONVERT([nvarchar](32), DECRYPTBYKEY(CreditCardNumber))
AS [CreditCardNumber]
FROM [dbo].[CreditCardInformation]



--���������� � ���������� ����������� ���������� ������
--����� ������� ���� ������� ���.
DROP MASTER KEY
--������� DMK � ���������� ������� � ���� ������ master.
USE [master]
GO

CREATE MASTER KEY ENCRYPTION BY
PASSWORD = '$tr0ngPa$$w0rd1'

CREATE CERTIFICATE EncryptedDBCert3
WITH SUBJECT = 'Certificate to encrypt EncyptedDB';
GO

--C������ ���� ���������� ���� ������ � �������� ���������� ��� ���� ������
USE [master]
GO
CREATE DATABASE ENCRYPTION KEY 
WITH ALGORITHM = AES_256
ENCRYPTION BY SERVER CERTIFICATE [EncryptedDBCert]

--C�������� ���������� ���� ��� ������ �� �������
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
