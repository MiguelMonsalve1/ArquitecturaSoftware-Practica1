using DentalCare.Refactored.Dominio.Entidades;

namespace DentalCare.Refactored.Dominio.Tarifas;

/// <summary>
/// Copago = cobertura del convenio sobre el costo de la especialidad + recargos.
/// No conoce ninguna especialidad, convenio ni recargo concreto: trabaja solo con abstracciones.
/// </summary>
public sealed class CalculadoraCopago : ICalculadoraCopago
{
    private readonly IEnumerable<IRecargoCita> _recargos;
    private readonly decimal _costoConsulta;

    public CalculadoraCopago(IEnumerable<IRecargoCita> recargos, decimal costoConsulta = 100.0m)
    {
        _recargos = recargos;
        _costoConsulta = costoConsulta;
    }

    public decimal Calcular(SolicitudCita solicitud)
    {
        var costoBase = solicitud.Odontologo.Especialidad.CalcularCostoBase(_costoConsulta);
        var valorPaciente = solicitud.Paciente.Convenio.AplicarCobertura(costoBase);
        return valorPaciente + _recargos.Sum(recargo => recargo.Calcular(solicitud));
    }
}
