namespace Almocharifado

namespace Entities
open System


//type Fotos = private Fotos of string []

[<CLIMutable>]
type Ferramenta = 
                        {
                           Id:int
                           Nome:string;Marca:string;
                           Modelo:string;DataCompra:DateTime;
                           Patrimonio:int;Fotos: string [];
                           Descricao:string
                           EmManutencao:bool
                        }

module Ferramenta =
   let CriarFerramenta id nome marca modelo dataCompra patrimonio fotos descricao = 
      match Seq.length fotos with
      | 0 -> Error "Ferramentas devem ter pelo menos uma foto."
      | _ -> Ok {Id=id;Nome=nome;Marca=marca;Modelo=modelo; DataCompra=dataCompra;Patrimonio=patrimonio;
               Fotos=fotos;Descricao=descricao; EmManutencao=false}
   
[<CLIMutable>]
type Funcionario = {Nome:string;CPF:string;Cargo:string;Email:string;Foto:string}

[<CLIMutable>]
type Alocacao = 
   {
      Id:int;Ferramentas:Ferramenta seq;
      Responsavel:Funcionario;ContratoLocacao:string;
      DataAlocacao:DateTime
   }

module Alocacoes=
   
   type FerramentasFetcher = unit->Ferramenta seq
   type AlocacoesFetcher = unit->Alocacao seq

   let FerramentaAlocada (listaAlocacoesFetcheer:AlocacoesFetcher) (ferramenta:Ferramenta) =
      listaAlocacoesFetcheer ()
      |> Seq.exists(fun aloc -> Seq.contains ferramenta aloc.Ferramentas; )

   let GetProximoPatrimonio (ferramentasFetcher:FerramentasFetcher) =
      let ferramentas = ferramentasFetcher () 
      if Seq.isEmpty ferramentas then 
         1
      else
         ferramentas
         |> Seq.map (fun fer -> fer.Patrimonio)
         |> Seq.max |> (+) 1
         
module Ferramentas=
   let FerramentaDisponivel  (alocacoes:Alocacao seq)  ferramenta =
      alocacoes
      |> Seq.exists (fun aloc -> Seq.contains ferramenta aloc.Ferramentas) 
      |> not
   
   let GetAlocacaoDeFerramentaAlocada (alocacoes:Alocacao seq) ferramenta =
      
      match FerramentaDisponivel alocacoes ferramenta with 
      |false -> 
         alocacoes
         |> Seq.filter (fun aloc -> Seq.contains ferramenta aloc.Ferramentas) 
         |> Seq.exactlyOne
         |> Some
      |true -> None
//namespace Dtos 
//open Entities
//open System
//open System.Collections.Generic
//   [<CLIMutable>]
//   type FuncionarioDto = Funcionario
      
//   [<CLIMutable>]
//   type FerramentaDto  = 
//      {
//            Id:int
//            Nome:string;Marca:string;
//            Modelo:string;DataDaCompra:DateTime;
//            Patrimonio:int;Fotos:ICollection<string>;
//            Descricao:string
//         }
//   open FluentValidation
//   [<AutoOpen>]
//   module FerramentaDto=
//      let FromDomain (ferramenta:Ferramenta) =
   
//                     {Id=ferramenta.Id;
//                     Nome=ferramenta.Nome;
//                     Modelo=ferramenta.Modelo;
//                     Patrimonio=ferramenta.Patrimonio;
//                     Fotos= new List<string>(ferramenta.Fotos) :> ICollection<string> ;
//                     Descricao=ferramenta.Descricao;
//                     Marca=ferramenta.Marca;
//                     DataDaCompra=ferramenta.DataCompra
//                     }
         
//      let ToDomain ferramentaDto :  Ferramenta =
//                    {
//                     Id=ferramentaDto.Id;
//                     Nome=ferramentaDto.Nome;
//                     Modelo=ferramentaDto.Modelo;
//                     Patrimonio=ferramentaDto.Patrimonio;
//                     Fotos= ferramentaDto.Fotos |> Array.ofSeq ;
//                     Descricao=ferramentaDto.Descricao;
//                     Marca=ferramentaDto.Marca;
//                     DataCompra=ferramentaDto.DataDaCompra
//                     }
      
//      //let ValidaiteFerramentaDto (builder:IRuleBuilder<>) ferDto =
         

//      type FerramentaValidator () as this = 
//         inherit AbstractValidator<FerramentaDto>()
//         do this.RuleFor(fun f -> f.DataDaCompra)
//                     .InclusiveBetween(DateTime(1980,1,1),DateTime.Now)
//                     .WithMessage("Data deve estar entre 1980 e hoje") 
//                     |> ignore

//         do this.RuleFor(fun f -> f.Nome).NotEmpty() |> ignore
//         do this.RuleFor(fun f -> f.Modelo).NotEmpty() |> ignore
//         do this.RuleFor(fun f -> f.Marca).NotEmpty() |> ignore
//         do this.RuleFor(fun f -> f.Fotos).NotEmpty() |> ignore


//   module DTO  =
//      [<CLIMutable>]
//      type AlocacaoDto =
//         {
//            Id:int;Ferramentas:ICollection<Ferramenta> ;
//            Responsavel:FuncionarioDto;ContratoLocacao:string;
//            DataAlocacao:DateTime
//         }
//      [<AutoOpen>]
//      module AlocacaoDto=
//         let ToDomain alocDto : Alocacao =
//            {
//               Id=alocDto.Id;
//               Responsavel=alocDto.Responsavel
//               ContratoLocacao=alocDto.ContratoLocacao
//               DataAlocacao=alocDto.DataAlocacao
//               Ferramentas=alocDto.Ferramentas
//            }
//         let FromDomain  (aloc:Alocacao) =
//            {
//               Id=aloc.Id;
//               Responsavel=aloc.Responsavel
//               ContratoLocacao=aloc.ContratoLocacao
//               DataAlocacao=aloc.DataAlocacao
//               Ferramentas= aloc.Ferramentas |> List<Ferramenta>
//            }
         
   
