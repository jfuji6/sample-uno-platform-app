using System.Threading.Tasks;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using MyUnoApp.Models;
using UnitsNet;
using System.Reactive.Linq;

namespace MyUnoApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IMotorModel _motor;
        
        public ReactiveCommand<double> MoveMotorCommand { get; }
        public IReadOnlyReactiveProperty<Length> Output { get; }
        public IReadOnlyReactiveProperty<bool> CanExecute { get; }
        public MainPageViewModel()
        {
            _motor = new MotorModel();
            
            CanExecute = _motor.IsBusy.Select(t => !t).ToReactiveProperty().AddTo(Disposables);            
            
            Output = _motor.Position;            
            MoveMotorCommand = new ReactiveCommand<double>(CanExecute)
                .WithSubscribe(async (x) => await MoveMotorAsync(x))
                .AddTo(Disposables);
        }

        private async Task MoveMotorAsync(double position)
        {            
            await _motor.MoveAsync(Length.FromCentimeters(position));            
        }
    }
}
