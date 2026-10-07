using DentalCare.Refactored.Dominio.Convenios;

namespace DentalCare.Refactored.Dominio.Entidades;

public sealed class Paciente
{
    public Paciente(string id, string nombreCompleto, string correo, string celular, IConvenio convenio, bool esPrimeraVez)
    {
        Id = id;
        NombreCompleto = nombreCompleto;
        Correo = correo;
        Celular = celular;
        Convenio = convenio ?? throw new ArgumentNullException(nameof(convenio));
        EsPrimeraVez = esPrimeraVez;
    }

    public string Id { get; }
    public string NombreCompleto { get; }
    public string Correo { get; }
    public string Celular { get; }
    public IConvenio Convenio { get; }
    public bool EsPrimeraVez { get; }
}
