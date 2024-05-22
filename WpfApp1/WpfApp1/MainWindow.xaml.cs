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
using System.Linq;
using Reactive.Bindings.Extensions;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowModel _model;

        public ReactiveCollection<DetailViewModel> Details { get; } = [];

        public ReactiveCommand UpCommand { get; } = new();
        public ReactiveCommand DownCommand { get; } = new();

        public MainWindow()
        {
            _model = new();

            InitDetails();

            UpCommand.Subscribe(() =>
            {
                if (IsSelectedRow(dataGrid))
                {
                    // TODO : 最小indexを含んだらやらない、そもそもボタン非活性
                    _model.MoveUp(GetSelectedRowIndexs(dataGrid));
                }
            });

            DownCommand.Subscribe(() =>
            {
                if (IsSelectedRow(dataGrid))
                {
                    // TODO : 最大indexを含んだらやらない、そもそもボタン非活性
                    _model.MoveDown(GetSelectedRowIndexs(dataGrid));
                }
            });

            _model.Details.ObserveMoveChanged().Subscribe(e =>
            {
                // Moveした要素の元のDetailsのindexを取得する
                var sourceIndex = Details.First(x => x.IsMatchModel(e.NewItem)).Number.Value - 1;

                // Up/Downはここでは判定できないので、元のIndexとその前後のViewModelのModel参照を更新する
                // Up/Down判定できない中で漏れなく更新する最小限の処理をしている形
                Details[sourceIndex].UpdateModel(_model.Details[sourceIndex]);
                if (0 <= sourceIndex - 1)
                {
                    Details[sourceIndex - 1].UpdateModel(_model.Details[sourceIndex - 1]);
                }
                if (Details.Count > sourceIndex + 1)
                {
                    Details[sourceIndex + 1].UpdateModel(_model.Details[sourceIndex + 1]);
                }
            });

            InitializeComponent();
        }

        private void InitDetails()
        {
            Details.Clear();
            for (int i = 0; i < _model.Details.Count; i++)
            {
                Details.Add(new(i + 1, _model.Details[i]));
            }
        }

        private bool IsSelectedRow(DataGrid grid)
        {
            return grid.SelectedIndex != -1;
        }

        private List<int> GetSelectedRowIndexs(DataGrid grid)
        {
            List<int> ret = [];
            foreach (var item in grid.SelectedItems)
            {
                if (item is DetailViewModel i)
                {
                    ret.Add(i.Number.Value - 1);
                }
            }

            return ret;
        }
    }
}