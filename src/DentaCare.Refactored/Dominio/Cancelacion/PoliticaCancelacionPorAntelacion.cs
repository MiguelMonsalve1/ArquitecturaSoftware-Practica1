using DentalCare.Refactored.Dominio.Entidades;

namespace DentalCare.Refactored.Dominio.Cancelacion;

/// <summary>
/// Si se cancela con menos horas de antelación que el mínimo, se cobra la multa base
/// más el recargo propio de la especialidad (polimorfismo, sin preguntar si es Cirugía).
/// </summary>
public sealed class PoliticaCancelacionPorAntelacion : IPoliticaCancelacion
{
    private readonly double _horasMinimas;
    private readonly decimal _multaBase;

    public PoliticaCancelacionPorAntelacion(double horasMinimas = 24, decimal multaBase = 50.0m)
    {
        _horasMinimas = horasMinimas;
        _multaBase = multaBase;
    }

    public decimal CalcularPenalizacion(Cita cita, DateTime fechaCancelacion)
    {
        var horasDeAntelacion = (cita.FechaHora - fechaCancelacion).TotalHours;
        if (horasDeAntelacion >= _horasMinimas)
            return 0m;

        return _multaBase + cita.Odontologo.Especialidad.RecargoCancelacionTardia;
    }
}
