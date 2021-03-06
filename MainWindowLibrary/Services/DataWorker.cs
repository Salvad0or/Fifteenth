using MainWindowLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindowLibrary.Services
{
    /// <summary>
    /// Класс работы с файлами
    /// </summary>
    public static class DataWorker
    {
        const string path = @"DataBase.json";
        public static void SaveData(ObservableCollection<Repository> DataBase)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(DataBase));
        }
    }
}
