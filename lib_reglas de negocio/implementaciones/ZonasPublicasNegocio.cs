using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria.interfaces;
using Zoologicos_libreria.Nucleo;
using Zoologicos_servicios.Controllers;

namespace Zoologicos_libreria.implementaciones
{
    public class ZonasPublicasNegocio : IZonasPublicasNegocio
    {
        private IConexion? iConexion;

        public List< ZonasPublicas> Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            return this.iConexion. ZonasPublicas!.ToList();
        }

        public  ZonasPublicas Guardar( ZonasPublicas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("ya se guardo");



            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.iConexion!. ZonasPublicas!.Add(entidad);
            this.iConexion!.SaveChanges();
            return entidad;
        }

        public  ZonasPublicas Modificar( ZonasPublicas entidad)
        {


            if (entidad == null)
                throw new Exception("FaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("NoSeGuardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            var entry = this.iConexion!.Entry< ZonasPublicas>(entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return entidad;
        }


        public bool Borrar(int id)


        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            // Primero buscamos el registro para poder borrarlo
            var entidad = this.iConexion. ZonasPublicas!.FirstOrDefault(x => x.Id == id);

            if (entidad == null)
            {
                throw new Exception("NoExisteRegistro");
            }
            this.iConexion. ZonasPublicas!.Remove(entidad);
            this.iConexion.SaveChanges();
            return true;

        }

    }
}
