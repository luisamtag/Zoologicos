using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Zoologicos_Pruebas.nucleo;

namespace Zoologicos_Pruebas.conexiones
{

    [TestClass]
    public class EntrenadoresUnitaria
    {
        private IConexion? iConexion;
        private Entrenadores? entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Guardar();
            Listar();
            Modificar();
            Borrar();
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.entidad = new Entrenadores()
            {
                Nombre = "Entrenador-" + DateTime.Now.ToString(),
                Cedula = "000111",
                Telefono = "333",
                Email = "ent@zoologico.com",
                Salario = 1800m,
                FechaContratacion = DateTime.Now,
                ZoologicoId = 1,
                Especialidad = "Mamíferos",
                TipoEntrenamiento = "Comportamiento"
            };

            this.iConexion.Entrenadores!.Add(this.entidad);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0) return;
            throw new Exception();
        }

        private void Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            var lista = this.iConexion.Entrenadores!.ToList();
            if (lista.Count > 0) return;
            throw new Exception();
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.entidad!.Especialidad = "Aves";
            var entry = this.iConexion!.Entry<Entrenadores>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion.SaveChanges();
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            this.iConexion.Entrenadores!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
