#if INTERACTIVE
#else
module Caleo
#endif

open System.IO

type  plan1 =
    {
        Nome:string;OAB:string;Email:string; Telefone:string; Observacoes:string;
        ADVOGADO_CONTATO:string;Eventos:string
    }

let plan1 = File.ReadAllLines(@"D:\oab\Lista OAB1.csv")

let formatTelefone (telstr:string) =
    match telstr with
    | "" -> ""
    | _ -> telstr.Replace(" ","").Replace("(","").Replace(")","").Replace("-","").Trim()

let advs1 =
    plan1
    |> Array.map (fun x -> x.Split(';'))
    |> Array.map (fun x -> 
                        {OAB=x.[0].Trim();Nome=x.[1];
                        Email=x.[2].ToUpper().Trim();
                        Telefone=formatTelefone x.[3];
                        Observacoes=x.[4]; ADVOGADO_CONTATO=x.[5];
                        Eventos=x.[6]
                        })

let plan2 = File.ReadAllLines(@"D:\oab\Lista OAB2.csv")
type plan2  =
    {
        Nome:string;OAB:string;Bairro:string;Email:string; Telefone:string[]; 
        Evento:string;Instagram:string;
        InteracoesWhats:string;ContatosDoClaider:string;
    }

let advs2 : plan2[] = 
    plan2  |> Array.map (fun x -> x.Split(';'))
           |> Array.map (fun x -> 
                            let telefones = 
                                [|x.[4];x.[5];x.[6];|] 
                                |> Array.collect (fun x -> x.Split('/')) 
                                |> Array.collect (fun x -> x.Split(" (")) 
                                |> Array.collect (fun x -> x.Split(" ")) 
                                |> Array.collect
                                     (fun x -> if x.Length > 10 then x.Split("61") else [|x|]) 
                                |> Array.map(fun x -> if x.Length <= 10 && not(x.StartsWith("61")) then  "61"+ x else x) 
                                |> Array.map formatTelefone
                                |> Array.filter ((<>) "")
                                |> Array.filter ((<>) "61")
                                |> Array.filter ((<>) "6161")

                            {Nome=x.[0];OAB=x.[1].Trim();Bairro=x.[2];
                            Email=x.[3]; Telefone=telefones;
                            Instagram=x.[7];Evento=x.[8];
                            InteracoesWhats=x.[9];ContatosDoClaider=x.[10];
                            })

//Array.filter (fun (adv:plan2) -> adv.Telefone |> Array.exists (String.length >> (<) 12)) advs2

let plan3 = File.ReadAllLines(@"D:\oab\subsecao Taguatinga.csv")
type plan3 =
    {
        OAB:string;Nome:string;Situacao:string;TelesfonesOab:string[];
        TelefonesCatta:string[];Escritorio:string;Subsecao:string;
        Endereco:string;CEP:string;Email2018:string[];EmailCatta:string[];
    }


let advs3 = 
            plan3 |> Array.map (fun x -> x.Split(';'))
                  |> Array.map (fun x -> 
                    printfn "%A" <| x.[17].Split('|') ;

                      {
                          OAB=x.[0];Nome=x.[1].Trim();
                          Situacao=x.[2];TelesfonesOab=[|x.[7];x.[8]|] |> Array.filter (fun x -> x.Length > 0);
                          TelefonesCatta=x.[9].Split('|') |> Array.map formatTelefone |> Array.filter (fun x -> x.Length > 0);
                          Subsecao=x.[11];
                          Endereco=x.[12] + x.[13];Escritorio=x.[10];
                          CEP=x.[14]; Email2018=x.[16].Split("|")  ;
                          EmailCatta=x.[17].Split("|") 
                      })


let a= Array.map (fun (x:plan3) -> x.Email2018, x.Nome) advs3  
fst a.[10]


