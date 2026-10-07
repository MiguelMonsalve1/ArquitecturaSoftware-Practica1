namespace DentalCare.Refactored.Aplicacion.Puertos;

/// <summary>Puerto de lectura para reportes, separado del de escritura (ISP).</summary>
public interface ICitaConsultas
{
    decimal SumarCopagosProgramados();
    decimal SumarPenalizaciones();
    int ContarCanceladas();
}
