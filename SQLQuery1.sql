create database dbTest
use dbTest

create table tblEmployee
(
	EmpId int identity(1,1) Primary key,
	Lname varchar(50),
	Fname varchar(50),
	Mname varchar(50),
	Birthdate Datetime,
	Age int
)

INSERT INTO tblEmployee(Lname,Fname,Mname,Birthdate,Age)
VALUES ('Alferez','Chan','Rotaquio','12/19/1996',20)

SELECT * FROM tblEmployee

---PROCEDURE-----------

create procedure sp_AddEmp
@Lname varchar(50),
@Fname varchar(50),
@Mname varchar(50),
@Birthdate Datetime,
@Age int
as
INSERT INTO tblEmployee(Lname,Fname,Mname,Birthdate,Age)
VALUES (@Lname,@Fname,@Mname,@Birthdate,@Age)

create procedure sp_UpdateEmp
@EmpId int,
@Lname varchar(50),
@Fname varchar(50),
@Mname varchar(50),
@Birthdate Datetime,
@Age int
as
UPDATE tblEmployee SET Lname=@Lname,Fname=@Fname,Mname=@Mname,Birthdate=@Birthdate,Age=@Age
WHERE EmpId=@EmpId

create procedure sp_DeleteEmp
@EmpId int
as
DELETE FROM tblEmployee WHERE EmpId=@EmpId