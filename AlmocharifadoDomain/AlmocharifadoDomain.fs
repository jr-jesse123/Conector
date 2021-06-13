namespace Almocharifado

namespace Entities
open System
open System.Collections.Generic


//type Fotos = private Fotos of string []

[<CLIMutable>]
type Ferramenta = 
                        {
                           Id:int
                           Nome:string;Marca:string;
                           Modelo:string;DataCompra:DateTime;
                           Patrimonio:int;Fotos: string [];
                           Descricao:string
                        }

module Ferramenta =
   let CriarFerramenta id nome marca modelo dataCompra patrimonio fotos descricao = 
      match Seq.length fotos with
      | 0 -> Error "Ferramentas devem ter pelo menos uma foto."
      | _ -> Ok {Id=id;Nome=nome;Marca=marca;Modelo=modelo; DataCompra=dataCompra;Patrimonio=patrimonio;
               Fotos=fotos;Descricao=descricao}
   
[<CLIMutable>]
type Funcionario = {Nome:string;CPF:string;Cargo:string;Email:string;Foto:string}

[<CLIMutable>]
type Alocacao = 
   {
      Id:int;Ferramentas:Ferramenta seq;
      Responsavel:Funcionario;ContratoLocacao:string;
      DataAlocacao:DateTime
   }
