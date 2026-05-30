using Microsoft.EntityFrameworkCore;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria.interfaces;
using Zoologicos_libreria.Nucleo;

namespace Zoologicos_libreria.implementaciones
{
    public class CuarentenasNegocio : ICuarentenasNegocio
    {
        private IConexion? iConexion;

        public List<Cuarentenas> Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            return this.iConexion.Cuarentenas!.ToList();
        }

        public Cuarentenas Guardar(Cuarentenas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            //Un animal no puede tener dos cuarentenas activas
            var cuarentenaActiva = this.iConexion.Cuarentenas!.Any(c =>
                c.AnimalId == entidad.AnimalId &&
                c.Estado == "Activa");

            if (cuarentenaActiva)
                throw new Exception("El animal ya tiene una cuarentena activa. Finalícela antes de crear una nueva");

            entidad.Estado = "Activa";
            entidad.FechaFin = null;
            entidad.FechaInicio = DateTime.Now;

            this.iConexion!.Cuarentenas!.Add(entidad);
            this.iConexion!.SaveChanges();
            return entidad;
        }

        public Cuarentenas Modificar(Cuarentenas entidad)
        {
            if (entidad == null)
                throw new Exception("FaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("NoSeGuardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            //Al finalizar, la FechaFin no puede ser mayor a la fecha actual
            if (entidad.Estado == "Finalizada" && entidad.FechaFin > DateTime.Now)
                throw new Exception("La fecha de fin no puede ser mayor a la fecha actual");

            var entry = this.iConexion!.Entry<Cuarentenas>(entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return entidad;
        }

        public bool Borrar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            var entidad = this.iConexion.Cuarentenas!.FirstOrDefault(x => x.Id == id);

            if (entidad == null)
                throw new Exception("NoExisteRegistro");

            // No se puede borrar una cuarentena activa
            if (entidad.Estado == "Activa")
                throw new Exception("No se puede eliminar una cuarentena activa. Primero finalícela");

            this.iConexion.Cuarentenas!.Remove(entidad);
            this.iConexion.SaveChanges();
            return true;
        }
    }
}
