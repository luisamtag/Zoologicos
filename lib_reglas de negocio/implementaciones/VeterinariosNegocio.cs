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
    public class VeterinariosNegocio : IVeterinariosNegocio
    {
        private IConexion? iConexion;

        public List<Veterinarios> Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            return this.iConexion.Veterinarios!.ToList();
        }

        public Veterinarios Guardar(Veterinarios entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("ya se guardo");

            //Los años de experiencia deben ser mayores a 0
            if (entidad.AñosExperiencia <= 0)
                throw new Exception("Los años de experiencia del veterinario deben ser mayores a 0");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.iConexion!.Veterinarios!.Add(entidad);
            this.iConexion!.SaveChanges();
            return entidad;
        }

        public Veterinarios Modificar(Veterinarios entidad)
        {


            if (entidad == null)
                throw new Exception("FaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("NoSeGuardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            var entry = this.iConexion!.Entry<Veterinarios>(entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return entidad;
        }


        public bool Borrar(int id)


        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            // Primero buscamos el registro para poder borrarlo
            var entidad = this.iConexion.Veterinarios!.FirstOrDefault(x => x.Id == id);

            if (entidad == null)
            {
                throw new Exception("NoExisteRegistro");
            }
            this.iConexion.Veterinarios!.Remove(entidad);
            this.iConexion.SaveChanges();
            return true;

        }

    }
}
