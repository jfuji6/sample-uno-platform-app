using System;
using System.Reactive.Disposables;
using MyUnoApp.Models;

namespace MyUnoApp.ViewModels
{
    public class ViewModelBase : BindableBase, IDisposable
    {
        protected CompositeDisposable Disposables { get; } = new CompositeDisposable();

        public void Dispose() => Disposables.Dispose();
    }
}