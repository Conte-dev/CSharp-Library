using System.Windows;
using BibliotecaWPF.ViewModels;

namespace BibliotecaWPF.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}