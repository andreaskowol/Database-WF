USE [master]
GO
/****** Object:  Database [Dapper-WF]    Script Date: 15.08.2022 10:44:35 ******/
CREATE DATABASE [Dapper-WF]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Dapper-WF', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Dapper-WF.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Dapper-WF_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Dapper-WF_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Dapper-WF] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Dapper-WF].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Dapper-WF] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Dapper-WF] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Dapper-WF] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Dapper-WF] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Dapper-WF] SET ARITHABORT OFF 
GO
ALTER DATABASE [Dapper-WF] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Dapper-WF] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Dapper-WF] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Dapper-WF] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Dapper-WF] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Dapper-WF] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Dapper-WF] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Dapper-WF] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Dapper-WF] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Dapper-WF] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Dapper-WF] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Dapper-WF] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Dapper-WF] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Dapper-WF] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Dapper-WF] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Dapper-WF] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Dapper-WF] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Dapper-WF] SET RECOVERY FULL 
GO
ALTER DATABASE [Dapper-WF] SET  MULTI_USER 
GO
ALTER DATABASE [Dapper-WF] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Dapper-WF] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Dapper-WF] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Dapper-WF] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Dapper-WF] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Dapper-WF] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Dapper-WF', N'ON'
GO
ALTER DATABASE [Dapper-WF] SET QUERY_STORE = OFF
GO
USE [Dapper-WF]
GO
/****** Object:  Table [dbo].[People]    Script Date: 15.08.2022 10:44:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[PersonId] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [varchar](255) NOT NULL,
	[FirstName] [varchar](255) NULL,
	[Age] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[People] ON 

INSERT [dbo].[People] ([PersonId], [LastName], [FirstName], [Age]) VALUES (2, N'Schrute', N'Dwight', 40)
INSERT [dbo].[People] ([PersonId], [LastName], [FirstName], [Age]) VALUES (3, N'Halpert', N'Jim', 32)
INSERT [dbo].[People] ([PersonId], [LastName], [FirstName], [Age]) VALUES (4, N'Beesly', N'Pamela', 31)
INSERT [dbo].[People] ([PersonId], [LastName], [FirstName], [Age]) VALUES (5, N'Scott', N'Michael', 45)
INSERT [dbo].[People] ([PersonId], [LastName], [FirstName], [Age]) VALUES (6, N'Malone', N'Kevin', 42)
INSERT [dbo].[People] ([PersonId], [LastName], [FirstName], [Age]) VALUES (7, N'Martin', N'Angela', 36)
SET IDENTITY_INSERT [dbo].[People] OFF
GO
/****** Object:  StoredProcedure [dbo].[DeletePerson]    Script Date: 15.08.2022 10:44:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeletePerson] 
	@PersonId int

AS
DELETE FROM People
	
WHERE PersonId = @PersonId
GO
/****** Object:  StoredProcedure [dbo].[GetAll]    Script Date: 15.08.2022 10:44:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAll]
AS
BEGIN
    SELECT * FROM People
END
GO
/****** Object:  StoredProcedure [dbo].[InsertPerson]    Script Date: 15.08.2022 10:44:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertPerson] 
	@LastName varchar(255), 
	@FirstName nvarchar(255),
	@Age int


AS
	insert into People(
	LastName,
	FirstName,
	Age
	)

	Values(
	@LastName,
	@FirstName,
	@Age
	)
GO
/****** Object:  StoredProcedure [dbo].[SelectBySearchPhrase]    Script Date: 15.08.2022 10:44:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectBySearchPhrase]
	@SearchPhrase1 nvarchar(255),
	@SearchPhrase2 nvarchar(255),
	@Age int

AS
IF @SearchPhrase2 = '' and @Age is null
	SELECT * FROM People
	WHERE LastName like '%' + @SearchPhrase1 + '%' or FirstName like '%' + @SearchPhrase1 + '%'

IF @SearchPhrase2 = '' and @Age is not null
	SELECT * FROM People
	WHERE (LastName like '%' + @SearchPhrase1 + '%' or FirstName like '%' + @SearchPhrase1 + '%') and Age = @Age

IF @SearchPhrase2 != '' and @Age is null
	SELECT * FROM People
	WHERE (LastName like '%' + @SearchPhrase1 + '%' or FirstName like '%' + @SearchPhrase1 + '%') and (LastName like '%' + @SearchPhrase2 + '%' or FirstName like '%' + @SearchPhrase2 + '%')

IF @SearchPhrase2 != '' and @Age is not null
	SELECT * FROM People
	WHERE (LastName like '%' + @SearchPhrase1 + '%' or FirstName like '%' + @SearchPhrase1 + '%') and (LastName like '%' + @SearchPhrase2 + '%' or FirstName like '%' + @SearchPhrase2 + '%') and Age = @Age

GO
/****** Object:  StoredProcedure [dbo].[UpdatePerson]    Script Date: 15.08.2022 10:44:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdatePerson] 
	@PersonId int,
	@LastName varchar(255),
	@FirstName varchar(255),
	@Age int

AS
UPDATE People
SET 
	LastName = @LastName,
	FirstName = @FirstName,
	Age = @Age
	

WHERE PersonId = @PersonId
GO
USE [master]
GO
ALTER DATABASE [Dapper-WF] SET  READ_WRITE 
GO
