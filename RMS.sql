create table category
(
catID int primary key identity,
catName varchar(50)
)


create table tables
(
tid int primary key identity,
tname varchar(15)
)


create table staff
(
staffID int primary key identity,
sName varchar(50),
sPhone varchar(50),
sRole varchar(50)
)


create table products
(
pID int primary key identity,
pName varchar(50),
pPrice int,
CategoryID int,
pImage image
)


create table tblMain
(
MainID int primary key identity,
aDate date,
aTime varchar(15),
TableName varchar(10),
WaiterName varchar(15),
status varchar(15),
orderType varchar(15),
total int,
received int,
change int
)

create table tblDetails
(
DetailID int primary key identity,
MainID int,
proID int,
qty int,
price int,
amount int
)

select * from tblMain m 
inner join tblDetails d on m.MainID = d.MainID