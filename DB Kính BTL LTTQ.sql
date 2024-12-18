-- Tạo database quanlybankinh
--drop data use 
CREATE DATABASE quanlybankinh;
USE quanlybankinh;

-- Tạo các bảng không có khóa ngoại
CREATE TABLE KhoiLuong (
    MaKhoiLuong INT PRIMARY KEY,
    TenKhoiLuong NVARCHAR(50)
);

CREATE TABLE LoaiKinh (
    MaLoai INT PRIMARY KEY,
    TenLoai NVARCHAR(100)
);

CREATE TABLE HinhDangMat (
    MaDangMat INT PRIMARY KEY,
    TenDangMat NVARCHAR(50)
);

CREATE TABLE ChatLieu (
    MaChatLieu INT PRIMARY KEY,
    TenChatLieu NVARCHAR(100)
);

CREATE TABLE NuocSanXuat (
    MaNuocSX INT PRIMARY KEY,
    TenNuocSX NVARCHAR(100)
);

CREATE TABLE MauSac (
    MaMau INT PRIMARY KEY,
    TenMau NVARCHAR(50)
);

CREATE TABLE Diop (
    MaDiop INT PRIMARY KEY,
    TenDiop NVARCHAR(50)
);

CREATE TABLE CongViec (
    MaCV INT PRIMARY KEY,
    TenCV NVARCHAR(100),
    MucLuong DECIMAL(10, 2)
);

CREATE TABLE CongDung (
    MaCongDung INT PRIMARY KEY,
    TenCongDung NVARCHAR(100)
);

CREATE TABLE KhachHang (
    MaKhach INT PRIMARY KEY,
    TenKhach NVARCHAR(100),
    DiaChi NVARCHAR(200),
    DienThoai NVARCHAR(15)
);

CREATE TABLE NhaCungCap (
    MaNCC INT PRIMARY KEY,
    TenNCC NVARCHAR(100),
    DiaChi NVARCHAR(200),
    DienThoai NVARCHAR(15)
);

CREATE TABLE DacDiem (
    MaDacDiem INT PRIMARY KEY,
    TenDacDiem NVARCHAR(100)
);

CREATE TABLE GongMat (
    MaLoaiGong INT PRIMARY KEY,
    TenLoaiGong NVARCHAR(100)
);

-- Tạo bảng NhanVien với các thông tin đăng nhập (emailNV, MkNV) và trạng thái tài khoản IsActive
CREATE TABLE NhanVien (
    MaNV INT PRIMARY KEY,
    TenNV NVARCHAR(100),
    GioiTinh NVARCHAR(10),
    NgaySinh DATE,
    DienThoai NVARCHAR(15),
    DiaChi NVARCHAR(200),
    MaCV INT,
    emailNV NVARCHAR(100) UNIQUE,  -- Email của nhân viên, đảm bảo không trùng lặp
    MkNV NVARCHAR(100),            -- Mật khẩu của nhân viên
    FOREIGN KEY (MaCV) REFERENCES CongViec(MaCV)
);

-- Tạo bảng DanhMucHangHoa với các khóa ngoại liên quan
CREATE TABLE DanhMucHangHoa (
    MaHang INT PRIMARY KEY,
    TenHang NVARCHAR(100),
    MaLoai INT,
    MaLoaiGong INT,
    MaDangMat INT,
    MaChatLieu INT,
    MaDiop INT,
    MaCongDung INT,
    MaDacDiem INT,
    MaMau INT,
    MaNuocSX INT,
    SoLuong INT,
    DonGiaNhap DECIMAL(10, 2),
    DonGiaBan DECIMAL(10, 2),
    ThoiGianBaoHanh INT,
    GhiChu NVARCHAR(200),
    IsActive BIT DEFAULT 1,          -- Trạng thái của sản phẩm
    FOREIGN KEY (MaLoai) REFERENCES LoaiKinh(MaLoai),
    FOREIGN KEY (MaLoaiGong) REFERENCES GongMat(MaLoaiGong),
    FOREIGN KEY (MaDangMat) REFERENCES HinhDangMat(MaDangMat),
    FOREIGN KEY (MaChatLieu) REFERENCES ChatLieu(MaChatLieu),
    FOREIGN KEY (MaDiop) REFERENCES Diop(MaDiop),
    FOREIGN KEY (MaDacDiem) REFERENCES DacDiem(MaDacDiem),
    FOREIGN KEY (MaMau) REFERENCES MauSac(MaMau),
    FOREIGN KEY (MaNuocSX) REFERENCES NuocSanXuat(MaNuocSX),
    FOREIGN KEY (MaCongDung) REFERENCES CongDung(MaCongDung)
);

