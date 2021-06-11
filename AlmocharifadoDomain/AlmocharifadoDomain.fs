namespace Almocharifado

namespace Entities
open System
open System.Collections.Generic

[<CLIMutable>]
type Ferramenta = 
         {
            Id:int
            Nome:string;Marca:string;
            Modelo:string;DataCompra:DateTime;
            Patrimonio:int;Fotos:string list;
            Descricao:string
         }
   
[<CLIMutable>]
type Funcionario = {Nome:string;CPF:string;Cargo:string;Email:string;Foto:string}

[<CLIMutable>]
type Alocacao = 
   {
      Id:int;Ferramentas:Ferramenta list;
      Responsavel:Funcionario;ContratoLocacao:string;
      DataAlocacao:DateTime
   }
