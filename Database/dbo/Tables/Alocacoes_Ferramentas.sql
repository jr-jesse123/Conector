CREATE TABLE [dbo].[Alocacoes_Ferramentas]
(
	[FerramentaId] INT NOT NULL  constraint FK_Alocacoes_Ferramentas references Ferramentas(PatrimonioId),
	AlocacaoId  int not null constraint FK_Aloccacoes references Alocacoes(AlocacaoId),
	DataDevolucao datetime2 null ,
	Observacoes varchar(max) null 
	CONSTRAINT [PK_aloc_fer] primary key (FerramentaId,AlocacaoId)

)
