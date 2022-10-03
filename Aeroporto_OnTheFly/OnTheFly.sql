CREATE DATABASE ONTHEFLY;

USE ONTHEFLY;

CREATE TABLE Passageiro(

	CPF varchar(11) NOT NULL,
	Nome varchar(50) NOT NULL,
	DataNasc Date NOT NULL,
	Sexo char(1) NOT NULL,
	Ultima_Compra DateTime NOT NULL,
	Data_Cadastro Date NOT NULL,
	Situacao char(7) NOT NULL,

	CONSTRAINT PK_CPF PRIMARY KEY (CPF)

);

CREATE TABLE Companhia_Aerea(

	CNPJ varchar(14) NOT NULL,
	RazaoSocial varchar(50) NOT NULL,
	Data_Abertura DateTime NOT NULL,
	Ultimo_Voo DateTime NOT NULL,
	Data_Cadastro Date NOT NULL,
	Situacao char(7) NOT NULL,

	CONSTRAINT PK_CNPJ PRIMARY KEY (CNPJ)

);

CREATE TABLE Aeronave(

	Inscricao_Aeronave varchar (6) NOT NULL,
	CNPJ varchar(14) NOT NULL,
	Capacidade int NOT NULL,
	Ultima_Venda DateTime NOT NULL,
	Data_Cadastro Date NOT NULL,
	Situacao char(7) NOT NULL,

	CONSTRAINT PK_Inscricao PRIMARY KEY (Inscricao_Aeronave),
	CONSTRAINT FK_CNPJ FOREIGN KEY (CNPJ) REFERENCES Companhia_Aerea(CNPJ)

);

CREATE TABLE Voo(

	ID_Voo varchar (5) NOT NULL,
	Inscricao_Aeronave varchar (6) NOT NULL,
	Destino varchar(3) NOT NULL,
	Data_HoraVoo DateTime NOT NULL,
	Data_Cadastro Date NOT NULL,
	AssentoOcup int NOT NULL,
	Situacao char (9) NOT NULL,

	CONSTRAINT PK_Voo PRIMARY KEY (ID_Voo),
	CONSTRAINT FK_Inscricao_Aeronave FOREIGN KEY (Inscricao_Aeronave) REFERENCES Aeronave(Inscricao_Aeronave)

);

CREATE TABLE Passagem_Voo(

	ID_Passagem varchar(6) NOT NULL,
	ID_Voo varchar (5) NOT NULL,
	Data_Ultima_Compra Datetime,
	Valor float NOT NULL,
	Situacao char(9) NOT NULL,

	CONSTRAINT PK_ID_Passagem PRIMARY KEY (ID_Passagem),
	CONSTRAINT FK_ID_Voo FOREIGN KEY (ID_Voo) REFERENCES Voo(ID_Voo)
);

DROP TABLE Passagem_Voo;

CREATE TABLE Venda(

	ID_Venda varchar(100) NOT NULL,
	CPF varchar (11) NOT NULL,
	Data_Venda DateTime NOT NULL,
	Valor_Total float NOT NULL,
	ID_Passagem varchar(6) NOT NULL,

	CONSTRAINT PK_ID_Venda PRIMARY KEY (ID_Venda),
	CONSTRAINT FK_CPF FOREIGN KEY (CPF) REFERENCES Passageiro(CPF),
	CONSTRAINT FK_ID_Passagem FOREIGN KEY (ID_Passagem) REFERENCES Passagem_Voo(ID_Passagem)
);

DROP TABLE Venda;


CREATE TABLE Cadastro_Restrito(

	CPF varchar (11) NOT NULL,

	CONSTRAINT PK_CPF_Rest PRIMARY KEY (CPF)
	
);

CREATE TABLE Cadastro_Bloqueado(

	CNPJ varchar (14) NOT NULL,

	CONSTRAINT PK_CNPJ_Bloq PRIMARY KEY (CNPJ)
	
);

CREATE TABLE Iatas(

	Iatas varchar(3) NOT NULL

	CONSTRAINT PK_Iatas PRIMARY KEY (Iatas)

);

CREATE TABLE Iatas(
	Siglas varchar(6) NOT NULL,
	Cidade varchar(30) NOT NULL

	CONSTRAINT PK_Siglas PRIMARY KEY (Siglas)
);

INSERT INTO Iatas (Siglas, Cidade) 
	VALUES ('FLN', 'Florianópolis'),
	('POA', 'Porto Alegre'),
	('REC', 'Recife'),
	('CWB', 'Curitiba'),
	('FOR', 'Fortaleza'),
	('NAT', 'Natal'),
	('MCZ', 'Maceio'),
	('FEN', 'Fernando de Noronha')
	
INSERT INTO Cadastro_Restrito (CPF) 
	VALUES ('66130698097'),
		('79699563001'),
		('85584038059'),
		('90175555052'),
		('64983887067')

INSERT INTO Cadastro_Bloqueado (CNPJ) 
	VALUES ('45801337000136'),
		('56682038000146'),
		('28122558000169'),
		('58992152000106'),
		('54125327000119')
	
SELECT * FROM Passageiro;
SELECT * FROM Companhia_Aerea;
SELECT * FROM Aeronave;
SELECT * FROM Voo;
SELECT * FROM Passagem_Voo;
SELECT * FROM Venda;
SELECT * FROM Cadastro_Restrito;
SELECT * FROM Cadastro_Bloqueado;
SELECT * FROM Iatas;

