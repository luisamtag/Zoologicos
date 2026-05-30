using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PersonalAseoController : ControllerBase
    {

        private IPersonalAseoNegocio? IPersonalAseoNegocio;

        public PersonalAseoController()
        {
            this.IPersonalAseoNegocio = new PersonalAseoNegocio();
        }

        [HttpGet]
        public List<PersonalAseo> Listar()
        {

            if (this.IPersonalAseoNegocio == null)
                throw new Exception("no implementado");
            return this.IPersonalAseoNegocio.Listar();
        }

        [HttpPost]
        public PersonalAseo Guardar(PersonalAseo entidad)
        {
            if (this.IPersonalAseoNegocio == null)
                throw new Exception("no implementado");

            return this.IPersonalAseoNegocio.Guardar(entidad);
        }

        [HttpPut]
        public PersonalAseo Modificar(PersonalAseo entidad)
        {
            if (this.IPersonalAseoNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IPersonalAseoNegocio.Modificar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Borrar([FromRoute] int id)
        {
            if (this.IPersonalAseoNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IPersonalAseoNegocio.Borrar(id);
        }
    }
}
