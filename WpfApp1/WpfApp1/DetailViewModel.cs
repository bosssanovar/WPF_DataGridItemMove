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
        private ReactivePropertySlim<DetailModel> _model = new();

        public ReactivePropertySlim<int> Number { get; } = new(1);

        public ReactivePropertySlim<string> Text1 { get; } = new(string.Empty);

        public ReactivePropertySlim<string> Text2 { get; } = new(string.Empty);

        public ReactivePropertySlim<string> Text3 { get; } = new(string.Empty);

        public DetailViewModel(int number, DetailModel model)
        {
            _model.Value = model;

            Number.Value = number;

            Text1 = _model.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                x => x.Text1,
                x =>
                {
                    _model.Value.Text1 = x;

                    return _model.Value;
                });

            Text2 = _model.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                x => x.Text2,
                x =>
                {
                    _model.Value.Text2 = x;

                    return _model.Value;
                });

            Text3 = _model.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                x => x.Text3,
                x =>
                {
                    _model.Value.Text3 = x;

                    return _model.Value;
                });
        }

        public void UpdateModel(DetailModel model)
        {
            _model.Value = model;
        }

        public bool IsMatchModel(DetailModel model)
        {
            return _model.Value == model;
        }
    }
}
