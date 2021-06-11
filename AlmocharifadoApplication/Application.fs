
module Alocacoes
open Entities
   
   let FerramentaAlocada (listaAlocacoesFetcheer:unit->Alocacao seq) (ferramenta:Ferramenta) =
      listaAlocacoesFetcheer ()
      |> Seq.exists(fun aloc -> aloc.Ferramentas.Contains(ferramenta))
      
   

      


   