using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria.interfaces;

namespace Zoologicos_libreria.implementaciones
{
    public class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<Auditorias>? Auditorias { get; set; }
        public DbSet<Zoologicos>? Zoologicos { get; set; }
        public DbSet<Especies>? Especies { get; set; }
        public DbSet<Enfermedades>? Enfermedades { get; set; }

        public DbSet<Visitantes>? Visitantes { get; set; }
        public DbSet<Habitats>? Habitats { get; set; }
        public DbSet<ZonasPublicas>? ZonasPublicas { get; set; }
        public DbSet<Inventarios>? Inventarios { get; set; }
        public DbSet<Jaulas>? Jaulas { get; set; }
        public DbSet<Alimentaciones>? Alimentaciones { get; set; }
        public DbSet<Empleados>? Empleados { get; set; }
        public DbSet<Veterinarios>? Veterinarios { get; set; }
        public DbSet<Gerentes>? Gerentes { get; set; }
        public DbSet<CuidadorAlimentaciones>? CuidadorAlimentaciones { get; set; }
        public DbSet<PersonalAseo>? PersonalAseo { get; set; }
        public DbSet<Entrenadores>? Entrenadores { get; set; }
        public DbSet<Diagnosticos>? Diagnosticos { get; set; }
        public DbSet<HistorialesMedicos>? HistorialesMedicos { get; set; }

        public DbSet<Vacunaciones>? Vacunaciones { get; set; }
        public DbSet<Alimentaciones>? Alimentaciones { get; set; }
        public DbSet<Entradas>? Entradas { get; set; }
        public DbSet<Areas>? Areas { get; set; }

        public DbSet<Mantenimientos>? Mantenimientos { get; set; }



        // 2. Interceptamos el guardado de datos
        public override int SaveChanges()
        {
            // Detectamos qué entidades han sido modificadas, agregadas o eliminadas
            var cambios = ChangeTracker.Entries()
                .Where(e => e.Entity.GetType().Name != "Auditorias" && // Evitamos un bucle infinito
                           (e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted))
                .ToList();

            foreach (var cambio in cambios)
            {
                // Creamos un registro de auditoría por cada cambio detectado
                var auditoria = new Auditorias
                {
                    Tabla = cambio.Entity.GetType().Name,
                    Accion = cambio.State.ToString(),
                    Fecha = DateTime.Now,
                    // Convertimos la entidad modificada en texto JSON para guardarla
                    Datos = JsonSerializer.Serialize(cambio.Entity)
                };

                // Lo agregamos a la base de datos
                this.Auditorias!.Add(auditoria);
            }

            // Finalmente, dejamos que EF Core guarde TODO (los cambios originales + las auditorías)
            return base.SaveChanges();
        }

    }
}
