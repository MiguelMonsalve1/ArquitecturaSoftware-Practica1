using DentalCare.Refactored.Aplicacion.Dtos;
using DentalCare.Refactored.Aplicacion.Puertos;
using DentalCare.Refactored.Dominio.Entidades;

namespace DentalCare.Refactored.Aplicacion.Mensajes;

/// <summary>Arma los textos que se envían al paciente. Antes estaban dentro de Cita y del notificador (SRP).</summary>
public sealed class FormateadorMensajesCita : IFormateadorMensajes
{
    public Mensaje Confirmacion(Cita cita) => new(
        "Confirmación de cita odontológica",
        $"Hola {cita.Paciente.NombreCompleto}, su cita #{cita.Id} con {cita.Odontologo.Nombre} " +
        $"({cita.Odontologo.Especialidad.Nombre}) quedó programada para el {cita.FechaHora:yyyy-MM-dd HH:mm}. " +
        $"Copago: ${cita.Copago:N2}.");

    public Mensaje Cancelacion(Cita cita) => new(
        "Cancelación de cita odontológica",
        $"Hola {cita.Paciente.NombreCompleto}, su cita #{cita.Id} del {cita.FechaHora:yyyy-MM-dd HH:mm} fue cancelada. " +
        (cita.Penalizacion > 0
            ? $"Por cancelar con poca antelación se aplicó una penalización de ${cita.Penalizacion:N2}."
            : "No se aplicó penalización."));
}
