using System;
using System.Collections.Generic;
using System.Text;
using Zoologicos_libreria.entidades;


namespace Zoologicos_libreria.interfaces
{
    public interface IAnimalesNegocio
    {
        List<Animales> Listar();

        Animales Guardar (Animales entidad);

        Animales Modificar(Animales entidad);

        bool Borrar(int id);
    }

}
