# ADR-002: Ajuste de políticas de cancelación y tarifas mediante Strategy

* **Estado:** Aceptado
* **Fecha:** 2026-10-07
* **Autores:** Miguel Monsalve Osorio

## Contexto

Las reglas de cobro están dentro de GestorCitasOdontologicas como cadenas de if/else con números: la especialidad en las líneas 22–37, el convenio en las líneas 40–47 y la multa de cancelación, con un caso especial para cirugía (`EspecialidadId == 3`), en las líneas 79–83. Agregar una especialidad o un convenio, o cambiar la regla de las 24 horas, obliga a modificar código que ya funciona (OCP). Además, el código pregunta por el tipo en vez de usar polimorfismo (LSP), y la especialidad llega por dos lados que pueden no coincidir.

Hay también defectos: una especialidad desconocida da costo 0 sin avisar, se puede cancelar una cita dos veces o después de su hora, y el total recaudado no descuenta las citas canceladas.

## Decisión

Encapsularemos cada regla de cobro en su propia clase, detrás de interfaces del Dominio, usando el patrón Strategy:

* `IEspecialidad`: cada especialidad calcula su costo base y declara su recargo por cancelación tardía (Cirugía, 40).
* `IConvenio`: EPS, Prepagada y Particular. Particular no descuenta nada (Null Object).
* `IRecargoCita`: primera vez y radiografía, que `CalculadoraCopago` suma sin conocerlos.
* `IPoliticaCancelacion`: el umbral de 24 horas y la multa base se reciben como parámetros.

La especialidad sale solo del odontólogo, y la entidad Cita valida que no se cancele dos veces ni después de su hora.

Se descartó reemplazar los números por un enum, porque cada caso nuevo seguiría obligando a modificar el mismo método. También se descartó guardar los factores en una tabla de configuración, porque no permite expresar reglas con comportamiento propio, como el recargo de cirugía.

## Consecuencias

* **Positiva:** agregar una especialidad, un convenio o un recargo es crear una clase nueva sin modificar las existentes. Ninguna parte del código pregunta por el tipo concreto. Cada regla tiene LCOM96b = 0.00, frente al 0.50 del gestor, y las 240 combinaciones probadas dan el mismo copago y la misma multa que el legado.
* **Trade-off:** las reglas de cobro pasan de un método a 16 clases e interfaces, así que para entender el cálculo completo hay que recorrer varios archivos. Los valores (1.2, 30 %, 20…) siguen en el código y cambiarlos requiere recompilar. Se acepta a cambio de poder extender las reglas sin riesgo de regresiones.
* **Cambio de comportamiento:** cancelar una cita ya cancelada o que ya pasó ahora se rechaza con `CancelacionInvalidaException`. Además, el total recaudado descuenta las citas canceladas y suma las multas.

## Cumplimiento (Compliance)

La prueba de las 240 combinaciones debe seguir pasando en cada cambio, y el escenario de `Program.cs` debe dar un copago de 130 y una multa de 90. En revisión de código se rechaza cualquier if o switch sobre el tipo de especialidad o convenio.
