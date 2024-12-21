using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.DataBase
{
    public class data
    {
        public static Upravlenie_bibliotekoyEntities _dbConnection = new Upravlenie_bibliotekoyEntities();

        // Метод для получения подключения к базе данных
        public static Upravlenie_bibliotekoyEntities GetContext()
        {
            if (_dbConnection == null)
            {
                _dbConnection = new Upravlenie_bibliotekoyEntities();
            }
            return _dbConnection;
        }
    }
}
