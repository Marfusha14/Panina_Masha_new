CREATE EVENT SESSION [�����] ON SERVER 
ADD EVENT sqlserver.database_created,
ADD EVENT sqlserver.database_dropped,
ADD EVENT sqlserver.database_started,
ADD EVENT sqlserver.database_stopped
ADD TARGET package0.event_file(SET filename=N'E:\3 ����\���������� � ������������� ��� ������\����������\6 ����������� �������\�����.xel',max_file_size=(2048),max_rollover_files=(8))
WITH (STARTUP_STATE=OFF)
GO


