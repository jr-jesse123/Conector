

#if INTERACTIVE
#r "nuget:AutoMapper"
#r "nuget: System.Data.SqlClient"
#r "nuget: FSharp.Quotations.Evaluator"
#load "AlmocharifadoDomain.fs"
#else
namespace Almocharifado.InfraEstrutura
#endif


open AutoMapper
open Entities
open System
open System.Data.SqlClient





[<CLIMutable>]
type FerramentaStore =
                  {
                       PatrimonioId:int;
                       Nome:string;
                       Marca:string;
                       Modelo:string;
                       DataCompra:DateTime;
                       Fotos: string [];
                       Descricao:string
                       EmManutencao:bool
                       Baixada:bool
                    }



[<CLIMutable>]
type FuncionarioStore = 
                              {
                                 Nome:string;
                                 CPF:string;
                                 Cargo:string;
                                 Email:string;
                                 Foto:string
                              }

[<CLIMutable>]
type DevolucaoStore = {FerramentaId:int;Data:DateTime;Observacoe:string }

[<CLIMutable>]
type AlocacaoStore = 
   {
      
      Id:int;
      FerramentasId:string seq;
      Responsavel:Funcionario;ContratoLocacao:string;
      DataAlocacao:DateTime
      DevolucoesId:int seq
   }

open System.Linq.Expressions
module MapperExtensions=
   type AutoMapper.IMappingExpression<'TSource, 'TDestination> with
    // The overloads in AutoMapper's ForMember method seem to confuse
    // F#'s type inference, forcing you to supply explicit type annotations
    // for pretty much everything to get it to compile. By simply supplying
    // a different name, 
    member this.ForMemberFs<'TMember>
            (destGetter:Expression<Func<'TDestination, 'TMember>>,
             sourceGetter:Action<IMemberConfigurationExpression<'TSource, 'TDestination, 'TMember>>) =
           this.ForMember(destGetter, sourceGetter)


open MapperExtensions
type RepositoryProfile() as this=
   
   inherit Profile() 
   do ignore  <|
               this.CreateMap<Ferramenta,FerramentaStore>() 
                     .ForMemberFs((fun d -> d.PatrimonioId),
                                  (fun opts -> opts.MapFrom<int>(fun m ->  m.Patrimonio )))
               
               
                  

type IFerramentaRepository =
   abstract member SalvarFerramenta:Ferramenta->unit
   //abstract member GetAllFerramentas:unit->Ferramenta []
   //abstract member BaixarFerramenta:Ferramenta->unit
   //abstract member EnviarFerramentaParaManutencao:Ferramenta->unit
   //abstract member FinalizarManutencao:Ferramenta->unit
   //abstract member DevolverFerramentas:Devolucao[]->unit
   

type IAlmocharifadoRepository =
   abstract member GetAllFuncionarios:unit->Funcionario []
   abstract member SalvarFuncionario:Funcionario->unit
   abstract member GetAllAlocacoes:unit->Alocacao []
   abstract member SalvarAlocacao:Alocacao->unit


open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Patterns
open FSharp.Quotations.Evaluator.QuotationEvaluationExtensions
[<AutoOpen>]
module RepositoryHelper=
   
   let CreateInParameter (nome , (vlr:'a)) =
      match nome with 
      | x when (x:string).StartsWith("@") ->  SqlParameter(nome, vlr)
      | _ -> SqlParameter("@" + nome, vlr)

   let GetNameAndValueTuple (e:Expr) =
      match e with
       | PropertyGet (eo, pi, li) -> pi.Name , e.CompileUntyped()
       | _ -> failwith "not a let-bound value"
   
   let GetNamesAndValues (propriedades:Expr list) =
         propriedades |> List.map GetNameAndValueTuple

module internal Repository=
   open RepositoryHelper
   let conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PatrimonioDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"

   let GetCommand dMLtext  (valores:Expr list) =
      let cmd = new SqlCommand(dMLtext)
      let values  =  GetNamesAndValues valores |> List.map (fun (x, y) -> SqlParameter(x,y)) |> Array.ofList
         
      cmd.Parameters.AddRange  values
      cmd
      
   let ExecutarComando (conection:SqlConnection) (cmd:SqlCommand) =
      try
         conection.Open()
      with
      | :? InvalidOperationException as ex ->
         conection.Close()
         conection.Open() 

      cmd.Connection <- conection
      let out = cmd.ExecuteScalar()
      conection.Close()
      out

   module FerramentaRepository=  
      let insertDML  =    "insert into Ferramentas (Nome,Marca,Modelo,Descricao,DataCompra) \
                          values(@Nome, @Marca, @Modelo, @Descricao, @DataCompra); \
                          select top 1 PatrimonioId from Ferramentas \
                          order by PatrimonioId Desc ;"

      let InserirFErramenta conection valores = 
         GetCommand insertDML valores 
         |> ExecutarComando conection
      

type  AlmocharifadoRepository() =
   
   interface IFerramentaRepository with 
      member this.SalvarFerramenta ferramenta = ()
  
module AssemblyInfo=

   open System.Runtime.CompilerServices

   [<assembly: InternalsVisibleTo("AlmocharifadoDomainTests")>]
   do()