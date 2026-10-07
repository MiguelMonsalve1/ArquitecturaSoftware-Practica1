using DentalCare.Refactored.Aplicacion.Puertos;
using DentalCare.Refactored.Dominio.Cancelacion;
using DentalCare.Refactored.Dominio.Entidades;

namespace DentalCare.Refactored.Aplicacion.Cancelacion;

public sealed class CancelacionCitasService : ICancelacionCitas
{
    private readonly IPoliticaCancelacion _politica;
    private readonly ICitaRepositorio _repositorio;
    private readonly INotificador _notificador;
    private readonly IFormateadorMensajes _mensajes;

    public CancelacionCitasService(
        IPoliticaCancelacion politica,
        ICitaRepositorio repositorio,
        INotificador notificador,
        IFormateadorMensajes mensajes)
    {
        _politica = politica;
        _repositorio = repositorio;
        _notificador = notificador;
        _mensajes = mensajes;
    }

    public decimal Cancelar(Cita cita, DateTime fechaCancelacion)
    {
        var penalizacion = cita.Cancelar(fechaCancelacion, _politica);

        _repositorio.ActualizarCancelacion(cita);
        _notificador.Notificar(cita.Paciente, _mensajes.Cancelacion(cita));
        return penalizacion;
    }
}
