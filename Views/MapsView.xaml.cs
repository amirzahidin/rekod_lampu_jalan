using CommunityToolkit.Maui.Views;
using Maui.GoogleMaps;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;
using RekodLampuJalan.Models;
using RekodLampuJalan.Utils;
using RekodLampuJalan.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using RekodLampuJalan.Services;
using Plugin.Maui.Audio;



namespace RekodLampuJalan.Views
{
    public partial class MapsView : ContentPage
    {

        MapsViewViewModel _viewModel;
        CommonApi ApiService;
        private IAudioManager audioManager = AudioManager.Current;
        public MapsView()
        {
            InitializeComponent();
            //LoadCurrentLocation();
            ApiService = new CommonApi();
            map.PinClicked += pinClicked;
            map.PinDragEnd += OnPinDragEnd;
            _viewModel = new MapsViewViewModel();
           this.BindingContext = _viewModel;
            foreach(Pin pin_ in _viewModel.Pins)
            {
                map.Pins.Add(pin_);
            }
            foreach (Polyline polyline_ in _viewModel.PolyLines)
            {
                polyline_.Clicked += FlyCable_Clicked;
                polyline_.IsClickable = true;
                map.Polylines.Add(polyline_);
            }
            ResetButtonStyles();
            btnStreet.BackgroundColor = Color.FromArgb("#007AFF");
            btnStreet.TextColor = Colors.White;
            map.ZIndex = 0;
            _viewModel.Pins.CollectionChanged += (s, e) =>
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
            _viewModel.PolyLines.CollectionChanged += (s, e) =>
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
            WeakReferenceMessenger.Default.Register<MapViewBusyMessage>(this, (r, message) =>
            {
                if(!message.IsBusy)
                {
                    _viewModel.IsBusy=false;
                }
                else if(message.IsBusy)
                {
                    _viewModel.IsBusy=true;
                }
            });
            WeakReferenceMessenger.Default.Register<MoveMapMessage>(this, (r, message) =>
            {
                var position = new Position(message.Latitude, message.Longitude);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(1000)), true);
            });

            WeakReferenceMessenger.Default.Register<LocationPermissionDeniedMessage>(this, (r, message) =>
            {
                DisplayAlert("Location Permission", "Location permission is required to show your position on the map.", "OK");
            });

