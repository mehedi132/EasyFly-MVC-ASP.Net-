Alter Login sa With Password = '123456'
Alter Login sa Enable

Create Database EasyFlycomDatabase;
Truncate Table EasyFlycomDatabase;

Use EasyFlycomDatabase;






/* ////////////////////////////////////////////////////////////////////////////////////////////// */






Drop Table SingleUserLog
Truncate Table SingleUserLog

Create Table SingleUserLog(
S_UserID Varchar (500) Not Null Primary Key,
S_Email Varchar (500) Not Null,
S_Photos Varchar (500) Null,
FirstName Varchar (500) Not Null,
LastName Varchar (500) Not Null,
S_Passkey Varchar (500) Not Null,
DOB Varchar (500) Not Null,
Gender Varchar (500) Not Null,
S_ContactNo Varchar (500) Not Null,
S_Country Varchar (500) Not Null
);


Select * From SingleUserLog






/* ////////////////////////////////////////////////////////////////////////////////////////////// */






Drop Table CompanyUserLog
Truncate Table CompanyUserLog

Create Table CompanyUserLog(
C_UserID Varchar (500) Not Null Primary Key,
C_Photos Varchar (500) Null,
C_Email Varchar (500) Not Null,
C_Name Varchar (500) Not Null,
C_Passkey Varchar (500) Not Null,
DateEstd Varchar (500) Not Null,
C_ContactNo Varchar (500) Not Null,
C_CountryName Varchar (500) Not Null
);


Select * From CompanyUserLog






/* ////////////////////////////////////////////////////////////////////////////////////////////// */






Drop Table Employee
Truncate Table Employee

Create Table Employee(
EmployeeID Varchar (500) Not Null Primary Key,
E_Email Varchar (500) Not Null,
E_Photos Varchar (500) Null,
E_Name Varchar (500) Not Null,
E_Age Int Not Null,
E_ContactNo Varchar (500) Not Null,
E_Designation Varchar (500) Not Null 
);


Insert Into Employee Values 
('E001' , 'emptest1@gmail.com', ''  ,'Rofiqul Islam'   , 35, '0161241573' , 'Pilot'			),
('E002' , 'emptest2@gmail.com', ''  ,'Nafisul Alim'    , 31, '0174574878' , 'Pilot'			),
('E003' , 'emptest3@gmail.com', ''  ,'Tarek Hasan'     , 29, '0197257274' , 'Pilot'			),
('E004' , 'emptest4@gmail.com', ''  ,'Samiur Rahman'   , 43, '0185727232' , 'Pilot'			),
('E005' , 'emptest5@gmail.com', ''  ,'Shofik Islam'    , 33, '0161241573' , 'Cabin Crew'	),
('E006' , 'emptest6@gmail.com', ''  ,'Faisal Alim'     , 40, '0174574878' , 'Cabin Crew'	),
('E007' , 'emptest7@gmail.com', ''  ,'Zia Uddin'       , 28, '0197257274' , 'Cabin Crew'	),
('E008' , 'emptest8@gmail.com', ''  ,'Karim Rahman'    , 38, '0185727232' , 'Cabin Crew'	),
('E009' , 'emptest9@gmail.com', ''  ,'Aziz Uddin'      , 27, '0197257274' , 'Cabin Crew'	),
('E010', 'emptest10@gmail.com', ''  ,'Rafik Rahman'    , 38, '0185727232' , 'Cabin Crew'	),
('E011', 'emptest11@gmail.com', ''  ,'ABC Uddin'       , 26, '0197257274' , 'Cabin Crew'	),
('E012', 'emptest12@gmail.com', ''  ,'Fajar Rahman'    , 38, '0185727232' , 'Cabin Crew'	),
('E013', 'emptest13@gmail.com', ''  ,'Taran Hasan'     , 36, '0197257274' , 'Pilot'			),
('E014', 'emptest14@gmail.com', ''  ,'Wasife Rahman'   , 43, '0185727232' , 'Pilot'			),
('E015', 'emptest15@gmail.com', ''  ,'Johny Rahman'    , 38, '0185727232' , 'Cabin Crew'	),
('E016', 'emptest16@gmail.com', ''  ,'Dutch Uddin'     , 26, '0197257274' , 'Cabin Crew'	),
('E017', 'emptest17@gmail.com', ''  ,'Abir Rahman'     , 38, '0185727232' , 'Cabin Crew'	),
('E018', 'emptest18@gmail.com', ''  ,'Maruk Hasan'     , 36, '0197257274' , 'Pilot'			),
('E019', 'emptest19@gmail.com', ''  ,'Musa Habib'      , 43, '0185727232' , 'Pilot'			),
('E020', 'emptest20@gmail.com', ''  ,'Fedrick Paul'    , 30, '0197257274' , 'Cabin Crew'	),
('E021', 'emptest21@gmail.com', ''  ,'Delta Sammy'     , 38, '0185727232' , 'Cabin Crew'	),
('E022', 'emptest22@gmail.com', ''  ,'Dutch Uddin'     , 26, '0197257274' , 'Cabin Crew'	),
('E023', 'emptest23@gmail.com', ''  ,'Abir Rahman'     , 38, '0185727232' , 'Assistant'		),
('E024', 'emptest24@gmail.com', ''  ,'Maruk Hasan'     , 36, '0197257274' , 'Assistant'		),
('E025', 'emptest25@gmail.com', ''  ,'Musa Habib'      , 43, '0185727232' , 'Assistant'		),
('E026', 'emptest26@gmail.com', ''  ,'Fedrick Paul'    , 30, '0197257274' , 'Assistant'		),
('E027', 'emptest27@gmail.com', ''  ,'Delta Sammy'     , 38, '0185727232' , 'Assistant'		);


