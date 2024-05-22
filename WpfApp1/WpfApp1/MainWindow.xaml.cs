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
        public ReactiveCollection<DetailViewModel> Details { get; } = [];

        public MainWindow()
        {
            InitDetails();

            InitializeComponent();
        }

        private void InitDetails()
        {
            for(int i=0; i<10; i++)
            {
                Details.Add(new(i + 1, $"item {i + 1}", $"item {i + 1}", $"item {i + 1}"));
            }
        }
    }
}