# DentalCare
## Diagnóstico cuantitativo, refactorización SOLID y ADRs

Este repositorio alberga la práctica de arquitectura y diseño sostenible sobre el módulo central de agendamiento y administración de citas de la clínica odontológica **DentalCare**. 

El proyecto aborda el diagnóstico arquitectónico de una solución monolítica (*God Class*), la medición cuantitativa de sus atributos de calidad (cohesión y acoplamiento), la refactorización basada en los principios SOLID y la formalización de decisiones técnicas mediante Registros de Decisiones de Arquitectura (ADRs).

# Objetivos del proyecto
1. Diagnóstico y evaluación de calidad:
Identificar y documentar violaciones explícitas a los cinco principios SOLID (SRP, OCP, LSP, ISP, DIP) y antipatrones de diseño dentro del código.

2. Refactorización sostenible:
Desacoplar responsabilidades centrales (reglas de agendamiento, cálculo de costos/copagos, multas de cancelación, persistencia SQL y pasarelas de notificación por SMS/Email). Aplicando inversión de dependencias e interfaces segregadas para eliminar instanciaciones concretas y dependencias de infraestructura hardcodeadas.

3. Cuantificación del impacto:
Recalcular las métricas arquitectónicas después de la refactorización, así se demuestra cuantitativamente la reducción del acoplamiento y el aumento de la cohesión mediante una matriz comparativa de un antes y un después.

4. Gobernanza y trazabilidad arquitectónica:
Justificar y documentar las decisiones de diseño estructural bajo ISO 42010:2022 a través de ADRs estructurados.
