if not exists(select * from sys.databases where name = 'Test')
begin 
create database Test
end