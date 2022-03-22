use Test;

insert into Users (UserKey, FirstName, LastName, Email, Age, CreatedDate) 
values ('3fa85f64-5717-4562-b3fc-2c963f66afa6','User1', 'LUser1', 'User1@gmail.com', 15, GETDATE());

insert into Users (UserKey, FirstName, LastName, Email, Age, CreatedDate) 
values ('3fa85f74-5717-4562-b3fc-2c963f66afa6', 'User2', 'LUser2', 'User2@gmail.com', 15, GETDATE());

insert into Users (UserKey, FirstName, LastName, Email, Age, CreatedDate) 
values ('3fa15f64-5717-4562-b3fc-2c963f66afa6', 'User3', 'LUser3', 'User3@gmail.com', 15, GETDATE());
