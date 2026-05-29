using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AlimentacionesController : ControllerBase
    {

        private IAlimentacionesNegocio? IAlimentacionesNegocio;

        public AlimentacionesController()
        {
            this.IAlimentacionesNegocio = new AlimentacionesNegocio();
        }

        [HttpGet]
        public List<Alimentaciones> Listar()
        {

            if (this.IAlimentacionesNegocio == null)
                throw new Exception("no implementado");
            return this.IAlimentacionesNegocio.Listar();
        }

        [HttpPost]
        public Alimentaciones Guardar(Alimentaciones entidad)
        {
            if (this.IAlimentacionesNegocio == null)
                throw new Exception("no implementado");

            return this.IAlimentacionesNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Alimentaciones Modificar(Alimentaciones entidad)
        {
            if (this.IAlimentacionesNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IAlimentacionesNegocio.Modificar(entidad);
        }

        [HttpDelete("{id}")] // 🟢 Ahora el ID es parte obligatoria de la ruta de navegación
        public bool Borrar([FromRoute] int id) // 🟢 Forzamos a leerlo desde la ruta
        {
            if (this.IAlimentacionesNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IAlimentacionesNegocio.Borrar(id);
        }
    }
}
