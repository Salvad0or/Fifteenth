using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Fourteeth.ViewModel.Base;

namespace Fourteeth.Model
{
    internal class Repository : ViewModels
    {
        public string Name { get; set; }
        public ObservableCollection<Clients> DataBase { get; set; }
      
                
    }
}
