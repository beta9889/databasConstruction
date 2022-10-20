-- Active: 1662618139196@@127.0.0.1@3306@a20behta

USE a20behta;
CREATE TRIGGER BeforeBaseRaces BEFORE INSERT ON BaseRaces
FOR EACH ROW BEGIN
    INSERT INTO Logging(Date_,UsernameTtableAccessed,ActionTaken) VALUES (NOW(),USER(), "BaseRaces", "INSERT");
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Table is UnChangable';
END ;


CREATE TRIGGER BeforeWorkingDeer BEFORE INSERT ON WorkingDeer
FOR EACH ROW BEGIN
    INSERT INTO Logging(Date_,Username,TableAccessed,ActionTaken) VALUES (NOW(),USER(), "WorkingDeer", "INSERT");
    IF(NEW.Smell > 5  OR NEW.Smell <= 0 ) THEN
        SIGNAL SQLSTATE '45201' SET MESSAGE_TEXT = 'Smell must be between 1 - 5';
    END IF;
    IF (NEW.pay < 0) THEN
        SIGNAL SQLSTATE '45202' SET MESSAGE_TEXT = 'Pay Needs to be above 0';
    END IF;
    UPDATE DeerGroup SET DeerGroup.Filled = DeerGroup.Filled + 1 WHERE DeerGroup.GroupName = NEW.DeerGroup; 
END ;

CREATE TRIGGER DeleteWorkingDeer AFTER DELETE ON WorkingDeer
FOR EACH ROW BEGIN
    INSERT INTO Logging(Date_,Username,TableAccessed,ActionTaken) VALUES (NOW(),USER(), "WorkingDeer", "DELETE");
    UPDATE DeerGroup SET DeerGroup.Filled = DeerGroup.Filled - 1 WHERE DeerGroup.groupName = OLD.DeerGroup; 
END ;

CREATE TRIGGER BeforePrices BEFORE INSERT ON Prices
FOR EACH ROW BEGIN
    INSERT INTO Logging(Date_,Username,TableAccessed,ActionTaken) VALUES (NOW(),USER(), "Prices", "INSERT"); 
END ;

CREATE TRIGGER BeforeRetiredDeer BEFORE INSERT ON RetiredDeer
FOR EACH ROW BEGIN
    INSERT INTO Logging(Date_,Username,TableAccessed,ActionTaken) VALUES (NOW(),USER(), "Retired", "INSERT"); 
	IF NOT EXISTS (SELECT * FROM WorkingDeer WHERE WorkingDeer.deerNr = NEW.deerNr) THEN
        SIGNAL SQLSTATE '45401' SET MESSAGE_TEXT = 'Deer needs to work before being retired';
    END IF;
END ;

CREATE TRIGGER BeforeColdPrices BEFORE INSERT ON ColdPrices
FOR EACH ROW BEGIN
    INSERT INTO Logging(Date_,Username,TableAccessed,ActionTaken) VALUES (NOW(),USER(), "ColdPrices", "INSERT"); 
END ;

CREATE TRIGGER BeforeDeerToDeer BEFORE INSERT ON DeerToDeer
FOR EACH ROW BEGIN
    INSERT INTO Logging(Date_,Username,TableAccessed,ActionTaken) VALUES (NOW(),USER(), "DeerToDeer", "INSERT"); 
    IF NEW.firstDeerNr = NEW.secondDeerNr THEN
        SIGNAL SQLSTATE '45600' SET MESSAGE_TEXT = "deer id's cannot be the same for both";
    END IF;

    IF NOT EXISTS (SELECT WorkingDeer.DeerNr FROM WorkingDeer Where WorkingDeer.DeerNr = new.firstDeerNr 
    UNION SELECT RetiredDeer.DeerNr FROM RetiredDeer Where RetiredDeer.DeerNr = new.firstDeerNr) THEN
            SIGNAL SQLSTATE '45601' SET MESSAGE_TEXT = "First deer Not found";
    END IF;

    IF NOT EXISTS (SELECT WorkingDeer.DeerNr FROM WorkingDeer Where WorkingDeer.DeerNr = new.secondDeerNr 
    UNION SELECT RetiredDeer.DeerNr FROM RetiredDeer Where RetiredDeer.DeerNr = new.secondDeerNr) THEN
            SIGNAL SQLSTATE '45601' SET MESSAGE_TEXT = "First deer Not found";
    END IF;

END ;

CREATE TRIGGER beforeDeerGroup BEFORE INSERT ON DeerGroup
FOR EACH ROW BEGIN
    INSERT INTO Logging(Date_,Username,TableAccessed,ActionTaken) VALUES (NOW(),USER(), "DeerGroup", "INSERT"); 
END ;

CREATE TRIGGER beforeBitToReg BEFORE INSERT ON BitToReg
FOR EACH ROW BEGIN
    INSERT INTO Logging(Date_,Username,TableAccessed,ActionTaken) VALUES (NOW(),USER(), "BitToReg", "INSERT");
    SIGNAL SQLSTATE '45100' SET MESSAGE_TEXT = "Cannot change this table";
END ;

CREATE TRIGGER beforeSled BEFORE INSERT ON Sled  
FOR EACH ROW BEGIN
    INSERT INTO Logging(Date_,Username,TableAccessed,ActionTaken) VALUES (NOW(),USER(), "Sled", "INSERT");
    IF (NEW.sledName = "Brynolf" OR NEW.sledName = "Rudolf") THEN
        SIGNAL SQLSTATE '45301' SET MESSAGE_TEXT ='Cannot be named Brynolf or Rudolf';
    END IF;
END ;
