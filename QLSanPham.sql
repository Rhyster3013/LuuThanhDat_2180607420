use QLSanPham1

create database QLSanPham1

create table LoaiSP(
MaLoai char(2) primary key,
TenLoai nvarchar(30) not null)

create table SanPham(
MaSP char(6) primary key,
TenSP nvarchar(30) not null,
NgayNhap DateTime not null,
MaLoai char(2) foreign key references LoaiSP(MaLoai) not null
)

INSERT INTO LoaiSP (MaLoai, TenLoai)
VALUES
('a1', 'Bánh')
    ('a2', 'Kẹo'),
    ('b1', 'Bong bóng');

INSERT INTO SanPham (MaSP, TenSP, NgayNhap, MaLoai)
VALUES
    ('SP0001', 'Bánh bơ sữa', '2014-08-20', 'a1'),
    ('SP0002', 'Bánh dừa', '2014-08-21', 'a1'),
    ('SP0003', 'Kẹo dừa', '2014-08-22', 'a2'),
    ('SP0004', 'Bóng bay', '2014-08-23', 'b1');