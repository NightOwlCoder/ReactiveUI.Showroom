using System;
using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using Sextant;
using Showroom.Base;
using Showroom.CollectionView;
using Splat;
using static Showroom.FontAwesomeIcons;

namespace Showroom.Navigation
{
    public class NavigationRootViewModel : ViewModelBase
    {
        private readonly IParameterViewStackService _parameterViewStackService;
        private ObservableCollection<NavigationItemViewModel> _navigationItems;

        public NavigationRootViewModel()
        {
            _parameterViewStackService = Locator.Current.GetService<IParameterViewStackService>();

            NavigationItems = new ObservableCollection<NavigationItemViewModel>
            {
                new NavigationItemViewModel { Title = "List View", Icon = ListAlt, IViewFor = typeof(ListOptionsViewModel) },
                new NavigationItemViewModel { Title = "Collection View", Icon = LayerGroup, IViewFor = typeof(CollectionOptionsViewModel) }
            };

            Navigate = ReactiveCommand.CreateFromObservable<NavigationItemViewModel, Unit>(ExecuteNavigate);
        }

        public ObservableCollection<NavigationItemViewModel> NavigationItems
        {
            get => _navigationItems;
            set => this.RaiseAndSetIfChanged(ref _navigationItems, value);
        }

        public ReactiveCommandBase<NavigationItemViewModel,Unit> Navigate { get; set; }

        private IObservable<Unit> ExecuteNavigate(NavigationItemViewModel navigationItemViewModel)
        {
            var navigable = (INavigable)Locator.Current.GetService(navigationItemViewModel.IViewFor);
            return _parameterViewStackService.PushPage(navigable, resetStack: true);
        }
    }
}