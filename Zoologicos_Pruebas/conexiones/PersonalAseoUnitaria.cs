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
    // 17. PersonalAseo (Hereda de Empleados)
    // ==========================================
    [TestClass]
    public class PersonalAseoUnitaria
    {
        private IConexion? iConexion;
        private PersonalAseo? entidad;

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

            this.entidad = new PersonalAseo()
            {
                Nombre = "Aseo-" + DateTime.Now.ToString(),
                Cedula = "A-123",
                Telefono = "444",
                Email = "aseo@z.com",
                Salario = 900m,
                FechaContratacion = DateTime.Now,
                ZoologicoId = 1,
                ZonaAsignada = "Baños Norte",
                Turno = "Mañana",
                ProductosAsignados = "Cloro, Escoba"
            };

            this.iConexion.PersonalAseo!.Add(this.entidad);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0) return;
            throw new Exception();
        }

        private void Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            var lista = this.iConexion.PersonalAseo!.ToList();
            if (lista.Count > 0) return;
            throw new Exception();
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.entidad!.ZonaAsignada = "Plaza Central";
            var entry = this.iConexion!.Entry<PersonalAseo>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion.SaveChanges();
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            this.iConexion.PersonalAseo!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
