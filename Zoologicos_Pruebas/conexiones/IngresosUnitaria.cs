using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.EntityFrameworkCore;
using Zoologicos_Pruebas.nucleo;

namespace Zoologicos_Pruebas.conexiones
{
    [TestClass]
    public class IngresosUnitaria
    {
        private IConexion? iConexion;
        private Ingresos? entidad;

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

            this.entidad = new Ingresos()
            {
                AnimalId = 1,
                ZoologicoId = 1,
                FechaIngreso = DateTime.Now,
                TipoIngreso = "Rescate",
                Procedencia = "Colombia - Corporación Autónoma",
                Estado = "Pendiente",
                Observaciones = "Prueba-" + DateTime.Now.ToString()
            };

            this.iConexion.Ingresos!.Add(this.entidad);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0) return;
            throw new Exception();
        }

        private void Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            var lista = this.iConexion.Ingresos!.ToList();
            if (lista.Count > 0) return;
            throw new Exception();
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.entidad!.Estado = "Aceptado";
            this.entidad!.Observaciones = "Ingreso aceptado correctamente";

            var entry = this.iConexion!.Entry<Ingresos>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion.SaveChanges();
        }

        private void Borrar()
        {
            // Primero lo regresamos a Pendiente para poder borrarlo
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.entidad!.Estado = "Pendiente";
            var entry = this.iConexion!.Entry<Ingresos>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion.SaveChanges();

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            this.iConexion.Ingresos!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
