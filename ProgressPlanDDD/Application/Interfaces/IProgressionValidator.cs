using ProgressPlanDDD.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressPlanDDD.Application.Interfaces
{
    public interface IProgressionValidator
    {
        void Validate(IEnumerable<Progression> existing, Progression newProgression);
    }
}
