using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CuarentenasController : ControllerBase
    {
        private ICuarentenasNegocio? ICuarentenasNegocio;

        public CuarentenasController()
        {
            this.ICuarentenasNegocio = new CuarentenasNegocio();
        }

        [HttpGet]
        public List<Cuarentenas> Listar()
        {
            if (this.ICuarentenasNegocio == null)
                throw new Exception("no implementado");

            return this.ICuarentenasNegocio.Listar();
        }

        [HttpPost]
        public Cuarentenas Guardar(Cuarentenas entidad)
        {
            if (this.ICuarentenasNegocio == null)
                throw new Exception("no implementado");

            return this.ICuarentenasNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Cuarentenas Modificar(Cuarentenas entidad)
        {
            if (this.ICuarentenasNegocio == null)
                throw new Exception("No implementado");

            return this.ICuarentenasNegocio.Modificar(entidad);
        }

        [HttpDelete]
        public bool Borrar(int id)
        {
            if (this.ICuarentenasNegocio == null)
                throw new Exception("No implementado");

            return this.ICuarentenasNegocio.Borrar(id);
        }
    }
}
