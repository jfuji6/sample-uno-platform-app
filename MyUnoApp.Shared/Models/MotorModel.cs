using System.Threading.Tasks;
using UnitsNet;
using Reactive.Bindings;

namespace MyUnoApp.Models
{
    interface IMotorModel
    {
        IReadOnlyReactiveProperty<bool> IsBusy { get; }
        IReadOnlyReactiveProperty<Length> Position { get; }
        Task MoveAsync(Length position);

    }

    class MotorModel : IMotorModel
    {
        readonly ReactiveProperty<bool> _busy;
        readonly ReactiveProperty<Length> _position;

        public IReadOnlyReactiveProperty<bool> IsBusy => _busy;
        public IReadOnlyReactiveProperty<Length> Position => _position;
        

        public MotorModel()
        {
            _busy = new ReactiveProperty<bool>();
            _position = new ReactiveProperty<Length>();
        }

        public async Task MoveAsync(Length position)
        {
            if ( _busy.Value ) 
                return;

            _busy.Value = true;
            
            try
            {
                if (_position.Value < position)
                {
                    while (_position.Value <= position)
                    {
                        await Task.Delay(3);
                        _position.Value += Length.FromMillimeters(1);
                    }
                }
                else
                {
                    while (_position.Value >= position)
                    {
                        await Task.Delay(3);
                        _position.Value += Length.FromMillimeters(-1);
                    }
                }
            }
            finally
            {
                _busy.Value = false;
            }
        }
    }
}
