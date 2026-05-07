using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Zoologicos_libreria.entidades;
using Microsoft.EntityFrameworkCore;
using Zoologicos_Pruebas.nucleo;

namespace Zoologicos_Pruebas.conexiones
{
    [TestClass]
    public class ZoologicosUnitaria
    {
        private IConexion? iConexion;
        private Zoologicos? entidad;

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

            this.entidad = new Zoologicos()
            {
                Nombre = "UT-" + DateTime.Now.ToString(),

                Ubicacion = "bogota",
            };

            this.iConexion.Zoologicos!.Add(this.entidad);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0)
                return;
            throw new Exception();
        }

        private void Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            var lista = this.iConexion.Zoologicos!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception();
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.entidad!.Ubicacion = "Cali";
            var entry = this.iConexion!.Entry<Zoologicos>(this.entidad!);
            entry.State = EntityState.Modified;

            this.iConexion.SaveChanges();
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.iConexion.Zoologicos!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
