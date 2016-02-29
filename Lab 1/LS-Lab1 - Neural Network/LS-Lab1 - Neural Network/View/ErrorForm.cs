/*
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

namespace LS_Lab1___Neural_Network.View
{
    public partial class ErrorForm : Form
    {
        /// <summary>
        /// Creating an error message form.
        /// </summary>
        /// <param name="errorHMsg"></param>
        /// <param name="errorMsg"></param>
        public ErrorForm(string errorHMsg, string errorMsg)
        {
            InitializeComponent();

            //Properties
            this.MinimumSize = new Size(280,420);
            this.Text = string.Format("Error: {0}",errorHMsg);

            //Label Header Error 
            this.lblErrorHmsg.Text = errorHMsg;

            //Textbox 
            this.tbErrorMessage.Text = errorMsg;
        }
    }
}
