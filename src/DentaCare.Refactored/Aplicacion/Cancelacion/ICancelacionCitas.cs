using DentalCare.Refactored.Dominio.Entidades;

namespace DentalCare.Refactored.Aplicacion.Cancelacion;

public interface ICancelacionCitas
{
    decimal Cancelar(Cita cita, DateTime fechaCancelacion);
}
