using DentalCare.Refactored.Dominio.Entidades;

namespace DentalCare.Refactored.Dominio.Tarifas;

public sealed class RecargoRadiografia : IRecargoCita
{
    private readonly decimal _valor;

    public RecargoRadiografia(decimal valor = 35.0m) => _valor = valor;

    public decimal Calcular(SolicitudCita solicitud) => solicitud.RequiereRadiografia ? _valor : 0m;
}
