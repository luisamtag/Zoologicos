using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HistorialesMedicosController : ControllerBase
    {

        private IHistorialesMedicosNegocio? IHistorialesMedicosNegocio;

        public HistorialesMedicosController()
        {
            this.IHistorialesMedicosNegocio = new HistorialesMedicosNegocio();
        }

        [HttpGet]
        public List<HistorialesMedicos> Listar()
        {

            if (this.IHistorialesMedicosNegocio == null)
                throw new Exception("no implementado");
            return this.IHistorialesMedicosNegocio.Listar();
        }

        [HttpPost]
        public HistorialesMedicos Guardar(HistorialesMedicos entidad)
        {
            if (this.IHistorialesMedicosNegocio == null)
                throw new Exception("no implementado");

            return this.IHistorialesMedicosNegocio.Guardar(entidad);
        }

        [HttpPut]
        public HistorialesMedicos Modificar(HistorialesMedicos entidad)
        {
            if (this.IHistorialesMedicosNegocio == null)
                throw new Exception("No implementado");

            // Usualmente se devuelve la entidad ya actualizada
            return this.IHistorialesMedicosNegocio.Modificar(entidad);
        }

        [HttpDelete]
        public bool Borrar(int id)
        {
            if (this.IHistorialesMedicosNegocio == null)
                throw new Exception("No implementado");

            // Retornamos un booleano para confirmar si se eliminó con éxito
            return this.IHistorialesMedicosNegocio.Borrar(id);
        }
    }
}
