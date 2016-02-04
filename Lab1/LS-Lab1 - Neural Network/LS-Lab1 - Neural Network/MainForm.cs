/*
    Authors: 
        Robin Calmegård,
        Dennis Stockhaus
    Created: @2016-01-26

    Description:
    

*/

#region Usings
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

        double[,] traningData = null;
        double[,] testData = null;

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

            //Sets the button to disabled
            tsButtonRemove.Enabled = false;

            //*****************************************************************************
            //      Buttons
            //*****************************************************************************
            //Button - Test the neural network
            btnTest.Enabled = false;

            //Button - Train the neural network
            btnTrain.Enabled = false;

            //Button - Show Test Matrix
            btnShowTestMatrix.Enabled = false;
            btnShowTestMatrix.Click += btnShowMatrix_Click;

            //Button - Show Train Matrix
            btnShowTrainMatrix.Enabled = false;
            btnShowTrainMatrix.Click += btnShowMatrix_Click;


            //*****************************************************************************
            //      Numeric up & down
            //*****************************************************************************
            //Numeric Iterations
            numericIterations.Value = 1000;


        }

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
                    LoadFileDialog("Traning", out path,out traningData);
                    ChangeLabelStatusTraning(path, traningData);

                    break;

                case "testDataToolStripMenuItem":
                    //Loads test
                    LoadFileDialog("Test",out path, out testData);
                    ChangeLabelStatusTest(path, testData);

                    break;

                case "loadBothToolStripMenuItem":
                    //Loads traning
                    LoadFileDialog("Traning",out path, out traningData);
                    ChangeLabelStatusTraning(path, traningData);
                   
                    //Loads test
                    LoadFileDialog("Test",out path,out traningData);
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
        private void ChangeLabelStatusTraning(string filename, double[,] data)
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
        private void ChangeLabelStatusTest(string filename, double[,] data)
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
        private void LoadFileDialog(string title,out string filepath, out double[,] data)
        {
            filepath = string.Empty;
            data = null;

            string temppath;
            double[,] tempData = null;
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
                        int[] arrayCheck =  { 2, 19, 20 };
                        tempData = FileReader.CollectInputFileData(temppath,3, arrayCheck);
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

        private string GetStringDataSize(double[,] data)
        {
            //((data.GetLength(1) != 0) ? data.GetLength(1) : "Error")
            return string.Format("Size of array X:{0} Y:{1}", data.GetLength(1), data.GetLength(0));
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
                    ShowMatrix("Artificial Nerual Network Matrix", "Training Matrix", "Show the current array.", traningData);
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
        private void ShowMatrix(string formname, string title, string description, double[,] data)
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
            //Checks if nn is already trained
            if (isNNTrained)
            {
                if ((MessageBox.Show("Ops!", "It seems that the neural network is already train. Do you want to retrain?", MessageBoxButtons.YesNo) == DialogResult.No))
                    return;
            }
            //Networked is trained
            Action action = () =>
            {
                isNNTrained = btnTest.Enabled = true;
                //double[] bestWeights = NeuralNetwork.Train(traningData, maxEpochs, learnRate, momentum);
                MessageBox.Show("Traning Done!","Traning ");
            };
            syncContext.Send(item => action.Invoke(), null);
            //NeuralNetwork.
            //Shows graph
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
