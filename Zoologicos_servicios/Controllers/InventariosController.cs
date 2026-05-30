using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class InventariosController : ControllerBase
    {

        private IInventariosNegocio? IInventariosNegocio;

        public InventariosController()
        {
            this.IInventariosNegocio = new InventariosNegocio();
        }

        [HttpGet]
        public List<Inventarios> Listar()
        {

            if (this.IInventariosNegocio == null)
                throw new Exception("no implementado");
            return this.IInventariosNegocio.Listar();
        }

        [HttpPost]
        public Inventarios Guardar(Inventarios entidad)
        {
            if (this.IInventariosNegocio == null)
                throw new Exception("no implementado");

            return this.IInventariosNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Inventarios Modificar(Inventarios entidad)
        {
            if (this.IInventariosNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IInventariosNegocio.Modificar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Borrar([FromRoute] int id)
        {
            if (this.IInventariosNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IInventariosNegocio.Borrar(id);
        }
    }
}
