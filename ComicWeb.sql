USE master
go

drop database ComicWeb
go

CREATE DATABASE ComicWeb
GO

USE ComicWeb
GO


CREATE TABLE Administrator
(
	UserID INT IDENTITY(1,1),
	Username VARCHAR(64),
	Passwords VARCHAR(256),
	Fullname NVARCHAR(64),
	Email VARCHAR(256),
	Avatar VARCHAR(256),
	isAdmin INT,
	Allowed INT,
	CONSTRAINT PK_BlA
	PRIMARY KEY(UserID)
)

CREATE TABLE Category
(
	CategoryID VARCHAR(64),
	Categoryname NVARCHAR(256) not null,
	Descriptions NVARCHAR(256),
	CONSTRAINT PK_BlC
	PRIMARY KEY(CategoryID)
)

CREATE TABLE Comic
(
	ComicID INT IDENTITY(1,1),
	Comicname NVARCHAR(512),
	Picture NVARCHAR(256),
	Author NVARCHAR(256),
	Brief NVARCHAR(2000),
	CreateDate DATETIME,
	Tags NVARCHAR(128),
	ViewNo INT,
	Statuss NVARCHAR(32),
	UserID INT not null,
	CONSTRAINT PK_BlP
	PRIMARY KEY(ComicID)
)

CREATE TABLE Category_Comic
(
	ComicID INT,
	CategoryID VARCHAR(64),
	Descriptions NVARCHAR(256),
	CONSTRAINT PK_Ca_Co
	PRIMARY KEY(ComicID, CategoryID)
)

CREATE TABLE TopComic
(
	TopComicID VARCHAR(64),
	TopName NVARCHAR(128),
	CONSTRAINT PK_TC
	PRIMARY KEY(TopComicID)
)

CREATE TABLE TopName
(
	TopComicID VARCHAR(64),
	ComicID INT,
	Descriptions NVARCHAR(256),
	CONSTRAINT PK_TN
	PRIMARY KEY(TopComicID, ComicID)
)

CREATE TABLE Follow
(
	UserID INT,
	ComicID INT,
	Descriptions NVARCHAR(256),
	CONSTRAINT PK_FL
	PRIMARY KEY(UserID, ComicID)
)
GO

ALTER TABLE Administrator ADD DEFAULT 0 FOR isAdmin
ALTER TABLE Administrator ADD DEFAULT 1 FOR Allowed
ALTER TABLE Comic ADD DEFAULT N'Đang Cập Nhật' FOR Author
ALTER TABLE Comic ADD DEFAULT N'Xem rồi sẽ biết' FOR Brief
ALTER TABLE Comic ADD DEFAULT 0 FOR ViewNo
ALTER TABLE Comic ADD DEFAULT GETDATE() FOR CreateDate
ALTER TABLE Comic ADD DEFAULT 1 FOR UserID
ALTER TABLE Comic ADD CONSTRAINT FK_Co_Ad FOREIGN KEY (UserID) REFERENCES Administrator(UserID)
ALTER TABLE Category_Comic ADD CONSTRAINT FK_CaCo_Co FOREIGN KEY (ComicID) REFERENCES Comic(ComicID)
ALTER TABLE Category_Comic ADD CONSTRAINT FK_CaCo_Ca FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID)
ALTER TABLE Follow ADD CONSTRAINT FK_FL_Ad FOREIGN KEY (UserID) REFERENCES Administrator(UserID)
ALTER TABLE Follow ADD CONSTRAINT FK_FL_Co FOREIGN KEY (ComicID) REFERENCES Comic(ComicID)
ALTER TABLE TopName ADD CONSTRAINT FK_TN_Ca FOREIGN KEY (ComicID) REFERENCES Comic(ComicID)
ALTER TABLE TopName ADD CONSTRAINT FK_TN_TC FOREIGN KEY (TopComicID) REFERENCES TopComic(TopComicID)
Go

INSERT INTO Administrator(Username,Passwords,Fullname,Avatar,Email,isAdmin,Allowed)
VALUES ('TPDUC', '123456', N'Trần Phước Đức', 'TrangQuynh.png', 'Tranphuocduc1998@gmail.com', 1, 1)
INSERT INTO Administrator(Username,Passwords,Fullname,Avatar,Email)
VALUES ('DUC', '123456', N'Trần Phước Đức', 'TrangQuynh.png', 'Tranphuocduc1998@gmail.com')
GO

