using Lab_assignment_2.Handlers;
using Lab_assignment_2.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace Lab_assignment_2.ViewModel
{
    //http://www.codeproject.com/Articles/151161/Fuzzy-Framework
    //    //https://github.com/MicheleBertoli/DotFuzzy
    /// <summary>
    /// 
    /// </summary>
    class MainViewModel : BaseViewModel
    {
        #region Variables
        private FuzzyLogic fuzzyLogic;
        #endregion

        #region Actions
        public Action CloseAction { get; set; }
        #endregion

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

        /// <summary>
        /// 
        /// </summary>
        public ICommand SaveFile { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ICommand SaveAsFile { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ICommand NewFuzzyLogic { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ICommand Quit { get; private set; }


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

            //****************************************************************
            //      a
            //****************************************************************
            IsEnabled = false;

            //****************************************************************
            //      Fuzzy logic controller
            //****************************************************************
            fuzzyLogic = new FuzzyLogic();
            //Predefined rulebook
            //IF (x1=short V long) AND (x2=middle V long) AND (x3=middle V long ) AND (x4=middle) THEN Iris Versicolor
            //IF (x3 = short V middle) AND(x4 = short) THEN Iris Setosa
            //IF (x2=short V middle) AND (x3=long) AND (x4=long) THEN Iris Virginica
            //IF (x1=middle) AND (x2=short  middle) AND (x3=short) and (x4=long) THEN Iris Versicolor

            //Tries to add the rules and validate them
            fuzzyLogic.Rules.Add(new FuzzyLogicRule("IF (x1 IS Short OR Long) AND (x2 IS Middle OR long) AND (x3 IS Middle OR Long) AND (x4 IS Middle) THEN Iris IS Versicolor"));
            fuzzyLogic.Rules.Add(new FuzzyLogicRule("IF (x3 IS Short OR Middle) AND (x4 IS Short) THEN Iris IS Setosa"));
            fuzzyLogic.Rules.Add(new FuzzyLogicRule("IF (x2 IS Short OR Middle) AND (x3 IS Long) AND (x4 IS Long) THEN Iris IS Virginica"));
            fuzzyLogic.Rules.Add(new FuzzyLogicRule("IF (x1 IS Middle) AND (x2 IS short OR Middle) AND (x3 IS Short) and (x4 IS Long) THEN Iris IS Versicolor"));

            LinguisticTerm irisTerms = new LinguisticTerm("Iris");
            irisTerms.MembershipFunctions.Add(new MembershipFunction("Short", 0, 0, 20, 40));
            irisTerms.MembershipFunctions.Add(new MembershipFunction("Middle", 30, 50, 50, 70));
            irisTerms.MembershipFunctions.Add(new MembershipFunction("Long", 50, 80, 100, 100));

            fuzzyLogic.Linguistics.Add(irisTerms);

            //Fetches data
            //double[][] data = Data.TextFile.ReadFileToJaggedArray<double>("C:\\Users\\dss10_000\\Documents\\GitHub\\LearningSystemsLabs\\Lab 2\\iris.txt");//"D:\\LearningSystemsLabs\\Lab 2\\iris.txt");
            double[][] data = Data.TextFile.ReadFileToJaggedArray<double>("D:\\LearningSystemsLabs\\Lab 2\\iris.txt");

            //Normalize the data
            double[][] normalizeddata = MathHelper.NormilizeData(data);
            try
            {
 
               // fuzzyLogic.Defuzzify()
            }
            catch (Exception)
            {

                throw;
            }
            

            //denormalize the data


            //****************************************************************
            //      Events 
            //****************************************************************
            StartSimulation = new RelayCommand<object>(StartSimulation_Event);
            ResetSimulation = new RelayCommand<object>(ResetSimulation_Event);
            StopSimulation = new RelayCommand<object>(StopSimulation_Event);
            OpenReadFile = new RelayCommand<object>(OpenReadfile_Event);
            SaveFile = new RelayCommand<object>(Savefile_Event);
            SaveAsFile = new RelayCommand<object>(SaveAsFile_Event);
            NewFuzzyLogic = new RelayCommand<object>(NewFuzzyLogic_Event);
            Quit = new RelayCommand<object>(Quit_Event);



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

            //Action that returns 
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

        private void NewFuzzyLogic_Event(object obj)
        {
            IsEnabled = true;
        }

        private void SaveAsFile_Event(object obj)
        {
            throw new NotImplementedException();
        }

        private void Savefile_Event(object obj)
        {
            throw new NotImplementedException();
        }

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
        #endregion

        #region String Properties
        /// <summary>
        /// Path of indata file, this only used to store data. If you want to change the variable, use property set instead.
        /// </summary>
        private string filepath = string.Empty;
        /// <summary>
        /// Filepath for the learning data
        /// </summary>
        public string Filepath
        {
            get
            {
                return string.Format("Filepath: {0}",(!string.IsNullOrEmpty(filepath) ? filepath : "No file loaded."));
            }
            private set
            {
                if (filepath != value)
                {
                    filepath = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string rulesfilepath = string.Empty;
        public string RulesFilepath
        {
            get
            {
                return rulesfilepath;
            }
            set
            {
                rulesfilepath = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Boolean Properties
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


        /// <summary>
        /// Is rules loaded from an XML-file
        /// </summary>
        private bool isRulesFromFile = false;
        /// <summary>
        /// Is rules loaded from an XML-file
        /// </summary>
        public bool IsRulesFromFile
        {
            get
            {
                return isRulesFromFile;
            }
            private set
            {
                isRulesFromFile = value;
                NotifyPropertyChanged();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private bool isReady = false;
        /// <summary>
        /// 
        /// </summary>
        public bool IsReady
        {
            get { return isReady; }
            private set
            {
                if (value != isReady)
                {
                    isReady = value;
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
        public ObservableCollection<string> Rulebook
        {
            get
            {
                ObservableCollection<string> rulebook = new ObservableCollection<string>();
                if (fuzzyLogic != null)
                {
                    int i = 1;
                    foreach (var rules in fuzzyLogic.Rules)
                    {
                        rulebook.Add(string.Format("{0}. {1}", i, (!string.IsNullOrEmpty(rules.Rule) ? rules.Rule : "Error!")));
                        i++;
                    }

                }
                return rulebook;
            }
        }

        public ObservableCollection<string> LinguisticTerms
        {
            get
            {
                ObservableCollection<string> lingustics = new ObservableCollection<string>();
                if (fuzzyLogic != null)
                {
                    int i = 1;
                    foreach (var lingu in fuzzyLogic.Linguistics)
                    {
                        lingustics.Add(string.Format("{0}. {1} |Current Input:{2}|", i, (!string.IsNullOrEmpty(lingu.Name) ? lingu.Name : "Error!"), lingu.Input));
                        if (lingu.MembershipFunctions.Count > 0)
                        {
                            lingustics.Add("Membership functions");
                            lingustics.Add("-------------------------------------");
                            foreach (var membershipfunctions in lingu.MembershipFunctions)
                            {

                                lingustics.Add(string.Format("    {0} X1:{1} X2:{2} X3:{3} X4:{4}", 
                                    (!string.IsNullOrEmpty(membershipfunctions.Name) ? membershipfunctions.Name : "Error!"),
                                    membershipfunctions.X0, 
                                    membershipfunctions.X1, 
                                    membershipfunctions.X2,
                                    membershipfunctions.X3));
                            }
                            lingustics.Add("-------------------------------------");
                        }
                        i++;
                    }

                }
                return lingustics;
            }
        }
        #endregion

        #region Methods

        #endregion
    }
}
