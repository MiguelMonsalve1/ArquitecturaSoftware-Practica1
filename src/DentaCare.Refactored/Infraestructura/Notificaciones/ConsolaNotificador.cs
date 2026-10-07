using DentalCare.Refactored.Aplicacion.Dtos;
using DentalCare.Refactored.Aplicacion.Puertos;
using DentalCare.Refactored.Dominio.Entidades;

namespace DentalCare.Refactored.Infraestructura.Notificaciones;

/// <summary>Canal de demostración: escribe en consola en lugar de enviar. Útil para la demo y para pruebas.</summary>
public sealed class ConsolaNotificador : INotificador
{
    private readonly string _canal;
    private readonly Func<Paciente, string> _direccion;

    public ConsolaNotificador(string canal, Func<Paciente, string> direccion)
    {
        _canal = canal;
        _direccion = direccion;
    }

    public void Notificar(Paciente destinatario, Mensaje mensaje) =>
        Console.WriteLine($"[{_canal} -> {_direccion(destinatario)}] {mensaje.Asunto}: {mensaje.Cuerpo}");
}
