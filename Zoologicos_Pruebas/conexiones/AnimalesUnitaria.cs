using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.EntityFrameworkCore;

using Zoologicos_Pruebas.nucleo;

namespace Zoologicos_Pruebas.conexiones
{
    [TestClass]
    public class AnimalesUnitaria
    {
        private IConexion? iConexion;
        private Animales? entidad;

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

            this.entidad = new Animales()
            {
                Nombre = "Animal-" + DateTime.Now.ToString(),
                Naturaleza = "Salvaje",
                FechaNacimiento = DateTime.Now.AddYears(-2),
                Alimentacion = "Carnivoro",
                Genero = "Macho",
                EspecieId = 1,
                JaulaId = 1
            };

            this.iConexion.Animales!.Add(this.entidad);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0) return;
            throw new Exception();
        }

        private void Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            var lista = this.iConexion.Animales!.ToList();
            if (lista.Count > 0) return;
            throw new Exception();
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.entidad!.Naturaleza = "Domestico";
            var entry = this.iConexion!.Entry<Animales>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion.SaveChanges();
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            this.iConexion.Animales!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
