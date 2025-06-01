using ProgressPlannerDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressPlanDDD.Application.Interfaces
{
    public interface ICanModifyItemPolicy
    {
        bool CanModify(TodoItem item);
    }
}
