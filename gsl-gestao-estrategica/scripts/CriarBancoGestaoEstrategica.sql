CREATE DATABASE db_gestao_estrategica
GO
USE db_gestao_estrategica
GO

CREATE TABLE Estoque (id UNIQUEIDENTIFIER NOT NULL, 
                         codigo INT, 
						 depositocodigo INT, 
						 mercadoriacodigo INT, 
						 datacriacao DATETIME, 
						 dataatualizacao DATETIME, 
						 PRIMARY KEY (id))

CREATE TABLE Pedido (id UNIQUEIDENTIFIER NOT NULL, 
                         codigo INT, 
						 clientecpf NVARCHAR(20), 
						 valortotal DECIMAL(18,2),  
						 datacriacao DATETIME, 
						 dataatualizacao DATETIME, 
						 PRIMARY KEY (id))
						 
CREATE TABLE ItemPedido (id UNIQUEIDENTIFIER NOT NULL, 
                         codigo INT, 
						 mercadoriacodigo INT, 
						 mercadoriaquantidade INT, 
						 valor DECIMAL(18,2), 						 
						 pedidoid UNIQUEIDENTIFIER,
						 datacriacao DATETIME, 
						 dataatualizacao DATETIME, 
						 PRIMARY KEY (id),						 
						 FOREIGN KEY (pedidoid) REFERENCES Pedido(id))
						 
CREATE TABLE Entrega (id UNIQUEIDENTIFIER NOT NULL, 
                         codigo NVARCHAR(20), 						 
						 pedidoid UNIQUEIDENTIFIER, 
						 latitude DECIMAL(18,5), 
						 longitude DECIMAL(18,5), 
						 status INT,
						 datacriacao DATETIME, 
						 dataatualizacao DATETIME, 
						 PRIMARY KEY (id),
						 FOREIGN KEY (pedidoid) REFERENCES Pedido(id))
