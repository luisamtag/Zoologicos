using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HabitatsController : ControllerBase
    {

        private IHabitatsNegocio? IHabitatsNegocio;

        public HabitatsController()
        {
            this.IHabitatsNegocio = new HabitatsNegocio();
        }

        [HttpGet]
        public List<Habitats> Listar()
        {

            if (this.IHabitatsNegocio == null)
                throw new Exception("no implementado");
            return this.IHabitatsNegocio.Listar();
        }

        [HttpPost]
        public Habitats Guardar(Habitats entidad)
        {
            if (this.IHabitatsNegocio == null)
                throw new Exception("no implementado");

            return this.IHabitatsNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Habitats Modificar(Habitats entidad)
        {
            if (this.IHabitatsNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IHabitatsNegocio.Modificar(entidad);
        }

        [HttpDelete]
        public bool Borrar(int id)
        {
            if (this.IHabitatsNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IHabitatsNegocio.Borrar(id);
        }
    }
}
