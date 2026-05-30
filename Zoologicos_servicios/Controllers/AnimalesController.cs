using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AnimalesController : ControllerBase
    {

        private IAnimalesNegocio? IAnimalesNegocio;

        public AnimalesController ()
        {
            this.IAnimalesNegocio = new AnimalesNegocio();
        }

        [HttpGet]
        public List<Animales> Listar()
        {

            if (this.IAnimalesNegocio == null)
                throw new Exception("no implementado");
            return this.IAnimalesNegocio.Listar();
        }

        [HttpPost]
        public Animales Guardar(Animales entidad)
        {
            if (this.IAnimalesNegocio == null)
                throw new Exception("no implementado");

            return this.IAnimalesNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Animales Modificar(Animales entidad)
        {
            if (this.IAnimalesNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IAnimalesNegocio.Modificar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Borrar([FromRoute] int id)
        {
            if (this.IAnimalesNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IAnimalesNegocio.Borrar(id);
        }
    }
}
