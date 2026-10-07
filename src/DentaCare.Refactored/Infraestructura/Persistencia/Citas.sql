-- Tabla que usa SqlServerCitaRepositorio
CREATE TABLE Citas (
    Id            VARCHAR(8)     NOT NULL PRIMARY KEY,
    PacienteId    VARCHAR(20)    NOT NULL,
    OdontologoId  VARCHAR(20)    NOT NULL,
    Fecha         DATETIME2      NOT NULL,
    Copago        DECIMAL(10, 2) NOT NULL,
    Estado        VARCHAR(20)    NOT NULL,
    Penalizacion  DECIMAL(10, 2) NOT NULL DEFAULT 0
);
