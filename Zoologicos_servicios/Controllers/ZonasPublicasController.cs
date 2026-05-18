using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ZonasPublicasController : ControllerBase
    {

        private IZonasPublicasNegocio? IZonasPublicasNegocio;

        public ZonasPublicasController()
        {
            this.IZonasPublicasNegocio = new ZonasPublicasNegocio();
        }

        [HttpGet]
        public List<ZonasPublicas> Listar()
        {

            if (this.IZonasPublicasNegocio == null)
                throw new Exception("no implementado");
            return this.IZonasPublicasNegocio.Listar();
        }

        [HttpPost]
        public ZonasPublicas Guardar(ZonasPublicas entidad)
        {
            if (this.IZonasPublicasNegocio == null)
                throw new Exception("no implementado");

            return this.IZonasPublicasNegocio.Guardar(entidad);
        }

        [HttpPut]
        public ZonasPublicas Modificar(ZonasPublicas entidad)
        {
            if (this.IZonasPublicasNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IZonasPublicasNegocio.Modificar(entidad);
        }

        [HttpDelete]
        public bool Borrar(int id)
        {
            if (this.IZonasPublicasNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IZonasPublicasNegocio.Borrar(id);
        }
    }
}
