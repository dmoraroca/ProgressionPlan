using ProgressPlannerDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressPlanDDD.Application.Interfaces
{
    public interface ITodoList
    {
        void AddItem(int id, string title, string description, string category);
        void UpdateItem(int id, string newDescription);
        void RemoveItem(int id);
        void RegisterProgression(int id, DateTime date, decimal percent);
        void PrintItems();

        // necesarias
        public Dictionary<int, TodoItem> GetAll();
        public void ClearItems();
    }
}
