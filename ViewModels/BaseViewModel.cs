﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudentGradeTracker.ViewModels
{
    internal class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            InvokeOnPropertyChangedEvent(propertyName);
            return true;
        }

        protected void InvokeOnPropertyChangedEvent([CallerMemberName] string propertyName = "")
        {
            var args = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, args);
        }
    }
}
