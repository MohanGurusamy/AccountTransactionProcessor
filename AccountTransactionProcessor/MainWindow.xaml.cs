using System.Windows;

namespace AccountTransactionProcessor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += delegate { DataContext = ViewModelFactory.CreateATPViewModel(Dispatcher); };
        }
    }
}
