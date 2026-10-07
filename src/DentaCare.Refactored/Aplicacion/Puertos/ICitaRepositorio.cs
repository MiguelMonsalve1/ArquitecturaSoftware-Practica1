using DentalCare.Refactored.Dominio.Entidades;

namespace DentalCare.Refactored.Aplicacion.Puertos;

/// <summary>Puerto de escritura. Lo declara la aplicación (quien lo usa), no la infraestructura (DIP).</summary>
public interface ICitaRepositorio
{
    void Guardar(Cita cita);
    void ActualizarCancelacion(Cita cita);
}
