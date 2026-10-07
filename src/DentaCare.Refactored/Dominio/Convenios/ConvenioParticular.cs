namespace DentalCare.Refactored.Dominio.Convenios;

/// <summary>Null Object: el paciente particular paga el valor completo, sin un caso especial.</summary>
public sealed class ConvenioParticular : IConvenio
{
    public string Nombre => "Particular";
    public decimal AplicarCobertura(decimal monto) => monto;
}