Select * From Employee

Alter Table Employee Add DutyStatus Varchar (500) Null


Select COUNT(*) From FlightInfo Inner Join CargoFlight On FlightInfo.FlightID = CargoFlight.FlightID Where FlightInfo.DepartureDate > ( Select GETDATE() )

Select COUNT(*) From FlightInfo Inner Join PassengerFlight On FlightInfo.FlightID = PassengerFlight.FlightID Where FlightInfo.DepartureDate > ( Select GETDATE() )

Select COUNT(*) From Employee Where DutyStatus = 'Free'


/* ////////////////////////////////////////////////////////////////////////////////////////////// */






Drop Table FlightInfo
Truncate Table FlightInfo


Create Table FlightInfo(
FlightID Varchar (500) Not Null PRIMARY KEY,
AirCraftID Varchar(500) Null Foreign Key References AircraftInfo (AC_ID),
From1 Varchar (500) Not Null,
To1 Varchar (500) Not Null,
DepartureDate  smalldatetime Not Null,
ArrivalDate  smalldatetime Not Null,
ClassType Varchar (500) Not Null,
Pilot1 Varchar(500) Null Foreign Key References Employee (EmployeeID),
Pilot2 Varchar(500) Null Foreign Key References Employee (EmployeeID),
CabinCrew1 Varchar(500) Null Foreign Key References Employee (EmployeeID),
CabinCrew2 Varchar(500) Null Foreign Key References Employee (EmployeeID),
CabinCrew3 Varchar(500) Null Foreign Key References Employee (EmployeeID),
Assistant1 Varchar(500) Null Foreign Key References Employee (EmployeeID),
Assistant2 Varchar(500) Null Foreign Key References Employee (EmployeeID),
Fare INT  Not Null,
AvailableSeats INT Null,
BookedSeats Varchar(500) Null
);
 

Alter Table FlightInfo Add FlightType Varchar (500) Null


Select * FROM FlightInfo


/* Flights which are going to depart after current time */
Select COUNT(*) From FlightInfo Where DepartureDate > ( Select GETDATE() )  


