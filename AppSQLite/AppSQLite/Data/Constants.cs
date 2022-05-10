using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppSQLite.Data
{
    public static class Constants
    {
        // Constante para abrir nuestro archivo SQLite en modo de lectura-escritura, crearlo si no existe y sea accesible en multihilo
        public const SQLite.SQLiteOpenFlags Flags = SQLite.SQLiteOpenFlags.ReadWrite |
                                                    SQLite.SQLiteOpenFlags.Create |
                                                    SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                // Formamos la ruta completa donde se guardará el archivo SQLite
                string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, "AppSQLite.db3");
            }
        }
    }
}
