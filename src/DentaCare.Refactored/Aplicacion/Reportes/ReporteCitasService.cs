using DentalCare.Refactored.Aplicacion.Puertos;

namespace DentalCare.Refactored.Aplicacion.Reportes;

/// <summary>
/// Los totales se calculan a partir de las citas guardadas, sin contadores en memoria.
/// Recaudado = copagos de las citas vigentes + penalizaciones de las canceladas.
/// </summary>
public sealed class ReporteCitasService : IReporteCitas
{
    private readonly ICitaConsultas _consultas;

    public ReporteCitasService(ICitaConsultas consultas) => _consultas = consultas;

    public decimal TotalRecaudado() => _consultas.SumarCopagosProgramados() + _consultas.SumarPenalizaciones();

    public int TotalCanceladas() => _consultas.ContarCanceladas();
}
