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
    // 13. HistorialesMedicos
    // ==========================================
    [TestClass]
    public class HistorialesMedicosUnitaria
    {
        private IConexion? iConexion;
        private HistorialesMedicos? entidad;

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

            this.entidad = new HistorialesMedicos()
            {
                AnimalId = 1,
                Tratamiento = "Tratamiento-" + DateTime.Now.ToString(),
                Medicamento = "Vitamina C",
                Dosis = "500mg",
                FechaControl = DateTime.Now,
                EstadoActual = "En observación"
            };

            this.iConexion.HistorialesMedicos!.Add(this.entidad);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0) return;
            throw new Exception();
        }

        private void Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            var lista = this.iConexion.HistorialesMedicos!.ToList();
            if (lista.Count > 0) return;
            throw new Exception();
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.entidad!.EstadoActual = "Dado de alta";
            var entry = this.iConexion!.Entry<HistorialesMedicos>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion.SaveChanges();
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            this.iConexion.HistorialesMedicos!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