INSERT INTO Comic(Comicname,Picture,ViewNo, Author)
VALUES (N'Chú mèo máy đến từ tương lai', 'Doremon.jpg', 1500, N'Fujiko Fujio')
INSERT INTO Comic(Comicname,Picture,ViewNo, Author)
VALUES (N'Thám tử Conan', 'Conan.jpg', 1450, N'Aoyama Gōshō')
INSERT INTO Comic(Comicname,Picture,ViewNo, Author)
VALUES (N'Vua hải tặc', 'OnePice.jpg', 1440, N'Oda Eiichiro')
INSERT INTO Comic(Comicname,Picture,ViewNo)
VALUES (N'Thần đồng đất Việt', 'TDVN.jpg', 1445)
INSERT INTO Comic(Comicname,Picture,ViewNo, Author)
VALUES (N'Naruto', 'Naruto.jpeg', 1430, N'Kishimoto Masashi')
INSERT INTO Comic(Comicname,Picture,ViewNo, Author)
VALUES (N'Fairy Tail', 'FairyTail.jpg', 1442, N'Mashima Hiro')
INSERT INTO Comic(Comicname,Picture,ViewNo)
VALUES (N'Ranma 1/2', 'ranma01.jpg', 1420)
INSERT INTO Comic(Comicname,Picture,ViewNo)
VALUES (N'Bạn trai hắc đạo của tôi', 'Ban-Trai-Hac-Dao-Cua-Toi.jpg', 1400)
INSERT INTO Comic(Comicname,Picture,ViewNo)
VALUES (N'Con tim rung động', 'con-tim-rung-dong.jpg', 1390)
INSERT INTO Comic(Comicname,Picture,ViewNo)
VALUES (N'Slime', 'imgSlime.jpg', 1388)
INSERT INTO Comic(Comicname,Picture,ViewNo)
VALUES (N'Dụ hoặc yêu miêu', 'yeu-meu.png', 1380)
INSERT INTO Comic(Comicname,Picture,ViewNo)
VALUES (N'Arifureta shokugyou de sekai saikyou', 'Arifureta.jpg', 1395)
INSERT INTO Comic(Comicname,Picture,ViewNo)
VALUES (N'Maou-jou de Oyasumi', 'Oyasumi.jpg', 1390)
GO

INSERT INTO Category(CategoryID,Categoryname)
VALUES('CHH', N'Hài Hước')
INSERT INTO Category(CategoryID,Categoryname)
VALUES('CHD', N'Hành Động')
INSERT INTO Category(CategoryID,Categoryname)
VALUES('CPL', N'Phiêu Lưu')
INSERT INTO Category(CategoryID,Categoryname)
VALUES('CKD', N'Kinh Dị')
INSERT INTO Category(CategoryID,Categoryname)
VALUES('CF', N'Fantasy')
INSERT INTO Category(CategoryID,Categoryname)
VALUES('CNT', N'Ngôn Tình')
INSERT INTO Category(CategoryID,Categoryname)
VALUES('CTC', N'Tình Cảm')
INSERT INTO Category(CategoryID,Categoryname)
VALUES ('CIS', N'Isekai')
GO

INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(1, 'CHH')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(1, 'CPL')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(2, 'CPL')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(2, 'CHD')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(2, 'CKD')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(3, 'CHH')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(3, 'CHD')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(3, 'CPL')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(4, 'CHH')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(5, 'CF')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(5, 'CHH')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(5, 'CHD')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(5, 'CPL')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(6, 'CF')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(6, 'CHH')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(6, 'CHD')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(6, 'CPL')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(7, 'CHH')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(7, 'CPL')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(7, 'CHD')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(8, 'CTC')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(8, 'CHH')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(9, 'CTC')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(9, 'CHH')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(10, 'CHH')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(10, 'CF')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(10, 'CHD')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES(10, 'CPL')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES (10, 'CIS')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES (11, 'CTC')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES (11, 'CHH')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES (12, 'CIS')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES (12, 'CPL')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES (12, 'CHH')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES (13, 'CHH')
INSERT INTO Category_Comic(ComicID, CategoryID)
VALUES (13, 'CIS')
GO

INSERT INTO TopComic(TopComicID, TopName)
VALUES ('TCG', N'Top Truyện dành cho nữ')
INSERT INTO TopComic(TopComicID, TopName)
VALUES ('TCM', N'Top Truyện dành cho nam')
GO

INSERT INTO TopName(TopComicID, ComicID)
VALUES ('TCG', 8)
INSERT INTO TopName(TopComicID, ComicID)
VALUES ('TCG', 9)
INSERT INTO TopName(TopComicID, ComicID)
VALUES ('TCG', 11)
INSERT INTO TopName(TopComicID, ComicID)
VALUES ('TCM', 10)
INSERT INTO TopName(TopComicID, ComicID)
VALUES ('TCM', 12)
INSERT INTO TopName(TopComicID, ComicID)
VALUES ('TCM', 13)
GO



SELECT Comicname, Comic.Author FROM Comic, Category_Comic WHERE Comic.ComicID=Category_Comic.ComicID AND CategoryID = 'CHH'

select * from Comic, TopName where Comic.ComicID = TopName.ComicID  and TopName.TopComicID = 'TCM' order by ViewNo DESC
                 