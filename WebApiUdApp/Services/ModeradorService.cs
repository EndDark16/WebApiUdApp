using System.Diagnostics;
using WebApiUdApp.Dtos;
using WebApiUdApp.Dtos.Request;
using WebApiUdApp.Dtos.Response;
using WebApiUdApp.Dtos.Response.ModeradorResponse;
using WebApiUdApp.Repositories;
namespace WebApiUdApp.Services
{
    public class ModeradorService
    {
        private readonly ModeradorRepositorio _moderadorRepositorio;

        public ModeradorService(ModeradorRepositorio moderadorRepositorio)
        {
            _moderadorRepositorio = moderadorRepositorio;
        }
        public List<ReporteDto> ObtenerPublicacionesReportadas()
        {
            try
            {
                List<ReporteDto> reportesDto = _moderadorRepositorio.ObtenerPublicacionesReportadasMod();
                return reportesDto;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
        public bool EliminarReporteMod(int idReporte)
        {
            try
            {
                return _moderadorRepositorio.EliminarReporteMod(idReporte);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
        public bool EliminarPublicacionReportada(int idReporte)
        {
            try
            {
                return _moderadorRepositorio.EliminarPublicacionMod(idReporte);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
    }
}
