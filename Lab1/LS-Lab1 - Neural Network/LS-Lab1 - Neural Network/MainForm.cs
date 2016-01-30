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
            Tuple<double[,], string> returnData = null;
            switch (button.Name)
            {
                case "trainingDataToolStripMenuItem":

                    returnData = LoadFileDialog("Traning");
                    traningData = returnData.Item1;

                    lblTraningData.Text = GetStringPath("Traning", returnData.Item2);
                    lblTraningInformation.Text = GetStringDataSize(returnData.Item1);

                    tsButtonRemove.Enabled = true;
                    btnTrain.Enabled = true;
                    btnShowTrainMatrix.Enabled = true;
                    break;
                case "testDataToolStripMenuItem":
                    returnData = LoadFileDialog("Test");
                    testData = returnData.Item1;
                    lblTestData.Text = GetStringPath("Test",returnData.Item2);
                    lblTestInformation.Text = GetStringDataSize(returnData.Item1);

                    tsButtonRemove.Enabled = true;
                    btnShowTestMatrix.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        

        /// <summary>
        /// Load a file
        /// </summary>
        /// <param name="title"></param>
        /// <returns>Data and path of file</returns>
        private Tuple<double[,],string> LoadFileDialog(string title)
        {
            double[,] rreturn = null;
            string filename = string.Empty;

            //File dialog
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = string.Format("{0} text-file", title);
            theDialog.Filter = "Txt files|*.txt";
            theDialog.InitialDirectory = @"C:\";
            //Open file dialog
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                //Sets the filedialog path to variable filedialog
                filename = theDialog.FileName;
                Action action = () =>
                    {
                        statusStripLabel.Text = string.Format("Loading {0}",title);
                        int[] arrayCheck =  { 2, 19, 20 };
                        rreturn = FileReader.CollectFileData(filename,3, arrayCheck);
                        statusStripLabel.Text = string.Format("Loading done: {0}", title);
                    };
                syncContext.Send(item => action.Invoke(), null);
            }
            return new Tuple<double[,],string>(rreturn, filename);
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
