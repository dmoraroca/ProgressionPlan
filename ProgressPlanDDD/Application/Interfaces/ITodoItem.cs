using ProgressPlanDDD.Domain.ValueObjects;

namespace ProgressPlanDDD.Domain.Shared
{
    public interface ITodoItem
    {
        string Category { get; }
        string Description { get; }
        bool IsCompleted { get; }
        IReadOnlyCollection<Progression> Progressions { get; }
        string Title { get; }
    }
}