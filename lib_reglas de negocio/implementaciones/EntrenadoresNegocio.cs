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
    public class EntrenadoresNegocio : IEntrenadoresNegocio
    {
        private IConexion? iConexion;

        public List<Entrenadores> Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            return this.iConexion.Entrenadores!.ToList();
        }

        public Entrenadores Guardar(Entrenadores entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            //El empleado debe existir antes de asignarle rol de Entrenador
            var empleado = this.iConexion.Empleados!.FirstOrDefault(e => e.Id == entidad.Id);
            if (empleado == null)
                throw new Exception("El empleado no existe. Regístrelo primero en Empleados antes de asignarle el rol de Entrenador");

            this.iConexion!.Entrenadores!.Add(entidad);
            this.iConexion!.SaveChanges();
            return entidad;
        }

        public Entrenadores Modificar(Entrenadores entidad)
        {


            if (entidad == null)
                throw new Exception("FaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("NoSeGuardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            var entry = this.iConexion!.Entry<Entrenadores>(entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return entidad;
        }


        public bool Borrar(int id)


        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            // Primero buscamos el registro para poder borrarlo
            var entidad = this.iConexion.Entrenadores!.FirstOrDefault(x => x.Id == id);

            if (entidad == null)
            {
                throw new Exception("NoExisteRegistro");
            }
            this.iConexion.Entrenadores!.Remove(entidad);
            this.iConexion.SaveChanges();
            return true;

        }

    }
}
