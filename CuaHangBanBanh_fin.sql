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

create table Banh
(
	Id int primary key,
	TenBanh nvarchar(100),
	GiaThanh int, 
	LoaiBanh nvarchar(255),
	NgaySanXuat date
)
go

create table NhaCungCap
(
	Id int primary key,
	TenNCC nvarchar(100),
	DiaChi nvarchar(100),
	SDT varchar(10),
	LoaiBanh nvarchar(255),
	NgayCungCap date
)
go

create table NguyenLieu
(
	id int primary key,
	TenNL nvarchar(255),
	GiaThanh int,
	SLTonKho int,
	NgayNhap date,
	HanSuDung date
)
go

insert into TaiKhoan(Username, Password)
values('admin', 'admin')
go

