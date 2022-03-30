use UsersDb;

if not exists(select * from sysobjects where name = 'Users' and xtype = 'U')
begin
create table Users(
Id bigint primary key identity,
UserKey uniqueidentifier unique default newid(),
FirstName nvarchar(128) not null,
LastName nvarchar(128) not null,
Email nvarchar(128) unique not null,
Age int not null,
CreatedDate datetime2,
ModifiedDate datetime2)
create index IX_Users_Email on Users(Email);
create index IX_Users_UserKey on Users(UserKey);
end