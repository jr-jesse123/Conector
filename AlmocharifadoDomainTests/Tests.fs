




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

let sqlCon () =   new SqlConnection(Repository.conStr)


type InteGrationTestDatabase ()=
   let con = sqlCon ()
   let rd = Random()
   let dbName = $"TESTE_{rd.Next()}"

   
   let dbName = $"TESTES_{rd.Next()}"
   
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
   

   interface IDisposable with 
      member this.Dispose () =
         let sqlcmd = $" USE MASTER \
                         DROP database {dbName}"

         let cmd = SqlCommand(sqlcmd,con)
         
         cmd.ExecuteNonQuery() |> ignore
         con.Dispose()
   member _.DataBaseConection = con



  



type InfraEstruturaTests(outputHelper:ITestOutputHelper)=
   let dbfixture = new InteGrationTestDatabase()
   let sqlcon = dbfixture.DataBaseConection

   let mapper = MapperConfiguration(fun cfg -> cfg.AddProfile<RepositoryProfile>()).CreateMapper()

   [<Fact>]
   let ``Teste  de configuração RepositorryProfile`` ()=
      let config = new MapperConfiguration(fun  cfg -> cfg.AddProfile<RepositoryProfile>())
      config.AssertConfigurationIsValid()
   
   [<Fact>]
   let ``Ferramenta é corretamente parseada e salva no banco de dados`` ()  =
      //String.length ferramenta.Nome > 5 ==> lazy
        
      ()
   interface IDisposable with member this.Dispose () =
      
      (dbfixture :> IDisposable).Dispose()
                  //let ferramentastore = mapper.Map<FerramentaStore>(ferramenta)



      
   //interface IClassFixture<DatabaseFixture>   
      
//let getContext ()=
//   let options = DbContextOptionsBuilder<AlmocharifadoContext>()
//                  //.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Almocharifado2Tests;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False").Options
//                  .UseInMemoryDatabase("Almocharifado2Tests").Options
//   let context = new AlmocharifadoContext(options)
//   do context.Database.EnsureDeleted() |> ignore
//   do context.Database.EnsureCreated() |> ignore
//   context

//type InfraEstruturaTests() =
//   let context = getContext()

//   [<Fact>]
//   let ``Funcionario é salvo  corretamente`` () =
//      let funcionario = Fixture().Build<Funcionario>().Create()

//      context.Funcionarios.Add(funcionario)
//      context.SaveChanges()

//      test <@ context.Funcionarios.CountAsync().Result = 1 @>

//      let funcRecuperado = context.Funcionarios.Find(funcionario.CPF)

//      test <@ funcRecuperado = funcionario @>
      
//   [<Fact>]
//   let ``Ferramenta é salvo  corretamente`` () =
//      let ferramenta = 
//         Fixture().Build<Ferramenta>()
//            //.With((fun x -> x.Id),0)
//            .Create()
  
//      context.Ferramentas.Add(ferramenta)
//      context.SaveChanges()
      
//      test <@ context.Ferramentas.CountAsync().Result = 1 @>
      
//      let ferramentaRecuperado = context.Ferramentas.Find(ferramenta.Patrimonio)
      
//      //test <@ ferramentaRecuperado.Id <> 0 @>
//      test <@ ferramentaRecuperado   = ferramenta @>
            
//   [<Fact>]
//   let ``Alocacao é salvo  corretamente`` () =
      
//      let ferramenta = Fixture().Build<Ferramenta>()
//                        //.With((fun x -> x.Id),0)
//                        .Create() 
//      let ferramenta2 = Fixture().Build<Ferramenta>()
//                                 //.With((fun x -> x.Id),0)
//                                 .Create() 
//      let ferramentas = [ferramenta;ferramenta2]

//      //let devolucao1 = Fixture().Build<Devolucao>()
//      //                  .With((fun x -> x.Id),0).Create() 
//      //let devolucao2 = Fixture().Build<Devolucao>()
//      //                  .With((fun x -> x.Id),0).Create() 

//      let aloc = 
//             Fixture().Build<Alocacao>()
//                  .With((fun x -> x.Id),0)
//                  .With((fun x -> x.Ferramentas), List(ferramentas) :> seq<Ferramenta> )
//                  .With((fun x -> x.Devolucoes), List() :> seq<Devolucao> )
//                  .Create()
                  
               

//      context.Alocaoes.Add(aloc)
//      context.SaveChanges()
    
//      test <@ aloc.Id <> 0 @>
      
//      let alocRecuperado = context.Alocaoes.Find(aloc.Id)
      
//      test <@ aloc = alocRecuperado @>

//      test <@ context.Ferramentas.CountAsync().Result = 2 @>
//      test <@ context.Funcionarios.CountAsync().Result = 1 @>

//   interface IDisposable with member this.Dispose()= context.Database.EnsureDeleted() |> ignore
     

