namespace DentalCare.Refactored.Infraestructura.Configuracion;

public sealed record TwilioOptions(string ApiKey, string NumeroOrigen)
{
    public static TwilioOptions DesdeEntorno() => new(
        Entorno.Requerida("DENTALCARE_TWILIO_API_KEY"),
        Entorno.Opcional("DENTALCARE_TWILIO_NUMERO") ?? "+10000000000");
}
