using MainWindowLibrary.ViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindowLibrary.Models
{
    /// <summary>
    /// Хранилище для всех клиентов
    /// </summary>
    public class Repository : ViewModels
    {
        public string Name { get; set; }
        public ObservableCollection<Clients> DataBase { get; set; }


    }
}
