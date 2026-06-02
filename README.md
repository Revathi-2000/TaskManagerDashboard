# **Task Manager Dashboard**

A modern desktop-based **Task Management Application** developed using **C#**, **WPF**, and the **MVVM (Model-View-ViewModel)** architecture pattern.

This application helps users efficiently manage daily tasks by providing features such as task creation, searching, filtering, sorting, completion tracking, and deletion. The project demonstrates core WPF concepts including Data Binding, Commands, ObservableCollection, and JSON-based local data storage.

---

# **Features**

* Add new tasks with priority levels
* Search tasks dynamically
* Filter tasks based on task status
* Sort tasks for better organization
* Mark tasks as completed
* Delete tasks
* Real-time UI updates using **INotifyPropertyChanged**
* Command handling using **ICommand** and **RelayCommand**
* Local data persistence using **JSON Serialization**
* Clean and maintainable **MVVM Architecture**

---

# **Technologies Used**

* **C#**
* **WPF (Windows Presentation Foundation)**
* **MVVM Design Pattern**
* **.NET Framework 4.7.2**
* **XAML**
* **JSON Serialization**

---

# **Project Structure**

```text
TaskManagerDashboard
│
├── Commands
│   └── RelayCommand.cs
│
├── Interfaces
│   └── IStorageService.cs
│
├── Models
│   └── TaskItem.cs
│
├── Services
│   └── JsonStorageService.cs
│
├── ViewModels
│   └── MainViewModel.cs
│
├── MainWindow.xaml
├── MainWindow.xaml.cs
└── App.xaml
```

---

# **Concepts Implemented**

## **MVVM Architecture**

* Separates UI, Business Logic, and Data Access.
* Improves maintainability, scalability, and testability.

## **Data Binding**

* Utilized WPF Data Binding to synchronize UI elements with ViewModel properties.

## **ICommand**

* Implemented command-based actions using **ICommand** and **RelayCommand**.

## **INotifyPropertyChanged**

* Enables automatic UI updates whenever property values change.

## **ObservableCollection**

* Automatically refreshes the DataGrid when tasks are added or removed.

## **JSON Storage**

* Stores task data locally using JSON Serialization for persistence.

---

# **Application Functionalities**

## **Add Task**

Users can create a new task by providing:

* Task Title
* Priority Level

The task is added to the dashboard and saved locally.

## **Search Tasks**

* Real-time search functionality.
* Dynamically filters tasks as the user types.

## **Filter Tasks**

Users can filter tasks based on status:

* All Tasks
* Completed Tasks
* Pending Tasks

## **Sort Tasks**

Tasks can be sorted based on selected criteria for better organization and accessibility.

## **Mark as Completed**

Users can mark selected tasks as completed, and the UI updates instantly.

## **Delete Task**

Users can remove unwanted tasks from the dashboard, and changes are automatically saved.

---

# **Key WPF Concepts Used**

* MVVM Pattern
* Data Binding
* ICommand
* RelayCommand
* INotifyPropertyChanged
* ObservableCollection
* CollectionViewSource
* DataGrid
* JSON Serialization

---

# **Future Enhancements**

* Task Due Dates
* Task Categories
* Task Editing Functionality
* Dark/Light Theme Support
* SQL Server Integration
* Dependency Injection
* Unit Testing

---

# **How to Run**

## **Clone the Repository**

```bash
git clone <repository-url>
```

## **Run the Application**

1. Open the solution in Visual Studio.
2. Build the project.
3. Press **F5** to run the application.

---

