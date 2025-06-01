using ProgressPlanDDD.Application.Interfaces;
using ProgressPlannerDDD.Application.Services;
using ProgressPlannerDDD.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressPlanTests.UnitTests
{
    public class TodoListTests
    {
        private readonly ITodoListRepository _repo = new InMemoryTodoRepository();
        ITodoList todoService = new TodoService();

        [Fact]
        public void CanAddAndPrintItem()
        {
            var id = _repo.GetNextId();

            todoService.AddItem(id, "Complete Project Report", "Finish the final report for the project", "Work");
            todoService.RegisterProgression(id, new DateTime(2025, 3, 18), 30);

            var output = new StringWriter();
            Console.SetOut(output);

            todoService.PrintItems(); // verificar por consola, o capturar output si se desea

            bool itemFound = output.ToString().Contains("18/03/2025 - 30% |OOOOOOOOOOOOOOO|");

            Assert.True(itemFound);
        }


        [Fact]
        public void CanNotFindItem()
        {
            var id = _repo.GetNextId();

            todoService.AddItem(id, "Complete Project Report", "Finish the final report for the project", "Work");
            todoService.RegisterProgression(id, new DateTime(2025, 3, 18), 30);

            var output = new StringWriter();
            Console.SetOut(output);

            todoService.PrintItems(); // verificar por consola, o capturar output si se desea

            bool itemNotFound = output.ToString().Contains("19/03/2025 - 80% |OOOOOOOOOOOOOOOOOOXXOOOOOOOOOOOOOOOOOOOOOO|");

            Assert.False(itemNotFound);
        }

        [Fact]
        public void CategoryDoesnExist()
        {
            var id = _repo.GetNextId();

            try 
            {
                todoService.AddItem(id, "Complete Project Report", "Finish the final report for the project", "NonExistentCategory");
                todoService.RegisterProgression(id, new DateTime(2025, 3, 18), 30);
            }
            catch (ArgumentException ex)
            {
                Assert.Equal("Invalid category", ex.Message);
            }
        }

    }
}
