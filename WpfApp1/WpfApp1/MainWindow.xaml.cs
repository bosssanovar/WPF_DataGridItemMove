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
using System.Reactive.Linq;
using System.DirectoryServices.ActiveDirectory;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowModel _model;

        private ReactivePropertySlim<bool> CanUp { get; } = new(false);
        private ReactivePropertySlim<bool> CanDown { get; } = new(false);

        public ReactiveCollection<DetailViewModel> Details { get; } = [];

        public ReactiveCommand UpCommand { get; } = new();
        public ReactiveCommand DownCommand { get; } = new();

        public MainWindow()
        {
            _model = new();

            InitDetails();

            UpCommand = CanUp.Select(x => x).ToReactiveCommand();
            UpCommand.Subscribe(() =>
            {
                if (IsSelectedRow(dataGrid))
                {
                    var indexes = GetSelectedRowIndexs(dataGrid);
                    _model.MoveUp(indexes);

                    var shiftedIndexes = indexes.Select(x => x - 1).ToList();
                    MoveSelectedRowIndex(shiftedIndexes);
                }
            });

            DownCommand = CanDown.Select(x => x).ToReactiveCommand();
            DownCommand.Subscribe(() =>
            {
                if (IsSelectedRow(dataGrid))
                {
                    var indexes = GetSelectedRowIndexs(dataGrid);
                    _model.MoveDown(indexes);

                    var shiftedIndexes = indexes.Select(x => x + 1).ToList();
                    MoveSelectedRowIndex(shiftedIndexes);
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

            firstAction = ActionFirst;

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

        private void MoveSelectedRowIndex(List<int> indexes)
        {
            // CurrentCell設定
            DataGridCellInfo cellInfo = new DataGridCellInfo(dataGrid.Items[indexes[0]], dataGrid.Columns[1]);
            dataGrid.CurrentCell = cellInfo;
            dataGrid.SelectedIndex = indexes[0];

            // 選択セル群設定
            dataGrid.SelectedItems.Clear();
            foreach (var index in indexes)
            {
                dataGrid.SelectedItems.Add(Details[index]);
            }

        }

        private Action<SelectionChangedEventArgs>? firstAction;

        private void ActionFirst(SelectionChangedEventArgs e)
        {
            CanUp.Value = true;
            CanDown.Value = true;

            foreach (DetailViewModel item in e.AddedItems)
            {
                if (item.Number.Value == 1)
                {
                    CanUp.Value = false;
                }
                else if (item.Number.Value == MainWindowModel.RowAmount)
                {
                    CanDown.Value = false;
                }
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (firstAction is not null)
            {
                firstAction(e);

                firstAction = null;
            }

            foreach (DetailViewModel item in e.AddedItems)
            {
                if (item.Number.Value == 1)
                {
                    CanUp.Value = false;
                }
                else if (item.Number.Value == MainWindowModel.RowAmount)
                {
                    CanDown.Value = false;
                }
            }

            foreach (DetailViewModel item in e.RemovedItems)
            {
                if (item.Number.Value == 1)
                {
                    CanUp.Value = true;
                }
                else if (item.Number.Value == MainWindowModel.RowAmount)
                {
                    CanDown.Value = true;
                }
            }
        }
        // TODO : 追従スクロール
    }
}