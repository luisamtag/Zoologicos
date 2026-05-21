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
    public class EntradasNegocio : IEntradasNegocio
    {
        private IConexion? iConexion;

        public List<Entradas> Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            return this.iConexion.Entradas!.ToList();
        }

        public Entradas Guardar(Entradas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("ya se guardo");

            decimal descuentoDia = entidad.ValorPagado;
            DateTime hoy = DateTime.Now;

            if (hoy.DayOfWeek == DayOfWeek.Monday)
            {
                descuentoDia *= 0.90m;

            }

            //Descuento extra si es horario nocturno(ej.después de las 8 PM)
            if (hoy.Hour >= 20)
            {
                descuentoDia -= 1.00m; // Un peso menos de descuento nocturno
            }

            entidad.ValorPagado = descuentoDia;


            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.iConexion!.Entradas!.Add(entidad);
            this.iConexion!.SaveChanges();
            return entidad;
        }

        public Entradas Modificar(Entradas entidad)
        {


            if (entidad == null)
                throw new Exception("FaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("NoSeGuardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            var entry = this.iConexion!.Entry<Entradas>(entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return entidad;
        }


        public bool Borrar(int id)


        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            // Primero buscamos el registro para poder borrarlo
            var entidad = this.iConexion.Entradas!.FirstOrDefault(x => x.Id == id);

            if (entidad == null)
            {
                throw new Exception("NoExisteRegistro");
            }
            this.iConexion.Entradas!.Remove(entidad);
            this.iConexion.SaveChanges();
            return true;

        }

    }
}
