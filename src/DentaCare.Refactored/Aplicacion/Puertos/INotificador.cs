using DentalCare.Refactored.Aplicacion.Dtos;
using DentalCare.Refactored.Dominio.Entidades;

namespace DentalCare.Refactored.Aplicacion.Puertos;

/// <summary>
/// Contrato de notificación: toda implementación debe entregar exactamente el mensaje recibido.
/// Así cualquier canal (o combinación de canales) es sustituible (LSP).
/// </summary>
public interface INotificador
{
    void Notificar(Paciente destinatario, Mensaje mensaje);
}
