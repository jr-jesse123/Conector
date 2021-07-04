CREATE TABLE [dbo].[Funcionarios]
(
	CPF varchar(11) NOT NULL Constraint PK_FuncionarioCPF PRIMARY KEY,
	Nome varchar(150) not null,
	Cargo varchar(150) not null,
	Email varchar(100) not null,
	Foto varchar(250) null,
	SysStartTime datetime2 generated always as row start not null,
	SysEndTime datetime2 generated always as row end not null,
	period for system_time (SysStartTime,SysEndTime)
)
