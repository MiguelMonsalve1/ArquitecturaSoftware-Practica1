namespace DentalCare.Refactored.Infraestructura.Configuracion;

public sealed record SmtpOptions(string Servidor, int Puerto, string Remitente, string? Usuario, string? Clave)
{
    public static SmtpOptions DesdeEntorno() => new(
        Entorno.Requerida("DENTALCARE_SMTP_SERVIDOR"),
        int.Parse(Entorno.Opcional("DENTALCARE_SMTP_PUERTO") ?? "587"),
        Entorno.Opcional("DENTALCARE_SMTP_REMITENTE") ?? "citas@dentacare.com",
        Entorno.Opcional("DENTALCARE_SMTP_USUARIO"),
        Entorno.Opcional("DENTALCARE_SMTP_CLAVE"));
}
