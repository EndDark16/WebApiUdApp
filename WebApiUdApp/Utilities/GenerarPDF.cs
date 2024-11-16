using iText.Kernel.Pdf;
using iText.Html2pdf;
using iText.Layout.Font;

namespace WebApiUdApp.Utilities
{
    public class GenerarPDF
    {

        public byte[] GeneratePdfFromHtml(string htmlContent)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    PdfWriter writer = new PdfWriter(memoryStream);
                    PdfDocument pdf = new PdfDocument(writer);

                    var properties = new ConverterProperties();
                    properties.SetCharset("utf-8");

                    HtmlConverter.ConvertToPdf(htmlContent, pdf, properties);

                    return memoryStream.ToArray();
                }
            }
            catch (FileNotFoundException fnfEx)
            {
                Console.WriteLine("Error al cargar la fuente: " + fnfEx.Message);
                throw new Exception("Error al cargar la fuente personalizada. Verifique la ruta y el archivo de fuente.", fnfEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al generar el PDF: " + ex.Message);
                throw new Exception("Error general al convertir HTML a PDF: " + ex.Message, ex);
            }
        }

        //// Método para convertir el HTML a PDF con DinkToPdf
        //public byte[] GeneratePdfFromHtmlx(string htmlContent)
        //{
        //    var doc = new HtmlToPdfDocument()
        //    {
        //        GlobalSettings = new GlobalSettings
        //        {
        //            PaperSize = PaperKind.A4,
        //            Orientation = Orientation.Portrait
        //        },
        //        Objects = { new ObjectSettings { HtmlContent = htmlContent, WebSettings = { DefaultEncoding = "utf-8" } } }
        //    };

        //    return _pdfConverter.Convert(doc);
        //}
    }
}
