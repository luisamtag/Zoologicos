using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReproduccionController : ControllerBase
    {
        private IReproduccionNegocio? IReproduccionNegocio;

        public ReproduccionController()
        {
            this.IReproduccionNegocio = new ReproduccionNegocio();
        }

        [HttpGet]
        public List<Reproduccion> Listar()
        {
            if (this.IReproduccionNegocio == null)
                throw new Exception("no implementado");
            return this.IReproduccionNegocio.Listar();
        }

        [HttpPost]
        public Reproduccion Guardar(Reproduccion entidad)
        {
            if (this.IReproduccionNegocio == null)
                throw new Exception("no implementado");

            return this.IReproduccionNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Reproduccion Modificar(Reproduccion entidad)
        {
            if (this.IReproduccionNegocio == null)
                throw new Exception("No implementado");

            return this.IReproduccionNegocio.Modificar(entidad);
        }

        [HttpDelete]
        public bool Borrar(int id)
        {
            if (this.IReproduccionNegocio == null)
                throw new Exception("No implementado");

            return this.IReproduccionNegocio.Borrar(id);
        }
    }
}
