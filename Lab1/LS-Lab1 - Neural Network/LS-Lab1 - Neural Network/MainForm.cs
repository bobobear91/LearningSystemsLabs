/*
    Authors: 
        Robin Calmegård,
        Dennis Stockhaus,

    Created: @2016-01-26
    Edited:

    Description:
    

*/

#region Usings-strings
using LS_Lab1___Neural_Network.Components;
using LS_Lab1___Neural_Network.View;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization;
#endregion

namespace LS_Lab1___Neural_Network
{
    public partial class MainForm : Form
    {
        #region Statics & Enums
        public enum State { Training, Testing }
        public enum CState { Ready, Ongoing }
        private readonly Size FormMinimumSize = new Size(620, 620);
        #endregion

        #region Variables
        /// <summary>
        /// synchronization context for the WFA model
        /// </summary>
        private WindowsFormsSynchronizationContext syncContext;

        double[][] trainingData = null;
        double[][] testData = null;
        NeuralNetwork NN;

        ToolTip tooltip;
        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();

            //*******************************************************************
            //      MenuForm
            //*******************************************************************
            this.Load += MainForm_Load;

            //Minimum size of the form
            this.MinimumSize = FormMinimumSize;
            this.Text = "Artificial Neural Network";

            //GUI update thread, will be used for updating the form
            this.syncContext = SynchronizationContext.Current as WindowsFormsSynchronizationContext;

            //*****************************************************************************
            //      Tooltip
            //*****************************************************************************
            tooltip = new ToolTip();
            // Set up the delays for the ToolTip.
            tooltip.AutoPopDelay = 5000;
            tooltip.InitialDelay = 1000;
            tooltip.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            tooltip.ShowAlways = true;


            //*****************************************************************************
            //      Menubar
            //*****************************************************************************
            //Sets the button to disabled
            tsButtonRemove.Enabled = false;

            //*****************************************************************************
            //      Buttons
            //*****************************************************************************
            //Button - Test the neural network
            btnTest.Enabled = false;

            //Button - Stop
            btnStop.Click += BtnStop_Click;
            btnStop.Enabled = false;

            //Button - Train the neural network
            btnTrain.Enabled = false;
            btnTrain.Click += btnTrain_Click;

            //Button - Show Test Matrix
            btnShowTestMatrix.Enabled = false;
            btnShowTestMatrix.Click += btnShowMatrix_Click;

            //Button - Show Train Matrix
            btnShowTrainMatrix.Enabled = false;
            btnShowTrainMatrix.Click += btnShowMatrix_Click;

            //*****************************************************************************
            //      Label tooltips
            //*****************************************************************************
            tooltip.SetToolTip(this.lblIterations, "Maximum iterations for the neural network.");
            tooltip.SetToolTip(this.lblLearnRate, "The difference in the learn rate for each new iteration.");

            //TODO
            tooltip.SetToolTip(this.lblMomentum, "TODO.");
            tooltip.SetToolTip(this.lblProcentage, "TODO.");


            //*****************************************************************************
            //      Numeric up & down
            //*****************************************************************************
            //default values
            numericIterations.Minimum = 1; //10e0
            numericIterations.Value = 1000; //10e3
            numericIterations.Maximum = 1000000; //10e6

            //
            numericLearnrate.Value = (decimal)0.005;
            numericLearnrate.Increment = (decimal)0.005;
            numericLearnrate.Minimum = (decimal)0.005;
            //Maximum?

            //numericMomentum

            numericPercentage.Value = 90;
            numericPercentage.Increment = 1;
            numericPercentage.Minimum = 1;
            numericPercentage.Maximum = 99;

            numericHidden.Value = 10;
            numericHidden.Increment = 1;
            numericHidden.Minimum = 1;
            numericHidden.Maximum = 1000; //10e^3

            //int[] arrayCheck = { 2, 19, 20, 21 }; //TODO: check criticalIndex
            // trainingData = FileReader.CollectJaggedFileDataArray("C://Users//dss10_000//Documents//GitHub//LearningSystemsLabs//Lab1//Data_Training.txt", 4, arrayCheck);
            // btnTrain.Enabled = true;

        

        }

   


