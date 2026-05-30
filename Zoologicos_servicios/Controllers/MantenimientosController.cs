using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MantenimientosController : ControllerBase
    {

        private IMantenimientosNegocio? IMantenimientosNegocio;

        public MantenimientosController()
        {
            this.IMantenimientosNegocio = new MantenimientosNegocio();
        }

        [HttpGet]
        public List<Mantenimientos> Listar()
        {

            if (this.IMantenimientosNegocio == null)
                throw new Exception("no implementado");
            return this.IMantenimientosNegocio.Listar();
        }

        [HttpPost]
        public Mantenimientos Guardar(Mantenimientos entidad)
        {
            if (this.IMantenimientosNegocio == null)
                throw new Exception("no implementado");

            return this.IMantenimientosNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Mantenimientos Modificar(Mantenimientos entidad)
        {
            if (this.IMantenimientosNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IMantenimientosNegocio.Modificar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Borrar([FromRoute] int id)
        {
            if (this.IMantenimientosNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IMantenimientosNegocio.Borrar(id);
        }
    }
}
