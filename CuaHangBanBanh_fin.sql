create database CUAHANG
go

use CUAHANG
go

create table TaiKhoan
(
	Id int primary key identity(1,1),
	Username varchar(50) unique not null,
	Password varchar(50) not null,
)
go

create table NhanVien
(
	Id int primary key identity(1,1),
	HoTen nvarchar(100),
	SDT varchar(10),
	Gioitinh nvarchar(10),
	DiaChi nvarchar(255),
	NgaySinh Date
)
go

create table LoaiBanh
(
	Id int primary key identity(1,1),
	TenLoaiBanh nvarchar(100),
	XuatXu nvarchar(100),
	NguyenLieu nvarchar(255)
)
go

create table Banh
(
	Id int primary key identity(1,1),
	TenBanh nvarchar(100),
	GiaThanh int, 
	LoaiBanh int,
	foreign key(LoaiBanh) references LoaiBanh(Id)
)
go

create table NhaCungCap
(
	Id int primary key identity(1,1),
	TenNCC nvarchar(100),
	DiaChi nvarchar(100),
	SDT varchar(10),
)
go

insert into TaiKhoan(Username, Password)
values('admin', 'admin')
go

ALTER TABLE NhaCungCap
ADD LoaiBanh nvarchar(255),
	NgayCungCap date;
GO

