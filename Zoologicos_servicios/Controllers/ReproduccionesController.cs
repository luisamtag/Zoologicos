using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReproduccionesController : ControllerBase
    {
        private IReproduccionesNegocio? IReproduccionesNegocio;

        public ReproduccionesController()
        {
            this.IReproduccionesNegocio = new ReproduccionesNegocio();
        }

        [HttpGet]
        public List<Reproducciones> Listar()
        {
            if (this.IReproduccionesNegocio == null)
                throw new Exception("no implementado");
            return this.IReproduccionesNegocio.Listar();
        }

        [HttpPost]
        public Reproducciones Guardar(Reproducciones entidad)
        {
            if (this.IReproduccionesNegocio == null)
                throw new Exception("no implementado");

            return this.IReproduccionesNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Reproducciones Modificar(Reproducciones entidad)
        {
            if (this.IReproduccionesNegocio == null)
                throw new Exception("No implementado");

            return this.IReproduccionesNegocio.Modificar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Borrar([FromRoute] int id)
        {
            if (this.IReproduccionesNegocio == null)
                throw new Exception("No implementado");

            return this.IReproduccionesNegocio.Borrar(id);
        }
    }
}
