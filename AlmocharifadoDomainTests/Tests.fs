

module Tests

open System
open Xunit
open InfraEstrutura
open Microsoft.EntityFrameworkCore
open AutoFixture
open Entities
open Swensen.Unquote
open System.Collections.Generic
open AutoFixture
open AlmocharifadoApplication

let getContext ()=
   let options = DbContextOptionsBuilder<AlmocharifadoContext>()
                  //.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Almocharifado2Tests;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False").Options
                  .UseInMemoryDatabase("Almocharifado2Tests").Options
   let context = new AlmocharifadoContext(options)
   do context.Database.EnsureDeleted() |> ignore
   do context.Database.EnsureCreated() |> ignore
   context

type InfraEstruturaTests() =
   let context = getContext()

   [<Fact>]
   let ``Funcionario é salvo  corretamente`` () =
      let funcionario = Fixture().Build<Funcionario>().Create()

      context.Funcionarios.Add(funcionario)
      context.SaveChanges()

      test <@ context.Funcionarios.CountAsync().Result = 1 @>

      let funcRecuperado = context.Funcionarios.Find(funcionario.CPF)

      test <@ funcRecuperado = funcionario @>
      
   [<Fact>]
   let ``Ferramenta é salvo  corretamente`` () =
      let ferramenta = 
         Fixture().Build<Ferramenta>()
            .With((fun x -> x.Id),0)
            .Create()
  
      context.Ferramentas.Add(ferramenta)
      context.SaveChanges()
      
      test <@ context.Ferramentas.CountAsync().Result = 1 @>
      
      let ferramentaRecuperado = context.Ferramentas.Find(ferramenta.Id)
      
      test <@ ferramentaRecuperado.Id <> 0 @>
      test <@ ferramentaRecuperado   = ferramenta @>
            
   [<Fact>]
   let ``Alocacao é salvo  corretamente`` () =
      
      let ferramenta = Fixture().Build<Ferramenta>().With((fun x -> x.Id),0).Create() 
      let ferramenta2 = Fixture().Build<Ferramenta>().With((fun x -> x.Id),0).Create() 
      let ferramentas = [ferramenta;ferramenta2]

      let aloc = 
             Fixture().Build<DTO.AlocacaoDto>()
                  .With((fun x -> x.Id),0)
                  .With((fun x -> x.Ferramentas), List(ferramentas) :> ICollection<Ferramenta> )
                  .Create()
                  
               |> DTO.AlocacaoDto.ToDomain

      context.Alocaoes.Add(aloc)
      context.SaveChanges()
    
      test <@ aloc.Id <> 0 @>
      
      let alocRecuperado = context.Alocaoes.Find(aloc.Id)
      
      test <@ aloc = alocRecuperado @>

      test <@ context.Ferramentas.CountAsync().Result = 2 @>
      test <@ context.Funcionarios.CountAsync().Result = 1 @>

   interface IDisposable with member this.Dispose()= context.Database.EnsureDeleted() |> ignore
     

type ApplicationTests() =
   let context = getContext()
   [<Fact>]
   let ``Ferramenta já alocada é reconhecida corretamente``()=
      
      let alocacoes = Fixture().Build<Alocacao>().CreateMany(5)
      let alocacoesFetcher () = alocacoes
      let ferramentas = alocacoes 
                        |> Seq.collect (fun aloc -> aloc.Ferramentas)
      test <@ ferramentas 
         |> Seq.forall (fun ferramenta -> Alocacoes.FerramentaAlocada alocacoesFetcher ferramenta ) @>

   [<Fact>]
   let ``Ferramenta não alocada é reconhecida corretamente``()=
   
      let alocacoes = Fixture().Build<Alocacao>().CreateMany(5)
      let alocaccoesFetcher () = alocacoes
      let ferramentas = alocacoes 
                        |> Seq.collect (fun aloc -> aloc.Ferramentas)

      let ferramentaNaoAlocada = Fixture().Build<Ferramenta>().Create()
      test <@ not(Alocacoes.FerramentaAlocada alocaccoesFetcher ferramentaNaoAlocada)  @>


   [<Theory>]
   [<InlineData(10,11)>]
   [<InlineData(20,21)>]
   [<InlineData(0,1)>]
   let ``Proximo número de patrimônio é obtido corretamente`` qtd expected =
      let mutable patrimonio = 0
      let proximoPatrimonio () = 
         patrimonio <- patrimonio + 1 
         patrimonio
      
      let ferramentasFetcher qtd () = 
         if qtd = 0 then 
            [] :> IEnumerable<Ferramenta>
         else
            Fixture().Build<Ferramenta>()
               .With((fun x -> x.Id),0)
               .With<int>((fun fer -> fer.Patrimonio), proximoPatrimonio )
               .CreateMany(qtd)
                                    

      let proximo = Alocacoes.GetProximoPatrimonio (ferramentasFetcher qtd)
      test <@ proximo = expected @>

      

   


      

