
CREATE TABLE Roles --F,C,E,A
(
	[RoleId] Char(1) CONSTRAINT pk_RoleId PRIMARY KEY,
    [RoleName] VARCHAR(20) CONSTRAINT uq_RoleName UNIQUE
)

CREATE TABLE Users --all users
(
	[Name] VARCHAR(100),
	[UId] INT Identity(100,1) CONSTRAINT pk_d PRIMARY KEY,
	[EmailId] VARCHAR(50) not null,
	[UserPassword] VARCHAR(15) NOT NULL,
	[RoleId] Char(1) CONSTRAINT fk_RoleId REFERENCES Roles(RoleId),
	[PAN] VARCHAR(10),
	[PhoneNo] bigint not null,
	[Address] VARCHAR(200) NOT NULL,
	[Security] varchar(100) not null,
	[Pin]varchar(6) Not Null 
)

CREATE TABLE Categories
(
	[CategoryId] int identity(0,1) CONSTRAINT pk_CategoryId PRIMARY KEY,
	[CategoryName] VARCHAR(20) CONSTRAINT uq_CategoryName UNIQUE NOT NULL 
)
CREATE TABLE ItemSellable
(
	[CategoryId]  int CONSTRAINT fk_CatId REFERENCES Categories(CategoryId),
	[CategoryName] VARCHAR(20) PRIMARY KEY 
)



create table farmerStock
(
	[UId] INT CONSTRAINT fk_hdrr REFERENCES users(UId) not null,
	[Quantity] INT not null,
	[Item] VARCHAR(50) not null,
	[PricePerUnit] Numeric(10,2) not null,
	[Pincode] varchar(6) not null,
	
	[CategoryId] int CONSTRAINT fk_ChioId REFERENCES Categories(CategoryId),
		 Constraint pk_farmerstocks 
       PRIMARY KEY (UId,[Item])
)

create table auctionstock
(
	[UId] INT CONSTRAINT fk_id REFERENCES users(UId) not null,
	[Quantity] INT not null,
	[Item] VARCHAR(50) not null,
	[PricePerUnit] Numeric(10,2) not null,
	
	
	[CategoryId] int CONSTRAINT fk_ChouiceId REFERENCES Categories(CategoryId),
		 Constraint pk_farmerstocksss
       PRIMARY KEY (UId,[Item])
)

--ALTER TABLE farmerStock
--    ADD  PRIMARY KEY (Uid,Item)
	

create table purchaseDetails
(
	[Buyer] int not null CONSTRAINT fk_UssId REFERENCES users(UId),
	[Seller] int not null,
	[Name] varchar(15),
	[PurchaseType] Char(1),					-- Bulk or Regular
	[PurchaseId] int identity(101,1) constraint pk_purchid primary key,		-- Like 101... agar ek kg wheat khridi den ek kg rice dono ki purchaseId alag alag hogi
	[DeliveryDate] date,			-- date+7
	[OrderedDate] datetime,
	[ItemName] varchar(50) not null,
	[QuantityPurchased] INT not null,
	[PricePerUnit] Numeric(10,2) not null,
	[TotalAmount] NUMERIC(10,2) not null
)

create table cart
(
[cartid] int identity(1,1) Constraint pk_cNo PRIMARY KEY,
	[Buyer] int, 
	[Seller] int,
	[PricePerUnit] Numeric(10,2) not null,
	[ItemName] VARCHAR(50) not null
	
)

create table farmBank
(
	[AccountNumber] numeric(15) Constraint pk_accNo PRIMARY KEY,
	[Branch] VARCHAR(50),
	--[PAN] VARCHAR(10) unique,
	[Amount] numeric(15),
	[IFSC] VARCHAR(11) not null,
	[UId] int constraint fk_farmID REFERENCES users(UId)
)


create table feedback --serves as query 
(
	[EmailId] VARCHAR(50) Constraint pk_ao PRIMARY KEY ,
	[Name] VARCHAR(50) not null,
	[Description] VARCHAR(1200) NOT NULL
)



