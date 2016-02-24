using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lab_assignment_2.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    class BaseViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
