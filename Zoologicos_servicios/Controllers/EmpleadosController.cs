using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmpleadosController : ControllerBase
    {

        private IEmpleadosNegocio? IEmpleadosNegocio;

        public EmpleadosController()
        {
            this.IEmpleadosNegocio = new EmpleadosNegocio();
        }

        [HttpGet]
        public List<Empleados> Listar()
        {

            if (this.IEmpleadosNegocio == null)
                throw new Exception("no implementado");
            return this.IEmpleadosNegocio.Listar();
        }

        [HttpPost]
        public Empleados Guardar(Empleados entidad)
        {
            if (this.IEmpleadosNegocio == null)
                throw new Exception("no implementado");

            return this.IEmpleadosNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Empleados Modificar(Empleados entidad)
        {
            if (this.IEmpleadosNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IEmpleadosNegocio.Modificar(entidad);
        }

        [HttpDelete]
        public bool Borrar(int id)
        {
            if (this.IEmpleadosNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IEmpleadosNegocio.Borrar(id);
        }
    }
}
