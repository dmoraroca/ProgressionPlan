Se inclue un manual del usuario en formato PDF

Los tres proyectos ConsolaApp, ProgessionPlanWeb y ProgressPlanApi se inician con Establecer proyecto de inicio.
Los test se han hecho con XUnit
Para ejecutar la web es necesario tener arrancado con otro VS2022 ProgressPlanApi

ProgressPlanApi al ejecutar generara una pagina con swagger

ConsolaApp
│
├── ConsolaApp.csproj
│
├── Program.cs                    # Punto de entrada de la aplicación de consola. Orquesta la ejecución principal.
│
├── Services
│   └── ConsoleTodoService.cs     # Servicio para interactuar con el usuario por consola y gestionar tareas.
│
├── Validators
│   └── ConsoleInputValidator.cs  # Validador para entradas del usuario desde la consola.
│
├── Helpers
│   └── ConsoleHelper.cs          # Métodos auxiliares para mostrar menús, leer datos, formatear salidas, etc.
│
└── Models
    ├── ConsoleTodoItem.cs        # Modelo que representa una tarea en el contexto de la consola.
    └── ConsoleProgression.cs     # Modelo que representa un progreso de tarea en la consola.

ProgessionPlanWeb
│
├── ProgessPlanWeb.csproj
├── Program.cs
├── appsettings.json
│
├── Pages
│   ├── Index.cshtml
│   ├── Index.cshtml.cs
│   ├── Error.cshtml
│   ├── Error.cshtml.cs
│   ├── _ViewImports.cshtml
│   ├── _ViewStart.cshtml
│   └── Shared
│       ├── _Layout.cshtml
│       └── _ValidationScriptsPartial.cshtml
│
├── wwwroot
│   ├── js
│   │   └── site.js
│   ├── css
│   └── lib
│
└── Properties

ProgressPlanApi
│
├── ProgressPlanApi.csproj
├── Program.cs
├── appsettings.json
│
├── Controllers
│   └── TodoListController.cs
│       ├── AddItem              [POST   api/TodoList/addItem]
│       ├── Update               [PUT    api/TodoList/update/{id}]
│       ├── Delete               [DELETE api/TodoList/{id}]
│       ├── AddProgression       [POST   api/TodoList/progress/{id}]
│       ├── Print                [GET    api/TodoList/print]
│       ├── GetAll               [GET    api/TodoList/items]
│       └── Clear                [POST   api/TodoList/clearitems]
│
└── Properties
    └── launchSettings.json
	
ProgressPlanDDD
│
├── ProgressPlanDDD.csproj
│
├── Domain
│   ├── Entities
│   │   ├── TodoItem.cs                # Entidad principal: tarea (título, descripción, estado, etc.)
│   │   ├── Progression.cs             # Entidad: progreso de una tarea (fecha, porcentaje)
│   │   ├── Category.cs                # Entidad: categoría de la tarea
│   │   └── User.cs                    # Entidad: usuario asignado a la tarea (si aplica)
│   │
│   ├── ValueObjects
│   │   ├── TodoId.cs                  # Value Object: identificador de tarea
│   │   ├── ProgressionId.cs           # Value Object: identificador de progreso
│   │   ├── CategoryName.cs            # Value Object: nombre de la categoría
│   │   └── Email.cs                   # Value Object: email de usuario
│   │
│   ├── Aggregates
│   │   └── TodoListAggregate.cs       # Agregado raíz: gestiona la colección de tareas y progresos
│   │
│   ├── Events
│   │   ├── TodoItemCompletedEvent.cs  # Evento: tarea completada
│   │   ├── ProgressionAddedEvent.cs   # Evento: progreso añadido
│   │   └── TodoItemCreatedEvent.cs    # Evento: tarea creada
│   │
│   ├── Interfaces
│   │   ├── ITodoListRepository.cs     # Contrato: operaciones de persistencia de tareas
│   │   ├── ICategoryRepository.cs     # Contrato: operaciones de persistencia de categorías
│   │   └── IUserRepository.cs         # Contrato: operaciones de persistencia de usuarios
│   │
│   └── Validators
│       ├── TodoItemValidator.cs       # Validador: reglas de negocio para tareas
│       ├── ProgressionValidator.cs    # Validador: reglas para progresos
│       ├── CategoryValidator.cs       # Validador: reglas para categorías
│       └── UserValidator.cs           # Validador: reglas para usuarios
│
├── Application
│   ├── Interfaces
│   │   ├── ITodoList.cs               # Contrato: lógica de negocio sobre la lista de tareas
│   │   └── IProgressionService.cs     # Contrato: lógica de negocio sobre progresos
│   └── Services
│       ├── TodoService.cs             # Servicio: implementación de la lógica de negocio de tareas
│       └── ProgressionService.cs      # Servicio: implementación de la lógica de negocio de progresos
│
└── Infrastructure
    └── Repositories
        ├── InMemoryTodoRepository.cs  # Implementación en memoria del repositorio de tareas
        ├── InMemoryCategoryRepository.cs # Implementación en memoria del repositorio de categorías
        └── InMemoryUserRepository.cs  # Implementación en memoria del repositorio de usuarios
		
ProgressPlanTest
│
├── ProgressPlanTest.csproj
│
├── UnitTests
│   ├── TodoServiceTests.cs           # Pruebas unitarias para la lógica de negocio de tareas (añadir, actualizar, eliminar, etc.)
│   ├── InMemoryTodoRepositoryTests.cs# Pruebas unitarias para el repositorio en memoria de tareas.
│   ├── ProgressionServiceTests.cs    # Pruebas unitarias para la lógica de negocio de progresos.
│   └── CategoryServiceTests.cs       # Pruebas unitarias para la lógica de negocio de categorías.
│
├── IntegrationTests
│   └── ApiIntegrationTests.cs        # Pruebas de integración para los endpoints de la API.
│
├── TestHelpers
│   └── TestDataFactory.cs            # Métodos auxiliares para crear datos de prueba reutilizables.
│
└── Mocks
    ├── MockTodoListRepository.cs     # Implementación mock del repositorio de tareas para pruebas.
    └── MockCategoryRepository.cs     # Implementación mock del repositorio de categorías para pruebas.