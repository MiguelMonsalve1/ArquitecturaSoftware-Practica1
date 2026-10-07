using DentalCare.Refactored.Dominio.Entidades;

namespace DentalCare.Refactored.Dominio.Tarifas;

public sealed class RecargoPrimeraVez : IRecargoCita
{
    private readonly decimal _valor;

    public RecargoPrimeraVez(decimal valor = 20.0m) => _valor = valor;

    public decimal Calcular(SolicitudCita solicitud) => solicitud.Paciente.EsPrimeraVez ? _valor : 0m;
}
