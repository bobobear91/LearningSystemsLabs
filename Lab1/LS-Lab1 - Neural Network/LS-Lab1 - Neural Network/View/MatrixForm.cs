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
        public MatrixForm(string matrixHMsg, string matrixMsg, double[,] data)
        {
            InitializeComponent();

            //Label Header
            lblHeader.Text = matrixHMsg;

            //Label Description
            lblDescription.Text = matrixMsg;

            //Data
            this.Data = data;
            if (Data != null)
            {
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    this.lbBoxMatrix.Items.Add(string.Format("1: {0} | 2 : {1} | 3 : {2}", Data[i,0], Data[i, 1], Data[i, 2]));
                }
            }
            else
            {

            }
        }

        public double[,] Data
        {
            get;
            set;
        }
    }
}
