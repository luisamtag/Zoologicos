using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class VacunacionesController : ControllerBase
    {

        private IVacunacionesNegocio? IVacunacionesNegocio;

        public VacunacionesController()
        {
            this.IVacunacionesNegocio = new VacunacionesNegocio();
        }

        [HttpGet]
        public List<Vacunaciones> Listar()
        {

            if (this.IVacunacionesNegocio == null)
                throw new Exception("no implementado");
            return this.IVacunacionesNegocio.Listar();
        }

        [HttpPost]
        public Vacunaciones Guardar(Vacunaciones entidad)
        {
            if (this.IVacunacionesNegocio == null)
                throw new Exception("no implementado");

            return this.IVacunacionesNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Vacunaciones Modificar(Vacunaciones entidad)
        {
            if (this.IVacunacionesNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IVacunacionesNegocio.Modificar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Borrar([FromRoute] int id)
        {
            if (this.IVacunacionesNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IVacunacionesNegocio.Borrar(id);
        }
    }
}
