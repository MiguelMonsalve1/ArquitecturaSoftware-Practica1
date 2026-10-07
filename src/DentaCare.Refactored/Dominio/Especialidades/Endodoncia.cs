namespace DentalCare.Refactored.Dominio.Especialidades;

public sealed class Endodoncia : IEspecialidad
{
    public string Nombre => "Endodoncia";
    public decimal RecargoCancelacionTardia => 0m;
    public decimal CalcularCostoBase(decimal costoConsulta) => costoConsulta * 1.8m;
}
