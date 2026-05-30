using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CuidadorAnimalesController : ControllerBase
    {

        private ICuidadorAnimalesNegocio? ICuidadorAnimalesNegocio;

        public CuidadorAnimalesController()
        {
            this.ICuidadorAnimalesNegocio = new CuidadorAnimalesNegocio();
        }

        [HttpGet]
        public List<CuidadorAnimales> Listar()
        {

            if (this.ICuidadorAnimalesNegocio == null)
                throw new Exception("no implementado");
            return this.ICuidadorAnimalesNegocio.Listar();
        }

        [HttpPost]
        public CuidadorAnimales Guardar(CuidadorAnimales entidad)
        {
            if (this.ICuidadorAnimalesNegocio == null)
                throw new Exception("no implementado");

            return this.ICuidadorAnimalesNegocio.Guardar(entidad);
        }

        [HttpPut]
        public CuidadorAnimales Modificar(CuidadorAnimales entidad)
        {
            if (this.ICuidadorAnimalesNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.ICuidadorAnimalesNegocio.Modificar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Borrar([FromRoute] int id)
        {
            if (this.ICuidadorAnimalesNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.ICuidadorAnimalesNegocio.Borrar(id);
        }
    }
}
