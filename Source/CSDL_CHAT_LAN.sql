
CREATE DATABASE Chatlan;

USE Chatlan;

CREATE TABLE nguoidung (
    ID INT NOT NULL IDENTITY(1,1),
    Username varchar(20) NOT NULL,
    Passwd varchar(20),
	Email varchar(50),
    PRIMARY KEY (ID)
)


SELECT * FROM nguoidung


CREATE TABLE Dethi(
	ID_Hoi int not null identity(1,1),
	CauHoi nvarchar(500) not null,	
	PRIMARY KEY (ID_Hoi)
)


SELECT * FROM Dethi

--câu 1
insert into Dethi(CauHoi) values(N'Tên gọi của máy tính để bàn?')
insert into Dethi(CauHoi) values(N'| .Desktop .Laptop .Mainframe .Computer .Desktop')
--câu 2
insert into Dethi(CauHoi) values(N'1 byte bằng bao nhiêu bit?')
insert into Dethi(CauHoi) values(N'| .12 bit .8 bit .16 bit .5 bit .8 bit')
--câu 3
insert into Dethi(CauHoi) values(N'Ai sáng lập ra Microsoft?')
insert into Dethi(CauHoi) values(N'| .Steve Jobs .Bill Gates .Elon Musk .Jeff Bezos .Bill Gates')
--câu 4
insert into Dethi(CauHoi) values(N'iPhone là tên gọi của hãng điện thoại nào?')
insert into Dethi(CauHoi) values(N'| .Motorola .Sony .Nokia .Apple .Apple')
--câu 5
insert into Dethi(CauHoi) values(N'Linux do ai sáng tạo ra?')
insert into Dethi(CauHoi) values(N'| .Bill Gates .Linus Torvalds .Steve Jobs .Larry Ellison .Linus Torvalds')
--câu 6
insert into Dethi(CauHoi) values(N'Thủ đô của Việt Nam?')
insert into Dethi(CauHoi) values(N'| .Sài Gòn .Đà Nẵng .Hà Nội .Đà Lạt .Hà Nội')
--câu 7
insert into Dethi(CauHoi) values(N'Nhật Bản thuộc châu lục nào?')
insert into Dethi(CauHoi) values(N'| .Châu Mỹ .Châu Âu .Châu Phi .Châu Á .Châu Á')
--câu 8
insert into Dethi(CauHoi) values(N'Samsung là của nước nào?')
insert into Dethi(CauHoi) values(N'| .Hàn Quốc .Nhật Bản .Việt Nam .Trung Quốc .Hàn Quốc')
--câu 9
insert into Dethi(CauHoi) values(N'Windows 10 là của hãng nào?')
insert into Dethi(CauHoi) values(N'| .BKAV .Kaspersky .IBM .Microsoft .Microsoft')
--câu 10
insert into Dethi(CauHoi) values(N'Tên gọi của bộ nhớ truy cập ngẫu nhiên?')
insert into Dethi(CauHoi) values(N'| .HDD .RAM .ROM .SSD .RAM')