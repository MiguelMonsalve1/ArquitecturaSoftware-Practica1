using DentalCare.Refactored.Aplicacion.Puertos;
using DentalCare.Refactored.Dominio.Entidades;

namespace DentalCare.Refactored.Infraestructura.Persistencia;

/// <summary>Repositorio en memoria para la demo de consola y para pruebas sin base de datos.</summary>
public sealed class EnMemoriaCitaRepositorio : ICitaRepositorio, ICitaConsultas
{
    private readonly Dictionary<string, Cita> _citas = new();

    public void Guardar(Cita cita) => _citas[cita.Id] = cita;

    public void ActualizarCancelacion(Cita cita)
    {
        if (!_citas.ContainsKey(cita.Id))
            throw new KeyNotFoundException($"No existe la cita #{cita.Id}.");
        _citas[cita.Id] = cita;
    }

    public decimal SumarCopagosProgramados() =>
        _citas.Values.Where(c => c.Estado == EstadoCita.Programada).Sum(c => c.Copago);

    public decimal SumarPenalizaciones() =>
        _citas.Values.Where(c => c.Estado == EstadoCita.Cancelada).Sum(c => c.Penalizacion);

    public int ContarCanceladas() =>
        _citas.Values.Count(c => c.Estado == EstadoCita.Cancelada);
}
