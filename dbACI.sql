create database dbACI
go
use dbACI;
go
create table Users
(	
	Id uniqueIdentifier not null,
	constraint pk_user primary key(Id),
	FirstName nvarchar(450) not null,
	LastName  nvarchar(256) not null,
	Email	  nvarchar(256) not null,
	NormalizedEmail nvarchar(256) not null,
	EmailConfirmed  bit,
	PasswordHash nvarchar(max),
	BirthDate datetime,
	RegistrationDate datetime default getdate()
)
go

create proc UspCreateUser
@Id						uniqueIdentifier,
@FirstName				nvarchar(450),
@LastName				nvarchar(256),
@Email					nvarchar(256),
@NormalizedEmail		nvarchar(256),
@PasswordHash			nvarchar(max),
@BirthDate				datetime
as
begin
	insert into Users(Id, FirstName, LastName, Email, NormalizedEmail, PasswordHash, BirthDate)
	values(@Id, @FirstName, @LastName, @Email, @NormalizedEmail, @PasswordHash, @BirthDate);
end
go

create proc UspUpdateUser
@Id uniqueIdentifier,
@FirstName nvarchar(450),
@LastName  nvarchar(256),
@Email	  nvarchar(256),
@NormalizedEmail nvarchar(256),
@EmailConfirmed  bit,
@PasswordHash nvarchar(max),
@BirthDate datetime
as
begin
	update Users set FirstName=@FirstName,
					 LastName=@LastName,
					 Email=@Email,
					 NormalizedEmail=@NormalizedEmail,
					 EmailConfirmed=@EmailConfirmed,
					 PasswordHash=@PasswordHash,
					 BirthDate=@BirthDate
	where Id=@Id;
end

go

create proc UspFindUserByEmail
@email nvarchar(256)
as
begin
	select 
	isnull(Id,'') as Id,
	isnull(FirstName,'') as FirstName,
	isnull(LastName,'') as LastName,
	isnull(Email,'') as Email,
	isnull(NormalizedEmail,'') as NormalizedEmail,
	isnull(EmailConfirmed,'') as EmailConfirmed,
	isnull(PasswordHash,'') as PasswordHash,
	isnull(RegistrationDate,'') as RegistrationDate,
	isnull(BirthDate,'') as BirthDate
	from Users
	where NormalizedEmail=@email
end
go

exec UspFindUserByEmail 'sample@sample.com';

go

select * from Users;