        #endregion

        #region Initialize Components

        #endregion

        #region Form
        private void MainForm_Load(object sender, EventArgs e)
        {
        }


        #endregion

        #region Menu Topbar
        /// <summary>
        /// Load data event (Click)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadDataMenuItem_Click(object sender, EventArgs e)
        {
            var button = (ToolStripMenuItem)sender;
            string path;
            switch (button.Name)
            {
                case "trainingDataToolStripMenuItem":
                    //Loads Traning
                    LoadFileDialog("Traning", out path,out trainingData);
                    ChangeLabelStatusTraning(path, trainingData);

                    break;

                case "testDataToolStripMenuItem":
                    //Loads test
                    LoadFileDialog("Test",out path, out testData);
                    ChangeLabelStatusTest(path, testData);

                    break;

                case "loadBothToolStripMenuItem":
                    //Loads traning
                    LoadFileDialog("Traning",out path, out trainingData);
                    ChangeLabelStatusTraning(path, trainingData);
                   
                    //Loads test
                    LoadFileDialog("Test",out path,out trainingData);
                    ChangeLabelStatusTest(path, testData);
                   
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="data"></param>
        private void ChangeLabelStatusTraning(string filename, double[][] data)
        {   
            if (!string.IsNullOrEmpty(filename))
            {
                lblTraningData.Text = GetStringPath("Traning", filename);
                lblTraningInformation.Text = GetStringDataSize(data);

                tsButtonRemove.Enabled = true;
                btnTrain.Enabled = true;
                btnShowTrainMatrix.Enabled = true;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="data"></param>
        private void ChangeLabelStatusTest(string filename, double[][] data)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                lblTestData.Text = GetStringPath("Test", filename);
                lblTestInformation.Text = GetStringDataSize(data);

                tsButtonRemove.Enabled = true;
                btnShowTestMatrix.Enabled = true;
            }
        }

        /// <summary>
        /// Load a file
        /// </summary>
        /// <param name="title"></param>
        /// <returns>Data and path of file</returns>
        private void LoadFileDialog(string title,out string filepath, out double[][] data)
        {
            filepath = string.Empty;
            data = null;

            string temppath;
            double[][] tempData = null;
            //File dialog
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = string.Format("{0} text-file", title);
            theDialog.Filter = "Txt files|*.txt";
            //theDialog.InitialDirectory = @"C:\";
            //Open file dialog
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                //Sets the filedialog path to variable filedialog
                temppath = theDialog.FileName;
                Action action = () =>
                    {
                        statusStripLabel.Text = string.Format("Loading {0}",title);
                        int[] arrayCheck =  { 1, 19, 20, 21}; //TODO: check criticalIndex
                        tempData = Data.TextFile.ReadFilePartToJaggedArray<double>(temppath, 4, arrayCheck);
                        trainingData = tempData; // TODO: I Added this to get data.
                        statusStripLabel.Text = string.Format("Loading done: {0}", title);                  
                    };
                syncContext.Send(item => action.Invoke(), null);
                filepath = temppath;
                data = tempData;
            }
        }

        private void loadNeuralNetworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NN != null)
            {
                if (MessageBox.Show("Do you want to overwrite it?", "Neural-Network allready exist!", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }
            // open dialog
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Txt files|*.txt|XML files|*.xml";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (NN != null)
                {
                    NN.LoadNN(dialog.FileName);
                }
                else
                {
                    NN = new NeuralNetwork(3, (int)numericHidden.Value, 1, (double)numericLearnrate.Value, (double)numericMomentum.Value);
                    NN.FireMaxAccuracyReached += NN_RecieveMaxAccuracyReached;
                    NN.FireMaxIterationsReached += NN_RecieveMaxIterationsReached;
                    NN.FirePerformanceInfo += NN_RecievePerformanceInfo;
                    NN.FireTrainingComplete += NN_RecieveTrainingComplete;
                    NN.FireOutputComparison += NN_RecieveOutputComparison;
                    NN.FireTestingComplete += NN_RecieveTestingComplete;
                    NN.FireTestingResultInfo += NN_RecieveTestingResultInfo;

                    NN.LoadNN(dialog.FileName);

                    numericHidden.Value = NN.NumberOfHidden;
                    numericLearnrate.Value = (decimal)NN.LearningRate;
                    numericMomentum.Value = (decimal)NN.Momentum;
                }
            }
        }

