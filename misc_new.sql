---online pharmacy

create database pharmacy199
use pharmacy199

create table Users
(
User_number int primary key identity(1, 1),
Nickname  nvarchar(50) not null,
Last_name nvarchar(50) not null,
Namee nvarchar(50) not null,
Patronymic nvarchar(50) not null,
Mail nvarchar(50) not null,
Phone_number nvarchar(25) not null,
Birthdate date not null
)
insert into Users
(
Nickname,
Last_name,
Namee,
Patronymic,
Mail,
Phone_number,
Birthdate
)
values
('misha55', 'polayana', 'misha', 'sergeevich', 'misha@mail.ru', '89234578658', '2000-12-12'),
('yana25', 'polayanaa', 'yana', 'sergeevich', 'yana@mail.ru', '89675437866', '2001-06-06')
create table Filterr
 (
 Id_categories int primary key identity(1, 1),
 product_availability bit not null,
 release_form nvarchar(50) not null,
 availability_of_discounts bit not null,
 discounts int null,
 vacation_from_the_pharmacy nvarchar(20) not null,
 indications nvarchar(50) not null,
 producer nvarchar(20) not null,
 expiration_date nvarchar(25) not null,
 brand_name nvarchar(25) not null,
 quantity_in_pack int not null,
 Price money not null,
 )
 insert into Filterr
(
 product_availability,
 release_form,
 availability_of_discounts,
 discounts,
 vacation_from_the_pharmacy,
 indications,
 producer,
 expiration_date,
 brand_name,
 quantity_in_pack,
 Price
 )
 values
 (1, 'kk', 0, 5, 'lkj', 'fgh', 'sjt', 'whf', 'tgd', 67, 430),
 (1, 'kt', 1, 10, 'gdd', 'sgy', 'rhk', 'kft', 'fgy', 35, 250)


create table Product 
(
Number_product int primary key identity(1, 1),
Id_categories int not null,
Namee nvarchar(50) not null,
Product_price decimal(25,2) not null,
Product_description nvarchar(50) not null,
Article VARCHAR(255) not null DEFAULT CONVERT(varchar(255), NEWID()),
foreign key (Id_categories) references Filterr(Id_categories),
)
insert into Product
(
Id_categories,
Namee,
Product_price,
Product_description
)
values
(1, 'Docktor mom', 430, 'holoso'),
(2, 'Paracetamol', 250, 'effectovno')



 create table Saved_address
(
User_idd INT NOT NULL,
Address_id INT NOT NULL IDENTITY(1, 1),
City nvarchar(50) not null,
Street nvarchar(50) not null,
House int not null,
Construction int not null,
Flat int not null,
Address_name nvarchar(100) not null,
primary key(User_idd, Address_id), 
foreign key (User_idd) references  Users(User_number)
)
insert into Saved_address
(
User_idd,
City,
Street,
House,
Construction,
Flat,
Address_name
)
values
(1, 'Ms', 'letov', 55, 2, 457, 'gjkhgu'),
(2, 'Par', 'may', 87, 6, 456, 'dyutyu')


create table Basket 
(
User_idd int,
Product_id int,
Quantity_of_goods int not null,
primary key(User_idd, Product_id),
foreign key (User_idd) references Users(User_number),
foreign key(Product_id) references Product(Number_product)
)
insert into Basket
(
User_idd,
Product_id,
Quantity_of_goods
)
values
(1, 1, 9),
(1, 2, 9),
(2, 1, 10)

create table Orderr
(
Order_Number int identity(1, 1),
User_idd int,
Number_product int,
Date_references datetime not null,
Statuss nvarchar(50) not null,
Quantity int not null,
primary key(Order_Number, User_idd, Number_product),
foreign key(User_idd) references Users(User_number),
foreign key(Number_product) references Product(Number_product)
)
insert into Orderr
(
User_idd,
Number_product,
Date_references,
Statuss,
Quantity
)
values
(1, 1, '2023-12-12', 'fdgh', 76),
(2, 2, '2023-06-06', 'fdgh', 43)
