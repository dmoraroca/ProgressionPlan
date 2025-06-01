using ProgressPlanDDD.Application.Interfaces;
using ProgressPlanDDD.Domain.Policies;
using ProgressPlanDDD.Domain.Shared;
using ProgressPlanDDD.Domain.ValueObjects;


namespace ProgressPlannerDDD.Domain.Entities
{
    public class TodoItem : BaseEntity, IAggregateRoot
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }

        private readonly List<Progression> _progressions = new();
        public IReadOnlyCollection<Progression> Progressions => _progressions.AsReadOnly();

        public decimal TotalProgress => _progressions.Sum(p => p.Percent);
        public bool IsCompleted => TotalProgress >= 100;

        private readonly IProgressionValidator _progressionValidator;
        private readonly ICanModifyItemPolicy _modificationPolicy;

        public TodoItem(int id, string title, string description, string category,
                        IProgressionValidator progressionValidator,
                        ICanModifyItemPolicy modificationPolicy)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required");
            if (string.IsNullOrWhiteSpace(category)) throw new ArgumentException("Category is required");

            Id = id;
            Title = title;
            Description = description;
            Category = category;

            _progressionValidator = progressionValidator;
            _modificationPolicy = modificationPolicy;
        }

        public void UpdateDescription(string newDescription)
        {
            if (!_modificationPolicy.CanModify(this))
                throw new InvalidOperationException("Cannot modify item with over 50% progress.");

            if (string.IsNullOrWhiteSpace(newDescription))
                throw new ArgumentException("Description cannot be empty.");

            Description = newDescription;
        }

        public void AddProgression(DateTime date, decimal percent)
        {
            var newProg = new Progression(date, percent);
            _progressionValidator.Validate(_progressions, newProg);
            _progressions.Add(newProg);
        }
    }
}