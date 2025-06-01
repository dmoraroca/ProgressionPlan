using ProgressPlanDDD.Application.Interfaces;
using ProgressPlanDDD.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressPlanDDD.Domain.Events
{
    public class ProgressionRegisteredEvent : IDomainEvent
    {
        public int TodoItemId { get; }
        public Progression Progression { get; }

        public DateTime OccurredOn => throw new NotImplementedException();

        public ProgressionRegisteredEvent(int itemId, Progression progression)
        {
            TodoItemId = itemId;
            Progression = progression;
        }
    }
}
