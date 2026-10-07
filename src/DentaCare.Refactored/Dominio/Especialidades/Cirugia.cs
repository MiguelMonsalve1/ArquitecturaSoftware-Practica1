namespace DentalCare.Refactored.Dominio.Especialidades;

public sealed class Cirugia : IEspecialidad
{
    public string Nombre => "Cirugía";
    public decimal RecargoCancelacionTardia => 40.0m;
    public decimal CalcularCostoBase(decimal costoConsulta) => costoConsulta * 2.5m;
}
