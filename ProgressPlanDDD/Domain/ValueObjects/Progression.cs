﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressPlanDDD.Domain.ValueObjects
{
    public class Progression
    {
        public DateTime Date { get; }
        public decimal Percent { get; }

        public Progression(DateTime date, decimal percent)
        {
            if (percent <= 0 || percent > 100)
                throw new ArgumentException("Progress must be between 0 and 100.");

            Date = date;
            Percent = percent;
        }

        // Igualdad estructural
        public override bool Equals(object obj) => obj is Progression p && p.Date == Date && p.Percent == Percent;
        public override int GetHashCode() => HashCode.Combine(Date, Percent);
    }
}
