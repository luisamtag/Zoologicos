using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.EntityFrameworkCore;

using Zoologicos_Pruebas.nucleo;

namespace Zoologicos_Pruebas.conexiones
{
    [TestClass]
    public class ReproduccionUnitaria
    {
        private IConexion? iConexion;
        private Reproduccion? entidad;

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

            // Requiere: AnimalMadreId con Genero='Hembra' y AnimalPadreId con Genero='Macho'
            this.entidad = new Reproduccion()
            {
                AnimalMadreId = 3,  // Lola - Hembra
                AnimalPadreId = 1,  // Simba - Macho
                FechaAppariamiento = DateTime.Now.AddMonths(-3),
                FechaNacimiento = DateTime.Now,
                CantidadCrias = 2,
                Metodo = "Natural",
                Estado = "Exitosa",
                Observaciones = "Prueba-" + DateTime.Now.ToString()
            };

            this.iConexion.Reproducciones!.Add(this.entidad);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0) return;
            throw new Exception();
        }

        private void Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            var lista = this.iConexion.Reproducciones!.ToList();
            if (lista.Count > 0) return;
            throw new Exception();
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.entidad!.Estado = "Exitosa";
            this.entidad!.CantidadCrias = 3;
            var entry = this.iConexion!.Entry<Reproduccion>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion.SaveChanges();
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            this.iConexion.Reproducciones!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