let nomesOab  = 
        let um =advs1|> Array.map (fun x -> x.OAB, x.Nome) 
        let dois = advs2|> Array.map (fun x -> x.OAB, x.Nome)
        let tres = Array.map (fun x -> x.OAB, x.Nome) advs3
        Array.concat [um; dois ;tres]
        |> Array.distinct



//TODO:JUNTAR EVENTO EM EVENTOS
//TODO:JUNTAR TELEFONES EM TELEFONES 2018
//
type advCompleto =
    {
        Nome:string;OAB:string;Email_Taguatinga:string[];
        TelefoneTaguatinga:string[]; Observacoes:string;
        ADVOGADO_CONTATO:string;Eventos:string;
        Bairro:string; Instagram:string;
        InteracoesWhats:string;ContatosDoClaider:string;
        Situacao:string;TelesfonesOab:string[];
        TelefonesCatta:string[];Escritorio:string;Subsecao:string;
        Endereco:string;CEP:string;Email2018:string[];EmailCatta:string[];
    }

let  advCompleto (nome, oab) :advCompleto = 
        let um = advs1|> Array.tryFind (fun x -> x.OAB = oab && x.Nome = nome) 
        let dois = advs2|> Array.tryFind (fun x -> x.OAB = oab && x.Nome = nome) 
        let tres = advs3 |> Array.tryFind (fun x -> x.OAB = oab && x.Nome = nome) 
        let emmailtagua = 
            match um ,dois with
            |Some x, Some y -> [|x.Email ; y.Email|]
            |Some x, None -> [|x.Email|]
            |None, Some y -> [| y.Email|]
            |None, None -> [||]
        
        let email2018 =
            match tres with
            |Some x -> x.Email2018 
            |None -> [||]
            |> Array.filter (fun x -> not <| Array.contains x emmailtagua)
       
        let emailCatta =
            match tres with
            |Some x -> printfn " qtd: %i" x.EmailCatta.Length ; x.EmailCatta
            |None -> [||]
            |> Array.filter (fun x -> not <|
                                             Array.contains x emmailtagua
                                             && Array.contains x email2018)

        
        let TelefoneTaguatinga = 
            match um ,dois with
            |Some x, Some y ->  Array.append [|x.Telefone|] y.Telefone 
            |Some x, None -> [|x.Telefone|]
            |None, Some y ->  y.Telefone
            |None, None -> [||]
            |> Array.map formatTelefone
           
        let TelefonesOab = 
            match tres with
            |Some x -> x.TelesfonesOab
            |None -> [||]
            // |Some x, Some y ->  Array.append [|x.Telefone|] y.Telefone
            // |Some x, None -> [|x.Telefone|]
            // |None, Some y ->  y.Telefone
            // |None, None -> [||]
            |> Array.map formatTelefone
            |> Array.filter (fun x -> not <| Array.contains x TelefoneTaguatinga)
        
        let telefonescatta  =
            
            let out = match tres with
                        |Some x ->  x.TelefonesCatta
                        |None -> [||]
                        |> Array.map formatTelefone
                        |> Array.filter (fun x -> 
                                                 not <| Array.contains x TelefoneTaguatinga
                                                        || Array.contains x  TelefonesOab)
            out

        let observacoes = if um.IsSome then um.Value.Observacoes else ""
        let advContato = if um.IsSome then um.Value.ADVOGADO_CONTATO else ""
        let eventos = 
            match um, dois with
            |Some x, Some y ->  x.Eventos + "|" + y.Evento
            |Some x, None -> x.Eventos
            |None, Some y -> y.Evento
            |None, None -> ""

        let bairro = if dois.IsSome then dois.Value.Bairro else ""
        
        let Instagram = if dois.IsSome then dois.Value.Instagram else ""

        let interacoes = if dois.IsSome then dois.Value.InteracoesWhats else ""
        
        let ContatosClaider = if dois.IsSome then dois.Value.ContatosDoClaider else ""
        
        let Situaccao = if tres.IsSome then tres.Value.Situacao else ""
        
        // let telOab = if tres.IsSome then tres.Value.TelesfonesOab else [||]
        // let telCatta = if tres.IsSome then tres.Value.TelefonesCatta else [||]
        let escritorio = if tres.IsSome then tres.Value.Escritorio else ""
        let subsecao = if tres.IsSome then tres.Value.Subsecao else ""
        let endereco = if tres.IsSome then tres.Value.Endereco else ""
        let cep = if tres.IsSome then tres.Value.CEP else ""
        // let email2018 = if tres.IsSome then tres.Value.Email2018 else [||]
        //                |> Array.filter (fun x -> not <| x.Contains("Cards"))
        // let emailcatta = if tres.IsSome then tres.Value.EmailCatta else [||]


        {
            Nome=nome;OAB=oab;Email_Taguatinga=emmailtagua;
            TelefoneTaguatinga=TelefoneTaguatinga; Observacoes=observacoes;
            ADVOGADO_CONTATO=advContato;Eventos=eventos;
            Bairro=bairro; Instagram=Instagram;
            InteracoesWhats=interacoes;ContatosDoClaider=ContatosClaider;
            Situacao=Situaccao;TelesfonesOab=TelefonesOab;
            TelefonesCatta=telefonescatta;Escritorio=escritorio;Subsecao=subsecao;
            Endereco=endereco;CEP=cep;Email2018=email2018;EmailCatta=emailCatta;
        }



