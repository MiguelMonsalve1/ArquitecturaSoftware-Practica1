using Microsoft.Data.SqlClient;
using DentalCare.Refactored.Aplicacion.Puertos;
using DentalCare.Refactored.Dominio.Entidades;
using DentalCare.Refactored.Infraestructura.Configuracion;

namespace DentalCare.Refactored.Infraestructura.Persistencia;

/// <summary>
/// Adapter de SQL Server para los puertos de persistencia. Es la única clase que conoce ADO.NET.
/// A diferencia del legado, guarda el Id y el Estado, así la cancelación encuentra la fila.
/// </summary>
public sealed class SqlServerCitaRepositorio : ICitaRepositorio, ICitaConsultas
{
    private readonly string _cadenaConexion;

    public SqlServerCitaRepositorio(SqlServerOptions opciones) => _cadenaConexion = opciones.CadenaConexion;

    public void Guardar(Cita cita) => Ejecutar(
        "INSERT INTO Citas (Id, PacienteId, OdontologoId, Fecha, Copago, Estado, Penalizacion) " +
        "VALUES (@id, @p, @o, @f, @c, @e, 0)",
        cmd =>
        {
            cmd.Parameters.AddWithValue("@id", cita.Id);
            cmd.Parameters.AddWithValue("@p", cita.Paciente.Id);
            cmd.Parameters.AddWithValue("@o", cita.Odontologo.Id);
            cmd.Parameters.AddWithValue("@f", cita.FechaHora);
            cmd.Parameters.AddWithValue("@c", cita.Copago);
            cmd.Parameters.AddWithValue("@e", cita.Estado.ToString());
        });

    public void ActualizarCancelacion(Cita cita) => Ejecutar(
        "UPDATE Citas SET Estado = @e, Penalizacion = @pen WHERE Id = @id",
        cmd =>
        {
            cmd.Parameters.AddWithValue("@e", cita.Estado.ToString());
            cmd.Parameters.AddWithValue("@pen", cita.Penalizacion);
            cmd.Parameters.AddWithValue("@id", cita.Id);
        });

    public decimal SumarCopagosProgramados() =>
        Escalar<decimal>($"SELECT ISNULL(SUM(Copago), 0) FROM Citas WHERE Estado = '{EstadoCita.Programada}'");

    public decimal SumarPenalizaciones() =>
        Escalar<decimal>($"SELECT ISNULL(SUM(Penalizacion), 0) FROM Citas WHERE Estado = '{EstadoCita.Cancelada}'");

    public int ContarCanceladas() =>
        Escalar<int>($"SELECT COUNT(*) FROM Citas WHERE Estado = '{EstadoCita.Cancelada}'");

    private void Ejecutar(string sql, Action<SqlCommand> parametros)
    {
        using var conexion = new SqlConnection(_cadenaConexion);
        conexion.Open();
        using var comando = new SqlCommand(sql, conexion);
        parametros(comando);
        comando.ExecuteNonQuery();
    }

    private T Escalar<T>(string sql)
    {
        using var conexion = new SqlConnection(_cadenaConexion);
        conexion.Open();
        using var comando = new SqlCommand(sql, conexion);
        return (T)Convert.ChangeType(comando.ExecuteScalar()!, typeof(T));
    }
}
