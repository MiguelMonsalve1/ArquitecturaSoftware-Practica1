using DentalCare.Refactored.Aplicacion.Dtos;
using DentalCare.Refactored.Dominio.Entidades;

namespace DentalCare.Refactored.Aplicacion.Puertos;

public interface IFormateadorMensajes
{
    Mensaje Confirmacion(Cita cita);
    Mensaje Cancelacion(Cita cita);
}
