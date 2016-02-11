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
            theDialog.InitialDirectory = @"C:\";
            //Open file dialog
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                //Sets the filedialog path to variable filedialog
                temppath = theDialog.FileName;
                Action action = () =>
                    {
                        statusStripLabel.Text = string.Format("Loading {0}",title);
                        int[] arrayCheck =  { 1, 19, 20, 21}; //TODO: check criticalIndex
                        tempData = FileReader.CollectJaggedFileDataArray(temppath,4, arrayCheck);
                        trainingData = tempData; // TODO: I Added this to get data.
                        statusStripLabel.Text = string.Format("Loading done: {0}", title);                  
                    };
                syncContext.Send(item => action.Invoke(), null);
                filepath = temppath;
                data = tempData;
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
        private bool isNNTrained = false;
        private void btnTrain_Click(object sender, EventArgs e)
        {
 
            MessageBox.Show("started training");
            //Checks if nn is already trained
            if (isNNTrained)
            {
                if ((MessageBox.Show("Ops!", "It seems that the neural network is already train. Do you want to retrain?", MessageBoxButtons.YesNo) == DialogResult.No))
                    return;
            }
            NN = new NeuralNetwork(3, (int)numericHidden.Value, 1, (int)numericLearnrate.Value, (double)numericMomentum.Value);
            NN.FireMaxAccuracyReached += NN_FireMaxAccuracyReached;
            NN.FireMaxIterationsReached += NN_FireMaxIterationsReached;
            NN.FirePerformanceInfo += NN_FirePerformanceInfo;
            //Networked is trained
            Action action = () =>
            {
                isNNTrained = btnTest.Enabled = true;

                NN.Train(trainingData, (int)numericIterations.Value, (double)((int)numericPercentage.Value/100));
                MessageBox.Show("Traning Done!","Traning ");
                
            };
            syncContext.Send(item => action.Invoke(), null);
        }


        private void NN_FirePerformanceInfo(double Error, double Accuracy, int iterations)
        {
            throw new NotImplementedException();
        }

        private void NN_FireMaxIterationsReached(int iterations, double accuracy)
        {
            throw new NotImplementedException();
        }

        private void NN_FireMaxAccuracyReached(double Accuracy, int iterations)
        {
            throw new NotImplementedException();
        }







        private void BtnStop_Click(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {

            //Compares the training results to the test results
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
    }
}
