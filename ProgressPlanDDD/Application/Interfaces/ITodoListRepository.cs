using ProgressPlannerDDD.Domain.Entities;

namespace ProgressPlanDDD.Application.Interfaces
{
    public interface ITodoListRepository
    {
        public int GetNextId();
        public IReadOnlyList<string> GetAllCategories();
    }
}
