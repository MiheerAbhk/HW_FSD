CREATE DATABASE StudentDB;
GO

USE StudentDB;
GO

CREATE TABLE Students (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100),
    Branch NVARCHAR(50),
    Gender NVARCHAR(10)
);

select * from Students