using ProgressPlanDDD.Application.Interfaces;
using ProgressPlannerDDD.Application.Services;
using ProgressPlannerDDD.Infrastructure.Repositories;

InMemoryTodoRepository inMemoryTodoRepository = new();

ITodoListRepository repository = inMemoryTodoRepository;
ITodoList todoService = new TodoService();

try
{
    todoService.AddItem(repository.GetNextId(), "Complete Project Report", "Finish the final report for the project", "Work");
        

    // Obtener el Id generado automáticamente por el repositorio

    // Agregar progresiones
    todoService.RegisterProgression(0, new DateTime(2025, 3, 18), 30);
    todoService.RegisterProgression(0, new DateTime(2025, 3, 19), 50);
    todoService.RegisterProgression(0, new DateTime(2025, 3, 20), 20);

    // Imprimir los ítems
    todoService.PrintItems();

    todoService.AddItem(repository.GetNextId(), "Complete Project Report", "Finish the final report for the project", "no exist");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}