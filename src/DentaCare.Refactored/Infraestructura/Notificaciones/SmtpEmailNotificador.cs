using System.Net;
using System.Net.Mail;
using DentalCare.Refactored.Aplicacion.Dtos;
using DentalCare.Refactored.Aplicacion.Puertos;
using DentalCare.Refactored.Dominio.Entidades;
using DentalCare.Refactored.Infraestructura.Configuracion;

namespace DentalCare.Refactored.Infraestructura.Notificaciones;

/// <summary>Adapter de correo: traduce INotificador a SmtpClient y envía el mensaje recibido, sin cambiarlo.</summary>
public sealed class SmtpEmailNotificador : INotificador
{
    private readonly SmtpOptions _opciones;

    public SmtpEmailNotificador(SmtpOptions opciones) => _opciones = opciones;

    public void Notificar(Paciente destinatario, Mensaje mensaje)
    {
        using var cliente = new SmtpClient(_opciones.Servidor, _opciones.Puerto) { EnableSsl = true };
        if (_opciones.Usuario is not null)
            cliente.Credentials = new NetworkCredential(_opciones.Usuario, _opciones.Clave);

        using var correo = new MailMessage(_opciones.Remitente, destinatario.Correo, mensaje.Asunto, mensaje.Cuerpo);
        cliente.Send(correo);
    }
}
