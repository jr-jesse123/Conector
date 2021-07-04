
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
open System.IO
open Swensen.Unquote

open Bogus.Extensions.Brazil

type TestsProfile() as this = 
   inherit Profile()
   do ignore <| this.CreateMap<Ferramenta,FerramentaInsert>() 
   do ignore <| this.CreateMap<FuncionarioInsert,Funcionario>() 
   do ignore <| this.CreateMap<Funcionario,FuncionarioInsert>() 
   do ignore <| this.CreateMap<Alocacao,AlocacaoInsert>() 


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
open AutoFixture
open Bogus
open AutoBogus

type InfraEstruturaTests(outputHelper:ITestOutputHelper)=
   let dbfixture = new InteGrationTestDatabase()
   let sqlcon = dbfixture.DataBaseConection

   let mapper = MapperConfiguration(fun cfg -> cfg.AddProfile<TestsProfile>()).CreateMapper()

   [<Fact>]
   let ``Teste  de configura��o RepositorryProfile`` ()=
      let config = new MapperConfiguration(fun  cfg -> cfg.AddProfile<RepositoryProfile>())
      config.AssertConfigurationIsValid()
   
   
   //[<Property(MaxTest = 100)>]
   [<Property(MaxTest=100)>]
   let ``Ferramenta � corretamente  salva no banco de dados`` (ferramenta:FerramentaInsert)  =
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
   let ``Ferramenta � corretamente recuperada do banco de dados`` 
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
   let ``Registro de ferramenta em manuten��o executado e recuperado coreretamente``() =
         
         let ferramentas = Fixture().Build<FerramentaInsert>().CreateMany() |> Array.ofSeq

         try
            let ferramentasInput = 
               ferramentas
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
   let ``Registro de saida de manuten��o executado e recuperado coreretamente``() =
           
      let ferramentas = Fixture().Build<FerramentaInsert>().CreateMany() |> Seq.toArray

      try
         let ferramentasInput = 
            ferramentas
            //mapper.Map<FerramentaInsert[]>(ferramentas)
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

   let ``Ferramenta � baixada corretamente `` ()=

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
   
   #if INTERACTIVE
   #r "nuget: Bogus"
   #r "nuget: AutoBogus"
   open Bogus
   open AutoBogus
   open Bogus.Extensions.Brazil
   #endif

   [<Property>]
   let ``Funcionario � armazenado e recuperado corretamente`` () =
      let funcionarioInsert = AutoFaker<FuncionarioInsert>()
                                 .Generate()
      let funcionarioInsert = {funcionarioInsert with CPF=Faker().Person.Cpf(false)}

      printfn "%A" funcionarioInsert
      AlmocharifadoRepository.InsertFuncionario sqlcon funcionarioInsert


      let funcionarios = AlmocharifadoRepository.GetAllFuncioarios sqlcon 

      let testMap = MapperConfiguration(fun cfg -> cfg.AddProfile<TestsProfile>()).CreateMapper()
      let funcionario = testMap.Map<Funcionario>(funcionarioInsert)
      printfn "mapeado %A" funcionario
      printfn "lista: %A" funcionarios
      funcionarios |> Array.contains funcionario
   
   [<Property>]
   let ``let alocacoes sao persistidas e recuperadas corretamente`` () =
      let alocacao = AutoFaker<AlocacaoInsert>().Generate()
      
      let ferramentasCorrigidas = alocacao.Ferramentas |> Array.map (fun f -> {f with Baixada=false;EmManutencao=false})
      let alocacao2 = {alocacao 
                           with Responsavel={alocacao.Responsavel with CPF=Faker().Person.Cpf(false)};
                                Ferramentas=ferramentasCorrigidas
                                }
      
      try
         let funcionarioInsert = mapper.Map<FuncionarioInsert>(alocacao2.Responsavel)
         let ferramentasInsert = mapper.Map<FerramentaInsert[]>(alocacao2.Ferramentas)
         AlmocharifadoRepository.InsertFuncionario sqlcon funcionarioInsert
         for ferramenta in ferramentasInsert do FerramentaRepository.InserirFErramenta sqlcon ferramenta
         AlmocharifadoRepository.InsertAlocacao sqlcon alocacao2

         let ferramentas = FerramentaRepository.GetAllFerramentas sqlcon
         
         //let ferramentasAloccadas = AlmocharifadoRepository.GetAllFerramentasAlocadasInfo sqlcon ferramentas 
         
         let funcionarios = AlmocharifadoRepository.GetAllFuncioarios sqlcon 
         let alocacoesPersistidas = AlmocharifadoRepository.GetAllAlocacoes sqlcon  ferramentas funcionarios

         let last = alocacoesPersistidas |>  Array.last 
         test <@ last.Responsavel = alocacao2.Responsavel @>
         test <@ last.ContratoLocacao = alocacao2.ContratoLocacao @>
         test <@ last.DataAlocacao.ToString()  = alocacao2.DataAlocacao.ToString()@>
         
         let aproxDatetime (datetime:DateTime) = 
            DateTime(datetime.Year,datetime.Month,datetime.Day)
         
         test <@ last.FerramentasAlocadas 
                  |> Array.map (fun fa -> {fa.Ferramenta with DataCompra = aproxDatetime fa.Ferramenta.DataCompra  }) 
                  |> Array.forall (fun f -> alocacao2.Ferramentas
                                          |> Array.map (fun f -> {f with DataCompra= aproxDatetime f.DataCompra})
                                          |> Array.contains f) @>
         //test <@ last.FerramentasAlocadas.[2].Ferramenta  = alocacao2.Ferramentas.[2] @>
   
      with
      | :? SqlException as ex when 
         ex.Message.Contains("UNIQUE KEY constraint") || ex.Message.Contains("duplicate key") -> test <@ true @> 
      | _ -> reraise()
   
   [<Property>]
   let ``Ferramentas são devolvidias corretamente`` (observacoes:string)  =
      observacoes |> isNull |> not ==> lazy
      let alocacao = AutoFaker<AlocacaoInsert>().Generate()
      
      let ferramentasCorrigidas = alocacao.Ferramentas |> Array.map (fun f -> {f with Baixada=false;EmManutencao=false})
      let alocacao2 = {alocacao 
                           with Responsavel={alocacao.Responsavel with CPF=Faker().Person.Cpf(false)};
                                Ferramentas=ferramentasCorrigidas
                                }
      try     
         let funcionarioInsert = mapper.Map<FuncionarioInsert>(alocacao2.Responsavel)
         let ferramentasInsert = mapper.Map<FerramentaInsert[]>(alocacao2.Ferramentas)
         AlmocharifadoRepository.InsertFuncionario sqlcon funcionarioInsert
         for ferramenta in ferramentasInsert do FerramentaRepository.InserirFErramenta sqlcon ferramenta
         AlmocharifadoRepository.InsertAlocacao sqlcon alocacao2

         let ferramentas = FerramentaRepository.GetAllFerramentas sqlcon
         let funcionarios = AlmocharifadoRepository.GetAllFuncioarios sqlcon 
         let alocadevolver = Array.last <| AlmocharifadoRepository.GetAllAlocacoes sqlcon  ferramentas funcionarios 
      
         AlmocharifadoRepository.DevolverFerramenta sqlcon
            alocadevolver alocadevolver.FerramentasAlocadas.[0].Ferramenta 
             System.DateTime.Now observacoes

         let ferramentas = FerramentaRepository.GetAllFerramentas sqlcon
         let funcionarios = AlmocharifadoRepository.GetAllFuncioarios sqlcon 
      
         let Alocacoes = AlmocharifadoRepository.GetAllAlocacoes sqlcon  ferramentas funcionarios 

         let alocacaoDevolvida = Alocacoes |> Array.find (fun aloc -> aloc.Id = alocadevolver.Id)

         alocacaoDevolvida.FerramentasAlocadas.[0].Devolvida 
            && not alocacaoDevolvida.FerramentasAlocadas.[1].Devolvida
            && alocacaoDevolvida.FerramentasAlocadas.[0].Observacoes = observacoes

      with
          | :? SqlException as ex when 
             ex.Message.Contains("UNIQUE KEY constraint") || ex.Message.Contains("duplicate key") -> true
          | _ -> reraise()
       

   interface IDisposable with 
      member this.Dispose () = (dbfixture :> IDisposable).Dispose()
                  //let ferramentastore = mapper.Map<FerramentaStore>(ferramenta)


