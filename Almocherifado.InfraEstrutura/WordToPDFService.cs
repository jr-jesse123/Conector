using Spire.Doc;

namespace Almocherifado.InfraEstrutura
{
    public class WordToPDFService
    {
        //string arqDoc = @"C:\Desenv\DocTeste.docx";
//        string arqPdf = @"C:\Desenv\DocTeste.pdf";
        public void ExportarWordParaPdf(string arqDoc)
        {
            var output = arqDoc.Replace(".docx", ".pdf");
            Document document = new Document();
            document.LoadFromFile(arqDoc);
            document.SaveToFile(output, FileFormat.PDF);
        }

    }
}