//type ApplicationTests() =
//   let context = getContext()
//   [<Fact>]
//   let ``Ferramenta já alocada é reconhecida corretamente``()=
      
//      let alocacoes = Fixture().Build<Alocacao>().CreateMany(5)
//      let alocacoesFetcher () = alocacoes
//      let ferramentas = alocacoes 
//                        |> Seq.collect (fun aloc -> aloc.Ferramentas)
//      test <@ ferramentas 
//         |> Seq.forall (fun ferramenta -> Alocacoes.FerramentaAlocada alocacoesFetcher ferramenta ) @>

//   [<Fact>]
//   let ``Ferramenta não alocada é reconhecida corretamente``()=
   
//      let alocacoes = Fixture().Build<Alocacao>().CreateMany(5)
//      let alocaccoesFetcher () = alocacoes
//      let ferramentas = alocacoes 
//                        |> Seq.collect (fun aloc -> aloc.Ferramentas)

//      let ferramentaNaoAlocada = Fixture().Build<Ferramenta>().Create()
//      test <@ not(Alocacoes.FerramentaAlocada alocaccoesFetcher ferramentaNaoAlocada)  @>


//   [<Theory>]
//   [<InlineData(10,11)>]
//   [<InlineData(20,21)>]
//   [<InlineData(0,1)>]
//   let ``Proximo número de patrimônio é obtido corretamente`` qtd expected =
//      let mutable patrimonio = 0
//      let proximoPatrimonio () = 
//         patrimonio <- patrimonio + 1 
//         string patrimonio
      
//      let ferramentasFetcher qtd () = 
//         if qtd = 0 then 
//            [] :> IEnumerable<Ferramenta>
//         else
//            Fixture().Build<Ferramenta>()
//               //.With((fun x -> x.Id),0)
//               .With<string>((fun fer -> fer.Patrimonio),fun _ -> proximoPatrimonio ()  )
//               .CreateMany(qtd)
                                    
//      let ferramentas = ferramentasFetcher qtd

//      let proximo = Alocacoes.GetProximoPatrimonio (ferramentas)
//      test <@ proximo = expected @>

    
    
//   [<Fact>]
//   let ``Ferramenta é devolvida corretamente`` () =
//      let context = getContext()
    
//      let ferramenta = Fixture().Build<Ferramenta>()
//                           .Create() 
//      let ferramenta2 = Fixture().Build<Ferramenta>()
//                           .Create() 
//      let ferramentas = [ferramenta;ferramenta2]
    
//      let aloc = Fixture().Build<Alocacao>()
//                     .With((fun x -> x.Id),0)
//                     .With((fun x -> x.Ferramentas), List(ferramentas) :> seq<Ferramenta> )
//                     .With((fun x -> x.Devolucoes), List() :> seq<Devolucao> )
//                     .Create()
       
//      context.Alocaoes.Add aloc |> ignore
//      context.SaveChanges()
    
      

//      let devolucao = {Id=0;Ferramenta=ferramenta;Data=DateTime.Now;Observacoe=""}

//      let alocacao = context.Alocaoes|> Seq.find (fun aloc -> aloc.Ferramentas |> Seq.contains(devolucao.Ferramenta)) 
//      let Devolucoes :seq<Devolucao> = devolucao :: (List.ofSeq alocacao.Devolucoes) :> _

//      let novoAloc = {alocacao with  Devolucoes=Devolucoes; }
//      let alocacaoOld = context.Find<Alocacao>(alocacao.Id)




//      test <@  novoAloc.Devolucoes |> Seq.length = 1  @>
//      printfn "%i"  devolucao.Id
//      context.Devolucoes.Add devolucao
//      printfn "%i"  devolucao.Id
//      test <@  context.Entry(devolucao).State = EntityState.Added @>
      
//      context.Entry(devolucao).Property("AlocacaoId").CurrentValue <- aloc.Id
//      let current = context.Entry(devolucao).Property("AlocacaoId").CurrentValue
      
//      context.SaveChanges()


//      let aloc = context.Alocaoes.ToListAsync().Result |> Seq.exactlyOne

//      test<@ aloc.Devolucoes |> Seq.length = 1 @>

//      let devolvida = Ferramentas.FerramentaDisponivel [aloc] ferramenta

//      test <@ devolvida @>


      
       


      

//[<Theory>]   
//[<InlineData(1)>]
//[<InlineData(2)>]
//[<InlineData(4)>]
//[<InlineData(9)>]
//let ``Ferramenta alocada é corretamente reconhecida como não disponível`` indice =
//   let alocacoes = Fixture().CreateMany<Alocacao>(10) |> Seq.toList

//   let ferramenta = alocacoes.[indice].Ferramentas |> Seq.head
   
//   let atual = Ferramentas.FerramentaDisponivel alocacoes ferramenta

