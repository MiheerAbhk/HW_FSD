use SimplyFlyDb
create database test
use test
go

select * from Users
select * from Airlines
select * from Flights
select * from FlightRoutes
select * from Bookings
select * from Cancellations
exec sp_help 'FlightRoutes'
exec sp_help 'Users'
drop database SimplyFlyDb

SELECT IDENT_CURRENT('FlightRoutes') AS Current_Identity;

INSERT INTO Users(Name,Email,PasswordHash, Role, Phone, Address, CreatedAt) Values
('Air India', 'admin@airindia.com', 'qwertyuiop','FlightOwner', 8856878497, 'AI HQ', GETDATE())
