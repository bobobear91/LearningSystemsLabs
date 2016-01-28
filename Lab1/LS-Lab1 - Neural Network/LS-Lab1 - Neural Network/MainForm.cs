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
        public MainForm()
        {
            InitializeComponent();

            //*******************************************************************
            //      Menu
            //*******************************************************************
            this.Load += MainForm_Load;


            this.Text = "Neural Network";

            //GUI update thread
            this.syncContext = SynchronizationContext.Current as WindowsFormsSynchronizationContext;

        }

        #region Form
        private void MainForm_Load(object sender, EventArgs e)
        {
        }


        #endregion

        private void ToolChangeStatusBar(string input)
        {
            
        }

        #region Menu
        private void trainingDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFileDialog("Traning text-file");
        }

        private void testDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFileDialog("Test text-file");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        private void LoadFileDialog(string title)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = title;
            theDialog.Filter = "Txt files|*.txt";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = theDialog.FileName;
                Action action = () =>
                    {
                        
                    };
                syncContext.Send(item => action.Invoke(), null);

            }
        }
        #endregion
    }
}
