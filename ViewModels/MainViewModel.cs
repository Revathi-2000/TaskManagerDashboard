using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using TaskManagerDashboard.Commands;
using TaskManagerDashboard.Interfaces;
using TaskManagerDashboard.Models;
using TaskManagerDashboard.Services;


namespace TaskManagerDashboard.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _taskTitle;
        private string _selectedPriority;
        private TaskItem _selectedTask;
        private string _selectedFilter;
        private string _searchText;
        private string _selectedSort;


        public ObservableCollection<TaskItem> Tasks { get; set; }
        public List<string> Priorities { get; set; }
        public List<string> Filters { get; set; }

        public List<string> SortOptions { get; set; }

        private readonly IStorageService _storageService;

        public string TaskTitle
        {
            get => _taskTitle;
            set
            {
                _taskTitle = value;
                OnPropertyChanged(nameof(TaskTitle));
            }
        }

        public string SelectedPriority
        {
            get => _selectedPriority;
            set
            {
                _selectedPriority = value;
                OnPropertyChanged(nameof(SelectedPriority));
            }
        }
        public TaskItem SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }
       

        public string SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                OnPropertyChanged(nameof(SelectedFilter));

                if (TaskView != null)
                {
                    TaskView.Refresh();
                }
            }
        }
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));

                if (TaskView != null)
                {
                    TaskView.Refresh();
                }
            }
        }
        public string SelectedSort
        {
            get => _selectedSort;
            set
            {
                _selectedSort = value;
                OnPropertyChanged(nameof(SelectedSort));

                ApplySorting();
            }
        }

        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand CompleteTaskCommand { get; }
        public ICollectionView TaskView { get; set; }

        public MainViewModel(IStorageService storageService)
        {
            _storageService = new JsonStorageService();
            Tasks = new ObservableCollection<TaskItem>();

            Priorities = new List<string>
            {
                "Low",
                "Medium",
                "High"
            };
            Filters = new List<string>
            {
                "All",
                "Completed",
                "Pending"
            };
            SortOptions = new List<string>
            {
                "None",
                "Title",
                "Priority"
            };

            SelectedSort = "None";
            TaskView = CollectionViewSource.GetDefaultView(Tasks);
            TaskView.Filter = FilterTasks;

            SelectedFilter = "All";

            AddTaskCommand =
                new RelayCommand(AddTask);

            DeleteTaskCommand =
                new RelayCommand(DeleteTask);

            CompleteTaskCommand =
                new RelayCommand(MarkComplete);
            _=LoadTasksAsync(); // this method is async and I'm intentionally not awaiting it


        }
        private async Task SaveTasksAsync()
        {
            await _storageService.SaveAsync(Tasks.ToList());
        }
        private async Task LoadTasksAsync()
        {
            var loadedTasks = await _storageService.LoadAsync();

            foreach (var task in loadedTasks)
            {
                Tasks.Add(task);
            }
        }

        private async void AddTask(object parameter)
        {
            if (string.IsNullOrWhiteSpace(TaskTitle))
                return;

            Tasks.Add(new TaskItem
            {
                Title = TaskTitle,
                Priority = SelectedPriority,
                IsCompleted = false
            });

            await SaveTasksAsync();

            TaskTitle = string.Empty;
            SelectedPriority = null;
        }
        private async void DeleteTask(object parameter)
        {
            if (SelectedTask == null)
                return;

            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to delete?",
                "Confirm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Tasks.Remove(SelectedTask);

                await SaveTasksAsync();
            }
        }
        private async void MarkComplete(object parameter)
        {
            if (SelectedTask != null)
            {
                SelectedTask.IsCompleted = true;

                TaskView.Refresh();

                await SaveTasksAsync();
            }
        }
        private bool FilterTasks(object obj)
        {
            TaskItem task = obj as TaskItem;

            if (task == null)
                return false;

            bool filterResult = true;

            switch (SelectedFilter)
            {
                case "Completed":
                    filterResult = task.IsCompleted;
                    break;

                case "Pending":
                    filterResult = !task.IsCompleted;
                    break;
            }

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filterResult = filterResult &&
                               task.Title.ToLower()
                                   .Contains(SearchText.ToLower());
            }

            return filterResult;
        }
        private void ApplySorting()
        {
            if (TaskView == null)
                return;

            TaskView.SortDescriptions.Clear();

            switch (SelectedSort)
            {
                case "Title":
                    TaskView.SortDescriptions.Add(
                        new System.ComponentModel.SortDescription(
                            "Title",
                            System.ComponentModel.ListSortDirection.Ascending));
                    break;

                case "Priority":
                    TaskView.SortDescriptions.Add(
                        new System.ComponentModel.SortDescription(
                            "Priority",
                            System.ComponentModel.ListSortDirection.Ascending));
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}