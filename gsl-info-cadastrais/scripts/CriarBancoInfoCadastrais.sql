CREATE DATABASE db_info_cadastrais

USE db_info_cadastrais

CREATE TABLE Mercadoria (id UNIQUEIDENTIFIER NOT NULL, 
                         codigo INT, 
						 nome NVARCHAR(200), 
						 quantidade INT, 
						 valor DECIMAL(18,2), 
						 FornecedorId UNIQUEIDENTIFIER,
						 datacriacao DATETIME, 
						 dataatualizacao DATETIME, 
						 PRIMARY KEY (id),
						 FOREIGN KEY (FornecedorId) REFERENCES Fornecedor(id))

CREATE TABLE Fornecedor (id UNIQUEIDENTIFIER NOT NULL,
						cnpj NVARCHAR(30),
						nome NVARCHAR(200),
						latitude DECIMAL(18,5),
						longitude DECIMAL(18,5),
						cep NVARCHAR(10),
						logradouro NVARCHAR(250),
						numero INT,
						complemento NVARCHAR(200),
						cidade NVARCHAR(200),
						estado NVARCHAR(50),
						datacriacao DATETIME,
						dataatualizacao DATETIME, 
						PRIMARY KEY (id))
					
CREATE TABLE Deposito (id UNIQUEIDENTIFIER NOT NULL,
					   codigo INT,
					   nome NVARCHAR(200),
					   latitude DECIMAL(18,5),
					   longitude DECIMAL(18,5),
					   cep NVARCHAR(10),
					   logradouro NVARCHAR(250),
					   numero INT,
					   complemento NVARCHAR(200),
					   cidade NVARCHAR(200),
					   estado NVARCHAR(50),
					   datacriacao DATETIME,
					   dataatualizacao DATETIME, 
					   PRIMARY KEY (id))
					
CREATE TABLE Cliente (id UNIQUEIDENTIFIER NOT NULL,
					  cpf NVARCHAR(20),
					  nome NVARCHAR(200),
					  aniversario DATETIME,
					  cep NVARCHAR(10),
					  logradouro NVARCHAR(250),
					  numero INT,
					  complemento NVARCHAR(200),
					  cidade NVARCHAR(200),
					  estado NVARCHAR(50),
					  datacriacao DATETIME,
					  dataatualizacao DATETIME, 
					  PRIMARY KEY (id))