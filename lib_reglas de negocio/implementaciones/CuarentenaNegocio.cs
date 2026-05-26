using Microsoft.EntityFrameworkCore;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria.interfaces;
using Zoologicos_libreria.Nucleo;

namespace Zoologicos_libreria.implementaciones
{
    public class CuarentenaNegocio : ICuarentenaNegocio
    {
        private IConexion? iConexion;

        public List<Cuarentena> Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            return this.iConexion.Cuarentenas!.ToList();
        }

        public Cuarentena Guardar(Cuarentena entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            // ✅Un animal no puede tener dos cuarentenas activas
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

        public Cuarentena Modificar(Cuarentena entidad)
        {
            if (entidad == null)
                throw new Exception("FaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("NoSeGuardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            // ✅ REGLA DE NEGOCIO 5: Validar estados permitidos
            var estadosValidos = new[] { "Activa", "Finalizada" };
            if (!estadosValidos.Contains(entidad.Estado))
                throw new Exception("Estado no válido. Use: Activa o Finalizada");

            // ✅ REGLA DE NEGOCIO 6: Si se finaliza, debe registrar FechaFin
            if (entidad.Estado == "Finalizada" && entidad.FechaFin == null)
                throw new Exception("Al finalizar una cuarentena debe registrar la fecha de fin");

            // ✅ REGLA DE NEGOCIO 7: FechaFin no puede ser anterior a FechaInicio
            if (entidad.FechaFin != null && entidad.FechaFin < entidad.FechaInicio)
                throw new Exception("La fecha de fin no puede ser anterior a la fecha de inicio");

            var entry = this.iConexion!.Entry<Cuarentena>(entidad);
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

            // ✅ REGLA DE NEGOCIO 8: No se puede borrar una cuarentena activa
            if (entidad.Estado == "Activa")
                throw new Exception("No se puede eliminar una cuarentena activa. Primero finalícela");

            this.iConexion.Cuarentenas!.Remove(entidad);
            this.iConexion.SaveChanges();
            return true;
        }
    }
}
