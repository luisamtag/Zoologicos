using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class JaulasController : ControllerBase
    {

        private IJaulasNegocio? IJaulasNegocio;

        public JaulasController()
        {
            this.IJaulasNegocio = new JaulasNegocio();
        }

        [HttpGet]
        public List<Jaulas> Listar()
        {

            if (this.IJaulasNegocio == null)
                throw new Exception("no implementado");
            return this.IJaulasNegocio.Listar();
        }

        [HttpPost]
        public Jaulas Guardar(Jaulas entidad)
        {
            if (this.IJaulasNegocio == null)
                throw new Exception("no implementado");

            return this.IJaulasNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Jaulas Modificar(Jaulas entidad)
        {
            if (this.IJaulasNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IJaulasNegocio.Modificar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Borrar([FromRoute] int id)
        {
            if (this.IJaulasNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IJaulasNegocio.Borrar(id);
        }
    }
}
