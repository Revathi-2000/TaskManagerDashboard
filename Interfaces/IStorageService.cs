using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDashboard.Models;

namespace TaskManagerDashboard.Interfaces
{
    public interface IStorageService
    {
        Task SaveAsync(List<TaskItem> tasks);
        Task<List<TaskItem>> LoadAsync();
    }
}
