using DentalCare.Refactored.Dominio.Entidades;

namespace DentalCare.Refactored.Aplicacion.Agendamiento;

public interface IValidadorDisponibilidad
{
    void Validar(Odontologo odontologo, DateTime fechaHora);
}
