




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
open System
open AutoFixture

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
   let ``Ferramenta é corretamente  salva no banco de dados`` (ferramenta:FerramentaInsert)  =
      helpers.getalltexts ferramenta |> Seq.forall helpers.naovazio ==> lazy

      let ferramentainput = {ferramenta with Fotos=Fixture().Build<string>().CreateMany() |> Seq.toArray}
      
      try
         FerramentaRepository.InserirFErramenta sqlcon ferramentainput  
            |> ignore
      
      with
      | :? SqlException as ex when
            ex.Message.Contains("UNIQUE KEY constraint")
            || ex.Message.Contains("duplicate key") -> ()
      | _ -> reraise()
      
 
   [<Property>]
   let ``Ferramenta é corretamente recuperada do banco de dados`` 
                                                (ferramenta:FerramentaInsert)  =
      (helpers.getalltexts ferramenta
         |> Seq.forall helpers.naovazio  ) ==> lazy

      let ferramentainput = {ferramenta with Fotos=Fixture().Build<string>().CreateMany() |> Seq.toArray}

      try
         FerramentaRepository.InserirFErramenta sqlcon ferramentainput|> ignore
           
         let ferramentas = FerramentaRepository.GetAllFerramentas sqlcon

         ferramentas 
                  |> Array.exists (fun x -> x.Nome = ferramentainput.Nome 
                                             && x.Marca = ferramentainput.Marca
                                             && x.Modelo = ferramentainput.Modelo
                                             && ferramentainput.DataCompra.ToString() = x.DataCompra.ToString()
                                             && ferramentainput.Descricao = x.Descricao
                                             && ferramentainput.Fotos = x.Fotos)  

      with
      | :? SqlException as ex when 
         ex.Message.Contains("UNIQUE KEY constraint") || ex.Message.Contains("duplicate key") -> true
      | _ -> reraise()

   let rd = Random()
   [<Property>]
   let ``Registro de ferramenta em manutenção executado e recuperado coreretamente``() =
         
         let ferramentas = Fixture().Build<Ferramenta>().CreateMany()

         try
            let ferramentasInput = 
               mapper.Map<FerramentaInsert[]>(ferramentas)
               |> Array.map (fun fer -> {fer with Fotos=Fixture().Build<string>().CreateMany() |> Seq.toArray })

            for ferInput in ferramentasInput do FerramentaRepository.InserirFErramenta sqlcon ferInput
            let ferramentasArmazenadas = FerramentaRepository.GetAllFerramentas sqlcon
      
            let emManutencao = ferramentasArmazenadas.[rd.Next (ferramentasArmazenadas.Length - 1)]
            FerramentaRepository.RegistrarManutencao sqlcon emManutencao.Patrimonio RegistroManutencao.Entrada

            let ferramentaEmManutencao = FerramentaRepository.GetAllFerramentas sqlcon
                                          |> Array.filter (fun fer -> fer.EmManutencao && fer.Patrimonio = emManutencao.Patrimonio )
                                          |> Array.exactlyOne
                                          

            test <@emManutencao.Nome = ferramentaEmManutencao.Nome 
               && emManutencao.Marca = ferramentaEmManutencao.Marca
               && emManutencao.Modelo = ferramentaEmManutencao.Modelo
               && emManutencao.DataCompra.ToString() = ferramentaEmManutencao.DataCompra.ToString()
               && emManutencao.Descricao = ferramentaEmManutencao.Descricao
               && emManutencao.Fotos = ferramentaEmManutencao.Fotos @>
      
         with
         | :? SqlException as ex when 
            ex.Message.Contains("UNIQUE KEY constraint") || ex.Message.Contains("duplicate key") -> ()
         | _ -> reraise()

   [<Property>]
   let ``Registro de saida de manutenção executado e recuperado coreretamente``() =
           
      let ferramentas = Fixture().Build<Ferramenta>().CreateMany()

      try
         let ferramentasInput = 
            mapper.Map<FerramentaInsert[]>(ferramentas)
            |> Array.map (fun fer -> {fer with Fotos=Fixture().Build<string>().CreateMany() |> Seq.toArray })

         for ferInput in ferramentasInput do FerramentaRepository.InserirFErramenta sqlcon ferInput
         let ferramentasArmazenadas = FerramentaRepository.GetAllFerramentas sqlcon
         for fer in ferramentasArmazenadas do 
         FerramentaRepository.RegistrarManutencao sqlcon fer.Patrimonio RegistroManutencao.Entrada

         let ManutencaoFinalizada = ferramentasArmazenadas.[rd.Next (ferramentasArmazenadas.Length - 1)]
         FerramentaRepository.RegistrarManutencao sqlcon ManutencaoFinalizada.Patrimonio RegistroManutencao.Saida

         let ferramentaEmManutencao = FerramentaRepository.GetAllFerramentas sqlcon
                                       |> Array.filter (fun fer -> not fer.EmManutencao && fer.Patrimonio = ManutencaoFinalizada.Patrimonio )
                                       |> Array.exactlyOne
                                            

         test <@ManutencaoFinalizada.Nome = ferramentaEmManutencao.Nome 
            && ManutencaoFinalizada.Marca = ferramentaEmManutencao.Marca
            && ManutencaoFinalizada.Modelo = ferramentaEmManutencao.Modelo
            && ManutencaoFinalizada.DataCompra.ToString() = ferramentaEmManutencao.DataCompra.ToString()
            && ManutencaoFinalizada.Descricao = ferramentaEmManutencao.Descricao
            && ManutencaoFinalizada.Fotos = ferramentaEmManutencao.Fotos @>
        
      with
      | :? SqlException as ex when 
         ex.Message.Contains("UNIQUE KEY constraint") || ex.Message.Contains("duplicate key") -> ()
      | _ -> reraise()

   let ``Ferramenta é baixada corretamente `` ()=

         let ferramentas = Fixture().Build<Ferramenta>()
                                    .With((fun x -> x.Baixada), false)
                                    .CreateMany()

         try
            let ferramentasInput = 
               mapper.Map<FerramentaInsert[]>(ferramentas)
               |> Array.map (fun fer -> {fer with Fotos=Fixture().Build<string>().CreateMany() |> Seq.toArray })

            for ferInput in ferramentasInput do FerramentaRepository.InserirFErramenta sqlcon ferInput
            let ferramentasArmazenadas = FerramentaRepository.GetAllFerramentas sqlcon
            for fer in ferramentasArmazenadas do 
            FerramentaRepository.RegistrarManutencao sqlcon fer.Patrimonio RegistroManutencao.Entrada

            let FerramentaBaixada = ferramentasArmazenadas.[rd.Next (ferramentasArmazenadas.Length - 1)]
            FerramentaRepository.RegistrarBaixaFerramenta sqlcon FerramentaBaixada.Patrimonio 

            let ferBaixadaPersistida = FerramentaRepository.GetAllFerramentas sqlcon
                                          |> Array.filter (fun fer -> not fer.EmManutencao && fer.Patrimonio = FerramentaBaixada.Patrimonio )
                                          |> Array.exactlyOne
                                               

            test <@FerramentaBaixada.Nome = ferBaixadaPersistida.Nome 
               && FerramentaBaixada.Marca = ferBaixadaPersistida.Marca
               && FerramentaBaixada.Modelo = ferBaixadaPersistida.Modelo
               && FerramentaBaixada.DataCompra.ToString() = ferBaixadaPersistida.DataCompra.ToString()
               && FerramentaBaixada.Descricao = ferBaixadaPersistida.Descricao
               && FerramentaBaixada.Fotos = ferBaixadaPersistida.Fotos @>
           
         with
         | :? SqlException as ex when 
            ex.Message.Contains("UNIQUE KEY constraint") || ex.Message.Contains("duplicate key") -> ()
         | _ -> reraise()
   
   interface IDisposable with 
      member this.Dispose () = (dbfixture :> IDisposable).Dispose()
                  //let ferramentastore = mapper.Map<FerramentaStore>(ferramenta)

