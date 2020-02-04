

USE Projet2_BancAppli;

CREATE TABLE Person (
id INT PRIMARY KEY IDENTITY(1,1),
"name" VARCHAR(50) NOT NULL,
"password" VARCHAR(255) NOT NULL
);

CREATE TABLE CurrentAccounts (
id INT PRIMARY KEY IDENTITY (100, 1),
CONSTRAINT FK_CurrentAccounts FOREIGN KEY (id) REFERENCES Person(id),
amount DECIMAL(10,2) NOT NULL,
overdraft INT NOT NULL,
openingDate DATETIME NOT NULL,
closeDate DATETIME
);

CREATE TABLE SavingAccounts (
id INT PRIMARY KEY IDENTITY (100, 1),
CONSTRAINT FK_SavingAccounts FOREIGN KEY (id) REFERENCES Person(id),
amount DECIMAL(10,2) NOT NULL,
"ceiling" INT NOT NULL,
openingDate DATETIME NOT NULL,
closeDate DATETIME
);

CREATE TABLE "Transaction" (
id INT PRIMARY KEY IDENTITY (1, 1),
CONSTRAINT FK_TransactionC FOREIGN KEY (id) REFERENCES CurrentAccounts(id),
CONSTRAINT FK_TransactionS FOREIGN KEY (id) REFERENCES SavingAccounts(id),
transactionType VARCHAR(80) NOT NULL,
beneficiaryAccount VARCHAR(80),
amount DECIMAL(10,2) NOT NULL,
"date" DATETIME NOT NULL
);
