use UsersDb;

if not exists(select * from Users where Email = 'User1@gmail.com' and Email = 'User2@gmail.com' and Email = 'User3@gmail.com')
begin
insert into Users (UserKey, FirstName, LastName, Email, Age, CreatedDate, ModifiedDate) 
values ('a7f50676-6fc4-4116-87b1-bec0eaca0893', 'User1', 'LUser1', 'User1@gmail.com', 15, GETUTCDATE(), GETUTCDATE());

insert into Users (UserKey, FirstName, LastName, Email, Age, CreatedDate, ModifiedDate) 
values ('a7f50676-6fc4-4116-87b1-bec0eaca0892', 'User2', 'LUser2', 'User2@gmail.com', 15, GETUTCDATE(), GETUTCDATE());

insert into Users (UserKey, FirstName, LastName, Email, Age, CreatedDate, ModifiedDate) 
values ('a7f50676-6fc4-4116-87b1-bec0eaca0891', 'User3', 'LUser3', 'User3@gmail.com', 15, GETUTCDATE(), GETUTCDATE());
end