-- Tạo bảng HoaDonBan
CREATE TABLE HoaDonBan (
    SoHDB INT PRIMARY KEY,
    MaNV INT,
    NgayBan DATE,
    MaKhach INT,
    TongTien DECIMAL(15, 2),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    FOREIGN KEY (MaKhach) REFERENCES KhachHang(MaKhach)
);

-- Tạo bảng HoaDonNhap
CREATE TABLE HoaDonNhap (
    SoHDN INT PRIMARY KEY,
    MaNV INT,
    NgayNhap DATE,
    MaNCC INT,
    TongTien DECIMAL(15, 2),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC)
);

-- Tạo bảng ChiTietHoaDonBan
CREATE TABLE ChiTietHoaDonBan (
    SoHDB INT,
    MaHang INT,
    SoLuong INT,
    GiamGia DECIMAL(5, 2),
    ThanhTien DECIMAL(15, 2),
    PRIMARY KEY (SoHDB, MaHang),
    FOREIGN KEY (SoHDB) REFERENCES HoaDonBan(SoHDB),
    FOREIGN KEY (MaHang) REFERENCES DanhMucHangHoa(MaHang)
);

-- Tạo bảng ChiTietHoaDonNhap
CREATE TABLE ChiTietHoaDonNhap (
    SoHDN INT,
    MaHang INT,
    SoLuong INT,
    DonGia DECIMAL(10, 2),
    GiamGia DECIMAL(5, 2),
    ThanhTien DECIMAL(15, 2),
    PRIMARY KEY (SoHDN, MaHang),
    FOREIGN KEY (SoHDN) REFERENCES HoaDonNhap(SoHDN),
    FOREIGN KEY (MaHang) REFERENCES DanhMucHangHoa(MaHang)
);


--ALTER TABLE KhachHang ADD IsActive BIT DEFAULT 1;
--ALTER TABLE NhanVien ADD IsActive BIT DEFAULT 1;
--ALTER TABLE NhaCungCap ADD IsActive BIT DEFAULT 1;
--ALTER TABLE DanhMucHangHoa ADD IsActive BIT DEFAULT 1;

CREATE TRIGGER trg_SoftDeleteKhachHang
ON KhachHang
INSTEAD OF DELETE
AS
BEGIN
    UPDATE KhachHang
    SET IsActive = 0
    WHERE MaKhach IN (SELECT MaKhach FROM deleted);

    -- Cập nhật HoaDonBan
    UPDATE HoaDonBan
    SET MaKhach = NULL
    WHERE MaKhach IN (SELECT MaKhach FROM deleted);
END;

CREATE TRIGGER trg_UpdateKhachHang
ON KhachHang
AFTER UPDATE
AS
BEGIN
    -- Cập nhật thông tin khách hàng trong các hóa đơn
    UPDATE HoaDonBan
    SET HoaDonBan.MaKhach = inserted.MaKhach
    FROM HoaDonBan
    JOIN inserted ON HoaDonBan.MaKhach = inserted.MaKhach;
END;

CREATE TRIGGER trg_UpdateKhachHang
ON KhachHang
AFTER UPDATE
AS
BEGIN
    -- Cập nhật thông tin khách hàng trong các hóa đơn
    UPDATE HoaDonBan
    SET HoaDonBan.MaKhach = inserted.MaKhach
    FROM HoaDonBan
    JOIN inserted ON HoaDonBan.MaKhach = inserted.MaKhach;
END;


-- Thêm dữ liệu vào bảng KhoiLuong
INSERT INTO KhoiLuong (MaKhoiLuong, TenKhoiLuong) VALUES
(1, N'Nhẹ'),
(2, N'Trung bình'),
(3, N'Nặng'),
(4, N'Siêu nhẹ'),
(5, N'Siêu nặng');

-- Thêm dữ liệu vào bảng LoaiKinh
INSERT INTO LoaiKinh (MaLoai, TenLoai) VALUES
(1, N'Kính cận'),
(2, N'Kính viễn'),
(3, N'Kính đa tròng'),
(4, N'Kính chống ánh sáng xanh'),
(5, N'Kính râm');

-- Thêm dữ liệu vào bảng HinhDangMat
INSERT INTO HinhDangMat (MaDangMat, TenDangMat) VALUES
(1, N'Oval'),
(2, N'Tròn'),
(3, N'Vuông'),
(4, N'Trái xoan'),
(5, N'Trái tim');

