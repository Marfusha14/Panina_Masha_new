Exec sp_addlogin 'Sidorov_Libb','Sidorov', 'политика_безопасности'
use [политика_безопасности ]

Exec sp_adduser 'Sidorov_Libb','Sidorov_Libb'


Exec sp_addlogin 'Borodin_Readb','Borodin', 'политика_безопасности'

use [политика_безопасности ]

Exec sp_adduser 'Borodin_Readb','Borodin_Readb'


USE master
ALTER LOGIN Sidorov_Libb
WITH CHECK_POLICY = OFF, CHECK_EXPIRATION = OFF;

ALTER LOGIN [политика_безопасности ] WITH   
     PASSWORD = 'Sidorov'   
     OLD_PASSWORD = 'Sidorovv';  
GO  

ALTER LOGIN [политика_безопасности ] DISABLE;
DROP LOGIN [политика_безопасности ];