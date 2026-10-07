using DentalCare.Refactored.Aplicacion.Dtos;
using DentalCare.Refactored.Aplicacion.Puertos;
using DentalCare.Refactored.Dominio.Entidades;

namespace DentalCare.Refactored.Infraestructura.Notificaciones;

/// <summary>
/// Composite: trata varios canales como si fueran uno. Agregar WhatsApp es registrar
/// un canal más en Program.cs, sin tocar los servicios (OCP).
/// </summary>
public sealed class NotificadorCompuesto : INotificador
{
    private readonly IReadOnlyList<INotificador> _canales;

    public NotificadorCompuesto(IEnumerable<INotificador> canales) => _canales = canales.ToList();

    public void Notificar(Paciente destinatario, Mensaje mensaje)
    {
        foreach (var canal in _canales)
            canal.Notificar(destinatario, mensaje);
    }
}
