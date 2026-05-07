using Zoologicos_libreria.entidades;
using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Microsoft.EntityFrameworkCore;
using Zoologicos_Pruebas.nucleo;

namespace Zoologicos_Pruebas.conexiones
{
    [TestClass]
    public class CuidadorAnimalesUnitaria
    {
        private IConexion? iConexion;
        private CuidadorAnimales? entidad;

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

            this.entidad = new CuidadorAnimales()
            {
                // Datos Empleado
                Nombre = "Cuidador-" + DateTime.Now.ToString(),
                Cedula = "123456",
                Telefono = "555-5555",
                Email = "cuidador@zoologico.com",
                Salario = 1500m,
                FechaContratacion = DateTime.Now,
                ZoologicoId = 1,
                // Datos Cuidador
                EspecieId = 1,
                Turno = "Diurno",
                AñosExperiencia = 3
            };

            this.iConexion.CuidadorAnimales!.Add(this.entidad);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0) return;
            throw new Exception();
        }

        private void Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            var lista = this.iConexion.CuidadorAnimales!.ToList();
            if (lista.Count > 0) return;
            throw new Exception();
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            this.entidad!.Turno = "Nocturno";
            var entry = this.iConexion!.Entry<CuidadorAnimales>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion.SaveChanges();
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            this.iConexion.CuidadorAnimales!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
