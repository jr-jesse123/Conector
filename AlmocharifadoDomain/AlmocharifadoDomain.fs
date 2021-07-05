namespace Almocharifado

namespace Entities
open System
open System.Collections.Generic


(*
alocada
em manutenção
Disponível
Baixada
*)

   

//type Fotos = private Fotos of string []

[<CLIMutable>]
type Ferramenta = 
                        {
                           Nome:string;Marca:string;
                           Modelo:string;DataCompra:DateTime;
                           Patrimonio:int;Fotos: string [];
                           Descricao:string
                           EmManutencao:bool
                           Baixada:bool
                        }

//module Ferramenta =
//   let CriarFerramenta id nome marca modelo dataCompra patrimonio fotos descricao = 
//      match Seq.length fotos with
//      | 0 -> Error "Ferramentas devem ter pelo menos uma foto."
//      | _ -> Ok {Id=id;Nome=nome;Marca=marca;Modelo=modelo; DataCompra=dataCompra;Patrimonio=patrimonio;
//               Fotos=fotos;Descricao=descricao; EmManutencao=false; }
   
[<CLIMutable>]
type Funcionario = {Nome:string;CPF:string;Cargo:string;Email:string;Foto:string}

//[<CLIMutable>]
//type Devolucao = {Ferramenta:Ferramenta;Data:DateTime;Observacoe:string }

//holds an datetype record with custom comparison

[<CLIMutable>]
type FerramentaAlocadaInfo =  {Ferramenta:Ferramenta;
                                 DataDevolucao:DateTime option; //TODO: ENVOVLER ESSE CARA NUM RECORDE COM CUSTOM CUSTOMEQUALITY E NOCCOMPARISSON CCOMO NO CURSO DE TYPE DRIVEN DEVELOPEMENTE PARA SIMPLIFICAR TESTES E COMPARAÇÕES
                                 Observacoes:string}
                               with member this.Devolvida = Option.isSome this.DataDevolucao
   

[<CLIMutable>]
type Alocacao = 
   {
      Id:int;
      FerramentasAlocadas:FerramentaAlocadaInfo [];
      Responsavel:Funcionario;ContratoLocacao:string;
      DataAlocacao:DateTime
   }
   with member this.Finalizada = 
      this.FerramentasAlocadas |> Array.forall (fun fa -> fa.Devolvida ) 


module Alocacoes=
   
   type FerramentasFetcher = unit->Ferramenta seq
   type FaInfoFetcher = unit->FerramentaAlocadaInfo seq

   //todo: implementar 
   let FerramentaAlocada (FaInfoFetcheer:FaInfoFetcher) (ferramenta:Ferramenta) =
      FaInfoFetcheer ()
      |> Seq.filter(fun fa -> fa.Ferramenta = ferramenta && fa.Devolvida)
      |> Seq.length
      |> function
      | 0 -> false
      | 1 -> true
      | _ -> failwith "Ferramenta parece estar aloccada mais de uma vez"

   let GetProximoPatrimonio (ferramentasFetcher:FerramentasFetcher) =
      let ferramentas = ferramentasFetcher () 
      if Seq.isEmpty ferramentas then 
         1
      else
         ferramentas
         |> Seq.map (fun fer -> int fer.Patrimonio)
         |> Seq.max |> (+) 1
         
module Ferramentas=
//TODO: TRAZER FILTROS PARA CA
//   let FiltrarPorTextoLivre ferramentas =
      
   
   let FerramentaDisponivel  (alocacoes:Alocacao seq)  ferramenta =

         let alocacaoContemFeramenta aloc ferramenta =
            aloc.FerramentasAlocadas 
            |> Array.map (fun fa -> fa.Ferramenta)
            |> Array.contains ferramenta

         let GetEmprestimoInfo aloc ferramenta = 
            Array.Find(aloc.FerramentasAlocadas,(fun fa -> fa.Ferramenta = ferramenta))
         

         if ferramenta.Baixada = true then
           false
         else
            alocacoes 
            |> Seq.sortByDescending (fun aloc -> aloc.Id)
            |> Seq.tryFind (fun aloc -> alocacaoContemFeramenta aloc ferramenta )
            |> function  
            |Some aloc -> Option.isSome (GetEmprestimoInfo aloc ferramenta).DataDevolucao
            |None -> true
            //| _ -> failwith "situação não prevista"
         

      //alocacoes
      //|> Seq.exists (fun aloc -> Seq.contains ferramenta aloc.Ferramentas) 
      //|> not


   let GetAlocacaoDeFerramentaAlocada (alocacoes:Alocacao seq) ferramenta  =
         
         if ferramenta.Baixada then
            None
         else

            match FerramentaDisponivel alocacoes ferramenta with 
            |false -> 
               alocacoes
               |> Seq.filter (fun aloc -> aloc.FerramentasAlocadas 
                                          |> Array.filter( fun fa -> not fa.Devolvida)
                                          |> Array.map (fun fa -> fa.Ferramenta) 
                                          |> Seq.contains ferramenta
                              )  
               |> Seq.exactlyOne
               |> Some
            |true -> None
