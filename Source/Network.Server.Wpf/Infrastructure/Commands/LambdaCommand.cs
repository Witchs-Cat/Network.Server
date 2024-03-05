using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Network.Server.Wpf.Infrastructure.Commands
{
    public class LambdaCommand : BaseCommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool> _canExecute;

        public LambdaCommand(Action<object?> execute) : this(execute, (parametr) => true) 
        { }

        public LambdaCommand(Action<object?> execute, Func<object?, bool> canExecute)
        {
            ArgumentNullException.ThrowIfNull(execute);
            ArgumentNullException.ThrowIfNull(canExecute);

            _execute = execute;
            _canExecute = canExecute;
        }

        public override bool CanExecute(object? parameter) => _canExecute(parameter);
        public override void Execute(object? parameter) => _execute(parameter);
    }
}