        private void saveNeuralNetworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NN == null)
            {
                MessageBox.Show("There is no Neural-Network to save.");
                return;
            }
            // open dialog
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Txt files|*.txt|XML files|*.xml";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                NN.SaveNN(dialog.FileName);
            }
        }
        #endregion

        #region String-Helpers
        private string GetStringPath(string type, string filename)
        {
            return string.Format("{0} data path: {1}", type, filename);
        }

        private string GetStringDataSize(double[][] data)
        {
            //((data.GetLength(1) != 0) ? data.GetLength(1) : "Error")
            return string.Format("Size of array X:{0} Y:{1}", data[0].Length, data.Length);
        }
        #endregion

        #region Form-UI
        /// <summary>
        /// Click event for Matrix Buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowMatrix_Click(object sender, EventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "btnShowTrainMatrix":
                    ShowMatrix("Artificial Nerual Network Matrix", "Training Matrix", "Show the current array.", trainingData);
                    break;
                case "btnShowTestMatrix":
                    ShowMatrix("Artificial Nerual Network Matrix", "Test Matrix", "Show & Edit", testData);
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Create matrix form method 
        /// </summary>
        /// <param name="formname"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="data"></param>
        private void ShowMatrix(string formname, string title, string description, double[][] data)
        {
            //Create an action directive
            Action action = () =>
            {
                MatrixForm mf = new MatrixForm(formname, title, description, data);
                mf.ShowDialog();
            };
            syncContext.Send(item => action.Invoke(), null);

        }


        #endregion

        //TODO
        private void btnTrain_Click(object sender, EventArgs e)
        {
            if (trainingData == null)
            {
                MessageBox.Show("No TrainingData loaded!");
                return;
            }
            // Check if NN is null. "first use"
            if (NN == null)
            {
                NN = new NeuralNetwork(3, (int)numericHidden.Value, 1, (double)numericLearnrate.Value, (double)numericMomentum.Value);
                NN.FireMaxAccuracyReached += NN_RecieveMaxAccuracyReached;
                NN.FireMaxIterationsReached += NN_RecieveMaxIterationsReached;
                NN.FirePerformanceInfo += NN_RecievePerformanceInfo;
                NN.FireTrainingComplete += NN_RecieveTrainingComplete;
                NN.FireOutputComparison += NN_RecieveOutputComparison;
                NN.FireTestingComplete += NN_RecieveTestingComplete;
                NN.FireTestingResultInfo += NN_RecieveTestingResultInfo;
            }
            // Alerts configuration change
            if (numericHidden.Value != NN.NumberOfHidden)
            {
                if (MessageBox.Show("Do you want to reset neural-network with new cofiguration?", "Configuration of hidden node has changed!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    NN = new NeuralNetwork(3, (int)numericHidden.Value, 1, (double)numericLearnrate.Value, (double)numericMomentum.Value); // TODO: (FIX)Slightly hardcoded for assignment
                    NN.FireMaxAccuracyReached += NN_RecieveMaxAccuracyReached;
                    NN.FireMaxIterationsReached += NN_RecieveMaxIterationsReached;
                    NN.FirePerformanceInfo += NN_RecievePerformanceInfo;
                    NN.FireTrainingComplete += NN_RecieveTrainingComplete;
                    NN.FireOutputComparison += NN_RecieveOutputComparison;
                    NN.FireTestingComplete += NN_RecieveTestingComplete;
                    NN.FireTestingResultInfo += NN_RecieveTestingResultInfo;
                }
                else
                {
                    numericHidden.Value = NN.NumberOfHidden;
                    return;
                }
            }
        
            Action action = () =>
            {
                NN.LearningRate = (double)numericLearnrate.Value;
                NN.Momentum = (double)numericMomentum.Value;
                NN.Train(trainingData, (int)numericIterations.Value, (double)(numericPercentage.Value/100), 10, (int)numericAccuracyFilter.Value);
                //MessageBox.Show("Traning Done!","Traning ");
            };

            SetupPerformanceChart(true);
            btnTrain.Enabled = false;
            btnTest.Enabled = false;
            btnReset.Enabled = false;
            btnStop.Enabled = true;
            Thread TrainingAction = new Thread(() => action());
            TrainingAction.Start();
        }

        private void NN_RecievePerformanceInfo(int iterations, double Error, double Accuracy)
        {
            //MessageBox.Show("");
        }

        private void NN_RecieveMaxIterationsReached(int iterations, double accuracy, double error)
        {
            MessageBox.Show(string.Format("Accuracy: {0}% Error: {1} Iterations:{2}", Math.Round(accuracy, 4) * 100, error, iterations), "Max iterations reached!");

        }

        private void NN_RecieveMaxAccuracyReached(int iterations, double Accuracy, double error)
        {
            MessageBox.Show(string.Format("Accuracy: {0}% Error: {1} Iterations:{2}", Math.Round(Accuracy, 4) * 100, error, iterations),"Max accuracy reached!");
        }

        private void NN_RecieveTrainingComplete()
        {
            if (btnTrain.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => btnTrain.Enabled = true));
                this.Invoke(new MethodInvoker(() => btnTest.Enabled = true));
                this.Invoke(new MethodInvoker(() => btnReset.Enabled = true));
                this.Invoke(new MethodInvoker(() => btnStop.Enabled = false));
            }
            else
            {
                btnTrain.Enabled = true;
                this.Invoke(new MethodInvoker(() => btnTest.Enabled = true));
                this.Invoke(new MethodInvoker(() => btnReset.Enabled = true));
                this.Invoke(new MethodInvoker(() => btnStop.Enabled = false));
            }
        }

        private void SetupPerformanceChart(bool isTraining)
        {
            PerformanceChart.ChartAreas.Clear();
            PerformanceChart.Series.Clear();

            PerformanceChart.ChartAreas.Add("Output_Compare");
            if (isTraining)
            {
                PerformanceChart.ChartAreas["Output_Compare"].AxisX.Minimum = 0;
                PerformanceChart.ChartAreas["Output_Compare"].AxisX.Maximum = 700;
                PerformanceChart.ChartAreas["Output_Compare"].AxisX.Interval = 500;
                PerformanceChart.ChartAreas["Output_Compare"].AxisY.Minimum = 0;
                PerformanceChart.ChartAreas["Output_Compare"].AxisY.Maximum = 0.5;
                PerformanceChart.ChartAreas["Output_Compare"].AxisY.Interval = 1 / 10;
            }
            else
            {
                PerformanceChart.ChartAreas["Output_Compare"].AxisX.Minimum = 0;
                PerformanceChart.ChartAreas["Output_Compare"].AxisX.Maximum = 67;
                PerformanceChart.ChartAreas["Output_Compare"].AxisX.Interval = 10;
                PerformanceChart.ChartAreas["Output_Compare"].AxisY.Minimum = 0;
                PerformanceChart.ChartAreas["Output_Compare"].AxisY.Maximum = 0.5;
                PerformanceChart.ChartAreas["Output_Compare"].AxisY.Interval = 1 / 10;
            }

            PerformanceChart.Series.Add("Target Output");
            PerformanceChart.Series["Target Output"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            PerformanceChart.Series["Target Output"].Color = Color.Blue;

            PerformanceChart.Series.Add("Neural-Network Output");
            PerformanceChart.Series["Neural-Network Output"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            PerformanceChart.Series["Neural-Network Output"].Color = Color.Red;

            //PerformanceChart.Series["Target Output"].Points.AddXY(0, 0);
            //PerformanceChart.Series["Neural-Network Output"].Points.AddXY(0, 0);
            //PerformanceChart.Series["Target Output"].Points.AddXY(50, 10);
            //PerformanceChart.Series["Neural-Network Output"].Points.AddXY(23, 42);
        }

        private void NN_RecieveOutputComparison(int iterator, double NN_Output, double t_Output)
        {
            this.Invoke(new MethodInvoker(() => updateChart(iterator, NN_Output, t_Output)));
        }

        private void NN_RecieveTestingComplete()
        {
            if (btnTrain.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => btnTrain.Enabled = true));
                this.Invoke(new MethodInvoker(() => btnTest.Enabled = true));
                this.Invoke(new MethodInvoker(() => btnReset.Enabled = true));
                this.Invoke(new MethodInvoker(() => btnStop.Enabled = false));
            }
            else
            {
                btnTrain.Enabled = true;
                this.Invoke(new MethodInvoker(() => btnTest.Enabled = true));
                this.Invoke(new MethodInvoker(() => btnReset.Enabled = true));
                this.Invoke(new MethodInvoker(() => btnStop.Enabled = false));
            }
        }

        private void NN_RecieveTestingResultInfo(double accuracy, double error)
        {
            MessageBox.Show(string.Format("Accuracy: {0}% error:{1}", Math.Round(accuracy,4)*100, error), "Testing Complete!");
        }

        private void updateChart(int iterator, double NN_Output, double t_Output)
        {
            if (doSplitOutput_box.Checked)
            {
                NN_Output += 0.05;
                t_Output += 0.3;
                PerformanceChart.ChartAreas["Output_Compare"].AxisY.Interval = 100;
            }
            else
            {
                PerformanceChart.ChartAreas["Output_Compare"].AxisY.Interval = 1 / 10;
            }

            PerformanceChart.Series["Target Output"].Points.AddXY(iterator, t_Output);
            PerformanceChart.Series["Neural-Network Output"].Points.AddXY(iterator, NN_Output);

            if (PerformanceChart.Series["Target Output"].Points.Count > 700)
            {
                PerformanceChart.ChartAreas["Output_Compare"].AxisX.Minimum++;
                PerformanceChart.ChartAreas["Output_Compare"].AxisX.Maximum++;
                PerformanceChart.Series["Target Output"].Points.RemoveAt(0);
                PerformanceChart.Series["Neural-Network Output"].Points.RemoveAt(0);
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            NN.StopTraining();
        }

        private void btnShowTrainMatrix_Click(object sender, EventArgs e)
        {

        }

        private void btnShowTestMatrix_Click(object sender, EventArgs e)
        {

        }

        private void lblTestData_Resize(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            using (Graphics g = CreateGraphics())
            {
                SizeF size = g.MeasureString(lbl.Text, lbl.Font, 495);
                lbl.Height = (int)Math.Ceiling(size.Height);
            }
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            if (NN != null)
            {
                NN = new NeuralNetwork(3, (int)numericHidden.Value, 1, (double)numericLearnrate.Value, (double)numericMomentum.Value);
                NN.FireMaxAccuracyReached += NN_RecieveMaxAccuracyReached;
                NN.FireMaxIterationsReached += NN_RecieveMaxIterationsReached;
                NN.FirePerformanceInfo += NN_RecievePerformanceInfo;
                NN.FireTrainingComplete += NN_RecieveTrainingComplete;
                NN.FireOutputComparison += NN_RecieveOutputComparison;
                NN.FireTestingComplete += NN_RecieveTestingComplete;
                NN.FireTestingResultInfo += NN_RecieveTestingResultInfo;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            // Check if NN is null. "first use"
            if (NN == null)
            {
                MessageBox.Show("Cant test an untrained Neural-Network.");
                return;
            }
            if (testData == null)
            {
                MessageBox.Show("No TestData loaded!");
                return;
            }

            Action action = () =>
            {
                NN.Test(testData, (int)numericAccuracyFilter.Value);
            };

            SetupPerformanceChart(false);
            btnTrain.Enabled = false;
            btnTest.Enabled = false;
            btnReset.Enabled = false;
            btnStop.Enabled = true;
            Thread TestingAction = new Thread(() => action());
            TestingAction.Start();
        }
    }
}
