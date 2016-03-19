using Lab_4.Handlers;
using Lab_4.Models;
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
        CityNodeCollection collection;
        Dictionary<char, int> DennisAlphabet;
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

        private bool isbellman = true;
        public bool IsBellman
        {
            get { return isbellman; }
            set
            {
                if (isbellman != value)
                {
                    isbellman = value;
                    IsBellmanNo = !value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool isbellmanNo = false;
        public bool IsBellmanNo
        {
            get { return isbellmanNo; }
            set
            {
                if (isdoublelinkedNo != value)
                {
                    isbellmanNo = value;
                    IsBellman = !value;
                    NotifyPropertyChanged();
                }
            }
        }



        private bool isdoublelinked = true;
        public bool IsDoubleLinked
        {
            get { return isdoublelinked; }
            set
            {
                if (isdoublelinked != value)
                {
                    isdoublelinked = value;
                    IsDoubleLinkedNo = !value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool isdoublelinkedNo = false;
        public bool IsDoubleLinkedNo
        {
            get { return isdoublelinkedNo; }
            set
            {
                if (isdoublelinkedNo != value)
                {
                    isdoublelinkedNo = value;
                    IsDoubleLinked = !value;
                    NotifyPropertyChanged();
                }
            }
        }



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
        public ObservableCollection<Type> TypesDropdownlist { get; set; }
        public ObservableCollection<char> FromDropdownlist { get; set; }
        public ObservableCollection<char> DestDropdownlist { get; set; }
        public ObservableCollection<string> OutputText { get; set; }
        #endregion

        #region Actions
        public Action CloseAction { get; set; }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            //**************************************************************
            //      Initialize GUI-Variables
            //**************************************************************
            FromDropdownlist = new ObservableCollection<char>();
            DestDropdownlist = new ObservableCollection<char>();
            OutputText = new ObservableCollection<string>();

            //**************************************************************
            //      Graph and loading the file
            //**************************************************************
            ShortestPath.CreateFile(); //Creates an xml file of the textfile
            collection = Data.XML.DeserializeFromFile<CityNodeCollection>("city 1.xml"); //Collection of all the paths
            HashSet<char> uniques = new HashSet<char>(); //Unique nodes
            DennisAlphabet = new Dictionary<char, int>();
            //Get all unique chars from city collection
            foreach (var item in collection)
            {
                uniques.Add(item.From);
                uniques.Add(item.Dest);
            }
            int i = 0;
            
            foreach (var unique in uniques)
            {
                //Adds to the drop down list
                FromDropdownlist.Add(unique);
                DestDropdownlist.Add(unique);

            }
            List<char> characters = new List<char>(FromDropdownlist);
            characters.Sort();
            foreach (var item in characters)
            {
                DennisAlphabet.Add(item, i);
                i++;
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
            //IsStopEnabled = true;
            //IsStartEnabled = false;
            //IsResetEnabled = true;

            //IsRunningEnabled = true;
            OutputText.Clear();
            RunApplication();
        }
        #endregion


        private void RunApplication()
        {
            //Answer for the assignment
            char finish = 'F';
            int doubledges = 0;
            //Different graphs
            DijkstrasGraph dijkstrasgraph = new DijkstrasGraph();
            BellmanGraph bellmanGraph;

            foreach (var unique in FromDropdownlist)
            {
                //Dictionary for handling all routes from unique to new nodes
                Dictionary<char, int> routes = new Dictionary<char, int>();

                //Get all possible routes both from the route and from dest to route
                foreach (var route in collection.Where(s => s.From == unique))
                {
                    routes.Add(route.Dest, route.Cost);
                    doubledges++;
                }

                //All paths is linked A-B then B->A
                if (IsDoubleLinked)
                {
                    foreach (var route in collection.Where(s => s.Dest == unique))
                    {
                        routes.Add(route.From, route.Cost);
                        doubledges++;
                    }
                }
                //Add the routes to the graph
                dijkstrasgraph.AddNewVertices(unique, routes);
            }
            int i = 0;
            //Checks if bellman is double linked 
            if (IsDoubleLinked)
            {
                bellmanGraph = new BellmanGraph(dijkstrasgraph.Vertex, doubledges);
                //Go through all edges
                foreach (var edge in collection)
                {
                    //Compare bellman graphs
                    bellmanGraph.edge[i] = new BellmanNode(edge.From, DennisAlphabet[edge.From], DennisAlphabet[edge.Dest], edge.Cost);
                    i++;
                    bellmanGraph.edge[i] = new BellmanNode(edge.Dest, DennisAlphabet[edge.Dest], DennisAlphabet[edge.From], edge.Cost);
                    i++;          
                }

            }
            else
            {
                //Vertices = 23, collection 35
                bellmanGraph = new BellmanGraph(FromDropdownlist.Count, collection.Count);
                //Go through all edges
                foreach (var edge in collection)
                {
                    //Compare bellman graphs
                    bellmanGraph.edge[i] = new BellmanNode(edge.From, DennisAlphabet[edge.From], DennisAlphabet[edge.Dest], edge.Cost);
                    i++;
                }

            }


            //**************************************************************
            //      Dijkstras & Bellman Ford
            //**************************************************************
            List<Path> answersDij = new List<Path>();
            List<int[]> bga = new List<int[]>();

            //STOPWATCH FOR Dijkstras
            foreach (var start in FromDropdownlist.Where(s => s != finish))
            {
                answersDij.Add(ShortestPath.PathShortest(dijkstrasgraph.Graph, start, finish));
            }
            //STOP

            //STOPWATCH FOR Alphabet
            foreach (var start in FromDropdownlist.Where(s => s != finish))
            {
                bga.Add(ShortestPath.BellmanFord(bellmanGraph, DennisAlphabet[start]));
            }
            //STOP

            //****************************************************************
            //      Creates the answer sheet
            //****************************************************************
            string spliter = "-------------------------";
            for (int x = 0; x < answersDij.Count; x++)
            {
                OutputText.Add(spliter);
                //Return string
                string textway = string.Format("{0} -> ", answersDij[x].Start);
                //Checks if there is any paths 
                if (answersDij[x].PathChars != null)
                {
                    foreach (var pathway in answersDij[x].PathChars)
                    {
                        textway = string.Concat(textway, (answersDij[x].PathChars.Last() != pathway) ? (string.Format(" {0} -> ", pathway)) : (string.Format("{0}", pathway)));
                    }

                    OutputText.Add(string.Format("[From: {0} to {1}] Dijkstras [{2}] Cost:{3} ", answersDij[x].Start, finish, textway, answersDij[x].Cost));
                    if (IsBellman)
                    {
                        OutputText.Add(string.Format("Bellman using arrays: Cost: {0}", bga[x][5]));
                    }
                }
                else //No path is possible
                {
                    OutputText.Add(string.Format("[From: {0} to {1}] No path is found. ", answersDij[x].Start, finish));
                }
                OutputText.Add(spliter);
                OutputText.Add(string.Empty);
            }
        }
    }

}
