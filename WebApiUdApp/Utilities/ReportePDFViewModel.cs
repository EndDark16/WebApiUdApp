using System.Collections.Generic;

namespace WebApiUdApp.Utilities
{
    public class ReportePDFViewModel
    {
        public string TipoReporte { get; set; }
        public List<PublicacionViewModel> PublicacionesConLikes { get; set; }
        public List<ReportePublicacionViewModel> PublicacionesReportadas { get; set; }
    }

    public class PublicacionViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Likes { get; set; }
    }

    public class ReportePublicacionViewModel
    {
        public int IdReporte { get; set; }
        public string Titulo { get; set; }
        public string Motivo { get; set; }
    }
}
