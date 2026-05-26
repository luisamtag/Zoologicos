using Microsoft.EntityFrameworkCore;
using Zoologicos_libreria.entidades;
using Zoologicos_libreria.interfaces;
using Zoologicos_libreria.Nucleo;

namespace Zoologicos_libreria.implementaciones
{
    public class ReproduccionNegocio : IReproduccionNegocio
    {
        private IConexion? iConexion;

        public List<Reproduccion> Listar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
            return this.iConexion.Reproducciones!.ToList();
        }


        public Reproduccion Guardar(Reproduccion entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            //Madre y padre deben ser de la misma especie
            var madre = this.iConexion.Animales!.FirstOrDefault(a => a.Id == entidad.AnimalMadreId);
            var padre = this.iConexion.Animales!.FirstOrDefault(a => a.Id == entidad.AnimalPadreId);
            if (madre == null || padre == null)
                throw new Exception("Uno de los animales especificados no existe");
            if (madre.EspecieId != padre.EspecieId)
                throw new Exception("Los animales deben ser de la misma especie para reproducirse");

            this.iConexion!.Reproducciones!.Add(entidad);
            this.iConexion!.SaveChanges();
            return entidad;
        }

        public Reproduccion Modificar(Reproduccion entidad)
        {
            if (entidad == null)
                throw new Exception("FaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("NoSeGuardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            var entry = this.iConexion!.Entry<Reproduccion>(entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return entidad;
        }

        public bool Borrar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.Obtener("StringConexion");

            var entidad = this.iConexion.Reproducciones!.FirstOrDefault(x => x.Id == id);

            if (entidad == null)
                throw new Exception("NoExisteRegistro");

            this.iConexion.Reproducciones!.Remove(entidad);
            this.iConexion.SaveChanges();
            return true;
        }
    }
}
