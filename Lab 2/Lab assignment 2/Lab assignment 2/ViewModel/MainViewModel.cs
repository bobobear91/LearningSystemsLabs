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
using System.Threading;
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
        private double[][] normalizeddata;
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
            fuzzyLogic.Rules.Add(new FuzzyLogicRule("IF (x1 IS Short) OR (x1 IS Long) AND (x2 IS Middle) OR (x2 IS Long) AND (x3 IS Middle) OR (x3 IS Long) AND (x4 IS Middle) THEN Iris IS Versicolor"));
            fuzzyLogic.Rules.Add(new FuzzyLogicRule("IF (x3 IS Short OR Middle) AND (x4 IS Short) THEN Iris IS Setosa"));
            fuzzyLogic.Rules.Add(new FuzzyLogicRule("IF (x2 IS Short) OR (x2 IS Middle) AND (x3 IS Long) AND (x4 IS Long) THEN Iris IS Virginica"));
            fuzzyLogic.Rules.Add(new FuzzyLogicRule("IF (x1 IS Middle) AND (x2 IS Short) OR (x2 IS Middle) AND (x3 IS Short) AND (x4 IS Long) THEN Iris IS Versicolor"));

            LinguisticTerm x1Terms = new LinguisticTerm("x1");
            x1Terms.MembershipFunctions.Add(new MembershipFunction("Short", 0, 0, 0, 0.6));
            x1Terms.MembershipFunctions.Add(new MembershipFunction("Middle", 0, 0.6, 0.6, 1));
            x1Terms.MembershipFunctions.Add(new MembershipFunction("Long", 0.6, 1, 1, 1));
            fuzzyLogic.Linguistics.Add(x1Terms);

            LinguisticTerm x2Terms = new LinguisticTerm("x2");
            x2Terms.MembershipFunctions.Add(new MembershipFunction("Short", 0, 0, 0, 0.6));
            x2Terms.MembershipFunctions.Add(new MembershipFunction("Middle", 0, 0.6, 0.6, 1));
            x2Terms.MembershipFunctions.Add(new MembershipFunction("Long", 0.6, 1, 1, 1));
            fuzzyLogic.Linguistics.Add(x2Terms);

            LinguisticTerm x3Terms = new LinguisticTerm("x3");
            x3Terms.MembershipFunctions.Add(new MembershipFunction("Short", 0, 0, 0, 0.6));
            x3Terms.MembershipFunctions.Add(new MembershipFunction("Middle", 0, 0.6, 0.6, 1));
            x3Terms.MembershipFunctions.Add(new MembershipFunction("Long", 0.6, 1, 1, 1));
            fuzzyLogic.Linguistics.Add(x3Terms);

            LinguisticTerm x4Terms = new LinguisticTerm("x4");
            x4Terms.MembershipFunctions.Add(new MembershipFunction("Short", 0, 0, 0, 0.6));
            x4Terms.MembershipFunctions.Add(new MembershipFunction("Middle", 0, 0.6, 0.6, 1));
            x4Terms.MembershipFunctions.Add(new MembershipFunction("Long", 0.6, 1, 1, 1));
            fuzzyLogic.Linguistics.Add(x4Terms);

            LinguisticTerm iris = new LinguisticTerm("Iris");
            iris.MembershipFunctions.Add(new MembershipFunction("Versicolor", 0, 0, 0, 0));
            iris.MembershipFunctions.Add(new MembershipFunction("Setosa", 0, 0.5, 0.5, 0.5));
            iris.MembershipFunctions.Add(new MembershipFunction("Virginica", 0, 1, 1, 1));

            fuzzyLogic.Linguistics.Add(iris);

            fuzzyLogic.Consequent = "Iris";
            //Fetches data
            string macAdress = Data.GetMacAdress();
            double[][] data;
            switch (macAdress) // just add your own case with directory =) 
            {
                case "448A5B8CF7AC":
                    data = Data.TextFile.ReadFileToJaggedArray<double>("C:\\Users\\dss10_000\\Documents\\GitHub\\LearningSystemsLabs\\Lab 2\\iris.txt");
                    break;
                default:
                    data = Data.TextFile.ReadFileToJaggedArray<double>("D:\\LearningSystemsLabs\\Lab 2\\iris.txt");
                    break;
            }

            //Normalize the data
            normalizeddata = MathHelper.NormilizeData(data);

            Action action = () =>
            {
                GA();
            };

            Thread GAThread = new Thread(() => action());
            GAThread.Start();

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

            GAThread.Join();
            TestBest(BestAnswer);
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
                return string.Format("Filepath: {0}", (!string.IsNullOrEmpty(filepath) ? filepath : "No file loaded."));
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

        // double[]population_DNA lista etc. . . 
        double[][] pop_DNA;
        double[] pop_Fitness;
        double[] correct_answers;
        double[][] children_DNA_of_Doom;
        Random rnd = new Random();
        double[] BestAnswer;
        // individ DNA[0,0,0,0,0,0,0,0,0,0,0,0] 
        //LinguisticTerm iris = new LinguisticTerm("Iris");
        //iris.MembershipFunctions.Add(new MembershipFunction("Versicolor", 0, 0, 0, 0));
        //iris.MembershipFunctions.Add(new MembershipFunction("Setosa", 0, 0.5, 0.5, 0.5));
        //iris.MembershipFunctions.Add(new MembershipFunction("Virginica", 0, 1, 1, 1));

        public void GA()
        {
            // SKapar första generation. inom scope 0-1 kanske 100 individer
            pop_DNA = InitializePopulation();
            double bestFitness = double.MaxValue;

            // SKapar barn container
            children_DNA_of_Doom = new double[50][];
            for (int i = 0; i < 50; i++)
            {
                children_DNA_of_Doom[i] = new double[12];
            }

            // SKapar fitness container
            pop_Fitness = new double[100];
            double[][] latestAnswers = new double[normalizeddata.Length][];

            // Fetch answetrs
            correct_answers = CollectCorrectAnswers();

            bestFitness = double.MaxValue;
            int iterations = 0;
            // repeat - tills itaration eller accuracy/fitness'
            while (iterations < 200)//bestFitness > 0.1) //godtyckligt skittal
            {
                iterations++;
                double[][] pop_Answers = new double[100][];
                for (int i = 0; i < 100; i++)
                {
                    pop_Answers[i] = new double[normalizeddata.Length]; // TODO: question count.
                }

                // for every individual
                for (int i = 0; i < 100; i++)
                {

                    LinguisticTerm tmp_iris = fuzzyLogic.Linguistics.Last();

                    // counter to iterate DNA
                    int counter = 0;
                    // change membership attributes
                    for (int x = 0; x < 3; x++)
                    {
                        tmp_iris.MembershipFunctions[x].X0 = pop_DNA[i][counter];
                        counter++;
                        tmp_iris.MembershipFunctions[x].X1 = pop_DNA[i][counter];
                        counter++;
                        tmp_iris.MembershipFunctions[x].X2 = pop_DNA[i][counter];
                        counter++;
                        tmp_iris.MembershipFunctions[x].X3 = pop_DNA[i][counter];
                        counter++;
                    }

                    fuzzyLogic.Linguistics[fuzzyLogic.Linguistics.Count - 1] = tmp_iris;

                    // Compute outputs
                    for (int x = 0; x < normalizeddata.Length; x++)
                    {
                        double x1 = normalizeddata[x][0];
                        double x2 = normalizeddata[x][1];
                        double x3 = normalizeddata[x][2];
                        double x4 = normalizeddata[x][3];
                        double r = normalizeddata[x][4];

                        //Sets the current network of lingustics term
                        fuzzyLogic.Linguistics[0].Input = x1;
                        fuzzyLogic.Linguistics[0].Input = x2;
                        fuzzyLogic.Linguistics[0].Input = x3;
                        fuzzyLogic.Linguistics[0].Input = x4;

                        //Fuzzification

                        //Defuzzification
                        pop_Answers[i][x] = fuzzyLogic.Defuzzification();
                    }

                    // fitness
                    pop_Fitness[i] = ComputeFitness(correct_answers, pop_Answers[i]);
                    bestFitness = FindBestFitness();
                    // Save latest answers;
                    latestAnswers = pop_Answers;
                }

                InvertedCrossBreeding();
            }

            BestAnswer = latestAnswers[FindBestFitnessIndex()];
        }

        private double[][] InitializePopulation()
        {
            double[][] population = new double[100][];
            for (int i = 0; i < 100; i++)
            {
                population[i] = new double[12]; // size of memfunction paraeters
                for (int j = 0; j < 12; j++)
                {
                    population[i][j] = rnd.NextDouble();
                }
            }
            return population;
        }
        private double ComputeFitness(double[] TargetOutput, double[] Individual_Output)
        {
            double sumSquaredError = 0.0;

            for (int i = 0; i < TargetOutput.Length; i++)
            {
                double error = TargetOutput[i] - Individual_Output[i];
                sumSquaredError += error * error;
            }
            return sumSquaredError / TargetOutput.Length;
        }
        private void InvertedCrossBreeding()
        {
            for (int i = 0; i < 50; i++) // iterate half of population
            {
                children_DNA_of_Doom[i] = Breed(pop_DNA[i], pop_DNA[(pop_DNA.Length -1) - i]);
            }

            int[] worstIndex = FindWorstIndividuals();

            for (int i = 0; i < 50; i++)
            {
                pop_DNA[worstIndex[i]] = children_DNA_of_Doom[i];
            }

        }
        private double[] Breed(double[] a, double[] b)
        {
            double[] child_DNA = new double[12];

            for (int i = 0; i < 12; i++)
            {
                double rnd_value = rnd.NextDouble();
                if (rnd_value > 0.5)
                {
                    child_DNA[i] = a[i];
                }
                else
                {
                    child_DNA[i] = b[i];
                }

                // mutation
                rnd_value = rnd.NextDouble();
                if (rnd_value > 0.8) // chance for mutation
                {
                    child_DNA[i] += (rnd.NextDouble() - (double)0.5)*0.1; //  -0.5 - 0.5

                    if (child_DNA[i] > 1)
                    {
                        child_DNA[i] = 1;
                    }
                    if (child_DNA[i] < 0)
                    {
                        child_DNA[i] = 0;
                    }
                }
            }
            return child_DNA;
        }

        private int[] FindWorstIndividuals()
        {
            int[] worstIndex = new int[50];
            for (int i = 0; i < 50; i++)
            {
                worstIndex[i] = i;
            }

            for (int i = 0; i < 100; i++)
            {
                for (int x = 0; x < 50; x++)
                {
                    if (pop_Fitness[i] > pop_Fitness[worstIndex[x]])
                    {
                        worstIndex[x] = i;
                        break;
                    }
                }
            }
            return worstIndex;
        }
        private double[] CollectCorrectAnswers()
        {
            double[] answers = new double[normalizeddata.Length];
            for (int i = 0; i < normalizeddata.Length; i++)
            {
                answers[i] = normalizeddata[i][4]; // get correct answer
            }
            return answers;
        }
        private double FindBestFitness()
        {
            double bestFitness = double.MaxValue;
            for (int i = 0; i < 100; i++)
            {
                if (pop_Fitness[i] < bestFitness)
                {
                    bestFitness = pop_Fitness[i];
                }
            }
            return bestFitness;
        }
        private int FindBestFitnessIndex()
        {
            int bestIndex = 0;
            for (int i = 0; i < 100; i++)
            {
                if (pop_Fitness[i] < pop_Fitness[bestIndex])
                {
                    bestIndex = i;
                }
            }
            return bestIndex;
        }

        // Test
        private void TestBest(double[] bestAnswers)
        {
            int TesterIndex = FindBestFitnessIndex();

            // print best fitness. 
            double accuracy = ComputeAccuracy(correct_answers, bestAnswers);
            string performance = string.Format("Accuracy: {0}%, Fitness: {1}", Math.Round(accuracy * 100, 2), FindBestFitness());
            OutputText.Add(performance);

            for (int i = 0; i < normalizeddata.Length; i++)
            {
                // string correct, string ind. answer, dec correct, dec answer
                string output_String = string.Format("Correct Iris: {0}, Fuzzy Answer: {1}, c:{2}, a:{3}", InterpretAns(correct_answers[i]), InterpretAns(bestAnswers[i]), correct_answers[i], bestAnswers[i]);
                OutputText.Add(output_String);
            }
        }
        private double ComputeAccuracy(double[] TargetOutput, double[] NNOutput)
        {
            int numCorrect = 0;
            int numWrong = 0;

            for (int y = 0; y < TargetOutput.Length; y++)
            {
                string fuzzy_answer = InterpretAns(NNOutput[y]);
                string t_answer = InterpretAns(TargetOutput[y]);

                if (fuzzy_answer != t_answer)
                {
                    ++numCorrect;
                }
                else
                {
                    ++numWrong;
                }
            }
            double result = (double)(numCorrect) / ((double)numCorrect + numWrong);
            return result;
        }

        private string InterpretAns(double value)
        {
            if (value < 0.33) // Versicolor
            {
                return "Versicolor";
            }
            if (value > 0.66) // Setosa
            {
                return "Verginica";
            }
            else // Vernigica
            {
                return "Setosa";
            }
        }
    }
}
