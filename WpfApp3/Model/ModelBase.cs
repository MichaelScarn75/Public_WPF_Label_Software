// <copyright file="ModelBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3.Model
{
    using System.ComponentModel;

    // abstract class contains virtual or abstract methods
    // abstract methods must be overriden by child class
    // whereas virtual methods are optional
    public abstract class ModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        // INotifyPropertyChanged implementation
        // Notify any listener about field error
        public void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
