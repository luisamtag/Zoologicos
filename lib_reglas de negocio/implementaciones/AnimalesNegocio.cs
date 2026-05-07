using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria.interfaces;
using Zoologicos_libreria.Nucleo;


namespace Zoologicos_libreria.implementaciones
{
    public class AnimalesNegocio : IAnimalesNegocio
    {
        private IConexion? iConexion;
        
        public List<Animales> Listar ()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            return this.iConexion.Animales!.ToList();
        }

        public Animales Guardar(Animales entidad)
        {
            if (entidad.Id != 0)
            throw new Exception("ya se guardo");



            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            
            this.iConexion!.Animales!.Add(entidad);
            this.iConexion!.SaveChanges();
            return entidad;
        }

        public Animales Modificar(Animales entidad)
        {
            
        
            if (entidad == null)
                throw new Exception("FaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("NoSeGuardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            var entry = this.iConexion!.Entry<Animales>(entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return entidad;
        }
        

        public bool Borrar(int id)
        
            
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            // Primero buscamos el registro para poder borrarlo
            var entidad = this.iConexion.Animales!.FirstOrDefault(x => x.Id == id);

            if (entidad == null)
            {
                throw new Exception("NoExisteRegistro");
            }
            this.iConexion.Animales!.Remove(entidad);
            this.iConexion.SaveChanges();
            return true;
            
        }
    
    }
}
