namespace DentalCare.Refactored.Dominio.Especialidades;

/// <summary>
/// Strategy: cada especialidad sabe su costo base y su recargo por cancelación tardía.
/// Agregar una especialidad nueva es crear una clase, sin tocar código existente (OCP).
/// </summary>
public interface IEspecialidad
{
    string Nombre { get; }
    decimal RecargoCancelacionTardia { get; }
    decimal CalcularCostoBase(decimal costoConsulta);
}
