




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
open System.Data.SqlClient
open System.Diagnostics





type InteGrationTestDatabase ()=

   let rd = Random()
   let dbName = $"TESTE_{rd.Next()}"

   let mutable con = new SqlConnection(Repository.conStr "master")
   
   let CreateCMD = $" IF not EXISTS (SELECT name FROM master.sys.databases WHERE name = N'{dbName}') \
                     Create DataBase {dbName}"
                    
   do con.Open()
   let cmd = new SqlCommand(CreateCMD,con)
   do cmd.ExecuteNonQuery() |> ignore
      cmd.Dispose()



   do con.ChangeDatabase(dbName)
   do SqlCommand($"USE {dbName}",con).ExecuteNonQuery() |> ignore

   do con <- new SqlConnection(Repository.conStr dbName)
   do con.Open()

   let script = 
      $"USE {dbName}" + Environment.NewLine +
      File.ReadAllText("./DbScripts/FerramentasTable.sql")
     
   let cmd = new SqlCommand(script,con)
   do cmd.ExecuteNonQuery() |> ignore
   do con.Close()
   do printfn "%s" "nova db"

   interface IDisposable with 
      member this.Dispose () =
         let sqlcmd = $" USE MASTER"

         try
            con <- new SqlConnection(Repository.conStr "master")  
           
         with 
         | :? InvalidOperationException as ex when ex.Message.Contains("current state is open") -> ()
         
         let cmd = SqlCommand(sqlcmd,con)
         cmd.Connection.Open()

         cmd.ExecuteNonQuery() |> ignore
         try
            do SqlCommand($"DROP database {dbName}",con).ExecuteNonQuery() |> ignore
         with| :? SqlException as ex -> ()

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
   [<Property(MaxTest=100)>]
   let ``Ferramenta é corretamente parseada e salva no banco de dados`` (ferramenta:Ferramenta)  =
      helpers.getalltexts ferramenta |> Seq.forall helpers.naovazio ==> lazy
      let ferramentaStore = mapper.Map<FerramentaStore>(ferramenta)
      //TODO: INCLUIR FOTOS NOS TESTES
      
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
      
 
   [<Property(MaxTest=100)>]
   let ``Ferramenta é corretamente recuperada do banco de dados`` 
                                                (ferramenta:Ferramenta)  =
      (helpers.getalltexts ferramenta
         |> Seq.forall helpers.naovazio ) ==> lazy


      let ferramentaStore = mapper.Map<FerramentaStore>(ferramenta)

      try
         FerramentaRepository.InserirFErramenta sqlcon [
                                                      <@ ferramentaStore.Nome @>
                                                      <@ ferramentaStore.Marca @>
                                                      <@ ferramentaStore.Modelo @>
                                                      <@ ferramentaStore.DataCompra @>
                                                      <@ ferramentaStore.Descricao @>
                                                              ] |> ignore
           
         let ferramentas = FerramentaRepository.getAllFerramentas sqlcon


         ferramentas 
                  |> List.exists (fun x -> x.Nome = ferramenta.Nome 
                                             && x.Marca = ferramenta.Marca
                                             && x.Modelo = ferramenta.Modelo
                                             && ferramenta.DataCompra.ToString() = x.DataCompra.ToString()
                                             && ferramenta.Descricao = x.Descricao)  

      with
      | :? SqlException as ex when ex.Message.Contains("UNIQUE KEY constraint") -> true
      | _ -> reraise()

   static member insertData :seq<obj[]>=
      seq {
         [|   { Nome = "a";
               Marca = "a";
               Modelo = "a";
               DataCompra = DateTime(1940,12,23) ;
               Patrimonio = 0;
               Fotos = [||];
               Descricao = "a";
               EmManutencao = false;
               Baixada = true ;}  :> obj;|] ;

         [|{ Nome = "?5\001F";
            Marca = "Fh^";
            Modelo = "\018qP";
            DataCompra = DateTime(1961,03,4,19,28,28) ;
            Patrimonio = -2;
            Fotos = [|""; ">"|];
            Descricao = "'";
            EmManutencao = true;
            Baixada = true } :> obj|]

           }
   interface IDisposable with member this.Dispose () =
                     (dbfixture :> IDisposable).Dispose()
                  //let ferramentastore = mapper.Map<FerramentaStore>(ferramenta)

