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

CREATE TABLE [dbo].[Fotos_Ferramentas]
(
	[FotoId] INT NOT NULL constraint PK_Foto_Ferramenta PRIMARY KEY identity,
	FerramentaId INT not null,
	Caminho varchar(100) not null, 
    CONSTRAINT [FK_Fotos_Ferramentas_ToFerramentas] FOREIGN KEY (FerramentaId) REFERENCES [Ferramentas]([PatrimonioId])
	
)

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
CREATE TABLE [dbo].[Alocacoes_Ferramentas]
(
	[FerramentaId] INT NOT NULL  constraint FK_Alocacoes_Ferramentas references Ferramentas(PatrimonioId),
	AlocacaoId  int not null constraint FK_Aloccacoes references Alocacoes(AlocacaoId),
	DataDevolucao datetime2 null ,
	Observacoes varchar(max) null 
	CONSTRAINT [PK_aloc_fer] primary key (FerramentaId,AlocacaoId)

)
