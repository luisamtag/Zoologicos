using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GerentesController : ControllerBase
    {

        private IGerentesNegocio? IGerentesNegocio;

        public GerentesController()
        {
            this.IGerentesNegocio = new GerentesNegocio();
        }

        [HttpGet]
        public List<Gerentes> Listar()
        {

            if (this.IGerentesNegocio == null)
                throw new Exception("no implementado");
            return this.IGerentesNegocio.Listar();
        }

        [HttpPost]
        public Gerentes Guardar(Gerentes entidad)
        {
            if (this.IGerentesNegocio == null)
                throw new Exception("no implementado");

            return this.IGerentesNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Gerentes Modificar(Gerentes entidad)
        {
            if (this.IGerentesNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IGerentesNegocio.Modificar(entidad);
        }

        [HttpDelete]
        public bool Borrar(int id)
        {
            if (this.IGerentesNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IGerentesNegocio.Borrar(id);
        }
    }
}
