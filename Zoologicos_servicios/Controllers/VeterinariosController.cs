using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class VeterinariosController : ControllerBase
    {

        private IVeterinariosNegocio? IVeterinariosNegocio;

        public VeterinariosController()
        {
            this.IVeterinariosNegocio = new VeterinariosNegocio();
        }

        [HttpGet]
        public List<Veterinarios> Listar()
        {

            if (this.IVeterinariosNegocio == null)
                throw new Exception("no implementado");
            return this.IVeterinariosNegocio.Listar();
        }

        [HttpPost]
        public Veterinarios Guardar(Veterinarios entidad)
        {
            if (this.IVeterinariosNegocio == null)
                throw new Exception("no implementado");

            return this.IVeterinariosNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Veterinarios Modificar(Veterinarios entidad)
        {
            if (this.IVeterinariosNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IVeterinariosNegocio.Modificar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Borrar([FromRoute] int id)
        {
            if (this.IVeterinariosNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IVeterinariosNegocio.Borrar(id);
        }
    }
}
