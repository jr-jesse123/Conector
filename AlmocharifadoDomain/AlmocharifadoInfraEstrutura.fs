namespace Almocharifado.InfraEstrutura
open MongoDB
open MongoDB.Driver
open Entities
open System
open MongoDB.Bson
open MongoDB.Bson.Serialization.Attributes
open MongoDB.Bson.Serialization.Attributes
open MongoDB.Driver



[<CLIMutable>]
type FerramentaStore =
                  {
                       [<BsonId>]Patrimonio:int;
                       Nome:string;
                       Marca:string;
                       Modelo:string;
                       DataCompra:DateTime;
                       Fotos: string [];
                       Descricao:string
                       EmManutencao:bool
                       Baixada:bool
                    }



[<CLIMutable>]
type FuncionarioStore = {Nome:string;[<BsonId>]CPF:string;Cargo:string;Email:string;Foto:string}

[<CLIMutable>]
type DevolucaoStore = {FerramentaId:int;Data:DateTime;Observacoe:string }

[<CLIMutable>]
type AlocacaoStore = 
   {
      [<BsonId>]
      Id:int;
      FerramentasId:string seq;
      Responsavel:Funcionario;ContratoLocacao:string;
      DataAlocacao:DateTime
      DevolucoesId:int seq
   }

module private  Repository=
   let constr = "mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false"
   let mongo (constr:string) = MongoClient(constr)
   let almocharifadoDb (mongo:IMongoClient) = mongo.GetDatabase("almocharifado");
   let GetFerramentas (db:IMongoDatabase) = db.GetCollection<FerramentaStore>("ferramentas")
   //let GetDevolucoes (db:IMongoDatabase) = db.GetCollection<DevolucaoStore>("devolucoes")
   let GetFuncionarios (db:IMongoDatabase) = db.GetCollection<FuncionarioStore>("funcionarios")
   let GetAlocacoes (db:IMongoDatabase)= db.GetCollection<AlocacaoStore>("alocacoes")



