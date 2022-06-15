using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Fourteeth.ViewModel.Base
{
    internal class ViewModels : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;



        public virtual void OnPropertyChanged([CallerMemberNameAttribute] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
