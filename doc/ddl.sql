CREATE TABLE IF NOT EXISTS Company (
  `Id` char NOT NULL PRIMARY KEY,
  `CompanyName` varchar DEFAULT NULL,
  `FirstName` varchar NOT NULL,
  `Lastname` varchar NOT NULL,
  `Street` varchar NOT NULL,
  `Housenumber` varchar NOT NULL,
  `Plz` varchar NOT NULL,
  `City` varchar NOT NULL,
  `CompanyNumber` varchar NOT NULL
);

CREATE UNIQUE INDEX `PRIMARY` ON Company (`Id`);

CREATE TABLE IF NOT EXISTS Culture (
  `Id` char NOT NULL PRIMARY KEY,
  `Name` varchar NOT NULL,
  `Code` varchar DEFAULT NULL,
  `ShortName` varchar DEFAULT NULL,
  `Comment` varchar DEFAULT NULL
);

CREATE UNIQUE INDEX `PRIMARY` ON Culture (`Id`);

CREATE TABLE IF NOT EXISTS Fertilizer (
  `Id` char NOT NULL PRIMARY KEY,
  `Name` varchar NOT NULL,
  `Detail` longtext DEFAULT NULL,
  `Comment` varchar DEFAULT NULL
);

CREATE UNIQUE INDEX `PRIMARY` ON Fertilizer (`Id`);

CREATE TABLE IF NOT EXISTS Field (
  `Id` char NOT NULL PRIMARY KEY,
  `Number` varchar NOT NULL,
  `Name` varchar NOT NULL,
  `Area` float NOT NULL,
  `Comment` varchar DEFAULT NULL
);

CREATE UNIQUE INDEX `PRIMARY` ON Field (`Id`);

CREATE TABLE IF NOT EXISTS HarvestUnit (
  `Id` char NOT NULL PRIMARY KEY,
  `YearFieldId` char NOT NULL,
  `Name` varchar NOT NULL,
  `Area` float NOT NULL,
  `CultureId` char DEFAULT NULL
);

CREATE UNIQUE INDEX `PRIMARY` ON HarvestUnit (`Id`);
CREATE INDEX `IX_HarvestUnit_CultureId` ON HarvestUnit (`CultureId`);
CREATE INDEX `IX_HarvestUnit_FieldId` ON HarvestUnit (`YearFieldId`);

CREATE TABLE IF NOT EXISTS Person (
  `Id` char NOT NULL PRIMARY KEY,
  `FirstName` varchar NOT NULL,
  `LastName` varchar NOT NULL,
  `Jobtitle` varchar DEFAULT NULL,
  `Comment` varchar DEFAULT NULL
);

CREATE UNIQUE INDEX `PRIMARY` ON Person (`Id`);

CREATE TABLE IF NOT EXISTS PlantProtectant (
  `Id` char NOT NULL PRIMARY KEY,
  `Name` varchar NOT NULL,
  `PlantProtectantType` int NOT NULL,
  `ActiveSubstance` varchar DEFAULT NULL,
  `Comment` varchar DEFAULT NULL
);

CREATE UNIQUE INDEX `PRIMARY` ON PlantProtectant (`Id`);

CREATE TABLE IF NOT EXISTS SeedCategory (
  `Id` char NOT NULL PRIMARY KEY,
  `Name` varchar NOT NULL,
  `Comment` varchar DEFAULT NULL
);

CREATE UNIQUE INDEX `PRIMARY` ON SeedCategory (`Id`);

CREATE TABLE IF NOT EXISTS SeedTechnology (
  `Id` char NOT NULL PRIMARY KEY,
  `Name` varchar NOT NULL,
  `Comment` varchar DEFAULT NULL
);

CREATE UNIQUE INDEX `PRIMARY` ON SeedTechnology (`Id`);

CREATE TABLE IF NOT EXISTS Unit (
  `Id` char NOT NULL PRIMARY KEY,
  `Name` varchar NOT NULL,
  `Comment` varchar DEFAULT NULL
);

CREATE UNIQUE INDEX `PRIMARY` ON Unit (`Id`);

CREATE TABLE IF NOT EXISTS HarvestYear (
  `Id` char NOT NULL PRIMARY KEY,
  `HarvestYear` varchar NOT NULL,
  `CompanyId` char NOT NULL
);

CREATE TABLE IF NOT EXISTS YearField (
  `Id` char NOT NULL,
  `HarvestYearId` char NOT NULL,
  `FieldId` char NOT NULL,
  PRIMARY KEY (`HarvestYearId`, `FieldId`)
);

CREATE TABLE IF NOT EXISTS Seed (
  `Id` char NOT NULL PRIMARY KEY,
  `HarvestUnitId` char NOT NULL,
  `Date` date NOT NULL,
  `PersonId` char,
  `CultureId` char,
  `IsMainCulture` tinyint NOT NULL,
  `VarietyName` varchar,
  `ApprovalNumber` varchar,
  `Quantity` double NOT NULL,
  `UnitId` char,
  `SeedCategoryId` char NOT NULL,
  `SeedTechnologyId` char NOT NULL,
  `Setting` varchar,
  `Comment` varchar
);

