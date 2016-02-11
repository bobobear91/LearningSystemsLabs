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

        //List for rules
        //private ICommand removeSubjectCommand;
        //public ICommand RemoveSubjectCommand
        //{
        //    get { return removeSubjectCommand ?? (removeSubjectCommand = new RelayCommand(param => this.RemoveSubject(), null)); }
        //}
        #endregion

        #region Constructor
        public MainViewModel()
        {
            //****************************************************************
            //      Member variables
            //****************************************************************
            OutputText = new ObservableCollection<string>();
            Rulebook = new ObservableCollection<string>();
            //Predefined rulebook
            Rulebook.Add("IF (x1=short V long) AND (x2=middle V long) AND (x3=middle V long ) AND (x4=middle) THEN iris versicolor");
            Rulebook.Add("IF (x3=short V middle) AND (x4=short) THEN iris setosa ");
            Rulebook.Add("IF (x2=short V middle) AND (x3=long) AND (x4=long) THEN iris virginica");
            Rulebook.Add("IF (x1=middle) AND (x2=short  middle) AND (x3=short) and (x4=long) THEN iris versicolor");

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
        /// Path of indata file
        /// </summary>
        private string filepath = string.Empty;
        /// <summary>
        /// Filepath for the learning data
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
        /// Is application running
        /// </summary>
        private bool isRunning = false;
        /// <summary>
        /// Is application running
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
        /// Is controls enabled
        /// </summary>
        private bool isEnabled = true;
        /// <summary>
        /// Is Controls enabled
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

        #region Obserable Collections
        /// <summary>
        /// Output for fuzzy logic machine
        /// </summary>
        public ObservableCollection<string> OutputText { get; set; }

        /// <summary>
        /// The rulebook of the fuzzy logic machine
        /// </summary>
        public ObservableCollection<string> Rulebook { get; set; }
        #endregion
    }
}
