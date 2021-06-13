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
         
   
namespace Dtos 
open Entities
open System
open System.Collections.Generic
   [<CLIMutable>]
   type FuncionarioDto = Funcionario
      
   [<CLIMutable>]
   type FerramentaDto  = 
      {
            Id:int
            Nome:string;Marca:string;
            Modelo:string;DataDaCompra:DateTime;
            Patrimonio:int;Fotos:ICollection<string>;
            Descricao:string
         }
   [<AutoOpen>]
   module FerramentaDto=
      let FromDomain (ferramenta:Ferramenta) =
   
                     {Id=ferramenta.Id;
                     Nome=ferramenta.Nome;
                     Modelo=ferramenta.Modelo;
                     Patrimonio=ferramenta.Patrimonio;
                     Fotos= new List<string>(ferramenta.Fotos) :> ICollection<string> ;
                     Descricao=ferramenta.Descricao;
                     Marca=ferramenta.Marca;
                     DataDaCompra=ferramenta.DataCompra
                     }
         
      //let ToDomain ferramentaDto :  Ferramenta =
      //              {
      //               Id=ferramentaDto.Id;
      //               Nome=ferramentaDto.Nome;
      //               Modelo=ferramentaDto.Modelo;
      //               Patrimonio=ferramentaDto.Patrimonio;
      //               Fotos= ferramentaDto.Fotos ;
      //               Descricao=ferramentaDto.Descricao;
      //               Marca=ferramentaDto.Marca;
      //               DataCompra=ferramentaDto.DataCompra
      //               }
    
   module DTO  =
      [<CLIMutable>]
      type AlocacaoDto =
         {
            Id:int;Ferramentas:ICollection<Ferramenta> ;
            Responsavel:FuncionarioDto;ContratoLocacao:string;
            DataAlocacao:DateTime
         }
      [<AutoOpen>]
      module AlocacaoDto=
         let ToDomain alocDto : Alocacao =
            {
               Id=alocDto.Id;
               Responsavel=alocDto.Responsavel
               ContratoLocacao=alocDto.ContratoLocacao
               DataAlocacao=alocDto.DataAlocacao
               Ferramentas=alocDto.Ferramentas
            }
         let FromDomain  (aloc:Alocacao) =
            {
               Id=aloc.Id;
               Responsavel=aloc.Responsavel
               ContratoLocacao=aloc.ContratoLocacao
               DataAlocacao=aloc.DataAlocacao
               Ferramentas= aloc.Ferramentas |> List<Ferramenta>
            }
         
   
