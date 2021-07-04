CREATE TABLE [dbo].[Alocacoes]
(
	[AlocacaoId] INT NOT NULL Constraint PK_AlocacaoId PRIMARY KEY identity(1,1),
	ResponsavelCPF varchar(11) not null Constraint FK_Alocacoes_Responsavel Foreign Key References Funcionarios(Cpf),
	DataAlocacao datetime2 not null  default getdate(),
	ContratoLocacao varchar(max) not null,
	SysStartTime  datetime2 generated always as row start not null,
	SysEndTime datetime2 generated always as row end not null,
	period for system_time(SysStartTime,SysEndTime)


)
