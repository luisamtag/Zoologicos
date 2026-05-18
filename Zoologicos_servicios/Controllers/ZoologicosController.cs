using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ZoologicosController : ControllerBase
    {

        private IZoologicosNegocio? IZoologicosNegocio;

        public ZoologicosController()
        {
            this.IZoologicosNegocio = new ZoologicosNegocio();
        }

        [HttpGet]
        public List<Zoologicos> Listar()
        {

            if (this.IZoologicosNegocio == null)
                throw new Exception("no implementado");
            return this.IZoologicosNegocio.Listar();
        }

        [HttpPost]
        public Zoologicos Guardar(Zoologicos entidad)
        {
            if (this.IZoologicosNegocio == null)
                throw new Exception("no implementado");

            return this.IZoologicosNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Zoologicos Modificar(Zoologicos entidad)
        {
            if (this.IZoologicosNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IZoologicosNegocio.Modificar(entidad);
        }

        [HttpDelete]
        public bool Borrar(int id)
        {
            if (this.IZoologicosNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IZoologicosNegocio.Borrar(id);
        }
    }
}
