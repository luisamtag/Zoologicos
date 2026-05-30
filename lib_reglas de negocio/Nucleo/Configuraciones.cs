using System;
using System.Collections.Generic;
using System.Text;

namespace Zoologicos_libreria.Nucleo
{
    public class Configuraciones
    {
        public static string? Obtener(string? clave)
        {
            return "server=LAPTOP-OP3JVRO8\\SQLEXPRESS;Integrated Security=True;TrustServerCertificate=true;database=db_Zoologicos;";
        }
    }
}
