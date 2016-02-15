using Lab_assignment_2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab_assignment_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables
        private MainViewModel viewModel;

        #endregion


        public MainWindow()
        {
            InitializeComponent();
            //***********************************************************************
            //      MainView Model
            //***********************************************************************
            this.viewModel = new MainViewModel();
            this.DataContext = viewModel;
            //Binds the close action to the view model
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(() => this.Close());

        }
    }
}
