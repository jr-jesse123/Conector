


namespace AlmocharifadoApplication

open Entities
open InfraEstrutura
open Microsoft.Extensions.DependencyInjection
open System.Runtime.CompilerServices

type IFerramentaRepository =
   abstract member SalvarFerramenta:Ferramenta->unit
   abstract member GetAllFerramentas:unit->Ferramenta []
   abstract member BaixarFerramenta:Ferramenta->unit
   abstract member EnviarFerramentaParaManutencao:Ferramenta->unit
   abstract member FinalizarManutencao:Ferramenta->unit
   

type IAlmocharifadoRepository =
   abstract member GetAllFuncionarios:unit->Funcionario []
   abstract member SalvarFuncionario:Funcionario->unit
   abstract member GetAllAlocacoes:unit->Alocacao []
   abstract member SalvarAlocacao:Alocacao->unit



open System.Linq
module AlmocharifadoRepository=
   let GetAllFerramentas (context:AlmocharifadoContext) = context.Ferramentas.ToArray()
   let SalvarFerramenta (context:AlmocharifadoContext) ferramenta = 
      context.Ferramentas.Add ferramenta |> ignore
      context.SaveChanges() |> ignore
   
   let GetAllFuncionarios (context:AlmocharifadoContext) = context.Funcionarios.ToArray()
   let SalvarFuncionario (context:AlmocharifadoContext) funcionario = 
      context.Funcionarios.Add funcionario
      context.SaveChanges() |> ignore


   let GetAllAlocacoes (context:AlmocharifadoContext)  = context.Alocaoes.ToArray();
   let SalvarAlocacao (context:AlmocharifadoContext)  alocacao = 
      context.Alocaoes.Add alocacao
      context.SaveChanges() |> ignore


   let BaixarFerrammenta (context:AlmocharifadoContext) ferramenta =
      let ferramentabaixada = {ferramenta with Baixada = true}
      let ferramentaold = context.Ferramentas.Find(ferramenta.Id)
      context.Entry(ferramentaold).CurrentValues.SetValues(ferramentabaixada)
      context.SaveChanges() |> ignore

   let EnviarParaManutencaoFerrammenta (context:AlmocharifadoContext) ferramenta = 
      let ferramentabaixada = {ferramenta with EmManutencao = true}
      context.Update ferramentabaixada
      context.SaveChanges() |> ignore

   let RetornarFerrammentaDaManutencao (context:AlmocharifadoContext) ferramenta = 
      let ferramentabaixada = {ferramenta with EmManutencao = false}
      
      context.Update ferramentabaixada
      context.SaveChanges() |> ignore

type FerramentaRepository (context:AlmocharifadoContext) =
   interface IFerramentaRepository with
      member this.SalvarFerramenta ferrament = AlmocharifadoRepository.SalvarFerramenta context ferrament
      member this.GetAllFerramentas () = AlmocharifadoRepository.GetAllFerramentas context
      member this.BaixarFerramenta ferramenta =  AlmocharifadoRepository.BaixarFerrammenta context ferramenta
      member this.EnviarFerramentaParaManutencao ferramenta = AlmocharifadoRepository.EnviarParaManutencaoFerrammenta context ferramenta
      member this.FinalizarManutencao ferramenta = AlmocharifadoRepository.RetornarFerrammentaDaManutencao context ferramenta
   

type AlmocharifadoRepository (context:AlmocharifadoContext) =
   interface IAlmocharifadoRepository with
      member this.GetAllFuncionarios () = AlmocharifadoRepository.GetAllFuncionarios context
      member this.SalvarFuncionario funcionario = AlmocharifadoRepository.SalvarFuncionario context funcionario
      member this.GetAllAlocacoes () = AlmocharifadoRepository.GetAllAlocacoes context
      member this.SalvarAlocacao alocacao = AlmocharifadoRepository.SalvarAlocacao context alocacao



module PatrimonioProvider =
   let GetProximoPatrimonioLivre (repo:IFerramentaRepository) =
      repo.GetAllFerramentas >> Seq.ofArray
      |> Alocacoes.GetProximoPatrimonio


type IProximoPatrimonioProvider = 
   abstract member GetProximoPatrimonio:unit->int

type ProximoPatrimonioProvider (repo:IFerramentaRepository) =
   
   interface IProximoPatrimonioProvider 
      with member  this.GetProximoPatrimonio () = PatrimonioProvider.GetProximoPatrimonioLivre repo


open FluentValidation.AspNetCore

[<Extension>]
type IServiceCollectionExt =
   [<Extension>]
   static member ConfigurarPatrimonio (services:IServiceCollection) = 
      let constr  = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Almocharifado2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
      services
         //.AddDbContext<AlmocharifadoContext>(fun options -> options.UseSqlServer(constr) |> ignore )
         .AddTransient<IAlmocharifadoRepository,AlmocharifadoRepository>()
         .AddTransient<IFerramentaRepository,FerramentaRepository>()   
         .AddTransient<IProximoPatrimonioProvider,ProximoPatrimonioProvider>()   
         //.AddFluentValidation(fun fv -> fv.RegisterValidatorsFromAssemblyContaining<cadastro>() |> ignore)

      
