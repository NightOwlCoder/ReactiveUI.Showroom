using System;
using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using Sextant;
using Showroom.Base;
using Showroom.Coffee;
using Showroom.CollectionView.Scroll;
using Showroom.Scroll;
using Splat;

namespace Showroom.CollectionView
{
    public class CollectionOptionsViewModel : ViewModelBase
    {
        private readonly IParameterViewStackService _viewStackService;

        public CollectionOptionsViewModel()
        {
            _viewStackService = Locator.Current.GetService<IParameterViewStackService>();

            Navigate = ReactiveCommand.CreateFromObservable<CollectionOptionViewModel, Unit>(ExecuteNavigate);

            Items = new ObservableCollection<CollectionOptionViewModel>
            {
                new CollectionOptionViewModel{ Option = CollectionOption.DetailNavigation },
                new CollectionOptionViewModel{ Option = CollectionOption.Search },
                new CollectionOptionViewModel{ Option = CollectionOption.InfiniteScroll }
            };
        }

        public ObservableCollection<CollectionOptionViewModel> Items { get; set; }

        public ReactiveCommand<CollectionOptionViewModel, Unit> Navigate { get; set; }

        private IObservable<Unit> ExecuteNavigate(CollectionOptionViewModel arg)
        {
            // HACK: [rlittlesii: July 04, 2020] Make this not suck, this is a great case for routes.
            switch (arg.Option)
            {
                case CollectionOption.DetailNavigation:
                    return _viewStackService.PushPage<SearchCollectionViewModel>();
                case CollectionOption.Search:
                    return _viewStackService.PushPage<SearchCollectionViewModel>();
                case CollectionOption.InfiniteScroll:
                    return _viewStackService.PushPage<InfiniteCollectionViewModel>();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}