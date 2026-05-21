using Zoologicos_libreria.implementaciones;
using Zoologicos_libreria.interfaces;
using Zoologicos_libreria.Nucleo;

Console.WriteLine("Hello, World!");


IConexion iConexion = new Conexion();

iConexion.StringConexion = Configuraciones.Obtener("StringConexion");
//iConexion.StringConexion = "server=DESKTOP-5ES77NS\\DEV;Integrated Security=True;TrustServerCertificate=true;database=db_Zooologicos;";
//conexion.StringConexion = "server=DESKTOP-5ES77NS\\DEV;Integrated Security=True;TrustServerCertificate=true;database=db_eps;";
var lista = iConexion.Zoologicos!.ToList();

Console.WriteLine("final");