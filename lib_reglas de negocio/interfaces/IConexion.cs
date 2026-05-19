using Zoologicos_libreria.entidades;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Zoologicos_libreria.interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }

         DbSet<Alimentaciones>? Alimentaciones { get; set; }
         DbSet<Zoologicos>? Zoologicos { get; set; }
         DbSet<Especies>? Especies { get; set; }
         DbSet<Enfermedades>? Enfermedades { get; set; }

         DbSet<Visitantes>? Visitantes { get; set; }
         DbSet<Habitats>? Habitats { get; set; }
         DbSet<ZonasPublicas>? ZonasPublicas { get; set; }
         DbSet<Inventarios>? Inventarios { get; set; }
         DbSet<Jaulas>? Jaulas { get; set; }
         //DbSet<Alimentaciones>? Alimentaciones { get; set; }
         DbSet<Empleados>? Empleados { get; set; }
         DbSet<Veterinarios>? Veterinarios { get; set; }
         DbSet<Gerentes>? Gerentes { get; set; }
         DbSet<CuidadorAnimales>? CuidadorAnimales { get; set; }
         DbSet<PersonalAseo>? PersonalAseo { get; set; }
         DbSet<Entrenadores>? Entrenadores { get; set; }
         DbSet<Diagnosticos>? Diagnosticos { get; set; }
         DbSet<HistorialesMedicos>? HistorialesMedicos { get; set; }
         DbSet<Vacunaciones>? Vacunaciones { get; set; }
         DbSet<Animales>? Animales { get; set; }
         DbSet<Entradas>? Entradas { get; set; }
         DbSet<Areas>? Areas { get; set; }
         DbSet<Mantenimientos>? Mantenimientos { get; set; }

        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}