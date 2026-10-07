namespace DentalCare.Refactored.Dominio.Especialidades;

public sealed class Ortodoncia : IEspecialidad
{
    public string Nombre => "Ortodoncia";
    public decimal RecargoCancelacionTardia => 0m;
    public decimal CalcularCostoBase(decimal costoConsulta) => costoConsulta * 1.2m;
}
