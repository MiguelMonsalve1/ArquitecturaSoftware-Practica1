using DentalCare.Refactored.Aplicacion.Puertos;
using DentalCare.Refactored.Dominio.Entidades;
using DentalCare.Refactored.Dominio.Tarifas;

namespace DentalCare.Refactored.Aplicacion.Agendamiento;

/// <summary>
/// Caso de uso: agendar una cita. Solo coordina; cada paso lo hace una abstracción
/// recibida por constructor (SRP + DIP).
/// </summary>
public sealed class AgendamientoCitasService : IAgendamientoCitas
{
    private readonly IValidadorDisponibilidad _disponibilidad;
    private readonly ICalculadoraCopago _calculadora;
    private readonly ICitaRepositorio _repositorio;
    private readonly INotificador _notificador;
    private readonly IFormateadorMensajes _mensajes;

    public AgendamientoCitasService(
        IValidadorDisponibilidad disponibilidad,
        ICalculadoraCopago calculadora,
        ICitaRepositorio repositorio,
        INotificador notificador,
        IFormateadorMensajes mensajes)
    {
        _disponibilidad = disponibilidad;
        _calculadora = calculadora;
        _repositorio = repositorio;
        _notificador = notificador;
        _mensajes = mensajes;
    }

    public Cita Agendar(SolicitudCita solicitud)
    {
        _disponibilidad.Validar(solicitud.Odontologo, solicitud.FechaHora);

        var copago = _calculadora.Calcular(solicitud);
        var cita = Cita.Programar(solicitud.Paciente, solicitud.Odontologo, solicitud.FechaHora, copago);

        _repositorio.Guardar(cita);
        _notificador.Notificar(cita.Paciente, _mensajes.Confirmacion(cita));
        return cita;
    }
}
