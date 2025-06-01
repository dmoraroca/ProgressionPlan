using ProgressPlanDDD.Application.Interfaces;
using ProgressPlannerDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressPlanDDD.Domain.Policies
{
    public class FiftyPercentProgressLockPolicy : ICanModifyItemPolicy
    {
        public bool CanModify(TodoItem item) => item.TotalProgress <= 50;
    }
}
