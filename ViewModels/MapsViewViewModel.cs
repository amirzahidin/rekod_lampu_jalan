
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Mvvm.Messaging;
using Maui.GoogleMaps;
using Microsoft.Maui.Devices.Sensors;
using RekodLampuJalan.Models;
using RekodLampuJalan.Services;
using RekodLampuJalan.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RekodLampuJalan.ViewModels
{
    public class MapsViewViewModel : ViewModelBase
    {
        private ObservableCollection<Pin> _pins;
        public ObservableCollection<Pin> Pins
        {
            get { return _pins; }
            set
            {
                if (value != _pins)
                {
                    _pins = value;
                    OnPropertyChanged("Pins");
                }
            }
        }

        private ObservableCollection<Polyline> _polyLine;
        public ObservableCollection<Polyline> PolyLines
        {
            get { return _polyLine; }
            set
            {
                if (value != _polyLine)
                {
                    _polyLine = value;
                    OnPropertyChanged("PolyLines");
                }
            }
        }
        private List<FeederPillar> _feederPillars;
        public List<FeederPillar> FeederPillars
        {
            get { return _feederPillars; }
            set
            {
                if (value != _feederPillars)
                {
                    _feederPillars = value;
                    OnPropertyChanged("FeederPillars");
                }
            }
        }
        private bool _isSwitchFlyCableToggled;

        public bool IsSwitchFlyCableToggled
        {
            get { return _isSwitchFlyCableToggled; }
            set
            {
                if (value != _isSwitchFlyCableToggled)
                {
                    _isSwitchFlyCableToggled = value;
                    OnPropertyChanged("IsSwitchToggled");
                    OnSwitchToggled(value);
                }
            }
        }
        private bool _isFlyCableMode;

        public bool IsSFlyCableMode
        {
            get { return _isFlyCableMode; }
            set
            {
                if (value != _isFlyCableMode)
                {
                    _isFlyCableMode = value;
                    OnPropertyChanged("IsSFlyCableMode");
                  
                }
            }
        }
        private bool _isPinStackLayoutVisible;

        public bool IsPinStackLayoutVisible
        {
            get { return _isPinStackLayoutVisible; }
            set
            {
                if (value != _isPinStackLayoutVisible)
                {
                    _isPinStackLayoutVisible = value;
                    OnPropertyChanged("IsPinStackLayoutVisible");

                }
            }
        }

        private bool _isLogin;

        public bool IsLogin
        {
            get { return _isLogin; }
            set
            {
                if (value != _isLogin)
                {
                    _isLogin = value;
                    OnPropertyChanged("IsLogin");

                }
            }
        }
        private bool _isSummaryVisible;

        public bool IsSummaryVisible
        {
            get { return _isSummaryVisible; }
            set
            {
                if (value != _isSummaryVisible)
                {
                    _isSummaryVisible = value;
                    OnPropertyChanged("IsSummaryVisible");

                }
            }
        }
        private string _addFlyCableText;
        public string AddFlyCableText
        {
            get { return _addFlyCableText; }
            set
            {
                if (value != _addFlyCableText)
                {
                    _addFlyCableText = value;
                    OnPropertyChanged("AddFlyCableText");
                }
            }
        }
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (value != _searchText)
                {
                    _searchText = value;
                    OnPropertyChanged("SearchText");
                }
            }
        }
        private string _loginButtonText;
        public string LoginButtonText
        {
            get { return _loginButtonText; }
            set
            {
                if (value != _loginButtonText)
                {
                    _loginButtonText = value;
                    OnPropertyChanged("LoginButtonText");
                }
            }
        }
        private bool _isBusy=false;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (value != _isBusy)
                {
                    _isBusy = value;
                    OnPropertyChanged("IsBusy");

                }
            }
        }
        public ICommand loginClicked { get { return new Command(async () => await loginClicked_()); } }
        public ICommand pinClicked { get { return new Command<Pin>(async (pin) => await OnPinClicked(pin));} }
        public ICommand pinDraged { get { return new Command<Pin>(async (pin) => await OnpinDraged(pin)); } }
        public ICommand setCurrentLocation { get { return new Command(async () => await setCurrentLocation_()); } }
        public ICommand searchLocationAsync { get { return new Command(async () => await searchLocationAsync_()); } }
        public ICommand summaryButtonClicked { get { return new Command(async () => await summaryButtonClicked_()); } }
        public ICommand insertNewMapPin{ get { return new Command<(string iconName, Location point)>(async tuple =>
        {
            await insertNewMapPin_(tuple.iconName, tuple.point);
        });
        }}
        public ICommand refreshAll { get { return new Command(async () => await refreshAll_()); } }
        
        private readonly IPopupService _popupService;
        common commonFunction;
        CommonApi ApiService;
        InsertNewData insertNewDataAPI;
        UpdateData updateDataAPI;
        LocationFunction locationFunction;
        Position flyCableFirst;
        bool firstPinClicked = false;
        public MapsViewViewModel()
        {
            commonFunction = new common();
            ApiService=new CommonApi();
            Pins = new ObservableCollection<Pin>();
            PolyLines = new ObservableCollection<Polyline>();
            _popupService = new PopupService();
            insertNewDataAPI = new InsertNewData();
            updateDataAPI=new UpdateData();
            locationFunction=new LocationFunction();
            IsLogin =false;
            IsPinStackLayoutVisible=true;
            IsSummaryVisible=true;
            LoginButtonText = "Login";
            getData();

        }
        private async void getData()
        {

            foreach (MapPin map_pin in App.MainData.map_pin)
            {

                Pin pin = new Pin
                {
                    Icon = BitmapDescriptorFactory.FromBundle("street_lamp_" + map_pin.jenis_item.ToString()),
                    Type = PinType.Place,
                    Label = App.DropDownItem.Jenis_Item.Where(x=>x.Id==map_pin.jenis_item).First().Item,
                    Position = new Position(map_pin.lattitude, map_pin.longitude),
                    IsDraggable = false,
                    Tag = ConstantValue.mapPinTag+map_pin.id.ToString()
                };
                Pins.Add(pin);
            }
            foreach (FlyCable flyCable in App.MainData.fly_cable)
            {
                Polyline polyLine = new Polyline();
                polyLine.StrokeWidth = 3f;
                polyLine.StrokeColor = Colors.Black;
                polyLine.StrokePattern = StrokePatternBuilder.DashedLine();
                polyLine.Positions.Add(new Position(flyCable.start_lattitude, flyCable.start_longitude));
                polyLine.Positions.Add(new Position(flyCable.end_lattitude, flyCable.end_longitude));
                //polyLine.Clicked += FlyCable_Clicked;
                polyLine.Tag=flyCable.id;
                PolyLines.Add(polyLine);
            }
            foreach (FeederPillar feederPillar in App.MainData.feeder_pillar)
            {
                Pin pin = new Pin
                {
                    Icon = BitmapDescriptorFactory.FromBundle("feeder"),
                    Type = PinType.Place,
                    Label = "Feeder Pillar",
                    Position = new Position(feederPillar.lattitude, feederPillar.longitude),
                    IsDraggable = false,
                    Tag = ConstantValue.feederPillarTag + feederPillar.id.ToString()
                };
                Pins.Add(pin);
            }
        }

        private async Task loginClicked_()
        {
            
            if (!IsLogin)
            {
                var result = await _popupService.ShowLoginPopupAsync();
                if(result == "Login successful")
                {
                    App.login =true;
                    IsLogin = true;
                    IsSummaryVisible = false;
                    LoginButtonText = "Logout";
                    refreshAll_();
                    await Application.Current.MainPage.DisplayAlert("Login", result, "OK");
                    foreach(Pin _pn in Pins)
                    {
                        _pn.IsDraggable = true;
                    }
                }
                else if(result!=null)
                {
                    await Application.Current.MainPage.DisplayAlert("Login Failed", result, "OK");
                }
                else
                {

                }
                
                //await Shell.Current.GoToAsync("///MapsEdit");
            }
            else if(IsLogin)
            {
                LoginButtonText = "Login";
                IsLogin = false;
                App.login = false;
                IsSummaryVisible = true;
                IsPinStackLayoutVisible = true;
                IsSFlyCableMode = false;
                refreshAll_();
                await Application.Current.MainPage.DisplayAlert("Logout", "Logout successful", "OK");
                App.userName = "";
                foreach (Pin _pn in Pins)
                {
                    _pn.IsDraggable = false;
                }
            }


        }
        private async Task OnPinClicked(Pin pin)
        {
            if(IsSwitchFlyCableToggled && !firstPinClicked)
            {
                IsBusy = true;
                flyCableFirst =pin.Position;
                firstPinClicked=true;
                AddFlyCableText = "Please select second point";
                IsBusy = false;
            }
            else if(IsSwitchFlyCableToggled && firstPinClicked)
            {
                IsBusy = true;

                FlyCable flyCable = new FlyCable();
                flyCable.start_lattitude = flyCableFirst.Latitude;
                flyCable.start_longitude = flyCableFirst.Longitude;
                flyCable.end_lattitude = pin.Position.Latitude;
                flyCable.end_longitude = pin.Position.Longitude;
                double jarak=commonFunction.CalculateDistanceInMeters(flyCable.start_lattitude, flyCable.start_longitude, flyCable.end_lattitude, flyCable.end_longitude);
                flyCable.jarak = jarak;
                var resultInsert = await insertNewDataAPI.InsertFlyCableAsync(flyCable);
                flyCable.id = resultInsert;
                App.MainData.fly_cable.Add(flyCable);
                Polyline polyLine = new Polyline();
                polyLine.StrokeWidth = 3f;
                polyLine.StrokeColor = Colors.Black;
                polyLine.StrokePattern = StrokePatternBuilder.DashedLine();
                polyLine.Positions.Add(new Position(flyCableFirst.Latitude, flyCableFirst.Longitude));
                polyLine.Positions.Add(new Position(pin.Position.Latitude, pin.Position.Longitude));
                polyLine.Tag = resultInsert;
                PolyLines.Add(polyLine);
                AddFlyCableText = "Please select first point";
                flyCableFirst = new Position();
                firstPinClicked = false;
                IsBusy = false;
            }
            else if(!IsLogin)
            {
                IsBusy = true;
                Pin clickedPin = pin;
                resultJenisItem _resultJenisItem = commonFunction.pinTagJenisItem(clickedPin.Tag as string);

                if (ConstantValue.feederPillarTag.Contains(_resultJenisItem.name))
                {
                    FeederPillar feederPillar = App.MainData.feeder_pillar.Where(x => x.id == _resultJenisItem.id).First();

                    var result = await _popupService.ShowFeederViewPopUpAsync(feederPillar);
                }
                else if (ConstantValue.mapPinTag.Contains(_resultJenisItem.name))
                {
                    MapPin mapPin = App.MainData.map_pin.Where(x => x.id == _resultJenisItem.id).First();
                    var result = await _popupService.ShowPinViewPopUpAsync(mapPin);

                }
                IsBusy = false;
            }
            else if (IsLogin)
            {
                IsBusy = true;
                Pin clickedPin = pin;
                resultJenisItem _resultJenisItem = commonFunction.pinTagJenisItem(clickedPin.Tag as string);

                if (ConstantValue.feederPillarTag.Contains(_resultJenisItem.name))
                {
                    FeederPillar feederPillar = App.MainData.feeder_pillar.Where(x => x.id == _resultJenisItem.id).First();
                    var result = await _popupService.ShowFeederPillarNewPopUpAsync(feederPillar);
                    if (result != null)
                    {
                        if (result.IsUpdate && result.IsDelete)
                        {
                            var resultDelete = await updateDataAPI.DeleteFeederPillarAsync(result.feederPillar.id);
                            var resultMessage = JsonSerializer.Deserialize<deleteRespond>(resultDelete);
                            string tagging = ConstantValue.feederPillarTag + feederPillar.id.ToString();
                            Pin pin1 = Pins.Where(x => tagging==x.Tag.ToString() ).First();
                            Pins.Remove(pin1);
                            App.MainData.feeder_pillar.Remove(feederPillar);
                            await Application.Current.MainPage.DisplayAlert("Delete", resultMessage.message, "OK");
                        }
                        else if (result.IsUpdate && !result.IsDelete)
                        {
                            var resultUpdate = await updateDataAPI.UpdateFeederPillarAsync(result.feederPillar);
                            if(resultUpdate.alamatInsert!=null)
                            {
                                AddressResponse addressResponse = await locationFunction.getAdressId(resultUpdate.alamatInsert);
                                resultUpdate.alamat = addressResponse.jalan_id;
                                resultUpdate.alamatFull = new alamatFull();
                                resultUpdate.alamatFull.jalan = new jalan_ { id = addressResponse.jalan_id, nama = resultUpdate.alamatInsert.nama_jalan, bandar_id = addressResponse.bandar_id };
                                resultUpdate.alamatFull.bandar = new bandar_ { id = addressResponse.bandar_id, nama = resultUpdate.alamatInsert.bandar, daerah_id = addressResponse.daerah_id };
                                resultUpdate.alamatFull.daerah = new daerah_ { id = addressResponse.daerah_id, nama = resultUpdate.alamatInsert.daerah, poskod_id = addressResponse.poskod_id };
                                resultUpdate.alamatFull.poskod = new poskod_ { id = addressResponse.poskod_id, nama = resultUpdate.alamatInsert.poskod, negeri_id = addressResponse.negeri_id };
                                resultUpdate.alamatFull.negeri = new negeri_ { id = addressResponse.negeri_id, nama = resultUpdate.alamatInsert.negeri };
                                feederPillar.alamatFull = resultUpdate.alamatFull;
                            }
                           
                            feederPillar.no_meter = resultUpdate.no_meter;
                            feederPillar.nilai_amp=resultUpdate.nilai_amp;
                            feederPillar.nilai_phase_r=resultUpdate.nilai_phase_r;
                            feederPillar.nilai_phase_y= resultUpdate.nilai_phase_y;
                            feederPillar.nilai_phase_b = resultUpdate.nilai_phase_b;
                            feederPillar.status_pintu= resultUpdate.status_pintu;
                            feederPillar.footing= resultUpdate.footing;
                            feederPillar.komen= resultUpdate.komen;
                            feederPillar.attachment= resultUpdate.attachment;
                            Pin _updatePin = new Pin
                            {
                                Icon = BitmapDescriptorFactory.FromBundle("feeder"),
                                Type = PinType.Place,
                                Label ="Feeder Pillar",
                                Position = new Position(resultUpdate.lattitude, resultUpdate.longitude),
                                IsDraggable = false,
                                Tag = ConstantValue.feederPillarTag + resultUpdate.id.ToString()
                            };
                            Pin updatePin = Pins.Where(x => x == pin).First();
                            updatePin = _updatePin;
                            await Application.Current.MainPage.DisplayAlert("Update", "Update is successful!", "OK");
                        }
                    }
                    
                }
                else if (ConstantValue.mapPinTag.Contains(_resultJenisItem.name))
                {
                    
                    MapPin mapPin = App.MainData.map_pin.Where(x => x.id == _resultJenisItem.id).First();
                    var result = await _popupService.ShowPinNewPopUpAsync(mapPin);
                    if(result!=null)
                    {
                        if (result.IsUpdate && result.IsDelete)
                        {
                            var resultDelete = await updateDataAPI.DeleteMapPinAsync(result.mapPin.id);
                            var resultMessage =JsonSerializer.Deserialize<deleteRespond>(resultDelete);
                            string tagging = ConstantValue.mapPinTag + result.mapPin.id.ToString();
                            Pin pin1 = Pins.Where(x => tagging == x.Tag.ToString()).First();
                            Pins.Remove(pin1);
                            App.MainData.map_pin.Remove(mapPin);
                            await Application.Current.MainPage.DisplayAlert("Delete", resultMessage.message, "OK");
                        }
                        else if (result.IsUpdate && !result.IsDelete)
                        {
                            var resultUpdate = await updateDataAPI.UpdateMapPinAsync(result.mapPin);
                            if(resultUpdate.alamatInsert!=null)
                            {
                                AddressResponse addressResponse = await locationFunction.getAdressId(resultUpdate.alamatInsert);
                                resultUpdate.alamat = addressResponse.jalan_id;
                                resultUpdate.alamatFull = new alamatFull();
                                resultUpdate.alamatFull.jalan = new jalan_ { id = addressResponse.jalan_id, nama = resultUpdate.alamatInsert.nama_jalan, bandar_id = addressResponse.bandar_id };
                                resultUpdate.alamatFull.bandar = new bandar_ { id = addressResponse.bandar_id, nama = resultUpdate.alamatInsert.bandar, daerah_id = addressResponse.daerah_id };
                                resultUpdate.alamatFull.daerah = new daerah_ { id = addressResponse.daerah_id, nama = resultUpdate.alamatInsert.daerah, poskod_id = addressResponse.poskod_id };
                                resultUpdate.alamatFull.poskod = new poskod_ { id = addressResponse.poskod_id, nama = resultUpdate.alamatInsert.poskod, negeri_id = addressResponse.negeri_id };
                                resultUpdate.alamatFull.negeri = new negeri_ { id = addressResponse.negeri_id, nama = resultUpdate.alamatInsert.negeri };
                                mapPin.alamatFull = resultUpdate.alamatFull;
                            }
               
                            mapPin.jenis_lampu = resultUpdate.jenis_lampu;
                            mapPin.tinggi_tiang = resultUpdate.tinggi_tiang;
                            mapPin.material_tiang = resultUpdate.material_tiang;
                            mapPin.jenis_pintu_penyelenggaraan= resultUpdate.jenis_pintu_penyelenggaraan;
                            mapPin.saiz_pintu_penyelenggaraan = resultUpdate.saiz_pintu_penyelenggaraan;
                            mapPin.panjang_lengan_lampu=resultUpdate.panjang_lengan_lampu;
                            mapPin.jenis_tiang=resultUpdate.jenis_tiang;
                            mapPin.jenama_lampu= resultUpdate.jenama_lampu;
                            mapPin.jenama_tiang = resultUpdate.jenama_tiang;
                            mapPin.status=resultUpdate.status;
                            mapPin.attachment=resultUpdate.attachment;
                           // mapPin.alamat=resultUpdate.alamat;
                            mapPin.komen=resultUpdate.komen;

                            Pin _updatePin = new Pin
                            {
                                Icon = BitmapDescriptorFactory.FromBundle("street_lamp_" + resultUpdate.jenis_item.ToString()),
                                Type = PinType.Place,
                                Label = App.DropDownItem.Jenis_Item.Where(x => x.Id == resultUpdate.jenis_item).First().Item,
                                Position = new Position(resultUpdate.lattitude, resultUpdate.longitude),
                                IsDraggable = false,
                                Tag = ConstantValue.mapPinTag + resultUpdate.id.ToString()
                            };
                            Pin updatePin = Pins.Where(x => x == pin).First();
                            updatePin = _updatePin;
                            await Application.Current.MainPage.DisplayAlert("Update", "Update is successful!", "OK");
                        }
                    }

                    
                }
                IsBusy = false;
            }

        }
        private async Task OnpinDraged(Pin pin)
        {
            IsBusy = true;
            resultJenisItem _resultJenisItem = commonFunction.pinTagJenisItem(pin.Tag as string);
          //  AlamatInsert address = await locationFunction.GetAddress(new Location(pin.Position.Latitude, pin.Position.Longitude));
            //AddressResponse addressResponse = await locationFunction.getAdressId(address);
            if (ConstantValue.feederPillarTag.Contains(_resultJenisItem.name))
            {
                var result = updateDataAPI.UpdateFeederPillarLocationAsync(_resultJenisItem.id,pin.Position.Latitude,pin.Position.Longitude);
                var oldPin=App.MainData.feeder_pillar.Where(x=>x.id== _resultJenisItem.id).FirstOrDefault();
                oldPin.lattitude= pin.Position.Latitude;
                oldPin.longitude= pin.Position.Longitude;
             //   oldPin.alamat= addressResponse.jalan_id;
             //   oldPin.alamatFull = new alamatFull();
             //   oldPin.alamatFull.jalan = new jalan_ { id = addressResponse.jalan_id, nama = address.nama_jalan, bandar_id = addressResponse.bandar_id };
             //   oldPin.alamatFull.bandar = new bandar_ { id = addressResponse.bandar_id, nama = address.bandar, daerah_id = addressResponse.daerah_id };
              //  oldPin.alamatFull.daerah = new daerah_ { id = addressResponse.daerah_id, nama = address.daerah, poskod_id = addressResponse.poskod_id };
              //  oldPin.alamatFull.poskod = new poskod_ { id = addressResponse.poskod_id, nama = address.poskod, negeri_id = addressResponse.negeri_id };
              //  oldPin.alamatFull.negeri = new negeri_ { id = addressResponse.negeri_id, nama = address.negeri };
            }
            else if (ConstantValue.mapPinTag.Contains(_resultJenisItem.name))
            {
                var result = updateDataAPI.UpdateMapPinLocationAsync(_resultJenisItem.id, pin.Position.Latitude, pin.Position.Longitude);
                var oldPin = App.MainData.map_pin.Where(x => x.id == _resultJenisItem.id).FirstOrDefault();
                oldPin.lattitude = pin.Position.Latitude;
                oldPin.longitude = pin.Position.Longitude;
               // oldPin.alamat = addressResponse.jalan_id;
               // oldPin.alamatFull = new alamatFull();
              //  oldPin.alamatFull.jalan = new jalan_ { id = addressResponse.jalan_id, nama = address.nama_jalan, bandar_id = addressResponse.bandar_id };
              //  oldPin.alamatFull.bandar = new bandar_ { id = addressResponse.bandar_id, nama = address.bandar, daerah_id = addressResponse.daerah_id };
              //  oldPin.alamatFull.daerah = new daerah_ { id = addressResponse.daerah_id, nama = address.daerah, poskod_id = addressResponse.poskod_id };
              //  oldPin.alamatFull.poskod = new poskod_ { id = addressResponse.poskod_id, nama = address.poskod, negeri_id = addressResponse.negeri_id };
              //  oldPin.alamatFull.negeri = new negeri_ { id = addressResponse.negeri_id, nama = address.negeri };
            }
            // var result= updateDataAPI.update
            IsBusy = false;
        }
        private async Task summaryButtonClicked_()
        {
            await Shell.Current.GoToAsync("///SummaryPage");
        }
        private async Task setCurrentLocation_()
        {
            IsBusy = true;
            try
            {
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    WeakReferenceMessenger.Default.Send(new LocationPermissionDeniedMessage());
                    return;
                }

                var location = await Geolocation.Default.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Best,
                    Timeout = TimeSpan.FromSeconds(10)
                });

                if (location != null)
                {
                    WeakReferenceMessenger.Default.Send(new MoveMapMessage(location.Latitude, location.Longitude));
                }
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new LocationErrorMessage(ex.Message));
            }
            IsBusy = false;
        }
        private async Task searchLocationAsync_()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
                return;
            IsBusy = true;
            //var location = await ApiService.GetLocationFromGoogle(SearchText);
            var locations = await Geocoding.GetLocationsAsync(SearchText);
            var location = locations?.FirstOrDefault();
            if (location != null)
            {
                WeakReferenceMessenger.Default.Send(new MoveMapMessage(location.Latitude, location.Longitude));
            }
            IsBusy = false;
        }
        private async Task refreshAll_()
        {
            IsBusy = true;
            common common = new common();
            LoadData loadReferences = new LoadData();
            App.DropDownItem = await loadReferences.GetDropDownAsync();
            if (IsLogin)
            {
                App.MainData = await loadReferences.GetMainByIDDataAsync(App.userId);
            }
            else
            {
                App.MainData = await loadReferences.GetMainDataAsync();
            }
            
            App.Alamat = await loadReferences.GetAlamatAsync();
            await common.SyncAlamat(App.MainData, App.Alamat);
            WeakReferenceMessenger.Default.Send(new MapsViewClearPinMessage());
            getData();
            IsBusy = false;
        }
      
        private async Task insertNewMapPin_(string iconName,Location Location)
        {
            IsBusy = true;
            AlamatInsert address = await locationFunction.GetAddress(Location);
            if (iconName.Contains("street"))
            {
                //resultJenisItem _resultJenisItem = commonFunction.PinImageJenisItem(iconName);
                int id = 0;
                if(iconName.Contains("double"))
                {
                    id = 1;
                }
                else if(iconName.Contains("single"))
                {
                    id = 2;
                }
                
                var result = await _popupService.ShowPinNewPopUpAsync(id,Location,address);
                
                if (result!=null)
                {
                    result.mapPin.lattitude = Location.Latitude;
                    result.mapPin.longitude = Location.Longitude;
                    
                    AddressResponse addressResponse = await locationFunction.getAdressId(result.mapPin.alamatInsert);
                    result.mapPin.alamat=addressResponse.jalan_id;
                    result.mapPin.alamatFull = new alamatFull();
                    result.mapPin.alamatFull.jalan = new jalan_ { id=addressResponse.jalan_id,nama= result.mapPin.alamatInsert.nama_jalan,bandar_id= addressResponse.bandar_id};
                    result.mapPin.alamatFull.bandar = new bandar_ { id = addressResponse.bandar_id, nama = result.mapPin.alamatInsert.bandar, daerah_id=addressResponse.daerah_id };
                    result.mapPin.alamatFull.daerah=new daerah_ { id=addressResponse.daerah_id,nama= result.mapPin.alamatInsert.daerah,poskod_id=addressResponse.poskod_id };
                    result.mapPin.alamatFull.poskod=new poskod_ { id=addressResponse.poskod_id,nama= result.mapPin.alamatInsert.poskod,negeri_id=addressResponse.negeri_id };
                    result.mapPin.alamatFull.negeri = new negeri_ { id = addressResponse.negeri_id, nama = result.mapPin.alamatInsert.negeri };
                    var resultInsert = await insertNewDataAPI.InsertMapPinAsync(result.mapPin);
                        //result.id = resultInsert;
                   App.MainData.map_pin.Add(resultInsert);
                        Pin _newPin = new Pin
                        {
                            Icon = BitmapDescriptorFactory.FromBundle("street_lamp_" + resultInsert.jenis_item.ToString()),
                            Type = PinType.Place,
                            Label = App.DropDownItem.Jenis_Item.Where(x => x.Id == resultInsert.jenis_item).First().Item,
                            Position = new Position(resultInsert.lattitude, resultInsert.longitude),
                            IsDraggable = true,
                            Tag = ConstantValue.mapPinTag + resultInsert.id.ToString()
                        };
                        Pins.Add(_newPin);
                        


                }
            }
            else if(iconName.Contains("feeder"))
            {
                IsBusy = true;
                var result = await _popupService.ShowFeederPillarNewPopUpAsync(Location,address);
                
                if (result != null)
                {
                    result.feederPillar.longitude = Location.Longitude;
                    result.feederPillar.lattitude = Location.Latitude;
                   // AlamatInsert address = await locationFunction.GetAddress(Location);
                    AddressResponse addressResponse = await locationFunction.getAdressId(result.feederPillar.alamatInsert);
                    result.feederPillar.alamat= addressResponse.jalan_id;
                    result.feederPillar.alamatFull = new alamatFull();
                    result.feederPillar.alamatFull.jalan = new jalan_ { id = addressResponse.jalan_id, nama = result.feederPillar.alamatInsert.nama_jalan, bandar_id = addressResponse.bandar_id };
                    result.feederPillar.alamatFull.bandar = new bandar_ { id = addressResponse.bandar_id, nama = result.feederPillar.alamatInsert.bandar, daerah_id = addressResponse.daerah_id };
                    result.feederPillar.alamatFull.daerah = new daerah_ { id = addressResponse.daerah_id, nama = result.feederPillar.alamatInsert.daerah, poskod_id = addressResponse.poskod_id };
                    result.feederPillar.alamatFull.poskod = new poskod_ { id = addressResponse.poskod_id, nama = result.feederPillar.alamatInsert.poskod, negeri_id = addressResponse.negeri_id };
                    result.feederPillar.alamatFull.negeri = new negeri_ { id = addressResponse.negeri_id, nama = result.feederPillar.alamatInsert.negeri };
                    var resultInsert = await insertNewDataAPI.InsertFeederPillarAsync(result.feederPillar);

                    //result.id = resultInsert;
                    App.MainData.feeder_pillar.Add(resultInsert);
                    Pin _newPin = new Pin
                    {
                        Icon = BitmapDescriptorFactory.FromBundle("feeder"),
                        Type = PinType.Place,
                        Label = "Feeder Pillar",
                        Position = new Position(resultInsert.lattitude, resultInsert.longitude),
                        IsDraggable = true,
                        Tag = ConstantValue.feederPillarTag + resultInsert.id.ToString()
                    };
                    Pins.Add(_newPin);
                }

            }
            IsBusy = false;
        }
        private void OnSwitchToggled(bool isToggled)
        {
            IsBusy = true;
            if (isToggled)
            {
                AddFlyCableText = "Please select first point";
                flyCableFirst = new Position();
                firstPinClicked = false;
                IsSFlyCableMode=true;
                IsPinStackLayoutVisible=false;
            }
            else
            {
                IsSFlyCableMode = false;
                IsPinStackLayoutVisible=true;
            }
            IsBusy = false;
        }

    }
}
