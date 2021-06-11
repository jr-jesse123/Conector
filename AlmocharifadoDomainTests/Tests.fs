

module Tests

open System
open Xunit
open InfraEstrutura
open Microsoft.EntityFrameworkCore
open AutoFixture
open Entities
open Swensen.Unquote
open Xunit
open Xunit
open Entities
open AutoFixture
open Entities
open System.Collections.Generic
open Entities
open Entities


type InfraEstruturaTests() =
   let options = DbContextOptionsBuilder<AlmocharifadoContext>()
                  .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Almocharifado2Tests;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False").Options
   let context = new AlmocharifadoContext(options)

   do context.Database.EnsureDeleted() |> ignore
   do context.Database.EnsureCreated() |> ignore

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
      
      let aloc = 
             Fixture().Build<Alocacao>()
                  .With((fun x -> x.Id),0)
                  .With((fun x -> x.Ferramentas), [|ferramenta;ferramenta2|] :> ICollection<Ferramenta>)
                  .Create()

      context.Alocaoes.Add(aloc)
      context.SaveChanges()
    
      test <@ aloc.Id <> 0 @>
      
      let alocRecuperado = context.Alocaoes.Find(aloc.Id)
      
      test <@ aloc = alocRecuperado @>

      test <@ context.Ferramentas.CountAsync().Result = 2 @>
      test <@ context.Funcionarios.CountAsync().Result = 1 @>

   //interface IDisposable with member this.Dispose()= context.
     

   