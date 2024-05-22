create database Encryp7

use Encryp7
go
create table [dbo].[CreditCardInformation]
([PersonID] int primary key,
[CreditCardNumber] nvarchar(50))

insert into CreditCardInformation
(
PersonID,
CreditCardNumber
)
values
(07, '9876-1234-8765-4321'),
(08, '9876-8765-0765-1234'),
(09, '9876-1234-1111-2222')
go


use Encryp7
create master key encryption by password = '$tr0ngPa$$w0rd1'
--- �������� DMK

backup master key to file = 'D:\Encryp7.bak'
encryption by password = 'Password1'
---�������� ��������� ����� �������� ����� 

create certificate EncryptedDBCert 
with subject = 'Certificate to encrypt EncryptedDB';
---�������� �����������

use [Encryp7]
go
create database encryption key
with algorithm = AES_256
encryption by server certificate [EncryptedDBCert]
alter database [Encryp7] 
set encryption on
---�������� ����� ���������� ���� ������ � ��������� ���������� ��� ���� ������, ������� ����� ��������

backup certificate EncryptedDBCert
to file = 'D:\EncryptedDBCert71'
with private key
(
file = 'D:\privateEncryptedDBCert71',
encryption by password = '$tr0nGPa$$w0rd'
);
---�������� ��������� ����� ����������� � �������� ������


use [master]
go
select db.[name]
, db.[is_encrypted] 
, dm.[encryption_state] 
, dm.[percent_complete] 
, dm.[key_algorithm] 
, dm.[key_length] 
FROM [sys].[databases]  db 
left outer join [sys].[dm_database_encryption_keys] dm
on db.[database_id] = dm.[database_id];





