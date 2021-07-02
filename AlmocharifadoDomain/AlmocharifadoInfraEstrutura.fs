

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
type FerramentaInsert =
                  {
                       Patrimonio:int
                       Nome:string;
                       Marca:string;
                       Modelo:string;
                       DataCompra:DateTime;
                       Fotos: string [];
                       Descricao:string
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
   //do ignore  <|
   //            this.CreateMap<Ferramenta,FerramentaInsert>() 
   //                  .ForMemberFs((fun d -> d.PatrimonioId),
   //                               (fun opts -> opts.MapFrom<int>(fun m ->  m.Patrimonio )))
               
               
                  

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
       | ValueWithName (value, tipo , name) -> name  , value   
       | _ -> failwith "not a let-bound value"
   
   let GetNamesAndValues (propriedades:Expr list) =
         propriedades |> List.map GetNameAndValueTuple

open System.Data
open System
open System.Data.Common

module internal Repository=
   open RepositoryHelper
   let conStr db= @$"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog={db};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
 
   let GetTable (conection:SqlConnection) (cmd:SqlCommand) =
      try
         conection.Open()
      with
      | :? InvalidOperationException as ex ->
         conection.Close()
         conection.Open() 

      cmd.Connection <- conection
      use adapter = new SqlDataAdapter(cmd)
      let dt = new DataTable()
      adapter.Fill dt |> ignore
      dt




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

   let ExecutaComandos (conection:SqlConnection) (cmds:SqlCommand[]) = 
      let transaction = conection.BeginTransaction()

      try
         for cmd in cmds do cmd.Transaction = transaction
         for cmd in cmds do cmd.ExecuteNonQuery()
         transaction.Commit()
      with |ex -> transaction.Rollback()



   module FerramentaRepository=
      let inserFerramentatDML  = 
            "insert into Ferramentas (PatrimonioId, Nome, Marca, Modelo, Descricao, DataCompra) \
             values (@Patrimonio, @Nome, @Marca, @Modelo, @Descricao, @DataCompra);"
                        
      let insertFotoDML = "insert into Fotos_Ferramentas(FerramentaId,Caminho) \
                           values (@Patrimonio, @Foto)"

      let GetAllFerramentasDML = "Select * from Ferramentas"
      let GetAllFotosDML = "Select * from [Fotos_Ferramentas]"

      let tableToFotos (table:DataTable) : string[] =
         [|for row in table.Rows do yield string row.["Caminho"]  |]

      let tableRowToFEramenta (row:DataRow) fotos : Ferramenta =
         {
            Nome = string row.["Nome"] ;
            Modelo = string  row.["Modelo"] ;
            Marca = string  row.["Marca"] ; 
            DataCompra =  Convert.ToDateTime row.["DataCompra"]   ;
            Patrimonio =  Convert.ToInt32 row.["PatrimonioId"];
            Fotos = fotos;
            Descricao = string row.["Descricao"] ;
            EmManutencao = Convert.ToBoolean row.["EmManutencao"] ;
            Baixada = false;
         }



      let InserirFErramenta conection (ferramenta:FerramentaInsert) = 
            GetCommand inserFerramentatDML [
               <@ferramenta.Patrimonio@>;
               <@ferramenta.Nome@>;
               <@ferramenta.Marca@>;
               <@ferramenta.Modelo@>;
               <@ferramenta.DataCompra@>;
               <@ferramenta.Descricao@>;
               ] 
            |> ExecutarComando conection |> ignore
            
            for foto in ferramenta.Fotos do
               GetCommand insertFotoDML [ <@ ferramenta.Patrimonio  @> ; <@  foto @>]
               |> ExecutarComando conection |> ignore
         
          
      
      let getAllFerramentas conection = 
         let table = GetCommand GetAllFerramentasDML []
                     |> GetTable conection

         let fotosTable = GetCommand GetAllFotosDML []
                          |> GetTable conection
                    
         [for row in table.Rows do
            let fotos = fotosTable.AsEnumerable() 
                        |> Seq.filter (fun dr -> dr.["FerramentaId"] = row.["PatrimonioId"])
            let filteredTable = fotos.CopyToDataTable()
            let fts = tableToFotos filteredTable
            yield tableRowToFEramenta row fts  ]


type  AlmocharifadoRepository() =
   
   interface IFerramentaRepository with 
      member this.SalvarFerramenta ferramenta = ()
  
module AssemblyInfo=

   open System.Runtime.CompilerServices

   [<assembly: InternalsVisibleTo("AlmocharifadoDomainTests")>]
   do()