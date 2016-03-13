using Lab_assignment_3.Handlers;
using Lab_Assignment_3.GA;
using Lab_Assignment_3.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Lab_Assignment_3.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Private varibles
        TravelingSalesmanAlgorithm TS;
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

        public ObservableCollection<Point> Cities { get; set; }
        public ObservableCollection<Point> Route { get; set; }
        public ObservableCollection<Point> Home { get; set; }
        public ObservableCollection<Point> Fitness { get; set; }

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

        private int population = 100; //10e3
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

        private int children = 50;
        
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
            List<Point> cityLayout = new List<Point>();
            for (int i = 0; i < cityCoordinates.Length; i++)
            {
                cityLayout.Add(cityCoordinates[i]);
            }     

            TS = new TravelingSalesmanAlgorithm(cityLayout);
            TS.AlgorithmIsDone_Event += TS_AlgorithmIsDone_Event;

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(UpdateCharts_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            dispatcherTimer.Start();

            // tmp
            Point[,] tmpArray = new Point[7, 10];
            int x = 0;
            int y = 0;
            for (int i = 0; i < tmpArray.GetLength(0); i++)
            {
                y += 160;
                x = 0;
                for (int j = 0; j < tmpArray.GetLength(1); j++)
                {
                    x += 160;
                    tmpArray[i, j] = new Point(x, y);
                }
            }

            Point[,] fixedArray = new Point[tmpArray.Length, 1];
            int index = 0;
            for (int i = 0; i < tmpArray.GetLength(0); i++)
            {
                for (int j = 0; j < tmpArray.GetLength(1); j++)
                {
                    fixedArray[index, 0] = tmpArray[i, j];
                    index++;
                }
            }

            Data.TextFile.WritePointArrayToFile("testLayout", fixedArray, true);

            //****************************************************************
            //      ObservableProperties
            //****************************************************************
            Cities = new ObservableCollection<Point>();
            for (int i = 0; i < cityCoordinates.GetLength(0); i++)
            {
                Cities.Add(cityCoordinates[i]);
            }
            
            Route = new ObservableCollection<Point>();
            Home = new ObservableCollection<Point>();
            Fitness = new ObservableCollection<Point>();

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
            // Setup UI States
            IsStopEnabled = false;
            IsStartEnabled = true;
            IsResetEnabled = true;
            IsRunningEnabled =false;

            // Stop Algorithm
            TS.StopAlgorithm();
        }

        private void ResetSimulation_Event(object obj)
        {
            if (TS.IsRunning)
            {
                // if algorithm is Running. do nothing.
                return;
            }

            // Setup UI States
            IsStopEnabled = false;
            IsStartEnabled = true;
            IsResetEnabled = false;         
            IsRunningEnabled = false;

            // Clear charts
            Fitness.Clear();
            Home.Clear();
            Route.Clear();

            // Reset Population
            TS.ResetPopulation();
        }

        private void StartSimulation_Event(object obj)
        {
            // Check UI Values
            if (TS.PopulationSize != Population)
            {
                // Reset Population
                TS.PopulationSize = Population;
                TS.ResetPopulation();
            }
            if (TS.CityLayout.Count != Cities.Count)
            {
                // Reset Population
                // Add file for cities
            }

            // Setup algorithm configuration
            TS.ReproductionVolume = Children;
            TS.MutationChance = MutationChance;

            // Setup UI State
            IsStopEnabled = true;
            IsStartEnabled = false;
            IsResetEnabled = false;
            IsRunningEnabled = true;
            Fitness.Clear();

            // Start Algorithm
            TS.StartAlgorithm(Iterations);
        }

        private void TS_AlgorithmIsDone_Event(double bestFitness)
        {
            MessageBox.Show(string.Format("Done! Best-fitness: {0}!", bestFitness));
            // Setup UI States
            IsStopEnabled = false;
            IsStartEnabled = true;
            IsResetEnabled = true;
            IsRunningEnabled = false;
        }

        public void LoadCityFileDialog(object sender, RoutedEventArgs e)
        {
            if (TS.IsRunning)
            {
                return;
            }

            string path;
            OpenFileDialog loadFile_Dialog = new OpenFileDialog();
            loadFile_Dialog.Title = "New Layout";
            loadFile_Dialog.Filter = "Tsp Files|*.tsp|Txt Files|*.txt";

            try
            {
                loadFile_Dialog.ShowDialog();
                path = loadFile_Dialog.FileName; 

                if (Data.FileExist(path))
                {
                    Point[] test = new Point[1];

                    Point[] cityCoordinates = Data.Converter.ArrayToPoint(Data.TextFile.ReadFileToArray<double>(path));
                    if (cityCoordinates != null)
                    {
                        List<Point> cityLayout = new List<Point>();
                        Cities.Clear();
                        for (int i = 0; i < cityCoordinates.Length; i++)
                        {
                            cityLayout.Add(cityCoordinates[i]);
                            Cities.Add(cityCoordinates[i]);
                        }

                        TS.CityLayout = cityLayout;
                        TS.ResetPopulation();
                    }
                }
            }
            catch {
                MessageBox.Show(string.Format("Ops! Something went wrong, try again later."));
            }
        }

        // TODO: do binding instead. 
        bool doSlowDown = false;
        public void ToggleSlowDown_Event(object sender, RoutedEventArgs e)
        {
            doSlowDown = !doSlowDown;
            TS.DoSlowDown = doSlowDown;
        }

        private void UpdateCharts_Tick(object sender, EventArgs e)
        {
            if (TS.IsRunning)
            {
                // Update Route
                Point[] tmp = TS.GetBestRoute();
                Route.Clear();
                for (int i = 0; i < tmp.GetLength(0); i++)
                {
                    Route.Add(tmp[i]);
                }
                // Update Home
                Home.Clear();
                Home.Add(Route[0]);
                // Update Fitness
                Point fitness = TS.GetBestFitness();
                Fitness.Add(fitness);
            }
        }
        #endregion      
    }
}
