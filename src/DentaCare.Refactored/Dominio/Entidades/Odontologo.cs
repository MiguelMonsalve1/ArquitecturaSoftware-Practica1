using DentalCare.Refactored.Dominio.Especialidades;

namespace DentalCare.Refactored.Dominio.Entidades;

public sealed class Odontologo
{
    public Odontologo(string id, string nombre, IEspecialidad especialidad, bool estaDisponible)
    {
        Id = id;
        Nombre = nombre;
        Especialidad = especialidad ?? throw new ArgumentNullException(nameof(especialidad));
        EstaDisponible = estaDisponible;
    }

    public string Id { get; }
    public string Nombre { get; }
    public IEspecialidad Especialidad { get; }
    public bool EstaDisponible { get; }
}
