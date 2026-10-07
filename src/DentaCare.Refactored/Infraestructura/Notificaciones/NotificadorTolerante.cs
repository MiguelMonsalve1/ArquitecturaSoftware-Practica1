using DentalCare.Refactored.Aplicacion.Dtos;
using DentalCare.Refactored.Aplicacion.Puertos;
using DentalCare.Refactored.Dominio.Entidades;

namespace DentalCare.Refactored.Infraestructura.Notificaciones;

/// <summary>
/// Decorator: envuelve un canal y registra sus fallos en lugar de propagarlos.
/// Si el SMTP se cae, la cita ya guardada no se pierde y los demás canales siguen enviando.
/// </summary>
public sealed class NotificadorTolerante : INotificador
{
    private readonly INotificador _interno;
    private readonly TextWriter _registro;

    public NotificadorTolerante(INotificador interno, TextWriter registro)
    {
        _interno = interno;
        _registro = registro;
    }

    public void Notificar(Paciente destinatario, Mensaje mensaje)
    {
        try
        {
            _interno.Notificar(destinatario, mensaje);
        }
        catch (Exception ex)
        {
            _registro.WriteLine($"[Aviso] No se pudo notificar a {destinatario.NombreCompleto} por {_interno.GetType().Name}: {ex.Message}");
        }
    }
}
