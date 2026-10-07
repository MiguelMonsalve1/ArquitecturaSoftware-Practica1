using DentalCare.Refactored.Dominio.Entidades;

namespace DentalCare.Refactored.Dominio.Tarifas;

/// <summary>Strategy: un recargo que se suma al copago si aplica a la solicitud.</summary>
public interface IRecargoCita
{
    decimal Calcular(SolicitudCita solicitud);
}
