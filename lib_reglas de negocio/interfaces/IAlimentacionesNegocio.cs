using System;
using System.Collections.Generic;
using System.Text;
using Zoologicos_libreria.entidades;


namespace Zoologicos_libreria.interfaces
{
    public interface IAlimentacionesNegocio
    {
        List<Alimentaciones> Listar();

        Alimentaciones Guardar(Alimentaciones entidad);

        Alimentaciones Modificar(Alimentaciones entidad);

        bool Borrar(int id);
    }

}
