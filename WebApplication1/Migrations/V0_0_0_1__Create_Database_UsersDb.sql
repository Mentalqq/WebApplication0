if not exists(select * from sys.databases where name = 'UsersDb')
begin 
create database UsersDb
end