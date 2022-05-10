using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppSQLite.Extensions
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Extensión de un Task para poderlo invocar sin la palabra await, 
        /// por ejemplo, en un constructor
        /// </summary>
        /// <param name="task">es la tarea que se están extediendo</param>
        /// <param name="returnToCallingContext">booleano que se mande al método ConfigureAwait</param>
        /// <param name="onException">Acción que se ejecuta cuando hay una excepción</param>
        public static async void SafeFireAndForget(this Task task, 
                                                   bool returnToCallingContext, 
                                                   Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(returnToCallingContext);
            }
            catch (Exception ex) when (onException != null)
            {
                onException(ex);
            }
        }
    }
}