let advsfull = nomesOab |> Array.map advCompleto        


let telOabCount = advsfull |> Array.maxBy (fun x -> x.TelesfonesOab.Length) 
telOabCount.TelesfonesOab.Length
2

let telCatta = advsfull |> Array.maxBy (fun x -> x.TelefonesCatta.Length) 
telCatta.TelefonesCatta.Length
7

let teltaguatinga = advsfull |> Array.maxBy (fun x -> x.TelefoneTaguatinga.Length) 
teltaguatinga.TelefoneTaguatinga.Length
10



let emailstagua = advsfull |> Array.maxBy (fun x -> x.Email_Taguatinga.Length) 
emailstagua.Email_Taguatinga.Length
1

let emailsccatta = advsfull |> Array.maxBy (fun x -> x.EmailCatta.Length) 
emailsccatta.EmailCatta.Length
2

let emails2018 = advsfull |> Array.maxBy (fun x -> x.Email2018.Length) 
emails2018.Email2018.Length
4



let advCompletoTocsv (advC:advCompleto) =

    let ajustArray (str:string)  =
        let atual = str.ToCharArray() |> Array.filter ((=) ';') |> Array.length      
        str +  String.replicate (10 - atual) ";"
                          


    let concatArra array size =
         match array with
            |[||] -> ajustArray ""
            | _ ->       Array.reduce (fun um dois -> um + ";" + dois)  array |> ajustArray  

    List.reduce (fun um dois -> um + ";" + dois)  
                                [advC.OAB ; advC.Nome; 
                                 advC.Observacoes; advC.Situacao;  
                                 advC.Bairro; advC.CEP;
                                 advC.ContatosDoClaider; advC.Eventos;
                                 advC.InteracoesWhats; advC.Endereco;
                                 advC.Escritorio; advC.Instagram;
                                 advC.Subsecao ; advC.ADVOGADO_CONTATO  ;
                                 concatArra advC.TelesfonesOab 10;
                                 concatArra advC.TelefonesCatta 10;
                                 concatArra advC.TelefoneTaguatinga 10;
                                 concatArra advC.Email2018 10;
                                 concatArra advC.EmailCatta 10;
                                 concatArra advC.Email_Taguatinga 10;
                                     ]

let output = advsfull |> Array.map advCompletoTocsv  
File.WriteAllLines(@"D:\oab\Consolidado.csv",output,System.Text.Encoding.UTF8)
advCompletoTocsv advsfull.[1]