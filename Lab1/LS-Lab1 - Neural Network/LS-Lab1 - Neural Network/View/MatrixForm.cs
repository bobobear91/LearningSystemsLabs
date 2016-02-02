using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LS_Lab1___Neural_Network.View
{
    public partial class MatrixForm : Form
    {
        public MatrixForm(string formText,string matrixTitleMsg, string matrixMsg, double[,] data)
        {
            InitializeComponent();
            //Form
            this.Text = (!string.IsNullOrEmpty(formText) ? formText : "Matrix show."); ;
            this.MinimumSize = new Size(480, 360);
            //Label Header
            lblHeader.Text = (!string.IsNullOrEmpty(matrixTitleMsg) ? matrixTitleMsg : "No Title.");

            //Label Description
            lblDescription.Text = (!string.IsNullOrEmpty(matrixMsg) ? matrixMsg : "No description."); ;

            //Data
            this.Data = data;
            this.lbBoxMatrix.SelectedIndexChanged += LbBoxMatrix_SelectedIndexChanged;

            //Checks if data is NULL
            if (Data != null)
            {
                lblDescription.Text += string.Format(" Matrix have rows:{0} and columns:{1}", data.GetLength(0), data.GetLength(1));
                for (int y = 0; y < data.GetLength(0); y++)
                {
                    string line = string.Format("{0}:", y);
                    for (int x = 0; x < data.GetLength(1); x++)
                    {
                        line += string.Format(" {0}     {1}", Data[y, x], (x < (data.GetLength(1)-1)) ? ("|") : string.Empty);

                    }
                    //this.lbBoxMatrix.Items.Add(string.Format("{0}. 1: {1} | 2 : {2} | 3 : {3}", y ,Data[y,0], Data[y, 1], Data[y, 2]));
                    this.lbBoxMatrix.Items.Add(line);
                }
            }
            else
            {
                this.lbBoxMatrix.Items.Add("Matrix is empty.");
            }

            btnEdit.Enabled = false;
        }

        private void LbBoxMatrix_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentSelected = ((ListBox)sender).SelectedIndex;
            MessageBox.Show(CurrentSelected.ToString());
        }

        public double[,] Data
        {
            get;
            set;
        }

        private int CurrentSelected = 0;
        private void btnEdit_Click(object sender, EventArgs e)
        {

        }


    }
}
