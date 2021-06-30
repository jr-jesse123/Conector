CREATE TABLE [dbo].[Ferramentas]
(
	[PatrimonioId] INT NOT NULL PRIMARY KEY NONCLUSTERED Identity(1,1),
	Nome varchar(100) not null unique,
	Marca varchar(50) not null,
	Modelo varchar(50) not null,
	Descricao  varchar(max) not null default '',
	DataCompra datetime2 not null,
	EmManutencao bit not null default 0,
	SysStartTime datetime2 generated always as row start not null,
	SysEndTime datetime2 generated always as row end not null,
	period for system_time (SysStartTime,SysEndTime)
	 
)


