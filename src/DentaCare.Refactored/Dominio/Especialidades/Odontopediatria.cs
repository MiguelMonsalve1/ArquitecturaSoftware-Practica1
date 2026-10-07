namespace DentalCare.Refactored.Dominio.Especialidades;

public sealed class Odontopediatria : IEspecialidad
{
    public string Nombre => "Odontopediatría";
    public decimal RecargoCancelacionTardia => 0m;
    public decimal CalcularCostoBase(decimal costoConsulta) => costoConsulta * 1.1m;
}
