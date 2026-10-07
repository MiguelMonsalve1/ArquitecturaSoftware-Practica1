using DentalCare.Refactored.Dominio.Entidades;

namespace DentalCare.Refactored.Dominio.Tarifas;

public interface ICalculadoraCopago
{
    decimal Calcular(SolicitudCita solicitud);
}
