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
    public class HistorialesMedicosNegocio : IHistorialesMedicosNegocio
    {
        private IConexion? iConexion;

        public List<HistorialesMedicos> Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            return this.iConexion.HistorialesMedicos!.ToList();
        }

        public HistorialesMedicos Guardar(HistorialesMedicos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("ya se guardo");

            //La fecha de control no puede ser una fecha futura
            if (entidad.FechaControl > DateTime.Now)
                throw new Exception("La fecha de control no puede ser una fecha futura");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.iConexion!.HistorialesMedicos!.Add(entidad);
            this.iConexion!.SaveChanges();
            return entidad;
        }


        public HistorialesMedicos Modificar(HistorialesMedicos entidad)
        {


            if (entidad == null)
                throw new Exception("FaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("NoSeGuardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            var entry = this.iConexion!.Entry<HistorialesMedicos>(entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return entidad;
        }


        public bool Borrar(int id)


        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            // Primero buscamos el registro para poder borrarlo
            var entidad = this.iConexion.HistorialesMedicos!.FirstOrDefault(x => x.Id == id);

            if (entidad == null)
            {
                throw new Exception("NoExisteRegistro");
            }
            this.iConexion.HistorialesMedicos!.Remove(entidad);
            this.iConexion.SaveChanges();
            return true;

        }

    }
}
