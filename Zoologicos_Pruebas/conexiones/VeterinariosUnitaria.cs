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
    public class VeterinariosUnitaria
    {
        private IConexion? iConexion;
        private Veterinarios? entidad;

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

            this.entidad = new Veterinarios()
            {
                Nombre = "Vet-" + DateTime.Now.ToString(),
                Cedula = "V-123",
                Telefono = "999",
                Email = "vet@z.com",
                Salario = 3000m,
                FechaContratacion = DateTime.Now,
                ZoologicoId = 1,
                Especialidad = "Cirugía Menor",
                AñosExperiencia = 5
            };

            this.iConexion.Veterinarios!.Add(this.entidad);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0) return;
            throw new Exception();
        }

        private void Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            var lista = this.iConexion.Veterinarios!.ToList();
            if (lista.Count > 0) return;
            throw new Exception();
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.entidad!.AñosExperiencia = 6;
            var entry = this.iConexion!.Entry<Veterinarios>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion.SaveChanges();
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            this.iConexion.Veterinarios!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
