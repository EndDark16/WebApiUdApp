using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Rotativa;

namespace UdApp.Utilities
{
    public class ReportesRotativa
    {
        public static byte[] GeneratePDFReport(ControllerContext controllerContext, ReportePDFViewModel viewModel)
        {
            // Creamos la vista que se utilizará para generar el PDF
            var view = new ViewAsPdf("ReportePDF", viewModel);

            // Convertimos la vista a un array de bytes que representa el PDF
            byte[] pdfBytes = view.BuildFile(controllerContext);

            return pdfBytes;
        }

        public static List<Publicacion> X()
        {
            // Lógica para obtener las publicaciones con más likes
            // Puedes reemplazar esto con tu lógica real de acceso a datos
            // Esta es solo una representación de ejemplo
            List<Publicacion> publicacionesConLikes = new List<Publicacion>
            {
                new Publicacion { Id = 1, Titulo = "Publicación 1", Likes = 100 },
                new Publicacion { Id = 2, Titulo = "Publicación 2", Likes = 90 },
                new Publicacion { Id = 3, Titulo = "Publicación 3", Likes = 80 }
            };

            return publicacionesConLikes;
        }

        public static List<ReportePublicacion> ObtenerPublicacionesReportadas()
        {
            // Lógica para obtener las publicaciones reportadas y sus motivos
            // Esta es solo una representación de ejemplo
            List<ReportePublicacion> publicacionesReportadas = new List<ReportePublicacion>
            {
                new ReportePublicacion { IdReporte = 1, Titulo = "Publicación A", Motivo = "Contenido inapropiado" },
                new ReportePublicacion { IdReporte = 2, Titulo = "Publicación B", Motivo = "Violación de términos de uso" }
            };

            return publicacionesReportadas;
        }
    }

    // Clases de ejemplo para representar datos en el reporte
    public class Publicacion
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Likes { get; set; }
    }

    public class ReportePublicacion
    {
        public int IdReporte { get; set; }
        public string Titulo { get; set; }
        public string Motivo { get; set; }
    }
}
