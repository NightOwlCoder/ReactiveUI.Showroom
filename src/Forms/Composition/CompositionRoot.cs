using System;
using ReactiveUI;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Rocket.Surgery.Airframe.Synthetic;
using Serilog;
using Sextant;
using Sextant.XamForms;
using Showroom.ListView;
using Showroom.Main;
using Showroom.Navigation;
using Showroom.Scroll;
using Showroom.Search;
using Showroom.ValueConverters;
using Splat;
using Splat.Serilog;
using Xamarin.Forms;
using CoffeeClientMock = Showroom.ListView.CoffeeClientMock;
using SearchListViewModel = Showroom.Search.SearchListViewModel;

namespace Showroom.Composition
{
    public class CompositionRoot
    {
        // TODO: [rlittlesii: July 03, 2020] Move more towards pure DI.
        public CompositionRoot(IPlatformRegistrar registrar)
        {
            
            RxApp.DefaultExceptionHandler = new ShowroomExceptionHandler();
            Locator.CurrentMutable.InitializeReactiveUI();
            Sextant.Sextant.Instance.InitializeForms();

            Locator
                .CurrentMutable
                .RegisterPlatform(registrar);

            Locator.CurrentMutable.UseSerilogFullLogger(Log.Logger);

            RegisterServices(Locator.CurrentMutable);
            RegisterViews(Locator.CurrentMutable);
            RegisterViewModels(Locator.GetLocator());
        }

        public Page StartPage<TViewModel>()
            where TViewModel : ViewModelBase
        {
            Locator
                .Current
                .GetService<IParameterViewStackService>()
                .PushPage<TViewModel>(resetStack: true, animate: false)
                .Subscribe();

            return Locator.Current.GetNavigationView("NavigationView");
        }

        private static void RegisterViews(IMutableDependencyResolver mutableDependencyResolver)
        {
            mutableDependencyResolver.RegisterView<MainPage, MainViewModel>();
            mutableDependencyResolver.RegisterView<NavigationRoot, NavigationRootViewModel>();
            mutableDependencyResolver.RegisterView<CoffeeList, CoffeeListViewModel>();
            mutableDependencyResolver.RegisterView<CoffeeDetail, CoffeeDetailViewModel>();
            mutableDependencyResolver.RegisterView<Collection, CollectionViewModel>();
            mutableDependencyResolver.RegisterView<ListOptions, ListOptionsViewModel>();
            mutableDependencyResolver.RegisterView<InfiniteScroll, InfiniteScrollViewModel>();
            mutableDependencyResolver.RegisterView<SearchList,SearchListViewModel>();
            mutableDependencyResolver.RegisterView<NewItem, NewItemViewModel>();
        }

        private static void RegisterViewModels(IDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterViewModel<MainViewModel>();
            dependencyResolver.RegisterViewModel<NavigationRootViewModel>();
            dependencyResolver.RegisterViewModel<ListOptionsViewModel>();
            dependencyResolver.RegisterViewModel(() => new CoffeeListViewModel(dependencyResolver.GetService<IParameterViewStackService>(), dependencyResolver.GetService<ICoffeeService>()));
            dependencyResolver.RegisterViewModel(() => new CoffeeDetailViewModel(dependencyResolver.GetService<ICoffeeService>()));
            dependencyResolver.RegisterViewModel<CollectionViewModel>();
            dependencyResolver.RegisterViewModel(() => new SearchListViewModel(dependencyResolver.GetService<IDrinkService>()));
            dependencyResolver.RegisterViewModel<NewItemViewModel>();
            dependencyResolver.RegisterViewModel(() => new InfiniteScrollViewModel(dependencyResolver.GetService<IInventoryDataService>()));
        }

        private static void RegisterServices(IMutableDependencyResolver mutableDependencyResolver)
        {
            mutableDependencyResolver.RegisterLazySingleton<ICoffeeService>(() => new CoffeeService(new CoffeeClientMock()));
            mutableDependencyResolver.RegisterLazySingleton<IDrinkService>(() => new DrinkDataService(new DrinkClientMock()));
            mutableDependencyResolver.RegisterLazySingleton<IInventoryDataService>(() => new InventoryDataService(new CoffeeInventoryMock()));
            mutableDependencyResolver.RegisterLazySingleton<IPopupNavigation>(() => PopupNavigation.Instance);

            // https://reactiveui.net/docs/handbook/data-binding/value-converters#registration
            // mutableDependencyResolver.RegisterConstant(new CamelCaseSplitConverter(), typeof(IBindingTypeConverter));
        }
    }
}