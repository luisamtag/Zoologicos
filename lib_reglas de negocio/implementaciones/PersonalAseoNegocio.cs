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
    public class PersonalAseoNegocio : IPersonalAseoNegocio
    {
        private IConexion? iConexion;

        public List<PersonalAseo> Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            return this.iConexion.PersonalAseo!.ToList();
        }

        public PersonalAseo Guardar(PersonalAseo entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("ya se guardo");

            // El turno solo puede ser Día, Noche o Mixto
            var turnosValidos = new[] { "Día", "Noche", "Mixto" };
            if (!turnosValidos.Contains(entidad.Turno))
                throw new Exception("Turno no válido. Use: Día, Noche o Mixto");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.iConexion!.PersonalAseo!.Add(entidad);
            this.iConexion!.SaveChanges();
            return entidad;
        }

        public PersonalAseo Modificar(PersonalAseo entidad)
        {


            if (entidad == null)
                throw new Exception("FaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("NoSeGuardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            var entry = this.iConexion!.Entry<PersonalAseo>(entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return entidad;
        }


        public bool Borrar(int id)


        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            // Primero buscamos el registro para poder borrarlo
            var entidad = this.iConexion.PersonalAseo!.FirstOrDefault(x => x.Id == id);

            if (entidad == null)
            {
                throw new Exception("NoExisteRegistro");
            }
            this.iConexion.PersonalAseo!.Remove(entidad);
            this.iConexion.SaveChanges();
            return true;

        }

    }
}
