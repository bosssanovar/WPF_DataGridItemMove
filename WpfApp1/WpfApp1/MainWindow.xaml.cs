using Reactive.Bindings;

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowModel _model;

        public ReactiveCollection<DetailViewModel> Details { get; } = [];

        public MainWindow()
        {
            _model = new();

            InitDetails();

            InitializeComponent();
        }

        private void InitDetails()
        {
            for(int i=0; i<_model.Details.Count; i++)
            {
                Details.Add(new(i + 1, _model.Details[i]));
            }
        }
    }
}