create table Requests --USED BY CUSTOMER/VIEWED BY EMP
(
	[CId] int  CONSTRAINT fk_CatryId REFERENCES Users(UId) not null,
	[Quantity] int not null,
	[Item] VARCHAR(50) not null,
	[RId] int identity(100,1) Constraint pk_ho PRIMARY KEY,
	[ForwardStatus] char(1)	
)
create table AdminForwardedRequest --passed to farmer
(
	[CId] int  CONSTRAINT fk_this REFERENCES Users(UId) not null,
	[CustomerRID] int,
	[Quantity] int not null,
	[Item] VARCHAR(50) not null,
	[ARId] int identity(100,1) Constraint pk_home PRIMARY KEY,	
	[FarmerId] int,
	[Status] char(1) 
)
create table Notifications --bell icon
(
	[UserId] int not null,
	[Description] varchar(100) not null,
	[FarmerId] int,
	[NId] int identity(100,1) Constraint pk_hop PRIMARY KEY	,
	[Created] DateTime
)

create table Auctioncart
(
	[cartid] int identity(1,1) Constraint pk_cNoo PRIMARY KEY,
	[AuctionId] int,
	[Buyer] int, 
	[Seller] int,
	[TotalPrice] Numeric(10,2) not null,
	[ItemName] VARCHAR(50) not null
	
)


create table AuctionItem --
(
	
	[SellerUId] INT CONSTRAINT fk_hdrrr REFERENCES users(UId) not null,
	[FinalQuantity] INT not null,
	[Item] VARCHAR(50) not null,
	[PricePerUnit] Numeric(10,2) not null,
	[BasePrice] Numeric(10,2) not null,
	[StartDate] DateTime,
	[EndDate] DateTime,
	[AuctionId] int Identity(0,1) primary Key,
	[EmpId] int CONSTRAINT fk_h REFERENCES users(UId) not null,
	[AuctionStatus] VARCHAR(6)
	
)

create table AuctionBid --
(
	[AuctionId] int CONSTRAINT fk_aucId REFERENCES AuctionItem(AuctionId),
	[BidderId] int  CONSTRAINT fk_Bidid REFERENCES users(UId) not null,
	[BidId] int Identity(0,1) primary Key,
	[BidDate] DateTime,
	[BidAmount]  Numeric(10,2) not null

)


create table PastAuctionResult --
(
	[AuctionId] int CONSTRAINT fk_auctionId REFERENCES AuctionItem(AuctionId),
	[WinnerId] int  CONSTRAINT fk_Biderid REFERENCES users(UId) not null,
	[FarmerId] int Constraint fk_farid references  users(UId) not null,
	[BidAmount]  Numeric(10,2) not null,
	[EndDate] DateTime,
	[AuctionResult] int identity(0,1) primary key

)



INSERT INTO Roles (RoleId, RoleName) VALUES ('F', 'Farmer')
INSERT INTO Roles (RoleId, RoleName) VALUES ('C', 'Customer')
INSERT INTO Roles (RoleId, RoleName) VALUES ('E', 'Employee')
INSERT INTO Roles (RoleId, RoleName) VALUES ('A', 'Admin')


INSERT INTO Users( Name,EmailId,UserPassword,RoleId,Address,PAN,PhoneNo,security,Pin) VALUES('Vaishali','vaishali13.trn@infosys.com','vaishali','F','kangra','GHEPS9772L',8628900913,'kangra','176001')
INSERT INTO Users( Name,EmailId,UserPassword,RoleId,Address,PAN,PhoneNo,security,Pin) VALUES('dikshit','dikshit01.trn@infosys.com','vaishali','F','Wakhnaghat','GHEPS9773L',8628900923,'kangra','173234')
INSERT INTO Users( Name,EmailId,UserPassword,RoleId,Address,PAN,PhoneNo,security,Pin) VALUES('nikesh','nikesh01.trn@infosys.com','vaishali','F','jalandhar','GHEPS9774L',8628900933,'kangra','144001')
INSERT INTO Users( Name,EmailId,UserPassword,RoleId,Address,PhoneNo,security,Pin) VALUES('abir','abir.trn@infosys.com','vaishali','C','shimla',8628900943,'kangra','171001')
INSERT INTO Users( Name,EmailId,UserPassword,RoleId,Address,PhoneNo,security,Pin) VALUES('sagar','sagar.trn@infosys.com','vaishali','C','Jalandhar east',8628900953,'kangra','144320')
INSERT INTO Users( Name,EmailId,UserPassword,RoleId,Address,PhoneNo,security,Pin) VALUES('Manika','manika.trn@infosys.com','vaishali','C','shimla',8628900983,'kangra','171001')
INSERT INTO Users( Name,EmailId,UserPassword,RoleId,Address,PhoneNo,security,Pin) VALUES('ekesh','ekesh.trn@infosys.com','vaishali','E','shimla',8628900783,'kangra','171006')
INSERT INTO Users( Name,EmailId,UserPassword,RoleId,Address,PhoneNo,security,Pin) VALUES('sakshi','saksho.trn@infosys.com','vaishali','E','shimla',8628900003,'kangra','171006')
INSERT INTO Users( Name,EmailId,UserPassword,RoleId,Address,PhoneNo,security,Pin) VALUES('himanshu','himanshu.trn@infosys.com','vaishali','E','shimla',8628900943,'kangra','176001')
INSERT INTO Users( Name,EmailId,UserPassword,RoleId,Address,PhoneNo,security,Pin) VALUES('admin','admin@biokart.com','vaishali','A','shimla',8628900943,'kangra','176001')



