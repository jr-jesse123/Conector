CREATE TABLE [dbo].[Alocacoes_Ferramentas]
(
	[FerramentaId] INT NOT NULL unique constraint FK_Alocacoes_Ferramentas references Ferramentas(PatrimonioId),
	AlocacaoId  int not null constraint FK_Aloccacoes references Alocacoes(AlocacaoId),
	DataDevolucao datetime2 null ,
	Observacoes varchar(max) null 
	primary key (FerramentaId,AlocacaoId)

)
