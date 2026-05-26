using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria.interfaces;
using Zoologicos_libreria.Nucleo;

namespace Zoologicos_libreria.implementaciones
{
    public class VacunacionesNegocio : IVacunacionesNegocio
    {
        private IConexion? iConexion;

        public List<Vacunaciones> Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            return this.iConexion.Vacunaciones!.ToList();
        }

        public Vacunaciones Guardar(Vacunaciones entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            //El animal no puede tener la misma vacuna activa sin vencer
            var vacunaActiva = this.iConexion.Vacunaciones!.Any(v =>
                v.AnimalId == entidad.AnimalId &&
                v.NombreVacuna == entidad.NombreVacuna &&
                (v.FechaProximaDosis == null || v.FechaProximaDosis > DateTime.Now));
            if (vacunaActiva)
                throw new Exception($"El animal ya tiene la vacuna '{entidad.NombreVacuna}' activa. Espere a que venza la dosis actual");

            this.iConexion!.Vacunaciones!.Add(entidad);
            this.iConexion!.SaveChanges();
            return entidad;
        }

        public Vacunaciones Modificar(Vacunaciones entidad)
        {


            if (entidad == null)
                throw new Exception("FaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("NoSeGuardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            var entry = this.iConexion!.Entry<Vacunaciones>(entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return entidad;
        }


        public bool Borrar(int id)


        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            // Primero buscamos el registro para poder borrarlo
            var entidad = this.iConexion.Vacunaciones!.FirstOrDefault(x => x.Id == id);

            if (entidad == null)
            {
                throw new Exception("NoExisteRegistro");
            }
            this.iConexion.Vacunaciones!.Remove(entidad);
            this.iConexion.SaveChanges();
            return true;

        }

    }
}
