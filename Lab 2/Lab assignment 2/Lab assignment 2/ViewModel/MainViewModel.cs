using Lab_assignment_2.Handlers;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;


namespace Lab_assignment_2.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        #region View-Events
        public ICommand StartSimulation { get; private set; }
        public ICommand StopSimulation { get; private set; }
        #endregion


        #region Constructor
        public MainViewModel()
        {

            OutputText = new ObservableCollection<string>();

            StartSimulation = new RelayCommand<object>(StartSimulationEvent);
        }
        #endregion

        #region Events
        public void StartSimulationEvent(object parameter)
        {
            OutputText.Add("Some dummy string");
            
        }
        #endregion

        #region Properties
        public ObservableCollection<string> OutputText { get; set; }

        private bool isRunning = false;
        public bool IsRunning
        {
            get { return isRunning; }
            private set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool isEnabled = true;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

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
