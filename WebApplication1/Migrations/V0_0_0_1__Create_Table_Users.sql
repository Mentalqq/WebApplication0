use UsersDb;

if not exists(select * from sysobjects where name = 'Users' and xtype = 'U')
begin
create table Users(
Id bigint identity not null,
UserKey uniqueidentifier not null,
FirstName nvarchar(128) not null,
LastName nvarchar(128) not null,
Email nvarchar(128) not null,
Age int not null,
CreatedDate datetime2 not null,
ModifiedDate datetime2 not null,
constraint PK_Users primary key (Id),
constraint UIX_Users_Email unique(Email),
constraint UIX_Users_UserKey unique(UserKey))
create index IX_Users_Email on Users(Email);
create index IX_Users_UserKey on Users(UserKey);
end