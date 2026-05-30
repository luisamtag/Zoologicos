using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuditoriasController : ControllerBase
    {
        private IAuditoriasNegocio? IAuditoriasNegocio;

        public AuditoriasController()
        {
            this.IAuditoriasNegocio = new AuditoriasNegocio();
        }

        [HttpGet]
        public List<Auditorias> Listar()
        {
            if (this.IAuditoriasNegocio == null)
                throw new Exception("no implementado");

            return this.IAuditoriasNegocio.Listar();
        }

        [HttpGet]
        public List<Auditorias> ListarPorTabla(string tabla)
        {
            if (this.IAuditoriasNegocio == null)
                throw new Exception("no implementado");

            return this.IAuditoriasNegocio.ListarPorTabla(tabla);
        }
    }
}
