create table [User]
(
	Id uniqueidentifier default NEWID() not null,
	Name nvarchar(50),
	Email nvarchar(100) not null,
	PhoneNumber nvarchar(25),
	Age int,
	PasswordHash nvarchar(75) not null,
	CreationDate datetime2
)
go

create unique index User_Id_uindex
	on [User] (Id)
go

alter table [User]
	add constraint User_pk
		primary key nonclustered (Id)
go