using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EntrenadoresController : ControllerBase
    {

        private IEntrenadoresNegocio? IEntrenadoresNegocio;

        public EntrenadoresController()
        {
            this.IEntrenadoresNegocio = new EntrenadoresNegocio();
        }

        [HttpGet]
        public List<Entrenadores> Listar()
        {

            if (this.IEntrenadoresNegocio == null)
                throw new Exception("no implementado");
            return this.IEntrenadoresNegocio.Listar();
        }

        [HttpPost]
        public Entrenadores Guardar(Entrenadores entidad)
        {
            if (this.IEntrenadoresNegocio == null)
                throw new Exception("no implementado");

            return this.IEntrenadoresNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Entrenadores Modificar(Entrenadores entidad)
        {
            if (this.IEntrenadoresNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IEntrenadoresNegocio.Modificar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Borrar([FromRoute] int id)
        {
            if (this.IEntrenadoresNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IEntrenadoresNegocio.Borrar(id);
        }
    }
}
