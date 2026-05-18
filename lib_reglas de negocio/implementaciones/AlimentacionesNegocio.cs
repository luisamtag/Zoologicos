using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria.interfaces;
using Zoologicos_libreria.Nucleo;


namespace Zoologicos_libreria.implementaciones
{
    public class AlimentacionesNegocio : IAlimentacionesNegocio
    {
        private IConexion? iConexion;

        public List<Alimentaciones> Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            return this.iConexion.Alimentaciones!.ToList();
        }

        public Alimentaciones Guardar(Alimentaciones entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("ya se guardo");



            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.iConexion!.Alimentaciones!.Add(entidad);
            this.iConexion!.SaveChanges();
            return entidad;
        }

        public Alimentaciones Modificar(Alimentaciones entidad)
        {


            if (entidad == null)
                throw new Exception("FaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("NoSeGuardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            var entry = this.iConexion!.Entry<Alimentaciones>(entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return entidad;
        }


        public bool Borrar(int id)


        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            // Primero buscamos el registro para poder borrarlo
            var entidad = this.iConexion.Alimentaciones!.FirstOrDefault(x => x.Id == id);

            if (entidad == null)
            {
                throw new Exception("NoExisteRegistro");
            }
            this.iConexion.Alimentaciones!.Remove(entidad);
            this.iConexion.SaveChanges();
            return true;

        }

    }
}
