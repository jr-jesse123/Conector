using Almocherifado.UI.Components.Models;
using Syncfusion.Blazor.Inputs;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Almocherifado.UI.Components.Helpers
{
    public static class FileHelper
    {
        public static void SaveFileToRoot(MemoryStream ms, string filePath)
        {
            File.WriteAllBytes(filePath, ms.ToArray());
        }

        private static string getPath(CadastroFerramentaModel ferramenta, int indice)
        {
            var nome_patrimonio = $"{ferramenta.Nome}_{ferramenta.Patrimonio}";
            var path = $"wwwroot/{nome_patrimonio}_{indice}.png";
            return path;
        }

        //public static (string[] paths ,Stream[] streams) getFotoFerramentaPath(CadastroFerramentaModel ferramenta, IEnumerable<UploadFiles> fotos)
        //{
            
        //    var streams = fotos.Select(f => f.Stream);

        //    var lista = new List<UploadFiles>(fotos);
        //    var paths = new List<string>();
        //    var indice = 0;
        //    lista.ForEach(f => paths.Add(getPath(ferramenta, indice++)));

        //    //File.WriteAllBytes(path, ms.ToArray());
        //    return (paths.ToArray(),streams.ToArray());
        //}

        internal static IEnumerable<string> getFotoFerramentaPath(CadastroFerramentaModel cf)
        {
           var indice = 1;
           return cf.Fotos.Select(ft => getPath(cf, indice++));
        }

        
    }
}
