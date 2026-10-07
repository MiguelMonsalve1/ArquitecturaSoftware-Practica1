using DentalCare.Refactored.Dominio.Entidades;
using DentalCare.Refactored.Dominio.Excepciones;

namespace DentalCare.Refactored.Aplicacion.Agendamiento;

/// <summary>Regla actual: el odontólogo debe estar marcado como disponible. Se puede reemplazar por una que consulte su agenda.</summary>
public sealed class ValidadorDisponibilidad : IValidadorDisponibilidad
{
    public void Validar(Odontologo odontologo, DateTime fechaHora)
    {
        if (!odontologo.EstaDisponible)
            throw new OdontologoNoDisponibleException(
                $"El odontólogo {odontologo.Nombre} no tiene disponibilidad para el {fechaHora:yyyy-MM-dd HH:mm}.");
    }
}
