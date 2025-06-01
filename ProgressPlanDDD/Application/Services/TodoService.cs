using ProgressPlanDDD.Application.Interfaces;
using ProgressPlanDDD.Domain.Policies;
using ProgressPlanDDD.Domain.Validators;
using ProgressPlanDDD.Domain.ValueObjects;
using ProgressPlannerDDD.Domain.Entities;
using ProgressPlannerDDD.Infrastructure.Repositories;

namespace ProgressPlannerDDD.Application.Services
{
    public class TodoService : ITodoList
    {
        private Dictionary<int, TodoItem> _items = new();
        private readonly ITodoListRepository _repo = new InMemoryTodoRepository();

        public void AddItem(int id, string title, string description, string category)
        {
            if (!_repo.GetAllCategories().Contains(category))
                throw new ArgumentException("Invalid category");

            IProgressionValidator progressionValidator = new ProgressionValidationStrategy();
            ICanModifyItemPolicy modificationPolicy = new FiftyPercentProgressLockPolicy();

            var item = new TodoItem(id, title, description, category, progressionValidator, modificationPolicy);

            if (!_items.Keys.Contains(id))
            {

                _items.Add(id, item);
            }
            else
            {
                _items[id] = item;
            }

                
        }

        public void UpdateItem(int id, string description)
        {
            if (_items.TryGetValue(id, out var item))
                item.UpdateDescription(description);
        }

        public void RemoveItem(int id)
        {
            if (_items[id].TotalProgress > 50)
                throw new InvalidOperationException("Cannot remove item with > 50% progress");

            _items.Remove(id);
        }

        public void RegisterProgression(int id, DateTime dateTime, decimal percent)
        {
            _items[id].AddProgression(dateTime, percent);
        }

        public void PrintItems()
        {
            foreach (var item in _items.Values.OrderBy(i => i.Id))
            {
                Console.WriteLine($"{item.Id}) {item.Title} - {item.Description} ({item.Category}) Completed:{item.IsCompleted}");

                decimal cumulative = 0;
                foreach (var p in item.Progressions)
                {
                    cumulative += p.Percent;
                    int barLength = (int)(cumulative / 2); // max 50 O's
                    Console.WriteLine($"{p.Date.ToShortDateString()} - {cumulative}% |{new string('O', barLength)}|");
                }
            }
        }

        public Dictionary<int, TodoItem>  GetAll()
        {
            return _items;
        }

        public void ClearItems()
        {
            _items = new();
        }
    }
}