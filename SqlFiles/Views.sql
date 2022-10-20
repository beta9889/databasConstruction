-- Active: 1662618139196@@127.0.0.1@3306@a20behta
USE a20behta;
CREATE VIEW ViewWorkingDeer AS SELECT  WorkingDeer.deerNr AS DeerNr, 
        CONCAT(WorkingDeer.ClanName, " ",BaseRaces.RaceString) AS 'DeerName',
        CONV(WorkingDeer.Smell,2,10) AS Smell, WorkingDeer.Pay, WorkingDeer.DeerGroup 
        FROM WorkingDeer INNER JOIN BaseRaces ON WorkingDeer.BaseRace = BaseRaces.BitValue;

CREATE VIEW ViewRetiredDeer AS SELECT  RetiredDeer.DeerNr AS DeerNr, 
        CONCAT(RetiredDeer.ClanName," ",BaseRaces.RaceString) AS 'DeerName' ,
        CONV(RetiredDeer.Smell,2,10) AS "Smell", RetiredDeer.Pay, RetiredDeer.DeerGroup, 
        RetiredDeer.CanNr AS "Can Number", RetiredDeer.Factory, RetiredDeer.Taste 
        FROM `RetiredDeer` 
                INNER JOIN BaseRaces ON
                BaseRaces.BitValue = RetiredDeer.BaseRace;
				
CREATE VIEW ViewAllDeer AS SELECT DeerNr,DeerName,Smell,DeerGroup, "Working" as retired FROM ViewWorkingDeer 
        UNION SELECT DeerNr,DeerName,Smell,DeerGroup,"Retired" as retired FROM ViewRetiredDeer;

CREATE VIEW ViewPrices AS SELECT GROUP_CONCAT(concat(Prices.PriceComment," " , Prices.Given, " \n ")) AS Price, 
	CONCAT(WorkingDeer.ClanName, " ",BaseRaces.RaceString) AS 'DeerName', Prices.DeerGivenTo
    FROM Prices INNER JOIN WorkingDeer ON Prices.DeerGivenTo = WorkingDeer.DeerNr 
                        INNER JOIN BaseRaces ON WorkingDeer.BaseRace = BaseRaces.BitValue
    GROUP BY WorkingDeer.deerNr;

CREATE VIEW ViewColdPrices AS SELECT GROUP_CONCAT(concat(ColdPrices.PriceComment," " ,ColdPrices.Given, " \n ")) AS Price, 
	CONCAT(RetiredDeer.ClanName, " ",BaseRaces.RaceString) AS 'DeerName', ColdPrices.DeerGivenTo
    FROM ColdPrices INNER JOIN RetiredDeer ON ColdPrices.DeerGivenTo = RetiredDeer.DeerNr 
                        INNER JOIN BaseRaces ON RetiredDeer.BaseRace = BaseRaces.BitValue
    GROUP BY RetiredDeer.DeerNr;
	
CREATE VIEW ViewAllPrices AS SELECT ViewPrices.* FROM ViewPrices
	UNION (SELECT ViewColdPrices.* FROM ViewColdPrices);


CREATE VIEW ViewDeerGroup AS SELECT DeerGroup.GroupName, DeerGroup.Capacity, DeerGroup.Filled AS "Raw Data Filled",
        (( DeerGroup.Filled / DeerGroup.Capacity ) * 100 ) AS "Filled As Procent", Group_ConCat(concat(ViewWorkingDeer.DeerName)) AS "Group Members"
        FROM DeerGroup INNER JOIN ViewWorkingDeer ON DeerGroup.GroupName = ViewWorkingDeer.DeerGroup GROUP BY DeerGroup.GroupName; 

CREATE VIEW ViewDeerConnection AS Select D1.DeerName as 'name1',D1.retired  as 'Deer1 Retired',D1.DeerNr as 'DeerNr1' ,D2.DeerName as 'name2', D2.retired  as 'Deer2 Retired',D2.DeerNr as 'DeerNr2'
        From ViewAllDeer as D1 inner Join ViewAllDeer as D2 
        Inner Join DeerToDeer ON DeerToDeer.FirstDeerNr = D1.DeerNr AND DeerToDeer.SecondDeerNr = D2.DeerNr; 