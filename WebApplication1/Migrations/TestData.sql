use UsersDb;

insert into Users (FirstName, LastName, Email, Age, CreatedDate) 
values ('User1', 'LUser1', 'User1@gmail.com', 15, GETUTCDATE());

insert into Users (FirstName, LastName, Email, Age, CreatedDate) 
values ('User2', 'LUser2', 'User2@gmail.com', 15, GETUTCDATE());

insert into Users (FirstName, LastName, Email, Age, CreatedDate) 
values ('User3', 'LUser3', 'User3@gmail.com', 15, GETUTCDATE());
