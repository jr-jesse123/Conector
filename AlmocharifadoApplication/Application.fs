

namespace AlmocharifadoApplication

open Entities
open InfraEstrutura
open Microsoft.Extensions.DependencyInjection
open System.Runtime.CompilerServices

type IAlmocharifadoRepository =
   abstract member GetAllFerramentas:unit->Ferramenta []


module AlmocharifadoRepository=
   open System.Linq
   let GetAllFerramentas (context:AlmocharifadoContext) = context.Ferramentas.ToArray()
   


type AlmocharifadoRepository (context:AlmocharifadoContext) =
   interface IAlmocharifadoRepository
      with member this.GetAllFerramentas () = AlmocharifadoRepository.GetAllFerramentas context

module PatrimonioProvider =
   let GetProximoPatrimonioLivre (repo:IAlmocharifadoRepository) =
      repo.GetAllFerramentas >> Seq.ofArray
      |> Alocacoes.GetProximoPatrimonio


type IProximoPatrimonioProvider = 
   abstract member GetProximoPatrimonio:unit->int

type ProximoPatrimonioProvider (repo:IAlmocharifadoRepository) =
   
   interface IProximoPatrimonioProvider 
      with member  this.GetProximoPatrimonio () = PatrimonioProvider.GetProximoPatrimonioLivre repo


open Microsoft.EntityFrameworkCore
[<Extension>]
type IServiceCollectionExt =
   [<Extension>]
   static member ConfigurarPatrimonio (services:IServiceCollection) = 
      let constr  = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Almocharifado2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
      services
         //.AddDbContext<AlmocharifadoContext>(fun options -> options.UseSqlServer(constr) |> ignore )
         .AddTransient<IAlmocharifadoRepository,AlmocharifadoRepository>()
         .AddTransient<IProximoPatrimonioProvider,ProximoPatrimonioProvider>()   
      