/* Flights which are already departed and still did Not arrived to destination */
Select COUNT(*) From FlightInfo Where DepartureDate < ( Select GETDATE() )  And ArrivalDate > ( Select GETDATE() )


/* Flights which are in this month */
Select COUNT(*) From FlightInfo Where DepartureDate Between (Select DATEAdd(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)) And (Select DATEAdd (DAY, -1, DATEAdd(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)))


/* Flights which are on this day */
Select COUNT(*) From FlightInfo Where DepartureDate Between (Select DATEAdd(HOUR, DATEDIFF(HOUR, 0, GETDATE()),0)) And (Select DATEAdd(HOUR, DATEDIFF(HOUR, 0, GETDATE()),1))


Select SUM(TotalFlightFare) From PassengerFlight


Select SUM(PassengerFlight.TotalFlightFare) From PassengerFlight Inner Join FlightInfo On FlightInfo.FlightID = PassengerFlight.FlightID Where DepartureDate < ( Select GETDATE() )


Select * From  FlightInfo  Where FlightInfo.DepartureDate > (Select GETDATE()) And BookedSeats != '' And FlightInfo.FlightType = 'Passenger'

Select * From  FlightInfo  Where FlightInfo.DepartureDate > (Select GETDATE()) And BookedSeats != '' And FlightInfo.FlightType = 'Cargo'

Select * From  FlightInfo  Inner Join Employee on FlightInfo.Pilot1 = Employee.EmployeeID AND Employee.E_Designation = 'Pilot' Where FlightInfo.ArrivalDate < (Select GETDATE())

Select * From  FlightInfo  Inner Join Employee on FlightInfo.Pilot2 = Employee.EmployeeID AND Employee.E_Designation = 'Pilot' Where FlightInfo.ArrivalDate < (Select GETDATE())

Select * From  FlightInfo  Inner Join Employee on FlightInfo.CabinCrew1 = Employee.EmployeeID AND Employee.E_Designation = 'Cabin Crew' Where FlightInfo.ArrivalDate < (Select GETDATE())

Select * From  FlightInfo  Inner Join Employee on FlightInfo.CabinCrew2 = Employee.EmployeeID AND Employee.E_Designation = 'Cabin Crew' Where (FlightInfo.ArrivalDate < (Select GETDATE()) Or Employee.DutyStatus = 'Free')

Select * From  FlightInfo  Inner Join Employee on FlightInfo.CabinCrew3 = Employee.EmployeeID AND Employee.E_Designation = 'Cabin Crew' Where FlightInfo.ArrivalDate < (Select GETDATE())

Select * From  FlightInfo  Inner Join Employee on FlightInfo.Assistant1 = Employee.EmployeeID AND Employee.E_Designation = 'Assistant' Where FlightInfo.ArrivalDate < (Select GETDATE())

Select * From  FlightInfo  Inner Join Employee on FlightInfo.Assistant1 = Employee.EmployeeID AND Employee.E_Designation = 'Assistant' Where FlightInfo.ArrivalDate < (Select GETDATE())

Select DISTINCT From1 from FlightInfo Where FlightInfo.FlightType = 'Passenger'

Select DISTINCT From1 from FlightInfo Where FlightInfo.FlightType = 'Cargo'






/* ////////////////////////////////////////////////////////////////////////////////////////////// */






Drop Table AircraftInfo
Truncate Table AircraftInfo

Create Table AircraftInfo(
AC_ID Varchar (500) Not Null Primary Key,
AC_Name Varchar (500) Not Null,
PassengerCapacity Int Null,
CargoCapacity Int Null
);

Insert Into AircraftInfo Values 
( 'ACB737M' , 'Boeing 737-Max' ,   70 , 100),
( 'ACB7478' , 'Boeing 747-8'   ,   60 , 100),
( 'ACAB318' , 'Airbus A318'    ,   60 , 100),
( 'ACD8400' , 'Dash 8-400'     ,   30 , 100);