-- Thêm dữ liệu vào bảng ChatLieu
INSERT INTO ChatLieu (MaChatLieu, TenChatLieu) VALUES
(1, N'Nhựa'),
(2, N'Kim loại'),
(3, N'Titan'),
(4, N'Acetate'),
(5, N'Nhựa dẻo');

-- Thêm dữ liệu vào bảng NuocSanXuat
INSERT INTO NuocSanXuat (MaNuocSX, TenNuocSX) VALUES
(1, N'Việt Nam'),
(2, N'Nhật Bản'),
(3, N'Hàn Quốc'),
(4, N'Ý'),
(5, N'Mỹ');

-- Thêm dữ liệu vào bảng MauSac
INSERT INTO MauSac (MaMau, TenMau) VALUES
(1, N'Đen'),
(2, N'Trắng'),
(3, N'Nâu'),
(4, N'Xanh dương'),
(5, N'Đỏ');

-- Thêm dữ liệu vào bảng Diop
INSERT INTO Diop (MaDiop, TenDiop) VALUES
(1, N'-1.00'),
(2, N'-2.00'),
(3, N'+1.50'),
(4, N'+2.50'),
(5, N'0.00');

-- Thêm dữ liệu vào bảng CongViec
--DELETE from CongViec WHERE MaCV=6
INSERT INTO CongViec (MaCV, TenCV, MucLuong) VALUES
(6, N'Quản lý', 10000000),
(1, N'Nhân viên bán hàng', 5000000),
(2, N'Kỹ thuật viên', 7000000),
(3, N'Quản lý cửa hàng', 10000000),
(4, N'Nhân viên kho', 5500000),
(5, N'Nhân viên chăm sóc khách hàng', 6000000);

-- Thêm dữ liệu vào bảng CongDung
INSERT INTO CongDung (MaCongDung, TenCongDung) VALUES
(1, N'Điều chỉnh thị lực'),
(2, N'Chống tia UV'),
(3, N'Chống chói'),
(4, N'Thời trang'),
(5, N'Bảo vệ mắt khi làm việc');

-- Thêm dữ liệu vào bảng KhachHang
INSERT INTO KhachHang (MaKhach, TenKhach, DiaChi, DienThoai) VALUES
(1, N'Nguyễn Văn A', N'Hà Nội', '0901234567'),
(2, N'Trần Thị B', N'Hồ Chí Minh', '0912345678'),
(3, N'Lê Văn C', N'Đà Nẵng', '0923456789'),
(4, N'Phạm Thị D', N'Hải Phòng', '0934567890'),
(5, N'Hoàng Văn E', N'Cần Thơ', '0945678901');

-- Thêm dữ liệu vào bảng NhaCungCap
INSERT INTO NhaCungCap (MaNCC, TenNCC, DiaChi, DienThoai) VALUES
(1, N'Công ty TNHH Kính mắt ABC', N'Hà Nội', '0241234567'),
(2, N'Công ty CP Kính mắt XYZ', N'Hồ Chí Minh', '0282345678'),
(3, N'Công ty TNHH Kính mắt 123', N'Đà Nẵng', '0233456789'),
(4, N'Công ty CP Kính mắt 456', N'Hải Phòng', '0254567890'),
(5, N'Công ty TNHH Kính mắt 789', N'Cần Thơ', '0295678901');

-- Thêm dữ liệu vào bảng DacDiem
INSERT INTO DacDiem (MaDacDiem, TenDacDiem) VALUES
(1, N'Chống trầy xước'),
(2, N'Chống nước'),
(3, N'Chống va đập'),
(4, N'Siêu nhẹ'),
(5, N'Có thể gập');

-- Thêm dữ liệu vào bảng GongMat
INSERT INTO GongMat (MaLoaiGong, TenLoaiGong) VALUES
(1, N'Gọng kim loại'),
(2, N'Gọng nhựa'),
(3, N'Gọng titan'),
(4, N'Gọng không viền'),
(5, N'Gọng nửa viền');
--use master
--DELETE FROM NhanVien
--WHERE MaNV = 6
-- Thêm dữ liệu vào bảng NhanVien
INSERT INTO NhanVien (MaNV, TenNV, GioiTinh, NgaySinh, DienThoai, DiaChi, MaCV, emailNV, MkNV) VALUES
(9, N'Trần Trường Thức', N'Nam', '2004-10-18', '0824095988', N'Thanh Hóa', 3, 'truongthuc1810@gmail.com', '123'),
(1, N'Nguyễn Thị F', N'Nữ', '1990-01-01', '0956789012', N'Hà Nội', 1, 'nguyenthif@example.com', 'password123'),
(2, N'Trần Văn G', N'Nam', '1992-02-02', '0967890123', N'Hồ Chí Minh', 2, 'tranvang@example.com', 'pass456'),
(3, N'Lê Thị H', N'Nữ', '1988-03-03', '0978901234', N'Đà Nẵng', 3, 'lethih@example.com', 'mypassword789'),
(4, N'Phạm Văn I', N'Nam', '1995-04-04', '0989012345', N'Hải Phòng', 4, 'phamvani@example.com', 'complexpass101'),
(5, N'Hoàng Thị K', N'Nữ', '1993-05-05', '0990123456', N'Cần Thơ', 5, 'hoangthik@example.com', 'simplepass202');

