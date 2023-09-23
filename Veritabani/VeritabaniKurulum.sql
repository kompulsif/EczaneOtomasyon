CREATE DATABASE Eczane;

go

use Eczane;

create table Eczaneler(
id int identity(1, 1),
adi nvarchar(100) not null,
adres nvarchar(500) not null,
turu char(1) not null,
kullaniciAdi nvarchar(30) not null,
parola nvarchar(50) not null,
);

go

create table Ilaclar(
kodu nvarchar(10) not null,
adi nvarchar(100) not null,
turu nvarchar(50) not null,
kullanim nvarchar(300) not null,
eczane int not null,
);

go

create table IlacTurleri(
id int identity(1, 1),
turu nvarchar(50) not null
);

go

insert into Eczaneler values (N'TEST NÖBETÇİ ECZANE', N'İstanbul/Kadıköy', '0', N'eczane1', N'qwerty');
insert into Eczaneler values (N'TEST NORMAL ECZANE', N'Ankara/Mamak', '1', N'eczane2', N'123456');

insert into IlacTurleri values (N'Tablet');
insert into IlacTurleri values (N'Toz');
insert into IlacTurleri values (N'Şurup');
insert into IlacTurleri values (N'Flakon');
insert into IlacTurleri values (N'Merhem');
insert into IlacTurleri values (N'Krem');

insert into Ilaclar values ('A3512', 'PAROL', 'Tablet', N'Ağızdan alınır, sabah akşam bir tane, tok içilecek', '1');
insert into Ilaclar values ('B412', 'Bengay', 'Krem', N'Ağrıyan bölgeye sürülür', '1');
insert into Ilaclar values ('C5124', 'Parasetamol', 'Tablet', N'Ağızdan alınır, sabah akşam öğlen bir kaşık, aç içilecek', '2');
insert into Ilaclar values ('B5124', 'Zoretanin', 'Tablet', N'Ağızdan alınır, öğlen bir kaşık, aç içilecek', '2');
