using System;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using Sextant;

namespace Showroom.Base
{
    public abstract class ViewModelBase : ReactiveObject, INavigable
    {
        public virtual string Id { get; }

        public virtual IObservable<Unit> WhenNavigatedTo(INavigationParameter parameter) =>
            Observable.Return(Unit.Default);

        public virtual IObservable<Unit> WhenNavigatedFrom(INavigationParameter parameter) =>
            Observable.Return(Unit.Default);

        public virtual IObservable<Unit> WhenNavigatingTo(INavigationParameter parameter) =>
            Observable.Return(Unit.Default);
    }
}