using CommunityToolkit.Maui.Views;
using Maui.GoogleMaps;
using Microsoft.Maui.Devices.Sensors;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RekodLampuJalan.Models;
using RekodLampuJalan.Utils;
using System.Text.Json;
using Microsoft.Maui.Controls.Internals;
using CommunityToolkit.Maui.Core.Primitives;

namespace RekodLampuJalan.Views;

public partial class MapsEdit : ContentPage
{
    List<MapPin> mapPins;
    List<FlyCable> flyCables;
    bool flyCableFlg = false;
    Position flyCableFirst;
    bool firstPinClicked=false;
    private const string ApiKey = "AIzaSyCMnvgz6mvIdF8-_gzdJJeag1DmE8IEaBs"; // Replace with your actual API key
    private readonly HttpClient _httpClient = new HttpClient();
    CommonApi ApiService;
    public MapsEdit()
	{
		InitializeComponent();
        LoadCurrentLocation();
        //temp
        // map.PinClicked += pinClicked;
        map.PinDragEnd += OnPinDragEnd;
        ApiService = new CommonApi();
        getData();
        //if (getSync != null && getSync.Result != null && getSync.Result.MapPin != null)
        //{
        //    mapPins = getSync.Result.MapPin;
        //}
        //else
        //{
        //    mapPins = new List<MapPin>();
        //}
        //if (getSync != null && getSync.Result != null && getSync.Result.FlyCable != null)
        //{
        //    flyCables = getSync.Result.FlyCable;
        //}
        //else
        //{
        //    flyCables = new List<FlyCable>();
        //}

        // ApiService = new Api();
    }
    private async void getData()
    {
        var stringRespond = await ApiService.GetDataAsync();
        DataResponse getSync = stringRespond;
        mapPins = getSync.map_pin;
        mapPins.ForEach(pinData =>
        {
            //temp
            //string iconName = "";
            //if (string.IsNullOrEmpty(iconName))
            //{
            //    if (pinData.jenis_item == "Tiang Long Double Arm")
            //        iconName = "doublearm";
            //    else if (pinData.jenis_item == "Tiang Long Single Arm")
            //        iconName = "singlearm";
            //    else if (pinData.jenis_item == "Feeder Pillar")
            //        iconName = "feeder";
            //}

            //var pin = new Pin
            //{
            //    Icon = BitmapDescriptorFactory.FromBundle(iconName),
            //    Type = PinType.Place,
            //    Label = pinData.jenis_item,
            //    Address=pinData.id.ToString(),
            //    Position = new Position(pinData.lattitude, pinData.longitude),
            //    IsDraggable = true
            //};

            //map.Pins.Add(pin);
        });
        flyCables = getSync.fly_cable;
        flyCables.ForEach(cableData =>
        {
            Polyline flyCable = new Polyline();
            flyCable.StrokeWidth = 3f;
            flyCable.StrokeColor = Colors.Black;
            flyCable.Positions.Add(new Position(cableData.start_lattitude,cableData.start_longitude));
            flyCable.Positions.Add(new Position(cableData.end_lattitude, cableData.end_longitude));
            flyCable.IsClickable = true;
            flyCable.Clicked += FlyCable_Clicked;
            map.Polylines.Add(flyCable);
        });
    }
    private async void LoadCurrentLocation()
    {
        try
        {
            // Request location permission
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            if (status != PermissionStatus.Granted)
            {
                await DisplayAlert("Location Permission", "Location permission is required to show your position on the map.", "OK");
                return;
            }

            // Get current location
            var location = await Geolocation.Default.GetLocationAsync(new GeolocationRequest
            {
                DesiredAccuracy = GeolocationAccuracy.Best,
                Timeout = TimeSpan.FromSeconds(10)
            });

            if (location != null)
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude,location.Longitude), Distance.FromMeters(1000)), true);
              // GoogleMap.MoveToRegion(Microsoft.Maui.Maps.MapSpan.FromCenterAndRadius(new Location(location.Latitude, location.Longitude),
                 //   Microsoft.Maui.Maps.Distance.FromKilometers(1))); // Zoom level


            }

           
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting location: {ex.Message}");
        }
    }
    private async void logoutClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MapsView??refresh=true");
    }
    private async void OnCurrentLocationClicked(object sender, EventArgs e)
    {
        var location = await Geolocation.Default.GetLocationAsync(new GeolocationRequest
        {
            DesiredAccuracy = GeolocationAccuracy.Best,
            Timeout = TimeSpan.FromSeconds(10)
        });

        if (location != null)
        {
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMeters(1000)), true);
        }
    }
    private async void OnPinDragEnd(object sender, PinDragEventArgs e)
    {
        Pin dragedPin = e.Pin;
       // MapPin dragedItem = mapPins.Where(x => x.id == dragedPin.Address).First();
        var resultAPI =await ApiService.UpdateMapPinLocationAsync(int.Parse(dragedPin.Address), dragedPin.Position.Latitude, dragedPin.Position.Longitude);
       // dragedItem._pin.Position = dragedPin.Position;
    }
    private async void OnDragStarting(object sender, DragStartingEventArgs e)
    {
        if (sender is DragGestureRecognizer dragGesture)
        {
            if (dragGesture.Parent is Image dragImage)
            {
                var iconName = dragImage.Source.ToString();
                e.Data.Properties.Add("Icon", Path.GetFileNameWithoutExtension(iconName.Replace("File: ", "").Trim()));
            }
        }


        e.Data.Text = "My text data goes here";
    }
    private async void OnDragOver(object sender, DragEventArgs e)
    {
        
       // e.AcceptedOperation = DataPackageOperation.None;
    }
    private async void OnDrop(object sender, DropEventArgs e)
    {

    }
    //temp
    //private async void OnDrop(object sender, DropEventArgs e)
    //{
    //    string iconName = e.Data.Properties["Icon"] as string;
    //    var position=e.GetPosition(map);
    //    var location = ConvertToLocation(position.Value);
    //    string title="";
    //    if(iconName=="doublearm")
    //    {
    //        title = "Tiang Long Double Arm";
    //    }
    //    else if(iconName=="singlearm")
    //    {
    //        title = "Tiang Long Single Arm";
    //    }
    //    else if(iconName=="feeder")
    //    {
    //        title = "Feeder Pillar";
    //    }
    //    if(iconName == "feeder")
    //    {
    //        var popup = new PopUp.FeederDetailPopUp("Feeder Pillar","", "","","","","","","");
    //        var result = await this.ShowPopupAsync(popup) as string;
    //        if(result=="yes")
    //        {

    //            //string uniqID = Guid.NewGuid().ToString();
    //            Pin _pin = new Pin()
    //            {
    //                Icon = BitmapDescriptorFactory.FromBundle(iconName),
    //                Type = PinType.Place,
    //                Label = title,
    //                Position = new Position(location.Latitude, location.Longitude),
    //                IsDraggable = true,
    //            };
    //            map.Pins.Add(_pin);
    //        }
    //        else
    //        {

    //        }
    //    }
    //    else
    //    {
    //        var popup = new PopUp.PinDetailsPopUp(title, "", "","","","","","");
    //        var result = await this.ShowPopupAsync(popup) as EntryPopUpResult;
    //        //string uniqID = Guid.NewGuid().ToString();
    //        Pin _pin = new Pin()
    //        {
    //            Icon = BitmapDescriptorFactory.FromBundle(iconName),
    //            Type = PinType.Place,
    //            Label = title,
    //            Position = new Position(location.Latitude, location.Longitude),
    //            IsDraggable = true,
    //        };
    //        MapPinItem _newMapPin = new MapPinItem();

    //        _newMapPin.lattitude = location.Latitude;
    //        _newMapPin.longitude = location.Longitude;
    //        if (result != null)
    //        {
    //            _newMapPin.jenis_item = result._Jenis;
    //            _newMapPin.jenis_lampu = result._JenisLampu;
    //            _newMapPin.tinggi_tiang = result._TinggiTiang;
    //            _newMapPin.material_tiang = result._MaterialTiang;
    //            _newMapPin.pintu_penyelenggaraan = result._PintuPenyelenggaraan;
    //            _newMapPin.panjang_lengan_lampu = result._PanjangLenganLampu;
    //            _newMapPin.jenis_tiang = result._JenisTiang;
    //            _newMapPin.attachment = result._AttachImagePath;
    //        }
    //        else
    //        {
    //            _newMapPin.jenis_item = "";
    //            _newMapPin.jenis_lampu = "";
    //            _newMapPin.tinggi_tiang = "";
    //            _newMapPin.material_tiang = "";
    //            _newMapPin.pintu_penyelenggaraan = "";
    //            _newMapPin.panjang_lengan_lampu = "";
    //            _newMapPin.jenis_tiang = "";
    //        }
    //        var ApiResult = await ApiService.InsertMapPin(_newMapPin.jenis_item, _newMapPin.jenis_lampu,
    //            _newMapPin.tinggi_tiang, _newMapPin.material_tiang,
    //            _newMapPin.pintu_penyelenggaraan, _newMapPin.panjang_lengan_lampu, _newMapPin.jenis_tiang, _newMapPin.lattitude, _newMapPin.longitude, _newMapPin.attachment);
    //        var jsonResponse = JsonConvert.DeserializeObject<JObject>(ApiResult);
    //        string newId = jsonResponse["id"].ToString();
    //        string newAttachment = "";
    //        if (jsonResponse["attachment"] != null)
    //        {
    //            newAttachment = jsonResponse["attachment"].ToString();
    //        }

    //        _newMapPin.id = int.Parse(newId);
    //        _newMapPin.attachment = newAttachment;
    //        _pin.Address = newId;
    //        mapPins.Add(_newMapPin);
    //        map.Pins.Add(_pin);
    //        map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMeters(1)), true);
    //    }

    //}
    //temp
    //private async void pinClicked(object sender, PinClickedEventArgs e)
    //{
    //    if(flyCableFlg)
    //    {
    //        if(!firstPinClicked)
    //        {
    //            flyCableFirst = e.Pin.Position;
    //            AddFlyCableLable.Text = "Please select second point";
    //            firstPinClicked = true;
    //        }
    //        else
    //        {
    //            Polyline flyCable = new Polyline();
    //            flyCable.StrokeWidth = 3f;
    //            flyCable.StrokeColor = Colors.Black;
    //            flyCable.Positions.Add(flyCableFirst);
    //            flyCable.Positions.Add(e.Pin.Position);
    //            flyCable.IsClickable = true;
    //            flyCable.Clicked += FlyCable_Clicked;
    //            FlyCable _flyCable = new FlyCable();
    //            _flyCable.start_lattitude = flyCableFirst.Latitude;
    //            _flyCable.start_longitude = flyCableFirst.Longitude;
    //            _flyCable.end_lattitude = e.Pin.Position.Latitude;
    //            _flyCable.end_longitude = e.Pin.Position.Longitude;
    //            var ApiResult =await ApiService.InsertFlyCable(_flyCable.start_lattitude, _flyCable.start_longitude, _flyCable.end_lattitude, _flyCable.end_longitude);
    //            var jsonResponse = JsonConvert.DeserializeObject<JObject>(ApiResult);
    //            string newId = jsonResponse["id"].ToString();
    //            _flyCable.id =int.Parse(newId);
    //            flyCables.Add(_flyCable);
    //            map.Polylines.Add(flyCable);

    //            flyCableFirst = new Position();
    //            AddFlyCableLable.Text = "Please select first point";
    //            firstPinClicked = false;
    //        }
    //    }
    //    else
    //    {
    //        Pin clickedPin = e.Pin;

    //        if(e.Pin.Label== "Feeder Pillar")
    //        {
    //            var popup = new PopUp.FeederPopUp("Feeder Pillar","", "","","","","","","");
    //            var result = await this.ShowPopupAsync(popup) as FeederEditResult;
    //            if(result._DeleteFlag)
    //            {
    //                map.Pins.Remove(clickedPin);
    //            }
    //        }
    //        else
    //        {
    //            MapPinItem clickedItem = mapPins.Where(x => x.id.ToString() == clickedPin.Address).First();
    //            var popup = new PopUp.ClickedPinPopUp(clickedItem.jenis_item, clickedItem.jenis_lampu, clickedItem.tinggi_tiang, clickedItem.jenis_tiang,
    //clickedItem.material_tiang, clickedItem.pintu_penyelenggaraan,
    //clickedItem.panjang_lengan_lampu, clickedItem.attachment);
    //            var result = await this.ShowPopupAsync(popup) as EditPopUpResult;
    //            if (result != null)
    //            {
    //                if (result._DeleteFlag)
    //                {
    //                    var ApiResult = await ApiService.DeleteMapPinAsync(mapPins[map.Pins.IndexOf(clickedPin)].id);
    //                    mapPins.Remove(clickedItem);
    //                    map.Pins.Remove(clickedPin);
    //                }
    //                else
    //                {

    //                    clickedItem.jenis_item = result._Jenis;
    //                    clickedItem.jenis_lampu = result._JenisLampu;
    //                    clickedItem.tinggi_tiang = result._TinggiTiang;
    //                    clickedItem.jenis_tiang = result._JenisTiang;
    //                    clickedItem.material_tiang = result._MaterialTiang;
    //                    clickedItem.pintu_penyelenggaraan = result._PintuPenyelenggaraan;
    //                    clickedItem.panjang_lengan_lampu = result._PanjangLenganLampu;
    //                    clickedItem.attachment = result._AttachImagePath;
    //                    var APIResult = await ApiService.UpdateMapPin(clickedItem.id, clickedItem.jenis_item, clickedItem.jenis_lampu, clickedItem.tinggi_tiang, clickedItem.jenis_tiang,
    //                clickedItem.material_tiang, clickedItem.pintu_penyelenggaraan,
    //                clickedItem.panjang_lengan_lampu, clickedItem.attachment);
    //                    var jsonResponse = JsonConvert.DeserializeObject<JObject>(APIResult);
    //                    // string newId = jsonResponse["id"].ToString();
    //                    string newAttachment = "";
    //                    if (jsonResponse["attachment"] != null)
    //                    {
    //                        newAttachment = jsonResponse["attachment"].ToString();
    //                    }

    //                    clickedItem.attachment = newAttachment;
    //                }
    //            }

    //        }

    //    }


    //}

    private async void FlyCable_Clicked(object? sender, EventArgs e)
    {
        if (sender is Polyline polyline)
        {
            // Show confirmation dialog
            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "Confirm Deletion",
                "Are you sure you want to delete this fly cable?",
                "Yes",
                "No");

            if (confirm)
            {
               // int ddd = map.Polylines.IndexOf(polyline);
                var ApiResult =await ApiService.DeleteFlyCableAsync(flyCables[map.Polylines.IndexOf(polyline)].id);
                flyCables.Remove(flyCables[map.Polylines.IndexOf(polyline)]);
                map.Polylines.Remove(polyline);
                
                Console.WriteLine("Polyline removed");
            }
        }
        //throw new NotImplementedException();
    }

    private void MySwitch_Toggled(object sender, ToggledEventArgs e)
    {
        bool isOn = e.Value;
        flyCableFlg = isOn;
        if(isOn)
        {
            stacklayoutAddIconTools.IsEnabled=false;
            stacklayoutAddIconTools.IsVisible = false;
            AddFlyCableLable.IsVisible=true;
            AddFlyCableLable.Text = "Please select first point";
            flyCableFirst = new Position();
            firstPinClicked = false;
        }
        else
        {
            stacklayoutAddIconTools.IsEnabled=true;
            stacklayoutAddIconTools.IsVisible = true;
            AddFlyCableLable.IsVisible = false;
            flyCableFirst = new Position();
            firstPinClicked = false;
        }
        //Console.WriteLine("Switch is " + (isOn ? "ON" : "OFF"));
    }
    private async void OnSearchButtonPressed(object sender, EventArgs e)
    {
        string searchText = SearchBox.Text;
        if (string.IsNullOrWhiteSpace(searchText))
            return;

        var location = await GetLocationFromGoogle(searchText);
        if (location != null)
        {
            map.MoveToRegion(MapSpan.FromCenterAndRadius
                (new Position(location.Latitude, location.Longitude)
                , Distance.FromMeters(1000)), true);
        }
    }
    private async Task<Location?> GetLocationFromGoogle(string query)
    {
        string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={query}&key={ApiKey}";
        var response = await _httpClient.GetStringAsync(url);

        using JsonDocument doc = JsonDocument.Parse(response);
        var results = doc.RootElement.GetProperty("results");

        if (results.GetArrayLength() > 0)
        {
            var location = results[0].GetProperty("geometry").GetProperty("location");
            double lat = location.GetProperty("lat").GetDouble();
            double lng = location.GetProperty("lng").GetDouble();
            return new Location(lat, lng);
        }
        return null;
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

}