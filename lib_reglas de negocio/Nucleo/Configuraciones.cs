using System;
using System.Collections.Generic;
using System.Text;

namespace Zoologicos_libreria.Nucleo
{
    public class Configuraciones
    {
        public static string? Obtener(string? clave)
        {
            return "server=DESKTOP-5ES77NS\\DEV;Integrated Security=True;TrustServerCertificate=true;database=db_Zooologicos;";
        }
    }
}
