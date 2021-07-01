﻿CREATE TABLE [dbo].[Fotos_Ferramentas]
(
	[FotoId] INT NOT NULL PRIMARY KEY identity,
	FerramentaId INT not null,
	Caminho varchar(100) not null, 
    CONSTRAINT [FK_Fotos_Ferramentas_ToFerramentas] FOREIGN KEY (FerramentaId) REFERENCES [Ferramentas]([PatrimonioId])
	
)