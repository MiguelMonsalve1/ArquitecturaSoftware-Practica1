namespace DentalCare.Refactored.Infraestructura.Configuracion;

/// <summary>La cadena de conexión ya no está en el código: se lee del entorno.</summary>
public sealed record SqlServerOptions(string CadenaConexion)
{
    public static SqlServerOptions DesdeEntorno() =>
        new(Entorno.Requerida("DENTALCARE_SQL_CONEXION"));
}
