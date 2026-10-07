using DentalCare.Refactored.Aplicacion.Agendamiento;
using DentalCare.Refactored.Aplicacion.Cancelacion;
using DentalCare.Refactored.Aplicacion.Mensajes;
using DentalCare.Refactored.Aplicacion.Puertos;
using DentalCare.Refactored.Aplicacion.Reportes;
using DentalCare.Refactored.Dominio.Cancelacion;
using DentalCare.Refactored.Dominio.Convenios;
using DentalCare.Refactored.Dominio.Entidades;
using DentalCare.Refactored.Dominio.Especialidades;
using DentalCare.Refactored.Dominio.Excepciones;
using DentalCare.Refactored.Dominio.Tarifas;
using DentalCare.Refactored.Infraestructura.Configuracion;
using DentalCare.Refactored.Infraestructura.Notificaciones;
using DentalCare.Refactored.Infraestructura.Persistencia;
using Microsoft.Extensions.DependencyInjection;

// ---------------------------------------------------------------------------
// Composition Root: el único lugar que conoce las clases concretas (DIP).
// DENTALCARE_MODO=demo (por defecto): repositorio en memoria y notificaciones por consola.
// DENTALCARE_MODO=produccion: SQL Server, SMTP y Twilio, con credenciales tomadas del entorno.
// ---------------------------------------------------------------------------
var modo = Environment.GetEnvironmentVariable("DENTALCARE_MODO") ?? "demo";
using var proveedor = ConfigurarServicios(modo).BuildServiceProvider();

var agendamiento = proveedor.GetRequiredService<IAgendamientoCitas>();
var cancelacion = proveedor.GetRequiredService<ICancelacionCitas>();
var reportes = proveedor.GetRequiredService<IReporteCitas>();

Console.WriteLine("=================================================");
Console.WriteLine(" DENTALCARE SYSTEM - MÓDULO REFACTORIZADO DE CITAS");
Console.WriteLine($" Modo: {modo}");
Console.WriteLine("=================================================\n");

try
{
    var paciente = new Paciente("PAC-101", "Ana María Gómez", "ana.gomez@email.com", "3001234567",
        new ConvenioEps(), esPrimeraVez: true);
    var odontologo = new Odontologo("ODO-202", "Dr. Roberto Martínez", new Cirugia(), estaDisponible: true);

    Console.WriteLine("---> [FLUJO 1]: AGENDAMIENTO DE CITA ODONTOLÓGICA");
    var cita = agendamiento.Agendar(new SolicitudCita(paciente, odontologo, DateTime.Now.AddHours(12), RequiereRadiografia: true));
    Console.WriteLine($"\n Cita generada exitosamente con ID: {cita.Id}");
    Console.WriteLine($" Copago Final Calculado: ${cita.Copago:N2}");
    Console.WriteLine($" Estado de la Cita: {cita.Estado}\n");

    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("---> [FLUJO 2]: CANCELACIÓN DE CITA (MENOS DE 24 HORAS)");
    var penalizacion = cancelacion.Cancelar(cita, DateTime.Now);
    Console.WriteLine($"\n Cita ID #{cita.Id} ha sido actualizada.");
    Console.WriteLine($" Nuevo Estado: {cita.Estado}");
    Console.WriteLine($" Penalización Aplicada: ${penalizacion:N2}\n");

    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("---> [FLUJO 3]: REPORTE DE TOTALES");
    Console.WriteLine($" Total Recaudado (copagos vigentes + penalizaciones): ${reportes.TotalRecaudado():N2}");
    Console.WriteLine($" Total Citas Canceladas: {reportes.TotalCanceladas()}\n");

    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("---> [FLUJO 4]: INTENTO DE CANCELAR DOS VECES LA MISMA CITA");
    try
    {
        cancelacion.Cancelar(cita, DateTime.Now);
    }
    catch (CancelacionInvalidaException ex)
    {
        Console.WriteLine($" Operación rechazada: {ex.Message}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"\n ERROR CRÍTICO EN EL SISTEMA: {ex.Message}");
}

Console.WriteLine("\n=================================================");
if (!Console.IsInputRedirected)
{
    Console.WriteLine("Presione cualquier tecla para salir...");
    Console.ReadKey();
}

static ServiceCollection ConfigurarServicios(string modo)
{
    var servicios = new ServiceCollection();

    // Dominio: estrategias de tarifas y cancelación (OCP: una regla nueva es un registro nuevo).
    servicios.AddSingleton<IRecargoCita>(_ => new RecargoPrimeraVez(valor: 20.0m));
    servicios.AddSingleton<IRecargoCita>(_ => new RecargoRadiografia(valor: 35.0m));
    servicios.AddSingleton<ICalculadoraCopago>(sp =>
        new CalculadoraCopago(sp.GetServices<IRecargoCita>(), costoConsulta: 100.0m));
    servicios.AddSingleton<IPoliticaCancelacion>(_ =>
        new PoliticaCancelacionPorAntelacion(horasMinimas: 24, multaBase: 50.0m));

    // Aplicación: casos de uso, cada uno con su propia interfaz (ISP).
    servicios.AddSingleton<IValidadorDisponibilidad, ValidadorDisponibilidad>();
    servicios.AddSingleton<IFormateadorMensajes, FormateadorMensajesCita>();
    servicios.AddTransient<IAgendamientoCitas, AgendamientoCitasService>();
    servicios.AddTransient<ICancelacionCitas, CancelacionCitasService>();
    servicios.AddTransient<IReporteCitas, ReporteCitasService>();

    // Infraestructura: se elige la implementación sin tocar los servicios (DIP).
    if (modo == "produccion")
    {
        servicios.AddSingleton(_ => SqlServerOptions.DesdeEntorno());
        servicios.AddSingleton(_ => SmtpOptions.DesdeEntorno());
        servicios.AddSingleton(_ => TwilioOptions.DesdeEntorno());

        servicios.AddSingleton<SqlServerCitaRepositorio>();
        servicios.AddSingleton<ICitaRepositorio>(sp => sp.GetRequiredService<SqlServerCitaRepositorio>());
        servicios.AddSingleton<ICitaConsultas>(sp => sp.GetRequiredService<SqlServerCitaRepositorio>());

        servicios.AddSingleton<INotificador>(sp => new NotificadorCompuesto(new INotificador[]
        {
            new NotificadorTolerante(new SmtpEmailNotificador(sp.GetRequiredService<SmtpOptions>()), Console.Error),
            new NotificadorTolerante(new TwilioSmsNotificador(sp.GetRequiredService<TwilioOptions>()), Console.Error)
        }));
    }
    else
    {
        servicios.AddSingleton<EnMemoriaCitaRepositorio>();
        servicios.AddSingleton<ICitaRepositorio>(sp => sp.GetRequiredService<EnMemoriaCitaRepositorio>());
        servicios.AddSingleton<ICitaConsultas>(sp => sp.GetRequiredService<EnMemoriaCitaRepositorio>());

        servicios.AddSingleton<INotificador>(_ => new NotificadorCompuesto(new INotificador[]
        {
            new ConsolaNotificador("Email", paciente => paciente.Correo),
            new ConsolaNotificador("SMS", paciente => paciente.Celular)
        }));
    }

    return servicios;
}
