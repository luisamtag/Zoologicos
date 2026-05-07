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
    // 11. Gerentes (Hereda de Empleados)
    // ==========================================
    [TestClass]
    public class GerentesUnitaria
    {
        private IConexion? iConexion;
        private Gerentes? entidad;

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

            this.entidad = new Gerentes()
            {
                Nombre = "Gerente-" + DateTime.Now.ToString(),
                Cedula = "G-123",
                Telefono = "123",
                Email = "g@z.com",
                Salario = 5000m,
                FechaContratacion = DateTime.Now,
                ZoologicoId = 1
            };

            this.iConexion.Gerentes!.Add(this.entidad);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0) return;
            throw new Exception();
        }

        private void Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            var lista = this.iConexion.Gerentes!.ToList();
            if (lista.Count > 0) return;
            throw new Exception();
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.entidad!.Salario = 5500m;
            var entry = this.iConexion!.Entry<Gerentes>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion.SaveChanges();
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            this.iConexion.Gerentes!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
