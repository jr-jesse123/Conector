
namespace Almocherifado.UI.Components
{

    public partial class Uploader
    {
        private string getPath(FerramentaDto FerramentaInput, string fileName)
        {
            return @"./wwwroot/" + FerramentaInput.Nome + "_" + FerramentaInput.Marca
                        + "_" + FerramentaInput.Modelo + "_" + fileName + ".png";
        }

        private List<MemoryStream> fotos { get; set; } = new List<MemoryStream>();

        private void OnChange(UploadChangeEventArgs args)
        {
            fotos = new List<MemoryStream>();
            foreach (var file in args.Files)
            {
                var ms = new MemoryStream();
                file.Stream.WriteTo(ms);
                fotos.Add(ms);

            }
        }
    }
}
