using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.EntityFrameworkCore;
using Zoologicos_Pruebas.nucleo;

namespace Zoologicos_Pruebas.conexiones
{
    [TestClass]
    public class CuarentenaUnitaria
    {
        private IConexion? iConexion;
        private Cuarentenas? entidad;

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

            this.entidad = new Cuarentenas()
            {
                AnimalId = 1,           // Simba
                VeterinarioId = 1,      // Carlos Vet
                FechaInicio = DateTime.Now,
                FechaFin = null,
                Motivo = "Animal Nuevo",
                Estado = "Activa",
                Observaciones = "Prueba-" + DateTime.Now.ToString()
            };

            this.iConexion.Cuarentenas!.Add(this.entidad);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0) return;
            throw new Exception();
        }

        private void Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            var lista = this.iConexion.Cuarentenas!.ToList();
            if (lista.Count > 0) return;
            throw new Exception();
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.entidad!.Estado = "Finalizada";
            this.entidad!.FechaFin = DateTime.Now;
            this.entidad!.Observaciones = "Cuarentena finalizada correctamente";

            var entry = this.iConexion!.Entry<Cuarentenas>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion.SaveChanges();
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            this.iConexion.Cuarentenas!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
