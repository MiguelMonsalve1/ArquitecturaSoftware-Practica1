using DentalCare.Refactored.Dominio.Cancelacion;
using DentalCare.Refactored.Dominio.Excepciones;

namespace DentalCare.Refactored.Dominio.Entidades;

public sealed class Cita
{
    private Cita(string id, Paciente paciente, Odontologo odontologo, DateTime fechaHora, decimal copago)
    {
        Id = id;
        Paciente = paciente;
        Odontologo = odontologo;
        FechaHora = fechaHora;
        Copago = copago;
        Estado = EstadoCita.Programada;
    }

    public string Id { get; }
    public Paciente Paciente { get; }
    public Odontologo Odontologo { get; }
    public DateTime FechaHora { get; }
    public decimal Copago { get; }
    public EstadoCita Estado { get; private set; }
    public decimal Penalizacion { get; private set; }

    /// <summary>Static Factory Method: toda cita nace programada y con un Id.</summary>
    public static Cita Programar(Paciente paciente, Odontologo odontologo, DateTime fechaHora, decimal copago)
    {
        var id = Guid.NewGuid().ToString("N")[..8];
        return new Cita(id, paciente, odontologo, fechaHora, copago);
    }

    /// <summary>
    /// Cancela la cita aplicando la política recibida. Valida antes de cambiar el estado:
    /// no se cancela dos veces ni una cita cuyo horario ya pasó.
    /// </summary>
    public decimal Cancelar(DateTime fechaCancelacion, IPoliticaCancelacion politica)
    {
        if (Estado == EstadoCita.Cancelada)
            throw new CancelacionInvalidaException($"La cita #{Id} ya estaba cancelada.");

        if (fechaCancelacion >= FechaHora)
            throw new CancelacionInvalidaException($"La cita #{Id} ya ocurrió y no se puede cancelar.");

        Penalizacion = politica.CalcularPenalizacion(this, fechaCancelacion);
        Estado = EstadoCita.Cancelada;
        return Penalizacion;
    }
}
