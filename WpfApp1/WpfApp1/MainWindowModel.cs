using Reactive.Bindings;
using Reactive.Bindings.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class MainWindowModel
    {
        public ReactiveCollection<DetailModel> Details { get; } = [];

        public MainWindowModel()
        {
            InitDetails();
        }

        private void InitDetails()
        {
            for (int i = 0; i < 10; i++)
            {
                Details.Add(new($"item 1-{i + 1}", $"item 2-{i + 1}", $"item 3-{i + 1}"));
            }
        }

        public void MoveUp(List<int> indexes)
        {
            var sortedIndexes = indexes.OrderBy(a => a);
            foreach (var index in sortedIndexes)
            {
                // TODO : Moveは使えない、設定値を1つずつ移動するほかなさそう
                // TODO : DetailModelのインスタンスを同期購読すれはいけるのでは？
                Details.Move(index, index - 1);
            }
        }

        public void MoveDown(List<int> indexes)
        {
            var sortedIndexes = indexes.OrderBy(a => -a);
            foreach (var index in sortedIndexes)
            {
                Details.Move(index, index + 1);
            }
        }
    }
}
