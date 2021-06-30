CREATE TABLE [dbo].[Alocacao_Devolucoes]
(
	[AlocacaoId] INT NOT NULL Constraint  FK_Devolucoes_Alocacao references Alocacoes(AlocacaoId),
	FerramentaId int not null Constraint FK_Devolucoes_Ferramenta references Ferramentas(PatrimonioId),
	DataDevolucao datetime2 not null  default getdate(),
	Observacoes varchar(max) null
)
