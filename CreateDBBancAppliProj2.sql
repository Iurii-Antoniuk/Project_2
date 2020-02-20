-- DROP TABLE "Transaction", CurrentAccounts, SavingAccounts, Person;

-- CREATE DATABASE Projet2_BancAppli;

USE Projet2_BancAppli;

CREATE TABLE Person (
id INT PRIMARY KEY IDENTITY(1,1),
"name" VARCHAR(50) NOT NULL,
"password" VARCHAR(255) NOT NULL
);

CREATE TABLE CurrentAccounts (
id INT PRIMARY KEY IDENTITY (100, 1),
client_id INT NOT NULL,
CONSTRAINT FK_CurrentAccounts FOREIGN KEY (client_id) REFERENCES Person(id),
amount DECIMAL(10,2) NOT NULL,
overdraft INT NOT NULL,
openingDate DATETIME NOT NULL,
closeDate DATETIME
);

CREATE TABLE SavingAccounts (
id INT PRIMARY KEY IDENTITY (1000, 1),
client_id INT NOT NULL,
CONSTRAINT FK_SavingAccounts FOREIGN KEY (client_id) REFERENCES Person(id),
amount DECIMAL(10,2) NOT NULL,
rate DECIMAL(10, 2),
"ceiling" INT NOT NULL,
openingDate DATETIME NOT NULL,
closeDate DATETIME
);

CREATE TABLE "Transaction" (
id INT PRIMARY KEY IDENTITY (1, 1),
currentAccount_id INT NULL,
CONSTRAINT FK_CurrentAccount FOREIGN KEY (currentAccount_id) REFERENCES CurrentAccounts(id),
savingAccount_id INT NULL,
CONSTRAINT FK_SavingAccount FOREIGN KEY (savingAccount_id) REFERENCES SavingAccounts(id),
transactionType VARCHAR(80) NOT NULL,
beneficiaryAccount VARCHAR(80),
amount DECIMAL(10,2) NOT NULL,
executionDate DATE NOT NULL,
lastExecutionDate DATE,
intervalDays INT
);

CREATE TABLE Donator (
id INT PRIMARY KEY IDENTITY (1, 1),
client_id INT NOT NULL,
"name" VARCHAR(50) NOT NULL,
FOREIGN KEY (client_id) REFERENCES Person(id)
ON UPDATE CASCADE 
ON DELETE CASCADE
);

INSERT INTO Person ("name", "password")
VALUES 
('admin', '?gB??\v???U?g?6#????E??x??F?'),
('georges', '?gB??\v???U?g?6#????E??x??F?'),
('Daniel', '?gB??\v???U?g?6#????E??x??F?'),
('Franck', '?gB??\v???U?g?6#????E??x??F?'),
('Maxime', '?gB??\v???U?g?6#????E??x??F?'),
('Joel', '?gB??\v???U?g?6#????E??x??F?'),
('Jeannot', '?gB??\v???U?g?6#????E??x??F?');

SELECT * FROM Person;

INSERT INTO CurrentAccounts (client_id, amount, overdraft, openingDate)
VALUES (2, 1000, -500, '2019-04-05'),
(3, 1000, -500, '2019-04-05'),
(4, 2000, -500, '2019-04-07'),
(5, 4000, -500, '2019-04-08'),
(6, 1000, -500, '2019-04-09'),
(7, 6000, -500, '2019-04-10');

SELECT * FROM CurrentAccounts;

INSERT INTO SavingAccounts (client_id, amount, rate,  "ceiling", openingDate)
VALUES (2, 1000, 0.1, 50000, '2019-04-05'),
(2, 1000, 0.1, 50000, '2019-04-05'),
(2, 2000, 0.1, 50000, '2019-04-07'),
(2, 4000, 0.1, 50000, '2019-04-08'),
(3, 1000, 0.1, 50000, '2019-04-09'),
(3, 6000, 0.1, 50000, '2019-04-10'),
(4, 1000, 0.1, 50000, '2019-04-05'),
(5, 2000, 0.1, 50000, '2019-04-07'),
(6, 4000, 0.1, 50000, '2019-04-08'),
(7, 1000, 0.1, 50000, '2019-04-09');

SELECT * FROM SavingAccounts;

INSERT INTO "Transaction" 
(currentAccount_id, savingAccount_id, transactionType, beneficiaryAccount, amount, executionDate, lastExecutionDate, intervalDays)
VALUES
(100, NULL, 'Withdrawal', NULL, 300, '2019-05-01', NULL, NULL),
(100, NULL, 'Withdrawal', NULL, 400, '2019-05-02', NULL, NULL),
(100, NULL, 'Withdrawal', NULL, 100, '2019-05-30', NULL, NULL),
(100, NULL, 'Money Transfer', 1000, 300, '2019-06-01', NULL, NULL),
(NULL, 1000, 'Money Transfer', 100, 300, '2019-06-01', NULL, NULL),
(101, NULL, 'Money Transfer', 100, 100, '2019-06-20', NULL, NULL),
(101, NULL, 'Money Transfer', 102, 50, '2019-06-21', '2020-01-05', 30),
(103, NULL, 'Withdrawal', NULL, 100, '2019-06-29', NULL, NULL),
(103, NULL, 'Withdrawal', NULL, 100, '2019-06-29', NULL, NULL),
(105, NULL, 'Withdrawal', NULL, 200, '2019-07-15', NULL, NULL),
(103, NULL, 'Money Transfer', 101, 1000, '2019-07-29', NULL, NULL),
(104, NULL, 'Money Transfer', 102, 100, '2019-08-01', '2019-12-31', 5)
;

SELECT * FROM "Transaction";

INSERT INTO Donator
(client_id, "name")
VALUES
(2, 'JEAN'),
(2, 'GEORGE'),
(2, 'JEAN'),
(2, 'Ru_MAFIA'),
(2, 'IT_MAFIA'),
(3, 'PAPA'),
(3, 'MAMA'),
(3, 'VOISIN');

SELECT * FROM Donator;
