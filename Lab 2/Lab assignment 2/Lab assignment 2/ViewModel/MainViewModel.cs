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
using Microsoft.Win32;

namespace Lab_assignment_2.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        #region View-Events
        /// <summary>
        /// Event for starting the simulation
        /// </summary>
        public ICommand StartSimulation { get; private set; }

        /// <summary>
        /// Event for reseting the simulation
        /// </summary>
        public ICommand ResetSimulation { get; private set; }
        
        /// <summary>
        /// Event for stopping the simulation
        /// </summary>
        public ICommand StopSimulation { get; private set; }

        /// <summary>
        /// Event for open read file dialog.
        /// </summary>
        public ICommand OpenReadFile { get; private set; }

        //TODO:
        //public IList<MenuItemViewModel> MenuItems { get; private set; }
        //http://stackoverflow.com/questions/1392160/mvvm-dynamic-menu-ui-from-binding-with-viewmodel

        #endregion

        #region Constructor
        public MainViewModel()
        {
            //****************************************************************
            //      Member variables
            //****************************************************************
            OutputText = new ObservableCollection<string>();


            //****************************************************************
            //      Events 
            //****************************************************************
            StartSimulation = new RelayCommand<object>(StartSimulation_Event);
            ResetSimulation = new RelayCommand<object>(ResetSimulation_Event);
            StopSimulation = new RelayCommand<object>(StopSimulation_Event);
            OpenReadFile = new RelayCommand<object>(OpenReadfile_Event);



        }
        #endregion


        #region Events
        /// <summary>
        /// Starts the simulation
        /// </summary>
        /// <param name="parameter"></param>
        private void StartSimulation_Event(object parameter)
        {
            OutputText.Add("Some dummy string");
            //Starts the Simulation & Enabled Stop buttom
            IsRunning = true;
            IsEnabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        private void StopSimulation_Event(object parameter)
        {
            IsRunning = false;
            IsEnabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        private void ResetSimulation_Event(object parameter)
        {
            OutputText.Clear();
            IsRunning = false;
            IsEnabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        private void OpenReadfile_Event(object parameter)
        {
            OpenFileDialog dialog = new OpenFileDialog();
        }


        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<string> OutputText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private string filepath = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string Filepath
        {
            get { return filepath; }
            private set
            {
                if (filepath != value)
                {
                    filepath = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private bool isRunning = false;
        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        private bool isEnabled = true;
        /// <summary>
        /// 
        /// </summary>
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
    }
}
