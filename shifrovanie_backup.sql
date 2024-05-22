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
--- Создание DMK

backup master key to file = 'D:\Encryp7.bak'
encryption by password = 'Password1'
---создание резервной копии главного ключа 

create certificate EncryptedDBCert 
with subject = 'Certificate to encrypt EncryptedDB';
---создание сертификата

use [Encryp7]
go
create database encryption key
with algorithm = AES_256
encryption by server certificate [EncryptedDBCert]
alter database [Encryp7] 
set encryption on
---создание ключа шифрования базы данных и включение шифрования для базы данных, которую нужно защитить

backup certificate EncryptedDBCert
to file = 'D:\EncryptedDBCert71'
with private key
(
file = 'D:\privateEncryptedDBCert71',
encryption by password = '$tr0nGPa$$w0rd'
);
---Создание резервной копии сертификата с закрытым ключом


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





