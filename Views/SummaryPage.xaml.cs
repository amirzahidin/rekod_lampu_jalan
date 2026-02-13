using CommunityToolkit.Mvvm.Messaging;
using Maui.GoogleMaps;
using RekodLampuJalan.Models;
using RekodLampuJalan.Services;
using RekodLampuJalan.ViewModels;
using RekodLampuJalan.Utils;

namespace RekodLampuJalan.Views;

public partial class SummaryPage : ContentPage
{
	SummaryViewModel ViewModel;
	public SummaryPage()
	{
		InitializeComponent();
		ViewModel = new SummaryViewModel();
		this.BindingContext = ViewModel;
        map.PinClicked += Map_PinClicked;
        ViewModel.Pins.CollectionChanged += (s, e) =>
        {
            if (e.NewItems != null)
            {
                foreach (Pin newPin in e.NewItems)
                {
                    map.Pins.Add(newPin);
                }
            }

            if (e.OldItems != null)
            {
                foreach (Pin oldPin in e.OldItems)
                {
                    map.Pins.Remove(oldPin);
                }
            }
        };
        ViewModel.PolyLines.CollectionChanged += (s, e) =>
        {
            if (e.NewItems != null)
            {
                foreach (Polyline newPin in e.NewItems)
                {
                    newPin.IsClickable = true;
                    newPin.Clicked += FlyCable_Clicked;
                    map.Polylines.Add(newPin);
                }
            }

            if (e.OldItems != null)
            {
                foreach (Polyline oldPin in e.OldItems)
                {
                    map.Polylines.Remove(oldPin);
                }
            }
        };
        WeakReferenceMessenger.Default.Register<SummaryMoveMapMessage>(this, (r, message) =>
        {
            var position = new Position(message.Latitude, message.Longitude);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(1000)), true);
        });
        WeakReferenceMessenger.Default.Register<SummaryClearPinMessage>(this, (r, message) =>
        {
            map.Pins.Clear();
            map.Polylines.Clear();
        });
    }
    private async void FlyCable_Clicked(object? sender, EventArgs e)
    {
        ViewModel.IsBusy = true;
        if (sender is Polyline polyline)
        {
            FlyCable clickedFlyCable = App.MainData.fly_cable.Where(x => x.id == ((int)polyline.Tag)).FirstOrDefault();
            IPopupService _popupService = new PopupService();
            if (clickedFlyCable != null)
            {
                var result = await _popupService.ShowFlyCablePopUpAsync(clickedFlyCable);
                
            }
        }
        //throw new NotImplementedException();
        ViewModel.IsBusy = false;
    }
    private void Map_PinClicked(object? sender, PinClickedEventArgs e)
    {
        if (ViewModel.pinClicked.CanExecute(e.Pin))
        {
            ViewModel.pinClicked.Execute(e.Pin);
        }
    }
    //private async void Search_Clicked(object? sender, EventArgs e)
    //{
    //    ViewModel.IsBusy = true;
    //    await ViewModel.searchClicked_();
    //    //ViewModel.searchClicked.Execute(null);
    //    //throw new NotImplementedException();
    //    ViewModel.IsBusy = false;
    //}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        ViewModel.IsBusy=true;
        //  ViewModel.LoadData.Execute(null);
        common common = new common();
        LoadData loadReferences = new LoadData();
        App.MainData = await loadReferences.GetMainDataAsync();
        App.Alamat = await loadReferences.GetAlamatAsync();
        await common.SyncAlamat(App.MainData, App.Alamat);
        ViewModel.IsBusy = false;
    }

}