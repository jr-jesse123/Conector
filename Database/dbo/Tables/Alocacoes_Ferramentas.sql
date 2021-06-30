CREATE TABLE [dbo].[Alocacoes_Ferramentas]
(
	[FerramentaId] INT NOT NULL unique constraint FK_Alocacoes_Ferramentas references Ferramentas(PatrimonioId),
	AlocacaoId  int not null constraint FK_Aloccacoes references Alocacoes(AlocacaoId),
	primary key (FerramentaId,AlocacaoId)

)