insert into Categories(CategoryName) values ('Fruits')
insert into Categories(CategoryName) values ('Vegetables')
insert into Categories(CategoryName) values ('Dairy')
insert into Categories(CategoryName) values ('Cereals')

insert into purchaseDetails values (104,100,'Jalebi Bai','R','04-16-2019','04-20-2019','Wheat',12,35,420)
insert into farmerStock(UId,Quantity,Item,PricePerUnit,CategoryId,Pincode) values (101,100,'Wheat',10,3,'173234')
insert into farmerStock(UId,Quantity,Item,PricePerUnit,CategoryId,Pincode) values (102,150,'Rice',10,3,'144001')
insert into farmerStock(UId,Quantity,Item,PricePerUnit,CategoryId,Pincode) values (102,150,'Apple',10,0,'144001')
insert into farmerStock(UId,Quantity,Item,PricePerUnit,CategoryId,Pincode) values (101,100,'Banana',10,0,'173234')
insert into farmerStock (UId,Quantity,Item,PricePerUnit,CategoryId,Pincode) values (101,100,'Plum',10,0,'173234')
insert into farmerStock (UId,Quantity,Item,PricePerUnit,CategoryId,Pincode) values (101,100,'Apricot',10,0,'173234')
insert into farmerStock (UId,Quantity,Item,PricePerUnit,CategoryId,Pincode) values (101,100,'Mangoes',10,0,'173234')

insert into farmerStock (UId,Quantity,Item,PricePerUnit,CategoryId,Pincode) values (101,100,'Cabbage',10,1,'173234')
insert into farmerStock (UId,Quantity,Item,PricePerUnit,CategoryId,Pincode) values (101,100,'Cauliflower',10,1,'173234')
insert into farmerStock (UId,Quantity,Item,PricePerUnit,CategoryId,Pincode) values (101,100,'Brocoli',10,1,'173234')
insert into farmerStock (UId,Quantity,Item,PricePerUnit,CategoryId,Pincode) values (101,100,'Potatoes',10,1,'173234')
insert into farmerStock (UId,Quantity,Item,PricePerUnit,CategoryId,Pincode) values (101,100,'Milk',10,2,'173234')
insert into farmerStock (UId,Quantity,Item,PricePerUnit,CategoryId,Pincode) values (101,100,'Curd',10,2,'173234')
insert into farmerStock (UId,Quantity,Item,PricePerUnit,CategoryId,Pincode) values (101,100,'Butter',10,2,'173234')
insert into farmerStock (UId,Quantity,Item,PricePerUnit,CategoryId,Pincode) values (101,100,'Lassi',10,2,'173234')
insert into farmerStock (UId,Quantity,Item,PricePerUnit,CategoryId,Pincode) values (101,100,'Cream',10,2,'173234')

insert into farmerStock (UId,Quantity,Item,PricePerUnit,CategoryId,Pincode) values (101,100,'Maize',10,3,'173234')
insert into farmerStock (UId,Quantity,Item,PricePerUnit,CategoryId,Pincode) values (101,100,'Barley',10,3,'173234')
insert into farmerStock (UId,Quantity,Item,PricePerUnit,CategoryId,Pincode) values (101,100,'Raggi',10,3,'173234')

insert into [feedback] values('abirsoni@gmail.com','Abir','Too Good! Doing well BioKart!')





