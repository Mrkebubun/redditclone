
USE RedditClone


if exists (select name from sys.tables 
	where name = 'VoteHistory')
	Drop table VoteHistory
go
if exists (select name from sys.tables 
	where name = 'Article')
	Drop table Article
go

if exists (select name from sys.tables 
	where name = 'UserInfo')
	Drop table UserInfo
go

create table UserInfo
(
Diggers nvarchar(50) primary key ,
password nvarchar(50) not null,
email nvarchar(50)
)



CREATE TABLE Article
(
  id int IDENTITY(1,1) PRIMARY KEY,
  Title nvarchar(max) not null,
  URL nvarchar(max) not null,
  Diggers nvarchar(50) not null foreign key references UserInfo(Diggers),
  submittedDate datetime not null,
  publishedDate datetime,
)
go



CREATE TABLE VoteHistory
(
  voteID int IDENTITY(1,1) PRIMARY KEY,
  diggers nvarchar(50) not null foreign key references UserInfo(Diggers),
  articleID int not null foreign key references Article(id),
voteChoice int not null
)
go

insert into UserInfo(Diggers, password, email) values ('Aaron', 'Aaron', null)
insert into UserInfo(Diggers, password, email) values ('Daniel', 'Daniel', null)
insert into UserInfo(Diggers, password, email) values ('Dennis', 'Dennis', null)
insert into UserInfo(Diggers, password, email) values ('Joseph', 'Joseph', null)
insert into UserInfo(Diggers, password, email) values ('Genius Boy', 'MoreGenius', null)
insert into UserInfo(Diggers, password, email) values ('Soon Hui', 'Soon Hui', null)

insert into Article(Title, URL, Diggers, submittedDate, publishedDate) 
values ('Zephyr the Test Management Tool', 'http://itscommonsensestupid.blogspot.com/2008/05/zephyr-test-management-tool.html', 'Soon Hui', '12/1/2007 12:00:00 AM', null)
insert into Article(Title, URL, Diggers, submittedDate, publishedDate) 
values ('Pledge you Allegiance to Download Firefox 3!', 'http://itscommonsensestupid.blogspot.com/2008/05/pledge-you-allegiance-to-download.html', 'Soon Hui', '12/1/2007 12:00:00 AM', null)
insert into Article(Title, URL, Diggers, submittedDate, publishedDate) 
values ('When the going gets tough', 'http://michaeleatonconsulting.com/blog/archive/2008/06/02/when-the-going-gets-tough-or-how-to-improve-your.aspx', 'Aaron', '5/24/2008 12:00:00 AM', null)
insert into Article(Title, URL, Diggers, submittedDate, publishedDate) 
values ('No, OOP doesnt Fail Us', 'http://itscommonsensestupid.blogspot.com/2008/06/no-oop-doesnt-fail-us.html', 'Soon Hui', '6/3/2008 5:09:22 PM', null)
insert into Article(Title, URL, Diggers, submittedDate, publishedDate) 
values ('DotNetKicks', 'http://www.dotnetkicks.com/', 'Soon Hui', '3/24/2007 12:00:00 AM', null)

insert into VoteHistory(diggers, articleID, voteChoice) values ('Soon Hui', 1,1)
insert into VoteHistory(diggers, articleID, voteChoice) values ('Aaron', 2,1)
insert into VoteHistory(diggers, articleID, voteChoice) values ('Aaron', 3,1)
insert into VoteHistory(diggers, articleID, voteChoice) values ('Soon Hui',4,1)
insert into VoteHistory(diggers, articleID, voteChoice) values ('Soon Hui', 5,1)
insert into VoteHistory(diggers, articleID, voteChoice) values ('Aaron', 1,1)
insert into VoteHistory(diggers, articleID, voteChoice) values ('Daniel', 5,2)
insert into VoteHistory(diggers, articleID, voteChoice) values ('Joseph', 5,2)
insert into VoteHistory(diggers, articleID, voteChoice) values ('Aaron', 5,2)