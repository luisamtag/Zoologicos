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
    public class DiagnosticosUnitaria
    {
        private IConexion? iConexion;
        private Diagnosticos? entidad;

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

            this.entidad = new Diagnosticos()
            {
                FechaDiagnostico = DateTime.Now,
                AnimalId = 1,
                EnfermedadId = 1,
                VeterinarioId = 1
            };

            this.iConexion.Diagnosticos!.Add(this.entidad);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0) return;
            throw new Exception();
        }

        private void Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            var lista = this.iConexion.Diagnosticos!.ToList();
            if (lista.Count > 0) return;
            throw new Exception();
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.entidad!.FechaCura = DateTime.Now.AddDays(7);
            var entry = this.iConexion!.Entry<Diagnosticos>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion.SaveChanges();
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            this.iConexion.Diagnosticos!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
