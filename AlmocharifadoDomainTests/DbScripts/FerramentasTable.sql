CREATE TABLE [dbo].[Ferramentas]
(
	[PatrimonioId] INT NOT NULL PRIMARY KEY ,
	Nome varchar(100) not null unique,
	Marca varchar(50) not null,
	Modelo varchar(50) not null,
	Descricao  varchar(max) not null default '',
	DataCompra datetime2 not null,
	EmManutencao bit not null default 0,
	Baixada bit not null default 0,
	SysStartTime datetime2 generated always as row start not null,
	SysEndTime datetime2 generated always as row end not null,
	period for system_time (SysStartTime,SysEndTime)
)

CREATE TABLE [dbo].[Fotos_Ferramentas]
(
	[FotoId] INT NOT NULL PRIMARY KEY identity,
	FerramentaId INT not null,
	Caminho varchar(100) not null, 
    CONSTRAINT [FK_Fotos_Ferramentas_ToFerramentas] FOREIGN KEY (FerramentaId) REFERENCES [Ferramentas]([PatrimonioId])
	
)

