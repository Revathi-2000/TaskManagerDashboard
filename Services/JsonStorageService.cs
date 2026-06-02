using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManagerDashboard.Interfaces;
using TaskManagerDashboard.Models;

namespace TaskManagerDashboard.Services
{
    public class JsonStorageService : IStorageService
    {
        private readonly string _filePath = "tasks.json";  //TaskManagerDashboard\TaskManagerDashboard\bin\Debug Json stored in this path

        public async Task SaveAsync(List<TaskItem> tasks)
        {
            await Task.Run(() =>
            {
                string json = JsonSerializer.Serialize(tasks,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

                File.WriteAllText(_filePath, json);
            });
        }

        public async Task<List<TaskItem>> LoadAsync()
        {
            return await Task.Run(() =>
            {
                if (!File.Exists(_filePath))
                {
                    return new List<TaskItem>();
                }

                string json = File.ReadAllText(_filePath);

                return JsonSerializer.Deserialize<List<TaskItem>>(json)
                       ?? new List<TaskItem>();
            });
        }
    }
}
