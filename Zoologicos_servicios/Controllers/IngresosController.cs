using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class IngresosController : ControllerBase
    {
        private IIngresosNegocio? IIngresosNegocio;

        public IngresosController()
        {
            this.IIngresosNegocio = new IngresosNegocio();
        }

        [HttpGet]
        public List<Ingresos> Listar()
        {
            if (this.IIngresosNegocio == null)
                throw new Exception("no implementado");

            return this.IIngresosNegocio.Listar();
        }

        [HttpPost]
        public Ingresos Guardar(Ingresos entidad)
        {
            if (this.IIngresosNegocio == null)
                throw new Exception("no implementado");

            return this.IIngresosNegocio.Guardar(entidad);
        }

        [HttpPost]
        public Ingresos Modificar(Ingresos entidad)
        {
            if (this.IIngresosNegocio == null)
                throw new Exception("No implementado");

            return this.IIngresosNegocio.Modificar(entidad);
        }

        [HttpPost]
        public bool Borrar([FromRoute] int id)
        {
            if (this.IIngresosNegocio == null)
                throw new Exception("No implementado");

            return this.IIngresosNegocio.Borrar(id);
        }
    }
}
