-- Active: 1662618139196@@127.0.0.1@3306@a20behta
DROP DATABASE IF EXISTS a20behta;
CREATE DATABASE a20behta;
USE a20behta;

CREATE TABLE DeerGroup(
    GroupName VARCHAR(40),
    Capacity TINYINT,
    Filled TINYINT,
    PRIMARY KEY(groupName)
)engine = innodb;

CREATE TABLE BaseRaces(
    BitValue BIT(3),
    RaceString CHAR(20),
    PRIMARY KEY(bitValue,RaceString)
)Engine=innodb;

INSERT INTO BaseRaces(BitValue,RaceString) 
	VALUES(0,"Pearyi"), (1,"Tarandus"),(2,"Buskensis"),
		   (3,"Caboti"),(4,"Dawsoni"),(5,"Sibericus");

CREATE TABLE WorkingDeer(
    DeerNr SMALLINT,
    ClanName VARCHAR(20) NOT NULL,
    BaseRace BIT(3) NOT NULL, 
    Smell BIT(3),
    Pay SMALLINT,
    BankAccount INT,
    DeerGroup VARCHAR(40),
    FOREIGN KEY (DeerGroup) REFERENCES DeerGroup(GroupName),
    FOREIGN KEY (BaseRace) REFERENCES BaseRaces(BitValue),
    PRIMARY KEY(DeerNr)
)ENGINE=innodb;

CREATE INDEX IndexWorkingDeerName ON WorkingDeer(ClanName,BaseRace);

CREATE TABLE Prices( 
    Id INT NOT NULL AUTO_INCREMENT,
    PriceComment VARCHAR(255) UNIQUE,
    Given DATE,
    DeerGivenTo SMALLINT,
    foreign key(DeerGivenTo) references WorkingDeer(DeerNr),
    primary KEY(Id)
)ENGINE = innodb;
 
CREATE TABLE RetiredDeer(
    DeerNr SMALLINT,
    ClanName VARCHAR(20),
    BaseRace BIT(3), 
    Smell BIT(3),
    Pay SMALLINT,
    BankAccount INT,
    DeerGroup VARCHAR(40),
    CanNr INT,
    Factory VARCHAR(15),
    Taste VARCHAR(30),
    FOREIGN KEY (DeerGroup) REFERENCES DeerGroup(GroupName),
    FOREIGN KEY (BaseRace) REFERENCES BaseRaces(BitValue),
    PRIMARY KEY(DeerNr)
)ENGINE = innodb;
CREATE INDEX IndexRetiredDeerName ON RetiredDeer(ClanName,BaseRace);

CREATE TABLE ColdPrices( --
    Id INT,
    PriceComment VARCHAR(255),
    Given DATE,
    DeerGivenTo SMALLINT,
    FOREIGN KEY(DeerGivenTo) REFERENCES RetiredDeer(DeerNr),
    PRIMARY KEY(Id)
)ENGINE = innodb;

CREATE TABLE DeerToDeer( --
	FirstDeerNr SMALLINT,
    FirstDeerRetired BOOLEAN,
    SecondDeerNr SMALLINT,
    SecondDeerRetired BOOLEAN,
    PRIMARY KEY(FirstDeerNr,SecondDeerNr)
)ENGINE = innodb;

CREATE TABLE BitToReg( --
	BitValue BIT(2),
    RegistrationText CHAR(12),
    PRIMARY KEY(BitValue,RegistrationText)
)ENGINE = innodb;

INSERT INTO BitToReg(BitValue,RegistrationText) 
	VALUES(0,"Registrerad"),(1,"Avegisterad")
		 ,(2,"Avegisterad"),(3,"Körförbud");

CREATE TABLE Sled(
    Id INT,
    SledName VARCHAR(20) ,
    Registration BIT(2), 
    RegionId INT,
    FOREIGN KEY (Registration) REFERENCES BitToReg(BitValue),
    PRIMARY KEY(Id)
)ENGINE = innodb;

CREATE TABLE SledModifications(
    SledId INT,
    ExpressLast BIT(2) NOT NULL,
    LastExtraStorage INT, 
    LastClimateType VARCHAR(50),
    ExpressBoosterRockets SMALLINT,
    ExpressBonusBreaks SMALLINT,
    Foreign Key (SledId) REFERENCES Sled(Id),
    PRIMARY KEY(SledId)
)ENGINE = innodb;

CREATE TABLE Logging(
    Id INT NOT NULL AUTO_INCREMENT,
    Date_ DATETIME,
    Username VARCHAR(255),
    TableAccessed CHAR(15),
    ActionTaken CHAR(15),
    PRIMARY KEY(id)
)ENGINE = innodb;

CREATE INDEX UsernameLoggingIndex ON Logging(Username);
CREATE INDEX TableAccessedLoggingIndex ON Logging(TableAccessed);