using DentalCare.Refactored.Dominio.Entidades;

namespace DentalCare.Refactored.Dominio.Cancelacion;

/// <summary>Strategy: decide cuánto se cobra por cancelar una cita.</summary>
public interface IPoliticaCancelacion
{
    decimal CalcularPenalizacion(Cita cita, DateTime fechaCancelacion);
}
