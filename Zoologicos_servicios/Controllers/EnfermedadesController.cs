using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EnfermedadesController : ControllerBase
    {

        private IEnfermedadesNegocio? IEnfermedadesNegocio;

        public EnfermedadesController()
        {
            this.IEnfermedadesNegocio = new EnfermedadesNegocio();
        }

        [HttpGet]
        public List<Enfermedades> Listar()
        {

            if (this.IEnfermedadesNegocio == null)
                throw new Exception("no implementado");
            return this.IEnfermedadesNegocio.Listar();
        }

        [HttpPost]
        public Enfermedades Guardar(Enfermedades entidad)
        {
            if (this.IEnfermedadesNegocio == null)
                throw new Exception("no implementado");

            return this.IEnfermedadesNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Enfermedades Modificar(Enfermedades entidad)
        {
            if (this.IEnfermedadesNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IEnfermedadesNegocio.Modificar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Borrar([FromRoute] int id)
        {
            if (this.IEnfermedadesNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IEnfermedadesNegocio.Borrar(id);
        }
    }
}
