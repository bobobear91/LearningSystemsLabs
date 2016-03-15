using Lab_4.ViewModel;
using System;
using System.Windows;
namespace Lab_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            DataContext = viewModel;
            //Binds the close action to the view model
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(() => this.Close());

        }
    }
}
