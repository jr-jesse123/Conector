//using Almocherifado.Application;
//using Microsoft.AspNetCore.Hosting;
//using System.IO;

//namespace Almocherifado.UI.Helpers.FileHelpers
//{
//    public class PathHelper : IPathHelper
//    {
//        readonly string basePath;

//        public string Ferramentas_Location { get; }
//        public string FotosTermos_Location { get; }
//        public string DocumentosTermos_Location { get; }



//        public string Ferramentas_Url { get; } = "\\fotosFerramentas\\";
//        public string FotosTermos_Url { get; } = "\\fotosTermos\\";
//        public string DocumentosTermos_Url { get; } = "\\Termos\\";

//        public PathHelper(IWebHostEnvironment environment)
//        {
//            //basePath = Path.GetDirectoryName(Path.Combine( environment.WebRootPath, @"wwwroot\"));
//            basePath = environment.WebRootPath;

//            Ferramentas_Location = basePath + Ferramentas_Url;
//            Directory.CreateDirectory(Ferramentas_Url);

//            FotosTermos_Location = basePath + FotosTermos_Url;
//            Directory.CreateDirectory(FotosTermos_Url);

//            DocumentosTermos_Location = basePath + DocumentosTermos_Url;
//            Directory.CreateDirectory(DocumentosTermos_Url);

//        }


//        static PathHelper()
//        {

//        }


//    }
//}

