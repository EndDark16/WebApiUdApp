using SkiaSharp;
using WebApiUdApp.Dtos;

namespace WebApiUdApp.Utilities
{
    public class GenerarHistogramaBase64
    {
        public string GenerarGLineasUsuariosB64(List<ReporteUsuariosDto> reporteUsuarios)
        {
            const int width = 800;
            const int height = 400;
            using var surface = SKSurface.Create(new SKImageInfo(width, height));
            var canvas = surface.Canvas;

            canvas.Clear(SKColors.White);

            // Define colores y estilos
            var lineColor = SKColors.CadetBlue;
            var pointColor = SKColors.DarkSlateGray;
            var textPaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 14,
                IsAntialias = true
            };
            var linePaint = new SKPaint
            {
                Color = lineColor,
                StrokeWidth = 2,
                IsAntialias = true
            };
            var pointPaint = new SKPaint
            {
                Color = pointColor,
                StrokeWidth = 5,
                IsAntialias = true,
                Style = SKPaintStyle.Fill
            };

            // Definir dimensiones y escala
            float maxCount = reporteUsuarios.Max(r => r.CantidadUsuarios);
            float padding = 50;
            float xStep = (width - 2 * padding) / 29; // Divisiones en el eje X para 30 días
            float yScale = (height - 2 * padding) / maxCount;

            // Dibujar ejes y etiquetas
            for (int i = 0; i < reporteUsuarios.Count; i++)
            {
                var item = reporteUsuarios[i];
                float x = padding + i * xStep;
                float y = height - padding - (item.CantidadUsuarios * yScale);

                // Etiquetas del eje X como "Día 1", "Día 2", etc.
                canvas.DrawText((i + 1).ToString(), x, height - padding + 20, textPaint);

                // Puntos de datos y líneas
                if (i > 0)
                {
                    var prevItem = reporteUsuarios[i - 1];
                    float prevX = padding + (i - 1) * xStep;
                    float prevY = height - padding - (prevItem.CantidadUsuarios * yScale);
                    canvas.DrawLine(prevX, prevY, x, y, linePaint);
                }

                // Dibujar puntos de los datos
                canvas.DrawCircle(x, y, 4, pointPaint);
            }

            // Etiquetas de cantidad en el eje Y
            int yStep = (int)Math.Ceiling(maxCount / 5.0); // Ajusta el paso según la escala
            for (float yValue = 0; yValue <= maxCount; yValue += yStep)
            {
                float y = height - padding - (yValue * yScale);
                canvas.DrawText(yValue.ToString(), padding - 30, y, textPaint);
                canvas.DrawLine(padding - 5, y, padding, y, textPaint);
            }

            // Convierte el gráfico en una imagen PNG base64
            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            var imageBytes = data.ToArray();
            return Convert.ToBase64String(imageBytes);
        }
    }
}