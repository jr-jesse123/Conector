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

        public static string getPath(CadastroFerramentaModel ferramenta, int indice)
        {
            var nome_patrimonio = $"{ferramenta.Nome}_{ferramenta.Patrimonio}";
            var path = $"wwwroot/{nome_patrimonio}_{indice}.png";
            return path;
        }


        public static IEnumerable<string> getFotoFerramentaPath(CadastroFerramentaModel cf)
        {
           var indice = 1;
           return cf.Fotos.Select(ft => getPath(cf, indice++));
        }

        internal static void SaveFilesToRoot(IEnumerable<UploadFiles> fotos1, string[] fotos2)
        {
            for (int i = 0; i < fotos2.Length; i++)
            {
                SaveFileToRoot(fotos1.ToArray()[i].Stream, fotos2[i]);
            }
        }

        internal static void SaveFilesToRoot(CadastroFuncionarioModel funcionarioInput)
        {
            var path = getFotFuncionarioPath(funcionarioInput);
            SaveFileToRoot(funcionarioInput.Foto.Stream, path);
        }

        public static string getFotFuncionarioPath(CadastroFuncionarioModel cfm)
        {
            var path = $"wwwroot/{cfm.CPF}.png";
            return path;
        }
    }
}
