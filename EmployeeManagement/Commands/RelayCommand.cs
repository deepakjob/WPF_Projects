using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManagement.Commands
{
    internal class RelayCommand:ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private Action DoWork;

        public RelayCommand(Action doWork)
        { this.DoWork = doWork; }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            DoWork();
        }
    }
}
