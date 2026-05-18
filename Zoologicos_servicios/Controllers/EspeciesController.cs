using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EspeciesController : ControllerBase
    {

        private IEspeciesNegocio? IEspeciesNegocio;

        public EspeciesController()
        {
            this.IEspeciesNegocio = new EspeciesNegocio();
        }

        [HttpGet]
        public List<Especies> Listar()
        {

            if (this.IEspeciesNegocio == null)
                throw new Exception("no implementado");
            return this.IEspeciesNegocio.Listar();
        }

        [HttpPost]
        public Especies Guardar(Especies entidad)
        {
            if (this.IEspeciesNegocio == null)
                throw new Exception("no implementado");

            return this.IEspeciesNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Especies Modificar(Especies entidad)
        {
            if (this.IEspeciesNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IEspeciesNegocio.Modificar(entidad);
        }

        [HttpDelete]
        public bool Borrar(int id)
        {
            if (this.IEspeciesNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IEspeciesNegocio.Borrar(id);
        }
    }
}
