using Lab_4.Handlers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Lab_4.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Private varibles
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

        private bool _isEnabledType = true;
        public bool IsEnabledType
        {
            get { return _isEnabledType; }
            set
            {
                if (_isEnabledType != value)
                {
                    _isEnabledType = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private bool _isEnabledfrom = true;
        public bool IsEnabledFrom
        {
            get { return _isEnabledfrom; }
            set
            {
                if (_isEnabledfrom != value)
                {
                    _isEnabledfrom = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool _isEnabledDest = true;
        public bool IsEnabledDest
        {
            get { return _isEnabledDest; }
            set
            {
                if (_isEnabledDest != value)
                {
                    _isEnabledDest = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public enum Type { One,Two}
        public ObservableCollection<Type> Types { get; set; }
        public ObservableCollection<string> From { get; set; }
        public ObservableCollection<string> Dest { get; set; }
        //private int population = 1000; //10e3
        //public int Population
        //{
        //    get { return population; }
        //    set
        //    {
        //        if (population != value)
        //        {
        //            population = value;
        //            NotifyPropertyChanged();
        //            NotifyPropertyChanged("MaxChildren");
        //        }
        //    }
        //}

        //private int children = 1;

        //public int MaxChildren
        //{
        //    get { return population; }
        //}


        //public int Children
        //{
        //    get { return children; }
        //    set
        //    {
        //        if (children != value)
        //        {
        //            children = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

        //private double mutationchance = 0.01;
        //public double MutationChance
        //{
        //    get { return mutationchance; }
        //    set
        //    {
        //        if (mutationchance != value)
        //        {
        //            mutationchance = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}


        //private int iterations = 10000;
        //public int Iterations
        //{
        //    get { return iterations; }
        //    set
        //    {
        //        if (iterations != value)
        //        {
        //            iterations = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}


        #endregion

        #region Actions
        public Action CloseAction { get; set; }
        #endregion

        #region Constructor
        public MainViewModel()
        {

            ShortestPath.CreateFile();
            CityNodeCollection collection = Data.XML.DeserializeFromFile<CityNodeCollection>("city 1.xml");
            HashSet<string> uniques = new HashSet<string>();
            foreach (var item in collection)
            {
                uniques.Add(item.From);
                uniques.Add(item.Dest);
            }

            From = new ObservableCollection<string>();
            Dest = new ObservableCollection<string>();
            foreach (var item in uniques)
            {
                From.Add(item);
                Dest.Add(item);
            }

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
            throw new NotImplementedException();
        }
        private void TS_FireBestFitnessInformation(double best_fitness, int iteration)
        {
            throw new NotImplementedException();
        }
        #endregion

    }

}
