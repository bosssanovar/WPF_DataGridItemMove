using Reactive.Bindings;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class DetailViewModel
    {
        public ReactivePropertySlim<int> Number { get; } = new(1);

        public ReactivePropertySlim<string> Text1 { get; } = new(string.Empty);

        public ReactivePropertySlim<string> Text2 { get; } = new(string.Empty);

        public ReactivePropertySlim<string> Text3 { get; } = new(string.Empty);

        public DetailViewModel(int number, string text1, string text2, string text3)
        {
            Number.Value = number;
            Text1.Value = text1;
            Text2.Value = text2;
            Text3.Value = text3;
        }
    }
}
