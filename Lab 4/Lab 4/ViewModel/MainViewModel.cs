﻿using Lab_4.Handlers;
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
            HashSet<char> uniques = new HashSet<char>(); //Unique nodes
            //Answer for the assignment
            char finish = 'F';
            //Different graphs
            DijkstrasGraph dijkstrasgraph = new DijkstrasGraph();
            BellmanGraph bellmanGraphList;
            BellmanFord bellmanArray = new BellmanFord();


            //Get all unique chars from city collection
            foreach (var item in collection)
            {
                uniques.Add(item.From);
                uniques.Add(item.Dest);
            }

            int i = 0;
            bellmanGraphList = new BellmanGraph(uniques.Count, collection.Count);

            foreach (var unique in uniques)
            {
                //Adds to the drop down list
                FromDropdownlist.Add(unique);
                DestDropdownlist.Add(unique);

                //Dictionary for handling all routes from unique to new nodes
                Dictionary<char, int> routes = new Dictionary<char, int>();

                //Get all possible routes both from the route and from dest to route
                foreach (var route in collection.Where(s => s.From == unique))
                {
                    routes.Add(route.Dest, route.Cost);
                }

                //All paths is linked A-B then B->A
                if (IsDoubleLinked)
                {
                    foreach (var route in collection.Where(s => s.Dest == unique))
                    {
                        routes.Add(route.From, route.Cost);
                    }
                }
                
                //Add the routes to the graph
                dijkstrasgraph.AddNewVertices(unique, routes);
            }

            //Go through all edges
            foreach (var edge in collection)
            {
                //Convert routes char's alphabet to integers from 0
                int from = edge.From - 65;
                int dest = edge.Dest - 65;

                //Compare bellman graphs
                bellmanGraphList.edge[i] = new BellmanNode(edge.From, from, dest, edge.Cost);
                bellmanArray.Edge.Add(new BellmanFord.Egde(from, dest, edge.Cost, edge.From));
            }


            //**************************************************************
            //      Dijkstras & Bellman Ford
            //**************************************************************
            List<Path> answersDij = new List<Path>();
            List<int[]> bgl = new List<int[]>();
            List<int[]> bga = new List<int[]>();
            int a = 'A' - 65;

            var f = bellmanGraphList.BellmanFord(bellmanGraphList,a);
            var anwers = bellmanArray.GetShortestPath(a, uniques.Count);
            //All paths from a unique node to the answer
            foreach (var start in uniques.Where(s => s != finish))
            {
                answersDij.Add(ShortestPath.PathShortest(dijkstrasgraph.Vertices, start, finish));
            }

            //****************************************************************
            //      Creates the answer sheet
            //****************************************************************
            string spliter = "-------------------------";
            foreach (var answer in answersDij)
            {
                OutputText.Add(spliter);
                //Return string
                string textway = string.Format("{0} -> ", answer.Start);
                //Checks if there is any paths 
                if (answer.PathChars != null)
                {
                    foreach (var pathway in answer.PathChars)
                    {
                        textway = string.Concat(textway, (answer.PathChars.Last() != pathway) ? (string.Format(" {0} -> ", pathway)) : (string.Format("{0}", pathway)));
                    }
                    OutputText.Add(string.Format("[From: {0} to {1}] Dijkstras [{2}] Cost:{3} {4}", answer.Start, finish, textway, answer.Cost, 0));
                    OutputText.Add(string.Format("Bellman using lists: Cost{0}",0));
                    OutputText.Add(string.Format("Bellman using arrays: Cost{0}",0));
                }
                else //No path is possible
                {
                    OutputText.Add(string.Format("[From: {0} to {1}] No path is found. ", answer.Start, finish));
                }
                OutputText.Add(spliter);
                OutputText.Add(string.Empty);
            }
        }
    }

}
