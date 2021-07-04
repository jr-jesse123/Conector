

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
type FuncionarioInsert = 
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
type AlocacaoInsert = 
   {
      Ferramentas:Ferramenta [];
      Responsavel:Funcionario;ContratoLocacao:string;
      DataAlocacao:DateTime
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
               //this.CreateMap<Ferramenta,FerramentaInsert>() 
               
   //                  .ForMemberFs((fun d -> d.PatrimonioId),
   //                               (fun opts -> opts.MapFrom<int>(fun m ->  m.Patrimonio )))
               
               
                  

type RegistroManutencao = 
   |Saida = 0
   |Entrada = 1

type IFerramentaRepository =
   abstract member SalvarFerramenta:FerramentaInsert->unit
   abstract member GetAllFerramentas:unit->Ferramenta []
   abstract member RegistrarBaixaDeFerramenta:Ferramenta->unit
   abstract member RegistrarManutencaoDeFerramenta:Ferramenta -> RegistroManutencao ->unit
   

type IAlmocharifadoRepository =
   abstract member GetAllFuncionarios:unit->Funcionario []
   abstract member SalvarFuncionario:FuncionarioInsert->unit
   //abstract member GetAllAlocacoes:unit->Alocacao []
   //abstract member SalvarAlocacao:Alocacao->unit


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
open System.Data
open System.Data
open System

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

   //let ExecutaComandos (conection:SqlConnection) (cmds:SqlCommand[]) = 
   //   let transaction = conection.BeginTransaction()

   //   try
   //      for cmd in cmds do cmd.Transaction <- transaction
   //      for cmd in cmds do cmd.ExecuteNonQuery()
   //      transaction.Commit()
   //   with |ex -> transaction.Rollback()
   let CreateSimpleCommand (conection:SqlConnection) sqltext = 
         SqlCommand(sqltext,conection)


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
            Baixada = Convert.ToBoolean row.["Baixada"];
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
         
      let GetAllFerramentas conection = 
         let table = GetCommand GetAllFerramentasDML []
                     |> GetTable conection

         let fotosTable = GetCommand GetAllFotosDML []
                          |> GetTable conection
                    
         [|for row in table.Rows do
            let fotos = fotosTable.AsEnumerable() 
                        |> Seq.filter (fun dr -> dr.["FerramentaId"] = row.["PatrimonioId"])
            let filteredTable = fotos.CopyToDataTable()
            let fts = tableToFotos filteredTable
            yield tableRowToFEramenta row fts  |]

      let RegistrarManutencao (conection:SqlConnection) patrimonio (registro:RegistroManutencao) = 
         sprintf "update Ferramentas set EmManutencao = %d where PatrimonioId = %i" (int registro) patrimonio
         |> CreateSimpleCommand conection
         |> ExecutarComando conection
         |> ignore

      let RegistrarBaixaFerramenta (conection:SqlConnection) patrimonio= 
            sprintf "update Ferramentas set Baixada = 1 where PatrimonioId = %i"  patrimonio
            |> CreateSimpleCommand conection
            |> ExecutarComando conection
            |> ignore
   

   module AlmocharifadoRepository=
      let GetAllFuncionarioDML = "Select * from Funcionarios"
      let InsertFuncionarioDML = "insert into Funcionarios(CPF,Nome,Cargo,Email,Foto) \
      	values(@CPF,@Nome, @Cargo, @Email, @Foto)"

      
         

      let tableToFuncionarios (table:DataTable) : Funcionario[] =
         let str = string
         [|for row in table.Rows do 
                     yield {CPF= str row.["Cpf"];Nome= str row.["Nome"];
                            Cargo=str row.["Cargo"];Email=str row.["Email"];
                            Foto= str row.["Foto"]}  |]


      let GetAllFuncioarios conection = 
               GetCommand GetAllFuncionarioDML []
               |> GetTable conection
               |> tableToFuncionarios 

      let InsertFuncionario conection (funcionario) = 
         GetCommand InsertFuncionarioDML [ 
            <@ funcionario.CPF   @> ; <@ funcionario.Nome @> ;
            <@ funcionario.Cargo@> ; <@ funcionario.Email @>;
            <@ funcionario.Foto @>]
         |> ExecutarComando conection
         |> ignore

      let InsertAlocacaoDML = "insert into Alocacoes(ResponsavelCPF,DataAlocacao,ContratoLocacao) \
                               OUTPUT inserted.AlocacaoId \
      	                      values(@CPF,@DataAlocacao,@ContratoLocacao)"

      let InsertAlocaccacoFerramentaDML = "insert into Alocacoes_Ferramentas(AlocacaoId, FerramentaId) \
                                           values(@AlocacaoId,@Patrimonio)"
   
      

      let GetAllAlocacoesDML = "Select * from Alocacoes"

      let  getAllFerramentasAlocadasDML = "select * from Alocacoes_Ferramentas"
      
      let ``Tabela para FerramentasAlocadas e IdAloaccoes`` ferramentas (table:DataTable) : (FerramentaAlocadaInfo * int) []=
         let  datafromnullabedb (dbvalue:obj) = 
            if  dbvalue = (box DBNull.Value ) then 
               None 
            else 
               Some <| Convert.ToDateTime dbvalue;

         [| for row in table.Rows 
               do yield { Ferramenta = ferramentas 
                                       |> Array.find (fun (x:Ferramenta) -> x.Patrimonio = Convert.ToInt32 row.["FerramentaId"]);
                             DataDevolucao = datafromnullabedb row.["DataDevolucao"];
                             //row.["DataDevolucao"] |> Option.ofObj |> Option.map Convert.ToDateTime;
                             Observacoes = row.["Observacoes"] |> string
                           } , Convert.ToInt32 row.["AlocacaoId"]
         |]

      let GetAllFerramentasAlocadasInfo conection ferramentas =
         GetCommand getAllFerramentasAlocadasDML []
         |> GetTable conection
         |> ``Tabela para FerramentasAlocadas e IdAloaccoes`` ferramentas

      let InsertAlocacao conection (alocacaco:AlocacaoInsert) = 
      
         let AlocacaoId = 
            GetCommand InsertAlocacaoDML [
                     <@alocacaco.Responsavel.CPF@>;
                     <@alocacaco.DataAlocacao@> ;
                     <@alocacaco.ContratoLocacao@>] 
            |> ExecutarComando conection 
            |> Convert.ToInt32

         
         for ferramenta in alocacaco.Ferramentas do
            GetCommand InsertAlocaccacoFerramentaDML [ <@ AlocacaoId @> ; <@ ferramenta.Patrimonio  @> ]
            |> ExecutarComando conection |> ignore 

      let tableRowToAlocacao (row:DataRow) ferramentas responsavel : Alocacao = 
               {
                  Id = Convert.ToInt32 row.["AlocacaoId"]; 
                  Responsavel = responsavel;
                  DataAlocacao = Convert.ToDateTime row.["DataAlocacao"];
                  FerramentasAlocadas = ferramentas;
                  ContratoLocacao = string row.["ContratoLocacao"]
               }

      /// retorna as ferramentas alocadas e o id da respectiva alocação
      

      let GetAllAlocacoes conection ferramentas responsaveis :Alocacao [] =
         let Alocacoestable = GetCommand GetAllAlocacoesDML []
                                 |> GetTable conection

         let FerramentasAlocadas =
            GetCommand getAllFerramentasAlocadasDML []
            |> GetTable conection
            |> ``Tabela para FerramentasAlocadas e IdAloaccoes`` ferramentas


         [|for AlocacaoRow in Alocacoestable.Rows do
            let ferramentas = 
               FerramentasAlocadas
               |> Array.filter (fun fa -> snd fa = Convert.ToInt32 AlocacaoRow.["AlocacaoId"])
               
           
            let responsavel = 
               responsaveis 
               |> Array.filter (fun (funcionario:Funcionario) -> funcionario.CPF = string AlocacaoRow.["ResponsavelCPF"] )
               |> Array.exactlyOne 

            yield tableRowToAlocacao AlocacaoRow (Array.map fst ferramentas) responsavel |]
        

open Repository
open FerramentaRepository
open AlmocharifadoRepository
type  AlmocharifadoRepository(conStr) =
   let conection = new SqlConnection(conStr)
   
   interface IFerramentaRepository with 
      member this.SalvarFerramenta ferramentainsert = InserirFErramenta conection ferramentainsert
      member this.GetAllFerramentas () = GetAllFerramentas conection 
      member this.RegistrarManutencaoDeFerramenta ferramenta registro = 
              RegistrarManutencao conection ferramenta.Patrimonio registro
      member thi.RegistrarBaixaDeFerramenta ferramenta = ()
      //member this.RegistrarFimManutencao = 

   interface IAlmocharifadoRepository with
      member this.GetAllFuncionarios () = GetAllFuncioarios conection
      member this.SalvarFuncionario funcionario = InsertFuncionario conection funcionario


   interface IDisposable with member this.Dispose () = conection.Dispose ()
module AssemblyInfo=

   open System.Runtime.CompilerServices

   [<assembly: InternalsVisibleTo("AlmocharifadoDomainTests")>]
   do()