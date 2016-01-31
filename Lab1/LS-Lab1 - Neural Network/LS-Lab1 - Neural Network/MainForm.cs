using LS_Lab1___Neural_Network.Components;
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

namespace LS_Lab1___Neural_Network
{
    public partial class MainForm : Form
    {
        private WindowsFormsSynchronizationContext syncContext;
        double[,] traningData = null;
        double[,] testData = null;

        #region Constructor
        public MainForm()
        {
            InitializeComponent();

            //*******************************************************************
            //      MenuForm
            //*******************************************************************
            this.Load += MainForm_Load;

            //Minimum size of the form
            this.MinimumSize = new Size(680, 520);
            this.Text = "Neural Network";

            //GUI update thread, will be used for updating the form
            this.syncContext = SynchronizationContext.Current as WindowsFormsSynchronizationContext;

            //Sets the button to disabled
            tsButtonRemove.Enabled = false;
            btnTest.Enabled = false;
            btnTrain.Enabled = false;
            btnShowTestMatrix.Enabled = false;
            btnShowTrainMatrix.Enabled = false;
        }
        #endregion

        #region Form
        private void MainForm_Load(object sender, EventArgs e)
        {
        }


        #endregion

        #region Menu
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
                        tempData = FileReader.CollectFileData(temppath,3, arrayCheck);
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
            return string.Format("Size of array X:{0} Y:{1}", data.GetLength(0), data.GetLength(1));
        }
        #endregion

        private bool isNNTrained = false;
        private void btnTrain_Click(object sender, EventArgs e)
        {
            //Checks if nn is already trained
            if (isNNTrained)
            {
               
            }
            //Networked is trained
            isNNTrained = btnTest.Enabled = true;

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
    }
}
