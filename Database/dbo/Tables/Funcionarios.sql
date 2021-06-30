CREATE TABLE [dbo].[Funcionarios]
(
	[Cpf] varchar(11) NOT NULL PRIMARY KEY,
	Nome varchar(50) not null,
	Cargo varchar(50) not null,
	Email varchar(100) not null,
	Foto varchar(50) null,
	SysStartTime datetime2 generated always as row start not null,
	SysEndTime datetime2 generated always as row end not null,
	period for system_time (SysStartTime,SysEndTime)
)
