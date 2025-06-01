using ProgressPlanDDD.Application.Interfaces;
using ProgressPlanDDD.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressPlanDDD.Domain.Validators
{
    public class ProgressionValidationStrategy : IProgressionValidator
    {
        public void Validate(IEnumerable<Progression> existing, Progression newProgression)
        {
            ValidatePercent(newProgression.Percent);
            ValidateDate(existing, newProgression.Date);
            ValidateTotalProgress(existing, newProgression.Percent);
        }

        private void ValidatePercent(decimal percent)
        {
            if (percent < 0 || percent > 100)
                throw new ArgumentOutOfRangeException(nameof(percent), "Percent must be > 0 and < 100");
        }
                                                                                                                                                                                                                                                                                                
        private void ValidateDate(IEnumerable<Progression> existing, DateTime newDate)
        {
            if (existing.Any() && newDate <= existing.Max(p => p.Date))
                throw new ArgumentException("Progression date must be newer than previous entries.");
        }

        private void ValidateTotalProgress(IEnumerable<Progression> existing,  decimal newPercent)
        {
            var total = existing.Sum(p => p.Percent) + newPercent;
            if (total > 100)
                throw new InvalidOperationException("Total progress cannot exceed 100%");
        }
    }
}
