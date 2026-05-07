using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.EntityFrameworkCore;

using Zoologicos_Pruebas.nucleo;

namespace Zoologicos_Pruebas.conexiones
{
    [TestClass]
    public class AlimentacionesUnitaria
    {
        private IConexion? iConexion;
        private Alimentaciones? entidad;

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

            this.entidad = new Alimentaciones()
            {
                AnimalId = 1, // Requiere que exista un Animal con Id 1
                TipoDieta = "Dieta-" + DateTime.Now.ToString(),
                CantidadDiaria = 5.5m
            };

            this.iConexion.Alimentaciones!.Add(this.entidad);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0) return;
            throw new Exception();
        }

        private void Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            var lista = this.iConexion.Alimentaciones!.ToList();
            if (lista.Count > 0) return;
            throw new Exception();
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.entidad!.TipoDieta = "Modificada";
            var entry = this.iConexion!.Entry<Alimentaciones>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion.SaveChanges();
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            this.iConexion.Alimentaciones!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
