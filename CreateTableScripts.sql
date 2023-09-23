CREATE TABLE Users (
    Id int IDENTITY(1,1) NOT NULL,
    UserName varchar(255) UNIQUE NOT NULL,
    Email varchar(255) UNIQUE NOT NULL,
    IsAdmin BIT NOT NULL,
    UserPassword varchar(255) NOT NULL,
    CONSTRAINT PK_User PRIMARY KEY (Id)
)

CREATE TABLE Videos (
	Id int IDENTITY(1,1) NOT NULL,
	UserId int NOT NULL FOREIGN KEY REFERENCES Users(Id),
	EmbedUrl varchar(255) NOT NULL,
    IsApproved BIT NOT NULL,
	CONSTRAINT PK_Video PRIMARY KEY (Id),
)

