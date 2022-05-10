using AppSQLite.Extensions;
using AppSQLite.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSQLite.Data
{
    public class TaskDatabase
    {
        // Instanciamos y abrimos la conexión a SQLite donde Lazy nos permite generarla sin que se bloquee nuestra app
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() => 
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        // Variable estática para regresa la conexión de SQLite
        static SQLiteAsyncConnection Database => lazyInitializer.Value;

        // Variable estática para saber si la base de datos de SQLite está inicializada
        static bool isInitialized = false;

        // Constructor
        public TaskDatabase()
        {
            // LLamamos el método de inicializar con la extensión de llamado seguro
            InitializeAsync().SafeFireAndForget(false);
        }

        // Método para crear la tabla de tasks si no existe
        async Task InitializeAsync()
        {
            if (!isInitialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(TaskModel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(TaskModel)).ConfigureAwait(false);
                    isInitialized = true;
                }
            }
        }

        // Métodos CRUD para TaskModel
        public Task<List<TaskModel>> GetAllTasksAsync()
        {
            // Todas las tareas
            return Database.Table<TaskModel>().ToListAsync();
        }

        public Task<TaskModel> GetTaskAsync(int id)
        {
            // Una sola tarea con base al ID
            return Database.Table<TaskModel>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<List<TaskModel>> GetTasksNotDoneAsync()
        {
            // Tareas pendientes
            return Database.QueryAsync<TaskModel>($"SELECT * FROM [{typeof(TaskModel).Name}] WHERE [Done] = 0");
        }

        public Task<List<TaskModel>> GetTasksDoneAsync()
        {
            // Tareas finalizadas
            return Database.QueryAsync<TaskModel>($"SELECT * FROM [{typeof(TaskModel).Name}] WHERE [Done] = 1");
        }

        public Task<int> SaveTaskAsync(TaskModel model)
        {
            if (model.ID == 0)
            {
                // Crear
                return Database.InsertAsync(model);
            }
            else
            {
                // Actualizar
                return Database.UpdateAsync(model);
            }
        }

        public Task<int> DeleteTaskAsync(TaskModel model)
        {
            // Eliminar
            return Database.DeleteAsync(model);
        }
    }
}
