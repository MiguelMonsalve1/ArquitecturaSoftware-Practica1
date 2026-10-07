namespace DentalCare.Refactored.Infraestructura.Configuracion;

internal static class Entorno
{
    public static string? Opcional(string nombre) => Environment.GetEnvironmentVariable(nombre);

    public static string Requerida(string nombre) =>
        Opcional(nombre) ?? throw new InvalidOperationException(
            $"Falta la variable de entorno {nombre}. Configúrela antes de ejecutar en modo produccion.");
}
