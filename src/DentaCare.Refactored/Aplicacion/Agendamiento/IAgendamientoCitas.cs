using DentalCare.Refactored.Dominio.Entidades;

namespace DentalCare.Refactored.Aplicacion.Agendamiento;

public interface IAgendamientoCitas
{
    Cita Agendar(SolicitudCita solicitud);
}
