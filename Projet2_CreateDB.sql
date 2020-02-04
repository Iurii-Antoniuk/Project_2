USE Projet2_BancAppli;

BEGIN TRANSACTION
CREATE TABLE Person (
id INT PRIMARY KEY IDENTITY(1,1),
"name" VARCHAR(50) NOT NULL,
"password" VARCHAR(255) NOT NULL
);

CREATE TABLE CurrentAccounts (
id INT PRIMARY KEY IDENTITY (100, 1),
--CONSTRAINT FK_CurrentAccounts FOREIGN KEY (id) REFERENCES Person(id),
client_id INT NOT NULL,
amount DECIMAL(10,2) NOT NULL,
overdraft INT NOT NULL,
openingDate DATETIME NOT NULL,
closeDate DATETIME,
FOREIGN KEY (client_id) REFERENCES Person(id)
ON UPDATE CASCADE 
ON DELETE CASCADE
);

CREATE TABLE SavingAccounts (
id INT PRIMARY KEY IDENTITY (100, 1),
--CONSTRAINT FK_SavingAccounts FOREIGN KEY (id) REFERENCES Person(id),
client_id INT NOT NULL,
amount DECIMAL(10,2) NOT NULL,
"ceiling" INT NOT NULL,
openingDate DATETIME NOT NULL,
closeDate DATETIME,
FOREIGN KEY (client_id) REFERENCES Person(id)
ON UPDATE CASCADE 
ON DELETE CASCADE
);

CREATE TABLE "Transaction" (
id INT PRIMARY KEY IDENTITY (1, 1),
--CONSTRAINT FK_TransactionC FOREIGN KEY (id) REFERENCES CurrentAccounts(id),
--CONSTRAINT FK_TransactionS FOREIGN KEY (id) REFERENCES SavingAccounts(id),
currentAccount_id INT NOT NULL,
savingAccount_id INT NOT NULL,
transactionType VARCHAR(80) NOT NULL,
beneficiaryAccount VARCHAR(80),
amount DECIMAL(10,2) NOT NULL,
"date" DATETIME NOT NULL
FOREIGN KEY (currentAccount_id) REFERENCES CurrentAccounts(id),
FOREIGN KEY (savingAccount_id) REFERENCES SavingAccounts(id)
);
COMMIT TRANSACTION
