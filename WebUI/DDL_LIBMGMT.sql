-- Create a new database called 'LIBMGMT'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT [name]
        FROM sys.databases
        WHERE [name] = N'LibMgmt'
)
CREATE DATABASE LibMgmt
GO

USE LIBMGMT
GO

-- Create a new table called '[Account]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Account]', 'U') IS NOT NULL
DROP TABLE [dbo].[Account]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Account]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Primary Key column
    [Email] NVARCHAR(50) NOT NULL,
    [Password] NVARCHAR(50) NOT NULL,
    [Role] INT NOT NULL
    -- Specify more columns here
);
GO

-- Create a new table called '[Author]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Author]', 'U') IS NOT NULL
DROP TABLE [dbo].[Author]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Author]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Primary Key column
    [name] NVARCHAR(50) NOT NULL,
    [Birthday] DATETIME NOT NULL
    -- Specify more columns here
);
GO

-- Create a new table called '[Book]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Book]', 'U') IS NOT NULL
DROP TABLE [dbo].[Book]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Book]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Primary Key column
    [name] NVARCHAR(100) NOT NULL,
    [ISBN] NVARCHAR(20) NOT NULL,
    [PublishedDate] DATETIME NOT NULL,
    [Author] INT NOT NULL FOREIGN KEY REFERENCES Author(Id)
    -- Specify more columns here
);
GO

-- Create a new table called '[BorrowItem]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[BorrowItem]', 'U') IS NOT NULL
DROP TABLE [dbo].[BorrowItem]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[BorrowItem]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Primary Key column
    [Book] INT NOT NULL FOREIGN KEY REFERENCES Book(Id),
    [Borrower] INT NOT NULL FOREIGN KEY REFERENCES Account(Id),
    [BorrowedDate] DATETIME NOT NULL,
    [ReturnedDate] DATETIME,
    [Period] INT NOT NULL
    -- Specify more columns here
);
GO

-- Insert rows into table 'Account' in schema '[dbo]'
INSERT INTO [dbo].[Account]
( -- Columns to insert data into
 [Email], [Password], [Role]  
)
VALUES
( -- First row: values for the columns in the list above
 'admin@libmgmt.com', 'admin', 1
)
-- Add more rows here
GO