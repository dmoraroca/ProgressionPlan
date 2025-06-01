using ProgressPlanDDD.Application.Interfaces;
using ProgressPlannerDDD.Domain.Entities;

namespace ProgressPlannerDDD.Infrastructure.Repositories
{
    public class InMemoryTodoRepository : ITodoListRepository
    {
        private readonly Dictionary<int, TodoItem> _storage = new();
        private int _idCounter = 0;
        private readonly List<string> _categories = new() { "Work", "Home", "Health" };

        public int GetNextId()
        {
            return _idCounter++;
        }

        public IReadOnlyList<string> GetAllCategories()
        {
            return _categories;
        }
    }
}