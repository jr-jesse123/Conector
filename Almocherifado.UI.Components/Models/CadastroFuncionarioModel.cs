using Almocherifado.UI.Components.Helpers;
using Syncfusion.Blazor.Inputs;

namespace Almocherifado.UI.Components.Models
{
    public record CadastroFuncionarioModel : IFileDependent
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Cargo { get; set; }
        public string Email { get; set; }
        public UploadFiles Foto { get; set; }

        public void SaveFilesToRoot()
        {
            FileHelper.SaveFileToRoot(Foto.Stream, Foto.FileInfo.Name);
        }
    }

}
