using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainWindowLibrary;



namespace Fifteenth_Module.ViewModel
{
    internal class MainWindowViewModel
    {
        private View _view;

        public View ViewModel { get;}

        
        public MainWindowViewModel()
        {
            ViewModel = new View();
        }

       
    }
}
