using AppSQLite.Models;
using AppSQLite.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppSQLite.ViewModels
{
    public class TasksListViewModel : BaseViewModel
    {
        // Variables
        static TasksListViewModel instance;

        // Comandos
        private Command _NewCommand;
        public Command NewCommand => _NewCommand ?? (_NewCommand = new Command(NewAction));

        // Propiedades
        private List<TaskModel> _Tasks;
        public List<TaskModel> Tasks
        {
            get => _Tasks;
            set => SetProperty(ref _Tasks, value);
        }

        private TaskModel _TaskSelected;
        public TaskModel TaskSelected
        {
            get => _TaskSelected;
            set
            {
                if (SetProperty(ref _TaskSelected, value))
                {
                    SelectTaskAction();
                }
            }
        }

        // Constructores
        public TasksListViewModel()
        {
            // Guardamos en memoria esta misma clase (this)
            instance = this;

            // Cargamos las tareas de inicio
            LoadTasks();
        }

        // Métodos
        public static TasksListViewModel GetInstance()
        {
            // Regresamos la instancia de esta misma clase (this)
            return instance;
        }

        public async void LoadTasks()
        {
            // Bindeamos todas las tareas existentes
            Tasks = await App.TaskDatabase.GetAllTasksAsync();
        }

        private void NewAction()
        {
            // Abrimos la página de detalle en modalidad de crear una tarea
            Application.Current.MainPage.Navigation.PushAsync(new TasksDetailPage());
        }

        private void SelectTaskAction()
        {
            // Abrimos la página de detalle en modalidad de abrir una tarea existente
            Application.Current.MainPage.Navigation.PushAsync(new TasksDetailPage(TaskSelected));
        }
    }
}
