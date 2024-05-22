using Reactive.Bindings;
using Reactive.Bindings.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class DetailViewModel
    {
        private DetailModel _model;

        public ReactivePropertySlim<int> Number { get; } = new(1);

        public ReactivePropertySlim<string> Text1 { get; private set; } = new(string.Empty);

        public ReactivePropertySlim<string> Text2 { get; private set; } = new(string.Empty);

        public ReactivePropertySlim<string> Text3 { get; private set; } = new(string.Empty);

        public DetailViewModel(int number, DetailModel model)
        {
            _model = model;

            Number.Value = number;

            InitSyncronized();
        }

        private void InitSyncronized()
        {
            Text1 = _model.Text1.ToReactivePropertySlimAsSynchronized(x => x.Value);
            Text2 = _model.Text2.ToReactivePropertySlimAsSynchronized(x => x.Value);
            Text3 = _model.Text3.ToReactivePropertySlimAsSynchronized(x => x.Value);
        }
    }
}
