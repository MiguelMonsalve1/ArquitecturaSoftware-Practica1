namespace DentalCare.Refactored.Dominio.Excepciones;

public sealed class CancelacionInvalidaException : Exception
{
    public CancelacionInvalidaException(string mensaje) : base(mensaje) { }
}
