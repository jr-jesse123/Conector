using Almocherifado.UI.Components.Models;
using Syncfusion.Blazor.Inputs;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Almocherifado.UI.Components.Helpers
{
    public static class FileHelper
    {
        //public static void GetFileInRoot(string Filename)
        //{
        //    return File.ReadAllBytes($"wwwroot/{FileOwner.GetFileName()}.png", ms.ToArray());
        //}

        public static void SaveFileToRoot(MemoryStream ms, string filename)
        {
            File.WriteAllBytes($"wwwroot/{filename}", ms.ToArray());
        }

        //public static string getPathForFileDependent(CadastroFerramentaModel ferramenta, int indice)
        //{
        
        //    return path;
        //}


        //public static IEnumerable<string> getFotoFerramentaPath(CadastroFerramentaModel cf)
        //{
        //   var indice = 1;
        //   return cf.Fotos.Select(ft => getPath(cf, indice++));
        //}

        //internal static void SaveFilesToRoot(IEnumerable<UploadFiles> fotos1, string[] fotos2)
        //{
        //    for (int i = 0; i < fotos2.Length; i++)
        //    {
        //        SaveFileToRoot(fotos1.ToArray()[i].Stream, fotos2[i]);
        //    }
        //}

        //internal static void SaveFilesToRoot(CadastroFuncionarioModel funcionarioInput)
        //{
        //    var path = getFotFuncionarioPath(funcionarioInput);
        //    SaveFileToRoot(funcionarioInput.Foto.Stream, path);
        //}

        //public static string getFotFuncionarioPath(CadastroFuncionarioModel cfm)
        //{
        //    var path = $"wwwroot/{cfm.CPF}.png";
        //    return path;
        //}
    }
}
