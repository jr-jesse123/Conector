CREATE TABLE [dbo].[Ferramentas]
(
	[PatrimonioId] INT NOT NULL PRIMARY KEY ,
	Nome varchar(100) not null constraint UQ_Ferramenta_Nome unique,
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

with (
	SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.FerramentasHistory)
)