//   test <@ not atual @>
   

      
//[<Theory>]   
//[<InlineData(1)>]
//[<InlineData(2)>]
//[<InlineData(4)>]
//[<InlineData(9)>]
//let ``Ferramenta não alocada é corretamente reconhecida Como disponível`` indice =
//   let alocacoes = Fixture().CreateMany<Alocacao>(10) |> Seq.toList
//   let ferramenta = Fixture().Create<Ferramenta>()
//   let atual = Ferramentas.FerramentaDisponivel alocacoes ferramenta
//   test <@  atual @>
   

//[<Theory>]   
//[<InlineData(1)>]
//[<InlineData(2)>]
//[<InlineData(4)>]
//let ``Ferramentas.GetAlocacaoDeFerramentaAlocada recupera a alocacao de uma ferramenta corretamente`` indice =
   
//   let alocacoes = Fixture().CreateMany<Alocacao>(10) |> Seq.toList
   
//   let ferramenta = alocacoes.[indice].Ferramentas |> Seq.head

//   let locacaoRecuperada = Ferramentas.GetAlocacaoDeFerramentaAlocada alocacoes ferramenta
   
//   test <@ Option.isSome locacaoRecuperada @>
//   test <@ locacaoRecuperada = Some alocacoes.[indice] @>

//[<Fact>]      
//let ``Ferramentas.GetAlocacaoDeFerramentaAlocada retorna none em caso de ferramenta livre``  =
   
//   let alocacoes = Fixture().CreateMany<Alocacao>(10) |> Seq.toList
   
//   let ferramenta = Fixture().Create<Ferramenta>()

//   let locacaoRecuperada = Ferramentas.GetAlocacaoDeFerramentaAlocada alocacoes ferramenta   
//   test <@ Option.isNone locacaoRecuperada @>
   
      

//[<Fact>]
//let ``Responsável é corretamente recuperado em alocacao `` =
//   let respo = Fixture().Create<Funcionario>();

//   let ferramenta = Fixture().Build<Ferramenta>()
//                              .Create() 
//   let ferramenta2 = Fixture().Build<Ferramenta>()
//                        .Create() 
//   let ferramentas = [ferramenta;ferramenta2]
   

//   let ferramentas  = Fixture().CreateMany<Ferramenta>();
 
//   let aloc = Fixture().Build<Alocacao>()
//                  .With((fun x -> x.Id),0)
//                  .With((fun x -> x.Ferramentas), List(ferramentas) :> seq<Ferramenta>)
//                  .With<Funcionario>((fun x -> x.Responsavel), respo )
//                  .With((fun x -> x.Devolucoes), List() :> seq<Devolucao> )
//                  .Create()

//   let context = getContext()
//   context.Add aloc
//   context.SaveChanges()

//   //let contex = getContext()
//   let aloc = context.Alocaoes.SingleAsync().Result

//   test <@ aloc.Responsavel = respo @>

//type Teste2()=

//   [<Fact>]
//   let ``Responsável é corretamente recuperado em alocacao `` () =
//      let respo = Fixture().Create<Funcionario>();

//      let ferramentas  = Fixture().CreateMany<Ferramenta>();
    
//      let aloc = Fixture().Build<Alocacao>()
//                     .With((fun x -> x.Id),0)
//                     .With((fun x -> x.Ferramentas), List(ferramentas) :> seq<Ferramenta>)
//                     .With<Funcionario>((fun x -> x.Responsavel), respo )
//                     .With((fun x -> x.Devolucoes), List() :> seq<Devolucao> )
//                     .Create()
   
//      let context = getContext()
//      context.Add aloc
//      context.SaveChanges()

//      //let context2 = getContext()
//      let aloc = context.Alocaoes.SingleAsync().Result

//      test <@ aloc.Responsavel = respo @>





   
   //[<Fact>]
   //let ``Ferramenta é devolvida corretamente `` () =
   //   let respo = Fixture().Create<Funcionario>();

   //   let ferramentas  = Fixture().CreateMany<Ferramenta>();
    
   //   let aloc = Fixture().Build<Alocacao>()
   //                  .With((fun x -> x.Id),0)
   //                  .With((fun x -> x.Ferramentas), List(ferramentas) :> seq<Ferramenta>)
   //                  .With<Funcionario>((fun x -> x.Responsavel), respo )
   //                  .With((fun x -> x.Devolucoes), List() :> seq<Devolucao> )
   //                  .Create()
   
   //   let context = getContext()
   //   context.Add aloc
   //   context.SaveChanges()

   //   //let context2 = getContext()
   //   let aloc = context.Alocaoes.SingleAsync().Result

   //   test <@ aloc.Responsavel = respo @>
   //   test <@ aloc.Ferramentas |> Seq.length = 3 @>
   //   test <@ aloc.Devolucoes |> Seq.length = 1 @>
