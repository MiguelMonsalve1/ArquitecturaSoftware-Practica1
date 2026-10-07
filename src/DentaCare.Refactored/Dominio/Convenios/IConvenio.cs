namespace DentalCare.Refactored.Dominio.Convenios;

/// <summary>Strategy: cada convenio decide qué parte del valor paga el paciente.</summary>
public interface IConvenio
{
    string Nombre { get; }
    decimal AplicarCobertura(decimal monto);
}
