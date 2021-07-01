




module Tests
open Almocharifado.InfraEstrutura
open System
open Xunit
open Xunit.Abstractions
open AutoMapper
open Entities
open FsCheck


open FsCheck.Xunit
open System.Data.SqlClient
open Xunit.Sdk
open System.Reflection
open System.IO
open System.Data.SqlClient
open System.Data.SqlClient
open AutoFixture.Xunit2
open AutoFixture.Xunit2
open Swensen.Unquote





type InteGrationTestDatabase ()=
   let con = new SqlConnection(Repository.conStr)
   let rd = Random()
   let dbName = $"TESTE_{rd.Next()}"

   let CreateCMD = $" IF not EXISTS (SELECT name FROM master.sys.databases WHERE name = N'TESTES') \
   Create DataBase {dbName}"
   do con.Open()
   let cmd = new SqlCommand(CreateCMD,con)
   do cmd.ExecuteNonQuery() |> ignore
      cmd.Dispose()


   let script = 
      $"USE {dbName}" + Environment.NewLine +
      File.ReadAllText("./DbScripts/FerramentasTable.sql")
     
   let cmd = new SqlCommand(script,con)
   do cmd.ExecuteNonQuery() |> ignore
   do con.Close()
   do printfn "%s" "nova db"

   interface IDisposable with 
      member this.Dispose () =
         let sqlcmd = $" USE MASTER \
                         DROP database {dbName}"

         let cmd = SqlCommand(sqlcmd,con)
         con.Open()
         cmd.ExecuteNonQuery() |> ignore
         con.Dispose()
   member _.DataBaseConection = con


module helpers=
   let naovazio = String.IsNullOrWhiteSpace >> not
   let getalltexts o = o.GetType().GetProperties() 
                        |> Seq.filter (fun x -> x.PropertyType = typeof<string>)
                        |> Seq.map (fun x -> x.GetValue o :?> string)


open Repository
open System.Runtime.Intrinsics.Arm

type InfraEstruturaTests(outputHelper:ITestOutputHelper)=
   let dbfixture = new InteGrationTestDatabase()
   let sqlcon = dbfixture.DataBaseConection

   let mapper = MapperConfiguration(fun cfg -> cfg.AddProfile<RepositoryProfile>()).CreateMapper()

   [<Fact>]
   let ``Teste  de configuração RepositorryProfile`` ()=
      let config = new MapperConfiguration(fun  cfg -> cfg.AddProfile<RepositoryProfile>())
      config.AssertConfigurationIsValid()
   
   
   //[<Property(MaxTest = 100)>]
   [<Property>]
   let ``Ferramenta é corretamente parseada e salva no banco de dados`` (ferramenta:Ferramenta)  =
      helpers.getalltexts ferramenta |> Seq.forall helpers.naovazio ==> lazy
      let ferramentaStore = mapper.Map<FerramentaStore>(ferramenta)
      try
         FerramentaRepository.InserirFErramenta sqlcon [
                                                            <@ ferramentaStore.Nome @>
                                                            <@ ferramentaStore.Marca @>
                                                            <@ ferramentaStore.Modelo @>
                                                            <@ ferramentaStore.DataCompra @>
                                                            <@ ferramentaStore.Descricao @>
                                                         ] |> ignore
      
      with
      | :? SqlException as ex when ex.Message.Contains("UNIQUE KEY constraint") -> ()
      | _ -> reraise()
      ()
   interface IDisposable with member this.Dispose () =
      
      (dbfixture :> IDisposable).Dispose()
                  //let ferramentastore = mapper.Map<FerramentaStore>(ferramenta)

