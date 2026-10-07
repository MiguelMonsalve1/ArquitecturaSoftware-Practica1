# ADR-001: Inversión de dependencias en persistencia y notificaciones

* **Estado:** Aceptado
* **Fecha:** 2026-10-07
* **Autores:** Miguel Monsalve Osorio

## Contexto

GestorCitasOdontologicas tiene las reglas de negocio de la clínica, pero crea por su cuenta las clases de infraestructura con `new SqlServerEjecutor()` y `new NotificacionServicio()` (líneas 7 y 8). Por eso no se puede probar ninguna regla sin un SQL Server y un servidor de correo reales, y cambiar de proveedor obliga a modificar la clase que calcula tarifas y multas. Además, las credenciales de la base de datos y de Twilio están escritas en el código, y `EnviarEmailYSms` ignora el mensaje que recibe: cuando se cancela una cita, el paciente recibe "Su cita quedó programada".

En la línea base, el gestor depende de 2 clases concretas de infraestructura, no tiene ninguna abstracción (A = 0) y el módulo completo cae en la Zona de Dolor (D = 1.00). Esto viola DIP e ISP.

## Decisión

Definiremos en la capa de Aplicación las interfaces `ICitaRepositorio`, `ICitaConsultas` e `INotificador`, y las implementaciones concretas se inyectarán por constructor desde `Program.cs`, que será el único lugar que las conoce.

En Infraestructura quedan `SqlServerCitaRepositorio` y `EnMemoriaCitaRepositorio` (Repository), un notificador por canal (Adapter), `NotificadorCompuesto` para enviar por varios canales a la vez (Composite) y `NotificadorTolerante` para que la falla de un canal no tumbe la operación (Decorator). Las credenciales pasan a variables de entorno.

Las interfaces se definen en Aplicación porque es quien las usa la que decide qué necesita. Así la dependencia apunta hacia el negocio y no hacia la infraestructura.

Se descartó dejar el gestor y solo extraerle interfaces, porque seguiría teniendo 11 responsabilidades. También se descartó un único notificador con `EnviarEmail` y `EnviarSms`, porque repetía el problema de ISP.

## Consecuencias

* **Positiva:** las reglas se prueban en memoria, sin base de datos ni correo. Así se compararon 240 combinaciones contra el legado, sin ninguna diferencia. Cambiar de base de datos o agregar un canal es crear una clase nueva sin tocar los servicios, y las credenciales salen del código. Las dependencias hacia infraestructura concreta pasan de 2 a 0, y el módulo sale de la Zona de Dolor (D de 1.00 a 0.35).
* **Trade-off:** hay más piezas (de 6 clases a 34 clases y 13 interfaces) y Ce sube de 5 a 11 en el servicio principal, aunque todas sus dependencias son abstracciones. Además, hay que configurar el contenedor de inyección y las variables de entorno al arrancar. Se acepta a cambio de poder probar y cambiar la infraestructura sin riesgo.

## Cumplimiento (Compliance)

Los namespaces de Dominio y Aplicación no pueden referenciar Infraestructura, SqlClient ni System.Net.Mail. Se puede verificar en el pipeline con una prueba de NetArchTest. En cada pull request se revisa que no haya `new` de clases de infraestructura fuera de `Program.cs` ni credenciales en el repositorio.
