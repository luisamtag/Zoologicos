using Microsoft.EntityFrameworkCore;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria.interfaces;
using Zoologicos_libreria.Nucleo;

namespace Zoologicos_libreria.implementaciones
{
    public class IngresosNegocio : IIngresosNegocio
    {
        private IConexion? iConexion;

        public List<Ingresos> Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            return this.iConexion.Ingresos!.ToList();
        }

        public Ingresos Guardar(Ingresos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            // ✅ REGLA DE NEGOCIO: Al aceptar el ingreso, crear automáticamente una Cuarentena
            if (entidad.Estado == "Aceptado")
            {
                var motivo = entidad.TipoIngreso == "Donación" ? "Animal Nuevo" : "Adaptación";
                var cuarentena = new Cuarentenas 
                {
                    AnimalId      = entidad.AnimalId,
                    VeterinarioId = 1,
                    FechaInicio   = DateTime.Now,
                    FechaFin      = null,
                    Motivo        = motivo,
                    Estado        = "Activa",
                    Observaciones = $"Cuarentena generada automáticamente por ingreso tipo '{entidad.TipoIngreso}'"
                };
                this.iConexion.Cuarentenas!.Add(cuarentena);
            }

            entidad.FechaIngreso = DateTime.Now;
            this.iConexion!.Ingresos!.Add(entidad);
            this.iConexion!.SaveChanges();
            return entidad;
        }

        public Ingresos Modificar(Ingresos entidad)
        {
            if (entidad == null)
                throw new Exception("FaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("NoSeGuardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            var entry = this.iConexion!.Entry<Ingresos>(entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return entidad;
        }

        public bool Borrar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            var entidad = this.iConexion.Ingresos!.FirstOrDefault(x => x.Id == id);

            if (entidad == null)
                throw new Exception("NoExisteRegistro");

            this.iConexion.Ingresos!.Remove(entidad);
            this.iConexion.SaveChanges();
            return true;
        }
    }
}
