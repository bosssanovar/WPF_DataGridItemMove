using Reactive.Bindings;

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
    }
}
