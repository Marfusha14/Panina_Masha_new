USE Demo_DataBase
EXEC sp_addrole 'LIBRAR' 

USE Demo_DataBase --сделать текущей БДDemo_DataBase

EXEC sp_addrole 'READER'

ALTER LOGIN LIBRAR
WITH CHECK_POLICY = OFF, CHECK_EXPIRATION = OFF;
 

USE Demo_DataBase --сделать текущей БДDemo_DataBase

EXEC sp_addrole 'Petrov_ReadS'

use Demo_DataBase

EXEC sp_droprolemember 'READER', 'Petrov_Read'

USE master
ALTER LOGIN Petrov_ReadS
WITH CHECK_POLICY = OFF, CHECK_EXPIRATION = OFF;

create table use11
(id int not null)
insert into use11(id) values
(555),
(999),
(777)

EXEC sp_addrole 'Admin555'
exec sp_addlogin 'new_admin555', '123456', 'aaa'


ALTER LOGIN new_admin555
WITHOLD_PASSWORD = '123456', 
PASSWORD = '1234567';

ALTER LOGIN new_admin555 WITH PASSWORD = '1234567';  
GO  

ALTER LOGIN new_admin555 WITH   
     PASSWORD = '123456'   
     OLD_PASSWORD = '1234567';  
GO  


use master
CREATE SERVER AUDIT SPECIFICATION [AuditM]
FOR SERVER AUDIT [AuditM]
ADD (DATABASE_ROLE_MEMBER_CHANGE_GROUP),
ADD (USER_CHANGE_PASSWORD_GROUP)
WITH (STATE = ON)
GO 

SELECT * FROM sys.fn_get_audit_file ('\\support\zaporozhec.a\Исходники 20ИС3-1\AuditM_CABBDE20-0C20-4A1D-8FA0-9307BE0AC5D8_0_133144362167100000.sqlaudit',default,default);
---использовать файл sys.fn_get_audit_file для чтения всех файлов аудита в определенном каталоге