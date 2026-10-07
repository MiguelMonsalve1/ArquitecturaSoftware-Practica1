using DentalCare.Refactored.Aplicacion.Dtos;
using DentalCare.Refactored.Aplicacion.Puertos;
using DentalCare.Refactored.Dominio.Entidades;
using DentalCare.Refactored.Infraestructura.Configuracion;

namespace DentalCare.Refactored.Infraestructura.Notificaciones;

/// <summary>
/// Adapter de SMS. Igual que en el legado, el envío por la API de Twilio está simulado:
/// para hacerlo real solo se cambia esta clase, no los servicios.
/// </summary>
public sealed class TwilioSmsNotificador : INotificador
{
    private readonly TwilioOptions _opciones;

    public TwilioSmsNotificador(TwilioOptions opciones) => _opciones = opciones;

    public void Notificar(Paciente destinatario, Mensaje mensaje) =>
        Console.WriteLine($"[SMS Twilio {_opciones.NumeroOrigen} -> {destinatario.Celular}] {mensaje.Cuerpo}");
}
