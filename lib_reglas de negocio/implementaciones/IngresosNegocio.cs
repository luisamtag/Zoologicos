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

            // ✅ REGLA DE NEGOCIO: Validar TipoIngreso permitido
            var tiposValidos = new[] { "Donación", "Rescate", "Traslado" };
            if (!tiposValidos.Contains(entidad.TipoIngreso))
                throw new Exception("Tipo de ingreso no válido. Use: Donación, Rescate o Traslado");

            // ✅ REGLA DE NEGOCIO: Validar que el animal existe
            var animal = this.iConexion.Animales!.FirstOrDefault(a => a.Id == entidad.AnimalId);
            if (animal == null)
                throw new Exception("El animal especificado no existe");

            // ✅ REGLA DE NEGOCIO: Al guardar con estado Aceptado,
            // crear automáticamente una Cuarentena para el animal
            if (entidad.Estado == "Aceptado")
            {
                // Determinar motivo según tipo de ingreso
                var motivo = entidad.TipoIngreso == "Donación" ? "Animal Nuevo" : "Adaptación";

                // Validar que no tenga ya una cuarentena activa
                var cuarentenaActiva = this.iConexion.Cuarentenas!.Any(c =>
                    c.AnimalId == entidad.AnimalId &&
                    c.Estado == "Activa");

                if (!cuarentenaActiva)
                {
                    var cuarentena = new Cuarentena
                    {
                        AnimalId = entidad.AnimalId,
                        VeterinarioId = 1, // Asignar veterinario por defecto o ajustar según lógica
                        FechaInicio = DateTime.Now,
                        FechaFin = null,
                        Motivo = motivo,
                        Estado = "Activa",
                        Observaciones = $"Cuarentena generada automáticamente por ingreso tipo '{entidad.TipoIngreso}'"
                    };
                    this.iConexion.Cuarentenas!.Add(cuarentena);
                }
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

            // ✅ REGLA DE NEGOCIO: Si se cambia a Aceptado y no tiene cuarentena activa, crearla
            if (entidad.Estado == "Aceptado")
            {
                var cuarentenaActiva = this.iConexion.Cuarentenas!.Any(c =>
                    c.AnimalId == entidad.AnimalId &&
                    c.Estado == "Activa");

                if (!cuarentenaActiva)
                {
                    var motivo = entidad.TipoIngreso == "Donación" ? "Animal Nuevo" : "Adaptación";
                    var cuarentena = new Cuarentena
                    {
                        AnimalId = entidad.AnimalId,
                        VeterinarioId = 1,
                        FechaInicio = DateTime.Now,
                        FechaFin = null,
                        Motivo = motivo,
                        Estado = "Activa",
                        Observaciones = $"Cuarentena generada automáticamente al aceptar ingreso tipo '{entidad.TipoIngreso}'"
                    };
                    this.iConexion.Cuarentenas!.Add(cuarentena);
                }
            }

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

            // ✅ REGLA DE NEGOCIO: No borrar un ingreso Aceptado
            if (entidad.Estado == "Aceptado")
                throw new Exception("No se puede eliminar un ingreso ya aceptado");

            this.iConexion.Ingresos!.Remove(entidad);
            this.iConexion.SaveChanges();
            return true;
        }
    }
}