            WeakReferenceMessenger.Default.Register<LocationErrorMessage>(this, (r, message) =>
            {
                DisplayAlert("Error", message.ErrorMessage, "OK");
            });
            WeakReferenceMessenger.Default.Register<MapsViewClearPinMessage>(this, (r, message) =>
            {
                map.Pins.Clear();
                map.Polylines.Clear();
            });
        }
        private async void pinClicked(object sender, PinClickedEventArgs e)
        {
        // Execute the command from ViewModel
            if (_viewModel.pinClicked.CanExecute(e.Pin))
            {
                _viewModel.pinClicked.Execute(e.Pin);
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.setCurrentLocation.Execute(null);
        }
        private async void OnDragStarting(object sender, DragStartingEventArgs e)
        {
            if (sender is DragGestureRecognizer dragGesture)
            {
                Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(100));
                var audioFile = await FileSystem.OpenAppPackageFileAsync("click.mp3");
                var player = audioManager.CreatePlayer(audioFile);
                player.Play();
                if (dragGesture.Parent is Image dragImage)
                {
                    var iconName = dragImage.Source.ToString();
                    e.Data.Properties.Add("Icon", Path.GetFileNameWithoutExtension(iconName.Replace("File: ", "").Trim()));
                }
            }


            e.Data.Text = "My text data goes here";
        }
        private async void OnPinDragEnd(object sender, PinDragEventArgs e)
        {
            Pin dragedPin = e.Pin;
            _viewModel.pinDraged.Execute(dragedPin);
            //var resultAPI = await ApiService.UpdateMapPinLocationAsync(int.Parse(dragedPin.Address), dragedPin.Position.Latitude, dragedPin.Position.Longitude);
            
        }
        private async void OnDrop(object sender, DropEventArgs e)
        {
            string iconName = e.Data.Properties["Icon"] as string;
            Point position = e.GetPosition(map).Value;
            Location dropLocation= ConvertToLocation(position);
            //var location = ConvertToLocation(position.Value);
            _viewModel.insertNewMapPin.Execute((iconName, dropLocation));
        }
        private async void FlyCable_Clicked(object? sender, EventArgs e)
        {
            _viewModel.IsBusy = true;
            if (sender is Polyline polyline)
            {
                FlyCable clickedFlyCable = App.MainData.fly_cable.Where(x=>x.id==((int)polyline.Tag)).FirstOrDefault();
                IPopupService _popupService =new PopupService();
                if(clickedFlyCable!=null)
                {
                   var result= await _popupService.ShowFlyCablePopUpAsync(clickedFlyCable);
                    if(result=="Delete")
                    {
                        if (App.login)
                        {
                            // Show confirmation dialog
                            bool confirm = await Application.Current.MainPage.DisplayAlert(
                                "Confirm Deletion",
                                "Are you sure you want to delete this fly cable?",
                                "Yes",
                                "No");

                            if (confirm)
                            {
                                int flycableId = (int)polyline.Tag;
                                // int ddd = map.Polylines.IndexOf(polyline);
                                var ApiResult = await ApiService.DeleteFlyCableAsync(flycableId);
                                App.MainData.fly_cable.Remove(App.MainData.fly_cable.Where(x => x.id == flycableId).First());
                                map.Polylines.Remove(polyline);
                                await Application.Current.MainPage.DisplayAlert("Delete", "Delete is successful", "OK");

                                // Console.WriteLine("Polyline removed");
                            }
                        }
                    }
                }
                

                

            }
            //throw new NotImplementedException();
            _viewModel.IsBusy = false;
        }
        public Location ConvertToLocation(Point dropPoint)
        {
            if (map == null)
                return null;

            // Get current map region
            var mapSpan = map.VisibleRegion;
            if (mapSpan == null)
                return null;

            // Map size
            double mapWidth = map.Width;
            double mapHeight = map.Height;

            // Center and bounds
            var center = mapSpan.Center;
            var latDelta = mapSpan.LatitudeDegrees;
            var lonDelta = mapSpan.LongitudeDegrees;

            // Relative position from top-left (0,0)
            double xRatio = dropPoint.X / mapWidth;
            double yRatio = dropPoint.Y / mapHeight;

            // Calculate latitude and longitude based on relative offset
            double lat = center.Latitude + (0.5 - yRatio) * latDelta;
            double lon = center.Longitude + (xRatio - 0.5) * lonDelta;

            return new Location(lat, lon);
        }
        private void OnMapTypeButtonClicked(object sender, EventArgs e)
        {
            // Reset all buttons to default
            ResetButtonStyles();

            var button = (Button)sender;
            button.BackgroundColor = Color.FromArgb("#007AFF");   // active color
            button.TextColor = Colors.White;

            switch (button.Text)
            {
                case "Street":
                    map.MapType = MapType.Street;
                    break;
                case "Satellite":
                    map.MapType = MapType.Satellite;
                    break;
                case "Hybrid":
                    map.MapType = MapType.Hybrid;
                    break;
                case "Terrain":
                    map.MapType = MapType.Terrain;
                    break;
            }
        }

        private void ResetButtonStyles()
        {
            btnStreet.BackgroundColor = Colors.LightGray;
            btnStreet.TextColor = Colors.Black;

            btnSatellite.BackgroundColor = Colors.LightGray;
            btnSatellite.TextColor = Colors.Black;

            btnHybrid.BackgroundColor = Colors.LightGray;
            btnHybrid.TextColor = Colors.Black;

            btnTerrain.BackgroundColor = Colors.LightGray;
            btnTerrain.TextColor = Colors.Black;
        }

    }
}

