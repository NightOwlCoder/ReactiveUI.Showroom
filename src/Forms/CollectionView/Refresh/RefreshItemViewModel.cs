using System;
using ReactiveUI;
using Showroom.Base;
using Showroom.Scroll;

namespace Showroom.CollectionView.Refresh
{
    public class RefreshItemViewModel : ItemViewModelBase
    {
        private Guid _id;
        private RoastType _roast;
        private string _brand;
        private string _coffee;
        private PackagedAs _packaging;

        public RefreshItemViewModel(InventoryDto inventoryDto)
        {
            Id = inventoryDto.Id;
            Brand = inventoryDto.BrandName;
            Coffee = inventoryDto.CoffeeName;
        }

        public Guid Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }

        public string Brand
        {
            get => _brand;
            set => this.RaiseAndSetIfChanged(ref _brand, value);
        }

        public string Coffee
        {
            get => _coffee;
            set => this.RaiseAndSetIfChanged(ref _coffee, value);
        }

        public RoastType Roast
        {
            get => _roast;
            set => this.RaiseAndSetIfChanged(ref _roast, value);
        }

        public PackagedAs Packaging
        {
            get => _packaging;
            set => this.RaiseAndSetIfChanged(ref _packaging, value);
        }
    }
}