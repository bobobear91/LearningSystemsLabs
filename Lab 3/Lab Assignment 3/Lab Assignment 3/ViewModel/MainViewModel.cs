using Lab_assignment_3.Handlers;
using Lab_Assignment_3.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Lab_Assignment_3.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Private varibles
        TravelingSalesman TS;
        #endregion

        #region Private Properties
        #endregion

        #region Public Properties
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



        private bool isRunningEnabled = false;
        public bool IsRunningEnabled
        {
            get { return !isRunningEnabled; }
            private set
            {
                if (isRunningEnabled != value)
                {
                    isRunningEnabled = value;
                    NotifyPropertyChanged();
                }
            }

        }

        private bool isStartEnabled = true;
        public bool IsStartEnabled
        {
            get { return isStartEnabled; }
            private set
            {
                if (isStartEnabled != value)
                {
                    isStartEnabled = value;
                    NotifyPropertyChanged();
                }
            }

        }

        private bool isStopEnabled = false;
        public bool IsStopEnabled
        {
            get { return isStopEnabled; }
            private set
            {
                if (isStopEnabled != value)
                {
                    isStopEnabled = value;
                    NotifyPropertyChanged();
                }
            }

        }

        private bool isResetEnabled = false;
        public bool IsResetEnabled
        {
            get { return isResetEnabled; }
            private set
            {
                if (isResetEnabled != value)
                {
                    isResetEnabled = value;
                    NotifyPropertyChanged();
                }
            }

        }

        private int population = 1000; //10e3
        public int Population
        {
            get { return population; }
            set
            {
                if (population != value)
                {
                    population = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("MaxChildren");
                }
            }
        }

        private int children = 1;
        
        public int MaxChildren
        {
            get { return population; }
        }


        public int Children
        {
            get { return children; }
            set
            {
                if (children != value)
                {
                    children = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double mutationchance = 0.01;
        public double MutationChance
        {
            get { return mutationchance; }
            set
            {
                if (mutationchance != value)
                {
                    mutationchance = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private int iterations = 10000;
        public int Iterations
        {
            get { return iterations; }
            set
            {
                if (iterations != value)
                {
                    iterations = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region Actions
        public Action CloseAction { get; set; }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            // Read file with cityCoordinates. 
            Point[] cityCoordinates = Data.Converter.ArrayToPoint(Data.TextFile.ReadFileToArray<double>(AppDomain.CurrentDomain.BaseDirectory + "berlin52.tsp"));

            TS = new TravelingSalesman(cityCoordinates);
            TS.FireBestFitnessInformation += TS_FireBestFitnessInformation;
            TS.FireBestRouteInformation += TS_FireBestRouteInformation;

            //****************************************************************
            //      Events 
            //****************************************************************
            StartSimulation = new RelayCommand<object>(StartSimulation_Event);
            ResetSimulation = new RelayCommand<object>(ResetSimulation_Event);
            StopSimulation = new RelayCommand<object>(StopSimulation_Event);
        }
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        #endregion

        #region Events
        /// <summary>
        /// Closes application
        /// </summary>
        /// <param name="obj"></param>
        private void Quit_Event(object obj)
        {
            if (CloseAction != null)
            {
                CloseAction();

            }
        }

        private void StopSimulation_Event(object obj)
        {
            IsStopEnabled = false;
            IsStartEnabled = true;
            IsResetEnabled = true;

        }

        private void ResetSimulation_Event(object obj)
        {
            //Clears the graphs

            //Stops and clears
            IsStopEnabled = false;
            IsStartEnabled = true;
            IsResetEnabled = false;


            IsRunningEnabled = false;
            
        }

        private void StartSimulation_Event(object obj)
        {
            IsStopEnabled = true;
            IsStartEnabled = false;
            IsResetEnabled = true;

            IsRunningEnabled = true;
        }

        private void TS_FireBestRouteInformation(Point[] best_Route, int iteration)
        {
            //throw new NotImplementedException();
        }
        private void TS_FireBestFitnessInformation(double best_fitness, int iteration)
        {
            //throw new NotImplementedException();
        }
        #endregion
    }
}
