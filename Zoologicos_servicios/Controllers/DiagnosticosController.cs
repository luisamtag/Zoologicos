using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DiagnosticosController : ControllerBase
    {

        private IDiagnosticosNegocio? IDiagnosticosNegocio;

        public DiagnosticosController()
        {
            this.IDiagnosticosNegocio = new DiagnosticosNegocio();
        }

        [HttpGet]
        public List<Diagnosticos> Listar()
        {

            if (this.IDiagnosticosNegocio == null)
                throw new Exception("no implementado");
            return this.IDiagnosticosNegocio.Listar();
        }

        [HttpPost]
        public Diagnosticos Guardar(Diagnosticos entidad)
        {
            if (this.IDiagnosticosNegocio == null)
                throw new Exception("no implementado");

            return this.IDiagnosticosNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Diagnosticos Modificar(Diagnosticos entidad)
        {
            if (this.IDiagnosticosNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IDiagnosticosNegocio.Modificar(entidad);
        }

        [HttpDelete]
        public bool Borrar(int id)
        {
            if (this.IDiagnosticosNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IDiagnosticosNegocio.Borrar(id);
        }
    }
}
