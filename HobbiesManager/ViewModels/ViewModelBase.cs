using HobbiesManager.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HobbiesManager.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            CommandManager.InvalidateRequerySuggested();
        }

        public ICommand ComboBoxDropDownOpenedCommand { get; }
        public ICommand ComboBoxDropDownClosedCommand { get; }

        public ViewModelBase()
        {
            ComboBoxDropDownOpenedCommand = new RelayCommand(param => OnDropDownOpened());
            ComboBoxDropDownClosedCommand = new RelayCommand(param => OnDropDownClosed());
        }

        public virtual void OnDropDownOpened() { }
        public virtual void OnDropDownClosed() { }
    }
}