Select * From AircraftInfo

Alter Table AircraftInfo Add AC_Type Varchar (500) Null

Select * From FlightInfo Inner Join AircraftInfo On FlightInfo.FlightID = AircraftInfo.FlightID






/* ////////////////////////////////////////////////////////////////////////////////////////////// */






Drop Table PassengerFlight
Truncate Table PassengerFlight

Create Table PassengerFlight(
FlightID Varchar(500) Not Null Foreign Key References FlightInfo (FlightID), 
S_UserID Varchar(500) Not Null Foreign Key References SingleUserlog (S_UserID),
PackageID Varchar(500) Null Foreign Key References PackageInfo (PackageID),
NoOfSeats Int Null,
SeatNumbers Varchar(500) Null,
HotelID Varchar(500) Null Foreign Key References HotelInfo (HotelID), 
TotalHotelRent Int Null,
TotalFlightFare Int Not Null
);


Select * From PassengerFlight





/* ////////////////////////////////////////////////////////////////////////////////////////////// */






Drop Table CargoFlight
Truncate Table CargoFlight

Create Table CargoFlight(
FlightID Varchar (500) Not Null Foreign Key References FlightInfo (FlightID),
C_UserID Varchar(500) Not Null Foreign Key References CompanyUserLog (C_UserID),
ProductType Varchar (500) Null,
ProductDimension Varchar (500) Null,
ProductWeight Varchar (500) Null,
ProductUnit Varchar (500) Null,
TotalCargoFare INT Not Null
);


Select * FROM CargoFlight






/* ////////////////////////////////////////////////////////////////////////////////////////////// */









Drop Table CustomerSupport
Truncate Table CustomerSupport

Create Table CustomerSupport(
S_UserID Varchar (500) Not Null Foreign Key References SingleUserLog (S_UserID),
C_UserID Varchar (500) Not Null Foreign Key References CompanyUserLog (C_UserID),
UserAdminConvo Varchar (8000) Not Null
);


Select * FROM CustomerSupport








/* ////////////////////////////////////////////////////////////////////////////////////////////// */








Drop Table Feedback
Truncate Table Feedback

Create Table Feedback(
FlightID Varchar (500) Not Null Foreign Key References FlightInfo (FlightID),
S_UserID Varchar (500) Not Null Foreign Key References SingleUserLog (S_UserID),
FlightRating Varchar (500) Not Null,
Feedback1 Varchar (5000) Not Null
);

Select Top 5 * From Feedback Order By S_UserID Desc
	
sp_rename 'Feedback.Feedback', 'Feedback1', 'COLUMN';

Select * From Feedback









/* ////////////////////////////////////////////////////////////////////////////////////////////// */










Drop Table Membership
Truncate Table Membership

Create Table Membership(
S_UserID Varchar (500)  Not Null Foreign Key References SingleUserLog (S_UserID),
MemberStatus Varchar (500) Not Null,
Point INT Not Null
);


Select * FROM Membership









/* ////////////////////////////////////////////////////////////////////////////////////////////// */







Drop Table HotelInfo
Truncate Table HotelInfo

Create Table HotelInfo(
HotelID Varchar(500) Not Null Primary Key,
HotelName Varchar(500) Not Null,
HotelAddress Varchar(500) Not Null,
HotelMail Varchar(500) Not Null,
BusinessCapacity INT Null,
EconomyCapacity INT Null,
HotelPhotos Varchar(5000) Not Null,
EcoRent INT Not Null,
BusiRent INT Not Null,
ContactNo Varchar(500) Not Null
);


Select * FROM HotelInfo






/* ////////////////////////////////////////////////////////////////////////////////////////////// */







Create Table PackageInfo(
PackageID Varchar(500) Not Null Primary Key,
HotelName Varchar(500) Null,
HotelImage Varchar(1000) Null,
Destination Varchar(500) Null,
PackageSubDesc Varchar(5000) Not Null,
PackageDesc Varchar(5000) Not Null,
PackagePrice int Null
);



