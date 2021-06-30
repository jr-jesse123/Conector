CREATE TABLE [dbo].[Alocacoes]
(
	[AlocacaoId] INT NOT NULL PRIMARY KEY identity(1,1),
	Responsavel varchar(11) not null Constraint FK_Alocacoes_Responsavel Foreign Key References Funcionarios(Cpf),
	DataAlocacao datetime2 not null  default getdate(),
	SysStartTime  datetime2 generated always as row start not null,
	SysEndTime datetime2 generated always as row end not null,
	period for system_time(SysStartTime,SysEndTime)


)
