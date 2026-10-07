namespace DentalCare.Refactored.Dominio.Convenios;

/// <summary>La EPS cubre el 70 %: el paciente paga el 30 %.</summary>
public sealed class ConvenioEps : IConvenio
{
    public string Nombre => "EPS";
    public decimal AplicarCobertura(decimal monto) => monto * 0.30m;
}
