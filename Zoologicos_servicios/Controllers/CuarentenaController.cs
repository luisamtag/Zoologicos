using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Zoologicos_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CuarentenaController : ControllerBase
    {
        private ICuarentenaNegocio? ICuarentenaNegocio;

        public CuarentenaController()
        {
            this.ICuarentenaNegocio = new CuarentenaNegocio();
        }

        [HttpGet]
        public List<Cuarentena> Listar()
        {
            if (this.ICuarentenaNegocio == null)
                throw new Exception("no implementado");

            return this.ICuarentenaNegocio.Listar();
        }

        [HttpPost]
        public Cuarentena Guardar(Cuarentena entidad)
        {
            if (this.ICuarentenaNegocio == null)
                throw new Exception("no implementado");

            return this.ICuarentenaNegocio.Guardar(entidad);
        }

        [HttpPut]
        public Cuarentena Modificar(Cuarentena entidad)
        {
            if (this.ICuarentenaNegocio == null)
                throw new Exception("No implementado");

            return this.ICuarentenaNegocio.Modificar(entidad);
        }

        [HttpDelete]
        public bool Borrar(int id)
        {
            if (this.ICuarentenaNegocio == null)
                throw new Exception("No implementado");

            return this.ICuarentenaNegocio.Borrar(id);
        }
    }
}
