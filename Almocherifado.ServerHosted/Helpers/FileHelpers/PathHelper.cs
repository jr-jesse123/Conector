using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Almocherifado.ServerHosted.Helpers.FileHelpers
{
    public class PathHelper
    {
        readonly string basePath;
        
        public string Ferramentas { get; }
        public string FotosTermos { get; }
        public string DocumentosTermos { get; }

        public PathHelper(IWebHostEnvironment environment)
        {
            //basePath = Path.GetDirectoryName(Path.Combine( environment.WebRootPath, @"wwwroot\"));
            basePath = environment.WebRootPath;
            
            Ferramentas =  basePath + "\\fotosFerramentas\\";
            Directory.CreateDirectory(Ferramentas);

            FotosTermos = basePath + "\\fotosTermos\\";
            Directory.CreateDirectory(FotosTermos);

            DocumentosTermos = basePath + "\\Termos\\";
            Directory.CreateDirectory(DocumentosTermos);
            Console.WriteLine(DocumentosTermos);
        }
     
        
    }
}

