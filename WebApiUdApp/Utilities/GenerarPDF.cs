using DinkToPdf;
using DinkToPdf.Contracts;

namespace WebApiUdApp.Utilities
{
    public class GenerarPDF
    {
        private readonly IConverter _pdfConverter;

        public GenerarPDF(IConverter pdfConverter)
        {
            _pdfConverter = pdfConverter;
        }
        // Método para convertir el HTML a PDF
        public byte[] GeneratePdfFromHtml(string htmlContent)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                Objects = { new ObjectSettings { HtmlContent = htmlContent, WebSettings = { DefaultEncoding = "utf-8" } } }
            };

            return _pdfConverter.Convert(doc);
        }
    }
}
