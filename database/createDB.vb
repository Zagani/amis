
'Imports System.Collections

Public Class createDB
    Private dbname As String
    Private vUSER As String
    Private vPASS As String
    Public Sub New(ByVal Database As String, ByVal User As String, ByVal Pass As String)
        dbname = Database
        vUSER = User
        vPASS = Pass
    End Sub

    Public Function createDB() As String()
        Dim dbUser = vUSER, dbPass = vPASS, dbTBCreate() As String
        Dim sqlList As New List(Of String)
        sqlList.Add("CREATE DATABASE IF NOT EXISTS `" & dbname & "` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;")
        sqlList.Add("USE `" & dbname & "`;")
        'sqlList.Add("set password for 'root'@'localhost'=PASSWORD('admin456')")
        'sqlList.Add("CREATE USER '" & dbUser & "'@'localhost' IDENTIFIED BY '" & dbPass & "'")
        'sqlList.Add("GRANT ALL PRIVILEGES ON `" & dbname & "\_%`.* TO '" & dbUser & "'@'localhost'")
        'EMPLOYEE TABLE
        sqlList.Add("CREATE TABLE IF NOT EXISTS `Employees` (
            `EmpID` int(30) Not NULL AUTO_INCREMENT,
            `NameFirst` varchar(45) Not NULL,
            `NameMiddle` varchar(45) Not NULL,
            `NameLast` varchar(45) Not NULL,
            `Gender` ENUM('M','F') NULL,
            `BirthDate` Date NULL,
            `BirthAddress` varchar(100) NULL,
            `MaritalStatus` ENUM('Single','Married') NULL,
            `AddressStreet` varchar(45) NULL,
            `AddressBarangay` varchar(45) NULL,
            `AddressMunCity` varchar(45) NULL,
            `AddressProvince` varchar(45) NULL,
            `AddressZip` varchar(5) NULL,
            `Contact` varchar(15) NULL,
            `EmploymentStatus` tinyint not NULL,
            `EmpImage` longblob NULL,
            PRIMARY KEY(`EmpID`)
        ) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;")

        sqlList.Add("CREATE TABLE IF NOT EXISTS `Users`(
           `UserID` int(30) Not NULL AUTO_INCREMENT,
           `EmpID` int(30) Not NULL,
            `Username` varchar(20) Not NULL,
            `Password` varchar(50) Not NULL,
            `Function` ENUM('Admin','Cashier','Manager') Not NULL,
            PRIMARY KEY(`UserID`),
            UNIQUE KEY `Username_UNIQUE` (`Username`)
           )")
        'INSERT INITIAL VALUE
        '2 tables must always be linked by not all employees have the access to the program
        'each user must have employee record. need only once.
        sqlList.Add("insert into employees(`namefirst`,`namemiddle`,`namelast`,`employmentstatus`) values('Administrator','Administrator','Administrator',1);")
        sqlList.Add("insert into Users(`Empid`,`username`,`password`,`function`) values(1,'Admin','Admin','Admin');")


        'THIS WILL CREATE A PURCHASE ORDERLIST VIEW
        'SELECT DATE_FORMAT( NOW( ) ,  '%Y-%m-%d' ) DATE
        'THIS WILL CREAE PO TABLE FOR SUMMARY OF PURCHASE ORDER LISTS
        sqlList.Add("CREATE TABLE IF NOT EXISTS `po` (
                  `POID` int(11) NOT NULL AUTO_INCREMENT,
                  `EmpID` int(11) NOT NULL,
                  `PODate` date NOT NULL ,
                  `TotalCost` double NOT NULL default 0.0,
                  `Status` tinyint(4) NOT NULL,
                  PRIMARY KEY (`POID`)
                )
            ")
        sqlList.Add("CREATE TABLE POList(
            POListID INT NOT NULL AUTO_INCREMENT ,
            POID INT NOT NULL ,
            ItemID INT NOT NULL,
            Quantity DOUBLE( 10, 2 ) NOT NULL ,
            Cost double not null default 0.0,
            PRIMARY KEY ( POListID )
            )")
        'THIS WILL CREATE ITEMS FOR SUMMARY OF ITEMS BEING PURCHASED
        sqlList.Add("CREATE TABLE ITEMS(
                ItemID INT NOT NULL AUTO_INCREMENT ,
                SupplierID INT NOT NULL ,
                Barcode INT NOT NULL ,
                Description VARCHAR( 45 ) NOT NULL ,
                Brand VARCHAR( 45 ) ,
                UnitType VARCHAR( 10 ) NOT NULL ,
                UnitValue DOUBLE NOT NULL DEFAULT 0.0,
                Category VARCHAR( 45 ) NOT NULL ,
                ItemType VARCHAR( 15 ) NOT NULL ,
                PRIMARY KEY ( ItemID, SupplierID )
                )")
        sqlList.Add("CREATE TABLE ItemInfo(
                ItemInfoID int not null auto_increment,
                Barcode varchar(45) not null,
                Description Varchar(45) not null,
                Brand varchar(45) not null,
                primary KEY(ItemInfoID)
                )")
        'CREATE USER ON DATABASE PHPMYADMIN
        'sqlList.Add("GRANT ALL PRIVILEGES ON  `" & dbname & "` . * TO  '" & dbUser & "'@'localhost' WITH GRANT OPTION")
        dbTBCreate = sqlList.ToArray()

        '        dbTBCreate(3) = "CREATE TABLE IF NOT EXISTS `category` (" &
        '                          "`CategoryID` int(11) NOT NULL AUTO_INCREMENT," &
        '                          "`CategoryName` varchar(150) NOT NULL," &
        '                          "`CategoryDescription` varchar(240) NOT NULL," &
        '                          "PRIMARY KEY (`CategoryID`)" &
        '                          ") ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1;"


        '
        '        dbTBCreate(5) = "CREATE TABLE IF NOT EXISTS `requisition` (
        '                       `PRNum` int(11) Not NULL AUTO_INCREMENT,
        '                      `Description` varchar(240) Not NULL,
        '                      `Amount` varchar(240) Not NULL,
        '                      `Purpose` varchar(240) Not NULL,
        '                      `Status` varchar(50) Not NULL,
        '                      `DateTime` varchar(30) Not NULL,
        '        PRIMARY KEY (`PRNum`)
        '                      ) ENGINE=InnoDB  Default CHARSET=latin1 AUTO_INCREMENT=1;"
        '        dbTBCreate(6) = "CREATE TABLE If Not EXISTS `purchase` (
        '                     `PurchaseORNum` int(20) Not NULL AUTO_INCREMENT,
        '                     `PRNum` int(20) Not NULL,
        '                    `SupplierID` int(20) Not NULL,
        '                    `TotalCost` float(20,4) Not NULL,
        '                    `Date` varchar(20) Not NULL,
        '                    `Remarks` text Not NULL,
        '        PRIMARY KEY(`PurchaseORNum`)
        '                    ) ENGINE=InnoDB Default CHARSET=latin1 AUTO_INCREMENT=1 ;"
        '        dbTBCreate(7) = "CREATE TABLE If Not EXISTS `purchaselist` (" &
        '                      "`PurchaseORNum` int(20) Not NULL," &
        '                      "`ProductID` int(20) Not NULL," &
        '                      "`CategoryID` int(20) Not NULL," &
        '                      "`UnitOfMeasure` varchar(20) Not NULL," &
        '                      "`UnitCost` float(12,4) Not NULL," &
        '                      "`Quantity` float(12,4) Not NULL" &
        '                    ") ENGINE=InnoDB Default CHARSET=latin1;"
        '        dbTBCreate(8) = "CREATE TABLE If Not EXISTS `supplier` (" &
        '                      "`SupplierID` int(20) Not NULL AUTO_INCREMENT," &
        '                      "`Company` int(200) Not NULL," &
        '                      "`AddressStreet` varchar(200) Not NULL," &
        '                      "`AddressCity` varchar(200) Not NULL," &
        '                      "`AddressZip` int(6) Not NULL," &
        '                      "`PhoneNum` int(20) Not NULL," &
        '                      "PRIMARY KEY(`SupplierID`)" &
        ''                      ") ENGINE=InnoDB Default CHARSET=latin1 AUTO_INCREMENT=1 ;"
        '       dbTBCreate(9) = "CREATE TABLE If Not EXISTS `products` (" &
        '                      "`ProductID` int(20) Not NULL AUTO_INCREMENT," &
        '                      "`CategoryID` int(20) Not NULL," &
        '                      "`Description` varchar(150) Not NULL," &
        '                      "`Price` float(12,4) Not NULL," &
        '                      "`Stocks` int(20) Not NULL," &
        '                      "`ReorderLevel` int(20) Not NULL," &
        '                      " PRIMARY KEY(`ProductID`), " &
        '                     " UNIQUE KEY `ProductID` (`ProductID`)" &
        '                    ") ENGINE=InnoDB Default CHARSET=latin1 AUTO_INCREMENT=1 ;"
        '        dbTBCreate(10) = "CREATE TABLE If Not EXISTS `orders` (" &
        '                      "`OrderNum` int(20) Not NULL AUTO_INCREMENT," &
        '                      "`CustomerNum` int(20) Not NULL," &
        '                      "`EmpID` int(20) Not NULL," &
        '                      "`QoutedPrice` float(4,4) Not NULL," &
        '                      "`Discount` float(4,2) Not NULL," &
        '                      "`OrderTime` varchar(10) Not NULL," &
        '                      "`OrderDate` varchar(25) Not NULL," &
        '                      "PRIMARY KEY (`OrderNum`)" &
        '                    ") ENGINE=InnoDB Default CHARSET=latin1 AUTO_INCREMENT=1 ;"
        '        dbTBCreate(11) = "CREATE TABLE If Not EXISTS `orderlist` (" &
        '                      "`OrderNum` int(20) Not NULL," &
        '                      "`ProductID` int(20) Not NULL," &
        '                      "`UnitPrice` float(10,2) Not NULL," &
        '                      "`Quantity` float(10,2) Not NULL" &
        '                    ") ENGINE=InnoDB Default CHARSET=latin1;"
        '        dbTBCreate(12) = "CREATE TABLE If Not EXISTS `customer` (" &
        '                      "`CustomerNum` int(30) Not NULL AUTO_INCREMENT," &
        '                      "`CitizenID` int(30) Not NULL," &
        '                      "`EmpID` varchar(200) Not NULL," &
        '                      "`NameFirst` varchar(200) Not NULL," &
        '                      "`NameMiddle` varchar(200) Not NULL," &
        '                      "`NameLast` varchar(200) Not NULL," &
        '                      "`AddressStreet` varchar(200) Not NULL," &
        '                      "`AddressBarangay` varchar(200) Not NULL," &
        '                      "`AddressMunCity` varchar(200) Not NULL," &
        '                      "`AddressProvince` varchar(200) Not NULL," &
        '                      "`Contact` varchar(200) Not NULL," &
        '                      "PRIMARY KEY (`CustomerNum`)," &
        '                      "UNIQUE KEY `CitizenID` (`CitizenID`)" &
        '                    ") ENGINE=InnoDB Default CHARSET=latin1 AUTO_INCREMENT=1 ;"
        '        dbTBCreate(13) = "CREATE TABLE If Not EXISTS `foodlist` (
        '  `ProductID` int(30) NOT NULL AUTO_INCREMENT,
        '  `CategoryID` int(30) NOT NULL,
        '  `Description` varchar(200) NOT NULL,
        '  `UnitPrice` double(12,2) NOT NULL,
        '  `Availability` varchar(15) NOT NULL,
        '        PRIMARY KEY (`ProductID`),
        '        UNIQUE KEY `ProductID` (`ProductID`)
        ') ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1;"
        '        dbTBCreate(14) = "CREATE USER '" & dbUser & "'@'localhost' IDENTIFIED BY '" & dbPass & "';"
        '        dbTBCreate(15) = "GRANT ALL PRIVILEGES ON `" & dbname & "\_%`.* TO '" & dbUser & "'@'localhost';"
        '        dbTBCreate(16) = "GRANT ALL PRIVILEGES ON  `" & dbname & "` . * TO  '" & dbUser & "'@'localhost' WITH GRANT OPTION;"
        Return dbTBCreate
    End Function
End Class
