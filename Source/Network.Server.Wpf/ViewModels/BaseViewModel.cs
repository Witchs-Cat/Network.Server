using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Network.Server.Wpf.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? properyName = default)
        {   
            PropertyChangedEventArgs args = new(properyName);
            PropertyChanged?.Invoke(this, args);    
        }

        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string? fieldName = null)
        {
            field = value;
            OnPropertyChanged(fieldName);
        }
    }
}
