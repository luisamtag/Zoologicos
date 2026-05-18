using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EntradasController : ControllerBase
    {

        private IEntradasNegocio? IEntradasNegocio;

        public EntradasController()
        {
            this.IEntradasNegocio = new EntradasNegocio();
        }

        [HttpGet]
        public List<Entradas> Listar()
        {

            if (this.IEntradasNegocio == null)
                throw new Exception("no implementado");
            return this.IEntradasNegocio.Listar();
        }

        [HttpPost]
        public Entradas Guardar(Entradas entidad)
        {
            if (this.IEntradasNegocio == null)
                throw new Exception("no implementado");

            return this.IEntradasNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Entradas Modificar(Entradas entidad)
        {
            if (this.IEntradasNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IEntradasNegocio.Modificar(entidad);
        }

        [HttpDelete]
        public bool Borrar(int id)
        {
            if (this.IEntradasNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IEntradasNegocio.Borrar(id);
        }
    }
}
