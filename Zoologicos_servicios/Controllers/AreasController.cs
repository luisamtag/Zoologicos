using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AreasController : ControllerBase
    {

        private IAreasNegocio? IAreasNegocio;

        public AreasController()
        {
            this.IAreasNegocio = new AreasNegocio();
        }

        [HttpGet]
        public List<Areas> Listar()
        {

            if (this.IAreasNegocio == null)
                throw new Exception("no implementado");
            return this.IAreasNegocio.Listar();
        }

        [HttpPost]
        public Areas Guardar(Areas entidad)
        {
            if (this.IAreasNegocio == null)
                throw new Exception("no implementado");

            return this.IAreasNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Areas Modificar(Areas entidad)
        {
            if (this.IAreasNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IAreasNegocio.Modificar(entidad);
        }

        [HttpDelete]
        public bool Borrar(int id)
        {
            if (this.IAreasNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IAreasNegocio.Borrar(id);
        }
    }
}
