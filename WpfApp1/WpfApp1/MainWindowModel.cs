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
        public const int RowAmount = 100;

        public ReactiveCollection<DetailModel> Details { get; } = [];

        public MainWindowModel()
        {
            InitDetails();
        }

        private void InitDetails()
        {
            for (int i = 0; i < RowAmount; i++)
            {
                Details.Add(new()
                {
                    Text1 = $"item 1-{i + 1}",
                    Text2 = $"item 2-{i + 1}",
                    Text3 = $"item 3-{i + 1}"
                });
            }
        }

        public void MoveUp(List<int> indexes)
        {
            var sortedIndexes = indexes.OrderBy(a => a);
            foreach (var index in sortedIndexes)
            {
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
