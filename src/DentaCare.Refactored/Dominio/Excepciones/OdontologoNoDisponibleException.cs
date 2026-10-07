namespace DentalCare.Refactored.Dominio.Excepciones;

public sealed class OdontologoNoDisponibleException : Exception
{
    public OdontologoNoDisponibleException(string mensaje) : base(mensaje) { }
}
