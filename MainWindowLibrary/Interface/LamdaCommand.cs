using MainWindowLibrary.Interface.Base_command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindowLibrary.Interface
{
    
    public class LamdaCommand : BaseCommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public LamdaCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public override bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;


        public override void Execute(object? parameter) => _execute(parameter);

    }
}
