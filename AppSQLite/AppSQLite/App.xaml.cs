using AppSQLite.Data;
using AppSQLite.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppSQLite
{
    public partial class App : Application
    {
        // Propiedad estática para instanciar y regresar la variable de SQLite
        private static TaskDatabase taskDatabase;
        public static TaskDatabase TaskDatabase
        {
            get
            {
                if (taskDatabase == null) taskDatabase = new TaskDatabase();
                return taskDatabase;
            }
        }

        public App()
        {
            InitializeComponent();

            // Asignamos colores desde los recursos de App.xaml y abrimos la página del listado de tareas
            NavigationPage nav = new NavigationPage(new TasksListPage());
            nav.BarBackgroundColor = (Color)App.Current.Resources["BackgroundColor"];
            nav.BarTextColor = (Color)App.Current.Resources["BarTextColor"];
            MainPage = nav;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