CREATE TABLE IF NOT EXISTS Fertilization (
  `Id` char NOT NULL PRIMARY KEY,
  `HarvestUnitId` char NOT NULL,
  `Date` date NOT NULL,
  `PersonId` char,
  `FertilizerId` char NOT NULL,
  `Dosage` double NOT NULL,
  `UnitId` char,
  `BBCH` varchar,
  `Setting` varchar,
  `Comment` varchar
);

CREATE TABLE IF NOT EXISTS PlantProtection (
  `Id` char NOT NULL PRIMARY KEY,
  `HarvestUnitId` char NOT NULL,
  `Date` date NOT NULL,
  `PersonId` char,
  `PlantProtectantId` char NOT NULL,
  `Dosage` double NOT NULL,
  `UnitId` char,
  `BBCH` varchar,
  `Setting` varchar,
  `Comment` varchar
);

CREATE TABLE IF NOT EXISTS Harvest (
  `Id` char NOT NULL PRIMARY KEY,
  `HarvestUnitId` char NOT NULL,
  `Date` date NOT NULL,
  `PersonId` char,
  `Quantity` double NOT NULL,
  `UnitId` char,
  `Setting` varchar,
  `Comment` varchar
);

ALTER TABLE HarvestUnit ADD CONSTRAINT `FK_HarvestUnit_Culture_CultureId` FOREIGN KEY (`CultureId`) REFERENCES Culture (`Id`);
ALTER TABLE HarvestYear ADD CONSTRAINT `HarvestYear_CompanyId_fk` FOREIGN KEY (`CompanyId`) REFERENCES Company (`Id`);
ALTER TABLE YearField ADD CONSTRAINT `YearField_HarvestYearId_fk` FOREIGN KEY (`HarvestYearId`) REFERENCES HarvestYear (`Id`);
ALTER TABLE YearField ADD CONSTRAINT `YearField_FieldId_fk` FOREIGN KEY (`FieldId`) REFERENCES Field (`Id`);
ALTER TABLE HarvestUnit ADD CONSTRAINT `HarvestUnit_YearFieldId_fk` FOREIGN KEY (`YearFieldId`) REFERENCES YearField (`Id`);
ALTER TABLE Seed ADD CONSTRAINT `Seed_CultureId_fk` FOREIGN KEY (`CultureId`) REFERENCES Culture (`Id`);
ALTER TABLE Seed ADD CONSTRAINT `Seed_UnitId_fk` FOREIGN KEY (`UnitId`) REFERENCES Unit (`Id`);
ALTER TABLE Seed ADD CONSTRAINT `Seed_SeedCategoryId_fk` FOREIGN KEY (`SeedCategoryId`) REFERENCES SeedCategory (`Id`);
ALTER TABLE Seed ADD CONSTRAINT `Seed_PersonId_fk` FOREIGN KEY (`PersonId`) REFERENCES Person (`Id`);
ALTER TABLE Fertilization ADD CONSTRAINT `Fertilization_HarvestUnitId_fk` FOREIGN KEY (`HarvestUnitId`) REFERENCES HarvestUnit (`Id`);
ALTER TABLE Fertilization ADD CONSTRAINT `Fertilization_PersonId_fk` FOREIGN KEY (`PersonId`) REFERENCES Person (`Id`);
ALTER TABLE Fertilization ADD CONSTRAINT `Fertilization_FertilizerId_fk` FOREIGN KEY (`FertilizerId`) REFERENCES Fertilizer (`Id`);
ALTER TABLE Fertilization ADD CONSTRAINT `Fertilization_UnitId_fk` FOREIGN KEY (`UnitId`) REFERENCES Unit (`Id`);
ALTER TABLE PlantProtection ADD CONSTRAINT `PlantProtection_HarvestUnitId_fk` FOREIGN KEY (`HarvestUnitId`) REFERENCES HarvestUnit (`Id`);
ALTER TABLE PlantProtection ADD CONSTRAINT `PlantProtection_PersonId_fk` FOREIGN KEY (`PersonId`) REFERENCES Person (`Id`);
ALTER TABLE PlantProtection ADD CONSTRAINT `PlantProtection_PlantProtectantId_fk` FOREIGN KEY (`PlantProtectantId`) REFERENCES PlantProtectant (`Id`);
ALTER TABLE PlantProtection ADD CONSTRAINT `PlantProtection_UnitId_fk` FOREIGN KEY (`UnitId`) REFERENCES Unit (`Id`);
ALTER TABLE Harvest ADD CONSTRAINT `Harvest_HarvestUnitId_fk` FOREIGN KEY (`HarvestUnitId`) REFERENCES HarvestUnit (`Id`);
ALTER TABLE Harvest ADD CONSTRAINT `Harvest_PersonId_fk` FOREIGN KEY (`PersonId`) REFERENCES Person (`Id`);
ALTER TABLE Harvest ADD CONSTRAINT `Harvest_UnitId_fk` FOREIGN KEY (`UnitId`) REFERENCES Unit (`Id`);
ALTER TABLE Seed ADD CONSTRAINT `Seed_SeedTechnologyId_fk` FOREIGN KEY (`SeedTechnologyId`) REFERENCES SeedTechnology (`Id`);
