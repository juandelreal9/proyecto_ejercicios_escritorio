Microagente 1: Arquitecto de Datos y Dominio (Data & Domain Agent)
Responsabilidad: Modelado de la base de datos y persistencia.

Tareas:

Crear las entidades en C# con sus respectivas propiedades.

Configurar el DbContext de Entity Framework Core para SQLite.

Establecer las restricciones de llaves primarias y foráneas mediante Fluent API.

Crear el script de inicialización (Seed) con ejercicios básicos de gimnasio y calistenia.

Archivos objetivo: Carpeta Domain/Entities/, Infrastructure/Data/.

🤖 Microagente 2: Desarrollador UI/UX (Frontend & Blazor Agent)
Responsabilidad: Construcción de la interfaz de usuario, maquetación y la lógica visual de arrastrar y soltar.

Tareas:

Desarrollar los componentes Razor (ExerciseCatalog.razor, RoutineBoard.razor, DayColumn.razor).

Implementar las APIs nativas de HTML5 (@ondragstart, @ondrop, @ondragover) garantizando un affordance claro para que el usuario entienda qué elementos son interactivos.

Aplicar estilos CSS/Tailwind para la vista de cuadrícula y el diseño responsivo.

Archivos objetivo: Carpeta UI/Components/, UI/Pages/, archivos .css.

🤖 Microagente 3: Gestor de Estado y Lógica de Negocio (ViewModel & State Agent)
Responsabilidad: Controlar el flujo de datos entre la UI interactiva y la base de datos sin acoplamiento directo.

Tareas:

Crear los ViewModels necesarios (ej. RoutineBuilderViewModel) y el contenedor de estado global inyectado como dependencia (RoutineBuilderState).

Implementar la lógica en C# que procesa el evento drop para instanciar un ItemRutina en el día correcto, actualizar el OrdenVisual y recalcular las posiciones si un ejercicio se elimina o reordena.

Archivos objetivo: Carpeta Application/State/, Application/ViewModels/.

🤖 Microagente 4: Ingeniero de Exportación y Reportes (PDF Agent)
Responsabilidad: Generación del documento final maquetado.

Tareas:

Implementar la librería QuestPDF.

Crear la clase de diseño que reciba el objeto RutinaPersonal cargado en memoria y lo transforme en un documento estructurado (tablas por día, renderizado de imágenes de los ejercicios).

Conectar el servicio de generación de PDF con las APIs nativas de almacenamiento de .NET MAUI para guardar el archivo en el dispositivo.

Archivos objetivo: Carpeta Infrastructure/Pdf/, Application/Services/Pdf/.

🤖 Microagente 5: Ingeniero de SQA y Pruebas (Testing Agent)
Responsabilidad: Garantizar la estabilidad de las reglas de negocio antes de la compilación.

Tareas:

Diseñar y ejecutar casos de prueba unitarios (usando xUnit/NUnit o el framework de preferencia).

Verificar que los métodos de los ViewModels actualicen correctamente las colecciones al añadir o remover un ejercicio.

Comprobar las restricciones del modelo (por ejemplo, que no se guarden series o repeticiones con valores negativos).

Archivos objetivo: Carpeta Tests/UnitTests/.