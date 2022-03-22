use Test;

if not exists(select * from sysobjects where name = 'Users' and xtype = 'U')
begin
create table Users(
Id bigint primary key identity,
UserKey uniqueidentifier unique,
FirstName nvarchar(128) not null,
LastName nvarchar(128) not null,
Email nvarchar(128) unique not null,
Age int not null,
CreatedDate datetime2,
ModifiedDate datetime2)
create index EmailIndex on Users(Email);
create index UserKeyIndex on Users(UserKey);
end