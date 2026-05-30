using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class VisitantesController : ControllerBase
    {

        private IVisitantesNegocio? IVisitantesNegocio;

        public VisitantesController()
        {
            this.IVisitantesNegocio = new VisitantesNegocio();
        }

        [HttpGet]
        public List<Visitantes> Listar()
        {

            if (this.IVisitantesNegocio == null)
                throw new Exception("no implementado");
            return this.IVisitantesNegocio.Listar();
        }

        [HttpPost]
        public Visitantes Guardar(Visitantes entidad)
        {
            if (this.IVisitantesNegocio == null)
                throw new Exception("no implementado");

            return this.IVisitantesNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Visitantes Modificar(Visitantes entidad)
        {
            if (this.IVisitantesNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IVisitantesNegocio.Modificar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Borrar([FromRoute] int id)
        {
            if (this.IVisitantesNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IVisitantesNegocio.Borrar(id);
        }
    }
}
