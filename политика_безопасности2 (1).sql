Exec sp_addlogin 'Sidorov_Libb','Sidorov', '��������_������������'
use [��������_������������ ]

Exec sp_adduser 'Sidorov_Libb','Sidorov_Libb'


Exec sp_addlogin 'Borodin_Readb','Borodin', '��������_������������'

use [��������_������������ ]

Exec sp_adduser 'Borodin_Readb','Borodin_Readb'


USE master
ALTER LOGIN Sidorov_Libb
WITH CHECK_POLICY = OFF, CHECK_EXPIRATION = OFF;

ALTER LOGIN [��������_������������ ] WITH   
     PASSWORD = 'Sidorov'   
     OLD_PASSWORD = 'Sidorovv';  
GO  

ALTER LOGIN [��������_������������ ] DISABLE;
DROP LOGIN [��������_������������ ];