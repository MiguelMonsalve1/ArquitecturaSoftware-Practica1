namespace DentalCare.Refactored.Dominio.Entidades;

/// <summary>
/// Parameter Object: agrupa los datos con los que se pide una cita.
/// La especialidad ya no se pasa aparte: sale del odontólogo (una sola fuente de verdad).
/// </summary>
public sealed record SolicitudCita(Paciente Paciente, Odontologo Odontologo, DateTime FechaHora, bool RequiereRadiografia);
