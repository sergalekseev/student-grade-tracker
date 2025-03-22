using System.Windows.Input;

namespace StudentGradeTracker
{
    internal class RelayCommand<TParam> : ICommand where TParam : class
    {
        private readonly Action<TParam> _action;

        public RelayCommand(Action<TParam> action)
        {
            _action = action;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is null)
            {
                _action?.Invoke(null);
            }
            else if (parameter is TParam param)
            {
                _action?.Invoke(param);
            }
            else
            {
                // throw Exception
            }
        }
    }
}
