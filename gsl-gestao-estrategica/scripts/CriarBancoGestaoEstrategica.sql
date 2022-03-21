CREATE DATABASE db_gestao_estrategica

USE db_gestao_estrategica

CREATE TABLE Mercadoria (id UNIQUEIDENTIFIER NOT NULL, 
                         codigo INT, 
						 nome NVARCHAR(200), 
						 quantidade INT, 
						 valor DECIMAL(18,2), 
						 datacriacao DATETIME, 
						 dataatualizacao DATETIME, 
						 PRIMARY KEY (id))

