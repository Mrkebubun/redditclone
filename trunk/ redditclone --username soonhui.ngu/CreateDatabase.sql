if exists(select * from master.dbo.sysdatabases where name ='RedditClone')
begin
   DROP DATABASE RedditClone
end


CREATE DATABASE RedditClone ON  PRIMARY 
    ( NAME = N'RedditClone_Data', 
FILENAME = N'D:\Soon HUi\Open Source\RedditClone\RedditClone\App_Data\RedditClone.mdf' , 
SIZE = 2MB, 
MAXSIZE = 20MB, 
FILEGROWTH = 1MB )

LOG ON 
    ( NAME = N'RedditClone_Log', 
FILENAME = N'D:\Soon HUi\Open Source\RedditClone\RedditClone\App_Data\RedditClone.ldf' , 
SIZE = 1MB ,
 MAXSIZE = 10MB, 
FILEGROWTH = 1MB )
