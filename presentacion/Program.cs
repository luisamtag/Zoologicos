using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;

Console.WriteLine("Hello, World!");


IConexion iConexion = new Conexion();

iConexion.StringConexion = "server=DESKTOP-5ES77NS\\DEV;Integrated Security=True;TrustServerCertificate=true;database=db_Zooologicos;";
//conexion.StringConexion = "server=DESKTOP-5ES77NS\\DEV;Integrated Security=True;TrustServerCertificate=true;database=db_eps;";
var lista = iConexion.Zoologicos!.ToList();

Console.WriteLine("final");