-- Thêm dữ liệu vào bảng DanhMucHangHoa
INSERT INTO DanhMucHangHoa (MaHang, TenHang, MaLoai, MaLoaiGong, MaDangMat, MaChatLieu, MaDiop, MaCongDung, MaDacDiem, MaMau, MaNuocSX, SoLuong, DonGiaNhap, DonGiaBan, ThoiGianBaoHanh, GhiChu) VALUES
(1, N'Kính cận thời trang', 1, 1, 1, 1, 1, 1, 1, 1, 1, 100, 500000, 750000, 12, N'Kính cận thời trang cho giới trẻ'),
(2, N'Kính râm cao cấp', 5, 2, 2, 2, 5, 2, 2, 2, 2, 50, 1000000, 1500000, 24, N'Kính râm cao cấp chống tia UV'),
(3, N'Kính đa tròng', 3, 3, 3, 3, 3, 3, 3, 3, 3, 75, 1500000, 2250000, 36, N'Kính đa tròng cho người trung niên'),
(4, N'Kính chống ánh sáng xanh', 4, 4, 4, 4, 5, 4, 4, 4, 4, 200, 800000, 1200000, 18, N'Kính chống ánh sáng xanh cho dân văn phòng'),
(5, N'Kính thể thao', 5, 5, 5, 5, 5, 5, 5, 5, 5, 80, 1200000, 1800000, 24, N'Kính thể thao chống va đập');

-- Thêm dữ liệu vào bảng HoaDonBan
INSERT INTO HoaDonBan (SoHDB, MaNV, NgayBan, MaKhach, TongTien) VALUES
(1, 1, '2024-03-01', 1, 750000),
(2, 2, '2024-03-02', 2, 1500000),
(3, 3, '2024-03-03', 3, 2250000),
(4, 4, '2024-03-04', 4, 1200000),
(5, 5, '2024-03-05', 5, 1800000);

-- Thêm dữ liệu vào bảng HoaDonNhap
INSERT INTO HoaDonNhap (SoHDN, MaNV, NgayNhap, MaNCC, TongTien) VALUES
(1, 1, '2024-02-01', 1, 50000000),
(2, 2, '2024-02-02', 2, 75000000),
(3, 3, '2024-02-03', 3, 100000000),
(4, 4, '2024-02-04', 4, 80000000),
(5, 5, '2024-02-05', 5, 90000000);

-- Thêm dữ liệu vào bảng ChiTietHoaDonBan
INSERT INTO ChiTietHoaDonBan (SoHDB, MaHang, SoLuong, GiamGia, ThanhTien) VALUES
(1, 1, 1, 0, 750000),
(2, 2, 1, 0, 1500000),
(3, 3, 1, 0, 2250000),
(4, 4, 1, 0, 1200000),
(5, 5, 1, 0, 1800000);

-- Thêm dữ liệu vào bảng ChiTietHoaDonNhap
INSERT INTO ChiTietHoaDonNhap (SoHDN, MaHang, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
(1, 1, 100, 500000, 0, 50000000),
(2, 2, 50, 1000000, 0, 50000000),
(3, 3, 75, 1500000, 0, 112500000),
(4, 4, 200, 800000, 0, 160000000),
(5, 5, 80, 1200000, 0, 96000000);

-- Giả sử các giá trị MaNV và MaCV đã tồn tại trong bảng NhanVien và CongViec
SELECT * from NhaCungCap
SELECT * FROM NhanVien
SELECT * FROM HoaDonNhap
select * FROM ChiTietHoaDonNhap
join HoaDonNhap on ChiTietHoaDonNhap.SoHDN=HoaDonNhap.SoHDN
SELECT * FROM HoaDonNhap
SELECT * FROM CongViec
SELECT * FROM HoaDonBan
SELECT * FROM DanhMucHangHoa
SELECT * FROM ChiTietHoaDonBan