Select * FROM PackageInfo




Insert Into PackageInfo Values 
( 'P001' , 'Hotel Sea Palace(Western Plaza)' ,'https://usbair.com//assets/frontend/img/sales/hotelseausba.webp',  'Cox''s Bazar' , '3 Days 2 Nights' , 'Return Air Ticket (Dhaka-Cox Bazar-Dhaka) with all taxes
#02 Nights Hotel Accommodation#Buffet Breakfast# Complementary Swimming pool, Free Wifi internet etc.
#Package rates may vary without prior Notice, subject to taxes or conversion rate changes.#This price is Not applicable during BLACKOUT PERIOD
#For Inquiry: 01777707881, 01777707882 and 01777707883',1882),

( 'P003' , 'Butterfly Park Bangladesh' ,'https://usbair.com//assets/frontend/img/sales/butterfly.jpg',  'Chittagong' , '3 Days 2 Nights' , 'Return Air Ticket  (Dhaka-Chittagong-Dhaka)  with all taxes
#02 Nights Hotel Accommodation#Buffet Breakfast# Complementary Swimming pool, Free Wifi internet etc.#Suttle Bus Service: Airport-Hotel-Airport
#Package rates may vary without prior Notice, subject to taxes or conversion rate changes.#This price is Not applicable during BLACKOUT PERIOD
#For Inquiry: 01777707881, 01777707882 and 01777707883',10990),

( 'P002' , 'Rose View' ,'https://usbair.com//assets/frontend/img/sales/roseview.jpg',  'Sylhet' , '3 Days 2 Nights' , 'Return Air Ticket (Dhaka-Sylhet-Dhaka) with all taxes
#02 Nights Hotel Accommodation#Buffet Breakfast# Complementary Swimming pool, Free Wifi internet etc.
#Package rates may vary without prior Notice, subject to taxes or conversion rate changes.#This price is Not applicable during BLACKOUT PERIOD
#For Inquiry: 01777707881, 01777707882 and 01777707883',11590),

( 'P004' , 'Hotel Grand Park' ,'https://usbair.com//assets/frontend/img/sales/grandpark.jpg',  'Barisal' , '3 Days 2 Nights' , 'Return Air Ticket (Dhaka-Barishal-Dhaka) with all taxes
#02 Nights Hotel Accommodation#Buffet Breakfast# Complementary Swimming pool, Free Wifi internet etc.
#Package rates may vary without prior Notice, subject to taxes or conversion rate changes.#This price is Not applicable during BLACKOUT PERIOD
#For Inquiry: 01777707881, 01777707882 and 01777707883',13590),

( 'P005' , 'Hotel The Cox Today' ,'https://usbair.com//assets/frontend/img/sales/pkg04_cxb.jpg',  'Cox''s Bazar ','3 Days 2 Nights' , 'Return Air Ticket (Dhaka-Cox Bazar-Dhaka) with all taxes
#02 Nights Hotel Accommodation#Buffet Breakfast# Complementary Swimming pool, Free Wifi internet etc.
#Package rates may vary without prior Notice, subject to taxes or conversion rate changes.#This price is Not applicable during BLACKOUT PERIOD
#For Inquiry: 01777707881, 01777707882 and 01777707883',12790);







/* ////////////////////////////////////////////////////////////////////////////////////////////// */







Create Table BookedPackages(
S_UserID Varchar (500) Not Null Foreign Key References SingleUserlog (S_UserID),
PackageID Varchar(500) Not Null Foreign Key References PackageInfo (PackageID),
CheckInDate Varchar(500) Not Null,
NoOfPerson int Not Null,
TotalPrice int Null
);

Select * FROM BookedPackages








/* ////////////////////////////////////////////////////////////////////////////////////////////// */
