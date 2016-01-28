/*
    Learning Systems
    Lab Assignment 1
    Authors:
    Robin Calmegård
    Dennis Stockhaus


*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LS_Lab1
{
    public partial class MainForm : Form
    {
        #region Variables

        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();

            //*******************************************************************************
            //      Controls
            //*******************************************************************************

            ///Menu
            //this.menuStripBar 

            ///Tools
            //this.toolStripBar

            ///Status
            //this.statusStripBar
            this.statuslStripBarTooLabel.Text = "Ready";

        }
        #endregion

        private void PresentCurrentState(string input)
        {

        }

    }
}
