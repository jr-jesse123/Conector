namespace Almocherifado.ServerHosted.Helpers.FileHelpers
{
    public interface IPathHelper
    {
        string DocumentosTermos_Url { get; }
        string Ferramentas_Url { get; }
        string FotosTermos_Url { get; }
        string Ferramentas_Location { get; }
        string FotosTermos_Location { get; }
        string DocumentosTermos_Location { get; }
    }
}