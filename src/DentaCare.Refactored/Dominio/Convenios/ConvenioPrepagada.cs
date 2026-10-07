namespace DentalCare.Refactored.Dominio.Convenios;

/// <summary>La prepagada cubre el 90 %: el paciente paga el 10 %.</summary>
public sealed class ConvenioPrepagada : IConvenio
{
    public string Nombre => "Prepagada";
    public decimal AplicarCobertura(decimal monto) => monto * 0.10m;
}
