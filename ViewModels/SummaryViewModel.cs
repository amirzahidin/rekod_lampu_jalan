using CommunityToolkit.Mvvm.Messaging;
using Maui.GoogleMaps;
using RekodLampuJalan.Models;
using RekodLampuJalan.Services;
using RekodLampuJalan.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RekodLampuJalan.ViewModels
{

    public class SummaryViewModel : ViewModelBase
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
        private Alamat _alamatList;
        public Alamat alamatList
        {
            get { return _alamatList; }
            set
            {
                if (value != _alamatList)
                {
                    _alamatList = value;
                    OnPropertyChanged("alamatList");
                }
            }
        }
        private AlamatInsert _selectedItem;
        public AlamatInsert selectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (value != _selectedItem)
                {
                    _selectedItem = value;
                    OnPropertyChanged("selectedItem");
                }
            }
        }
        private string _lampuDoubleCount;
        public string lampuDoubleCount
        {
            get { return _lampuDoubleCount; }
            set
            {
                if (value != _lampuDoubleCount)
                {
                    _lampuDoubleCount = value;
                    OnPropertyChanged("lampuDoubleCount");
                }
            }
        }
        private string _lampuSingleCount;
        public string lampuSingleCount
        {
            get { return _lampuSingleCount; }
            set
            {
                if (value != _lampuSingleCount)
                {
                    _lampuSingleCount = value;
                    OnPropertyChanged("lampuSingleCount");
                }
            }
        }
        private string _feederPillarCount;
        public string feederPillarCount
        {
            get { return _feederPillarCount; }
            set
            {
                if (value != _feederPillarCount)
                {
                    _feederPillarCount = value;
                    OnPropertyChanged("feederPillarCount");
                }
            }
        }
        private string _flyCableCount;
        public string flyCableCount
        {
            get { return _flyCableCount; }
            set
            {
                if (value != _flyCableCount)
                {
                    _flyCableCount = value;
                    OnPropertyChanged("flyCableCount");
                }
            }
        }
        private string _ToggleButtonText;
        public string ToggleButtonText
        {
            get { return _ToggleButtonText; }
            set
            {
                if (value != _ToggleButtonText)
                {
                    _ToggleButtonText = value;
                    OnPropertyChanged("ToggleButtonText");
                }
            }
        }

        // public string ToggleButtonText => IsMapVisible ? "Show Summary" : "Show Map";

        private bool _isMapVisible;
        public bool IsMapVisible
        {
            get { return _isMapVisible; }
            set
            {
                if (value != _isMapVisible)
                {
                    _isMapVisible = value;
                    OnPropertyChanged("IsMapVisible");
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
        private bool _isBusy = false;
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
        private bool _isEnPoskod = false;
        public bool IsEnPoskod
        {
            get { return _isEnPoskod; }
            set
            {
                if (value != _isEnPoskod)
                {
                    _isEnPoskod = value;
                    OnPropertyChanged("IsEnPoskod");

                }
            }
        }
        private bool _isEnDaerah = false;
        public bool IsEnDaerah
        {
            get { return _isEnDaerah; }
            set
            {
                if (value != _isEnDaerah)
                {
                    _isEnDaerah = value;
                    OnPropertyChanged("IsEnDaerah");

                }
            }
        }
        private bool _isEnBandar = false;
        public bool IsEnBandar
        {
            get { return _isEnBandar; }
            set
            {
                if (value != _isEnBandar)
                {
                    _isEnBandar = value;
                    OnPropertyChanged("IsEnBandar");

                }
            }
        }
        private bool _isEnJalan = false;
        public bool IsEnJalan
        {
            get { return _isEnJalan; }
            set
            {
                if (value != _isEnJalan)
                {
                    _isEnJalan = value;
                    OnPropertyChanged("IsEnJalan");

                }
            }
        }
        private List<negeri_> _NegeriList=new List<negeri_>();
        public List<negeri_> NegeriList
        {
            get { return _NegeriList; }
            set
            {
                if (value != _NegeriList)
                {
                    _NegeriList = value;
                    OnPropertyChanged("NegeriList");
                   
                }
            }
        }
        private List<poskod_> _PoskodList = new List<poskod_>();
        public List<poskod_> PoskodList
        {
            get { return _PoskodList; }
            set
            {
                if (value != _PoskodList)
                {
                    _PoskodList = value;
                    OnPropertyChanged("PoskodList");

                }
            }
        }
        private List<daerah_> _DaerahList = new List<daerah_>();
        public List<daerah_> DaerahList
        {
            get { return _DaerahList; }
            set
            {
                if (value != _DaerahList)
                {
                    _DaerahList = value;
                    OnPropertyChanged("DaerahList");

                }
            }
        }
        private List<bandar_> _BandarList = new List<bandar_>();
        public List<bandar_> BandarList
        {
            get { return _BandarList; }
            set
            {
                if (value != _BandarList)
                {
                    _BandarList = value;
                    OnPropertyChanged("BandarList");

                }
            }
        }
        private List<jalan_> _JalanList = new List<jalan_>();
        public List<jalan_> JalanList
        {
            get { return _JalanList; }
            set
            {
                if (value != _JalanList)
                {
                    _JalanList = value;
                    OnPropertyChanged("JalanList");

                }
            }
        }
        private negeri_ _selectedNegeri;
        public negeri_ selectedNegeri
        {
            get { return _selectedNegeri; }
            set
            {
                if (value != _selectedNegeri)
                {
                    _selectedNegeri = value;
                    OnPropertyChanged("selectedNegeri");
                    OnNegeriChanged();
                }
            }
        }
        private poskod_ _selectedPoskod;
        public poskod_ selectedPoskod
        {
            get { return _selectedPoskod; }
            set
            {
                if (value != _selectedPoskod)
                {
                    _selectedPoskod = value;
                    OnPropertyChanged("selectedPoskod");
                    OnPoskodChanged();
                }
            }
        }
        private daerah_ _selectedDaerah;
        public daerah_ selectedDaerah
        {
            get { return _selectedDaerah; }
            set
            {
                if (value != _selectedDaerah)
                {
                    _selectedDaerah = value;
                    OnPropertyChanged("selectedDaerah");
                    OnDaerahChanged();
                }
            }
        }
        private bandar_ _selectedBandar;
        public bandar_ selectedBandar
        {
            get { return _selectedBandar; }
            set
            {
                if (value != _selectedBandar)
                {
                    _selectedBandar = value;
                    OnPropertyChanged("selectedBandar");
                    OnBandarChanged();
                }
            }
        }
        private jalan_ _selectedJalan;
        public jalan_ selectedJalan
        {
            get { return _selectedJalan; }
            set
            {
                if (value != _selectedJalan)
                {
                    _selectedJalan = value;
                    OnPropertyChanged("selectedJalan");
                    
                }
            }
        }
        public ICommand searchClicked { get { return new Command(async () => await searchClicked_()); } }
        public ICommand ToggleMapSummaryCommand { get { return new Command(async () => await ToggleMapSummaryCommand_()); } }
        public ICommand BackCommand { get { return new Command(async () => await BackCommand_()); } }
        public ICommand LoadData { get { return new Command(async () => await LoadData_()); } }
        public ICommand pinClicked { get { return new Command<Pin>(async (pin) => await OnPinClicked(pin)); } }
        public ICommand refreshAll { get { return new Command(async () => await refreshAll_()); } }
        common commonFunction;
        private readonly IPopupService _popupService;
        public SummaryViewModel()
        {
            NegeriList = App.Alamat.negeri;
            Pins = new ObservableCollection<Pin>();
            PolyLines = new ObservableCollection<Polyline>();
            selectedItem = new AlamatInsert();
            IsMapVisible=true;
            ToggleButtonText = "Show Summary";
            IsSummaryVisible = false;
            commonFunction = new common();
            _popupService = new PopupService();
        }
        private async Task searchClicked_()
        {
            IsBusy = true;
            await Task.Delay(100);
            Pins.Clear();
            PolyLines.Clear();
            WeakReferenceMessenger.Default.Send(new SummaryClearPinMessage());
            //AlamatInsert alamatInsert = new AlamatInsert();
            List<MapPin> mapPins = App.MainData.map_pin;
            List<FeederPillar> feederPillars = App.MainData.feeder_pillar;
            if (selectedNegeri != null)
            {
                var resultMapPin = mapPins.Where(x => x.alamatFull.negeri.id == selectedNegeri.id).ToList();

                    mapPins = resultMapPin;
                
                var resultfeederPillar = feederPillars.Where(x => x.alamatFull.negeri.id == selectedNegeri.id).ToList();
      
                    feederPillars = resultfeederPillar;
                

            }
            if (selectedPoskod != null)
            {
                var resultMapPin = mapPins.Where(x => x.alamatFull.poskod.id == selectedPoskod.id).ToList();

                    mapPins = resultMapPin;
                
                var resultfeederPillar = feederPillars.Where(x => x.alamatFull.poskod.id == selectedPoskod.id).ToList();

                    feederPillars = resultfeederPillar;
                
            }
            if (selectedDaerah != null)
            {
                var resultMapPin = mapPins.Where(x => x.alamatFull.daerah.id == selectedDaerah.id).ToList();

                    mapPins = resultMapPin;
                
                var resultfeederPillar = feederPillars.Where(x => x.alamatFull.daerah.id == selectedDaerah.id).ToList();

                    feederPillars = resultfeederPillar;
                
            }
            if (selectedBandar != null)
            {
                var resultMapPin = mapPins.Where(x => x.alamatFull.bandar.id == selectedBandar.id).ToList();

                    mapPins = resultMapPin;
                
                var resultfeederPillar = feederPillars.Where(x => x.alamatFull.bandar.id == selectedBandar.id).ToList();

                    feederPillars = resultfeederPillar;
                
            }
            if (selectedJalan != null)
            {
                var resultMapPin = mapPins.Where(x => x.alamatFull.jalan.id == selectedJalan.id).ToList();

                    mapPins = resultMapPin;
                
                var resultfeederPillar = feederPillars.Where(x => x.alamatFull.jalan.id == selectedJalan.id).ToList();
       
                    feederPillars = resultfeederPillar;
                
                //alamatInsert.nama_jalan = selectedItem.nama_jalan;
            }
            int singlearmCount = 0;
            int doublearmCount=0;
            int flyCableCount_ = 0;
            int feederPillarCount_ = 0;
            foreach(var item in mapPins)
            {
                if(item.jenis_item==1)
                {
                    doublearmCount++;
                }
                else if(item.jenis_item == 2)
                {
                    singlearmCount++;
                }
                Pin _newPin = new Pin
                {
                    Icon = BitmapDescriptorFactory.FromBundle("street_lamp_" + item.jenis_item.ToString()),
                    Type = PinType.Place,
                    Label = App.DropDownItem.Jenis_Item.Where(x => x.Id == item.jenis_item).First().Item,
                    Position = new Position(item.lattitude, item.longitude),
                    IsDraggable = false,
                    Tag = ConstantValue.mapPinTag + item.id.ToString()
                };
               
                Pins.Add(_newPin);

                List<FlyCable> selectedFlyCable = App.MainData.fly_cable.Where(x => x.start_lattitude == item.lattitude && x.start_longitude == item.longitude).ToList();
                foreach (var flycable in selectedFlyCable)
                {
                    
                    Polyline polyLine = new Polyline();
                    polyLine.StrokeWidth = 3f;
                    polyLine.StrokeColor = Colors.Black;
                    polyLine.StrokePattern = StrokePatternBuilder.DashedLine();
                    polyLine.Positions.Add(new Position(flycable.start_lattitude, flycable.start_longitude));
                    polyLine.Positions.Add(new Position(flycable.end_lattitude, flycable.end_longitude));
                    //polyLine.Clicked += FlyCable_Clicked;
                    polyLine.Tag = flycable.id;
                    PolyLines.Add(polyLine);
                    flyCableCount_++;
                }
                
            }


            foreach(var item in feederPillars)
            {   
                feederPillarCount_++;
                Pin _newPin = new Pin
                {
                    Icon = BitmapDescriptorFactory.FromBundle("feeder"),
                    Type = PinType.Place,
                    Label = "Feeder Pillar",
                    Position = new Position(item.lattitude, item.longitude),
                    IsDraggable = false,
                    Tag = ConstantValue.feederPillarTag + item.id.ToString()
                };
                Pins.Add(_newPin);

                List<FlyCable> selectedFlyCable = App.MainData.fly_cable.Where(x => x.start_lattitude == item.lattitude && x.start_longitude == item.longitude).ToList();
                foreach (var flycable in selectedFlyCable)
                {
                    Polyline polyLine = new Polyline();
                    polyLine.StrokeWidth = 3f;
                    polyLine.StrokeColor = Colors.Black;
                    polyLine.StrokePattern = StrokePatternBuilder.DashedLine();
                    polyLine.Positions.Add(new Position(flycable.start_lattitude, flycable.start_longitude));
                    polyLine.Positions.Add(new Position(flycable.end_lattitude, flycable.end_longitude));
                    //polyLine.Clicked += FlyCable_Clicked;
                    polyLine.Tag = flycable.id;
                    PolyLines.Add(polyLine);
                    flyCableCount_++;
                }
            }
            lampuDoubleCount = doublearmCount.ToString();
            lampuSingleCount = singlearmCount.ToString();
            feederPillarCount = feederPillarCount_.ToString();
            flyCableCount=flyCableCount_.ToString();
            Pin firstPin=Pins.FirstOrDefault();
            if(firstPin!=null)
            {
                WeakReferenceMessenger.Default.Send(new SummaryMoveMapMessage(firstPin.Position.Latitude, firstPin.Position.Longitude));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("No Data", "No data recorded at the location", "OK");
            }
            IsBusy = false;
        }
        private async Task ToggleMapSummaryCommand_()
        {
            if(IsMapVisible)
            {
                IsMapVisible = false;
                IsSummaryVisible = true;
                ToggleButtonText = "Show Map";
            }
            else
            {
                IsMapVisible=true;
                IsSummaryVisible = false;
                ToggleButtonText = "Show Summary";
            }
        }
        private async Task BackCommand_()
        {
            await Shell.Current.GoToAsync("///MapsView");
        }
        private async Task LoadData_()
        {
            common common = new common();
            LoadData loadReferences = new LoadData();
            App.MainData = await loadReferences.GetMainDataAsync();
            App.Alamat = await loadReferences.GetAlamatAsync();
            await common.SyncAlamat(App.MainData, App.Alamat);
        }
        private void OnNegeriChanged()
        {
            // Do whatever you need when the selection changes
            if (selectedNegeri != null)
            {
                DaerahList.Clear();
                BandarList.Clear();
                JalanList.Clear();
                selectedPoskod = null;
                selectedDaerah = null;
                selectedBandar = null;
                selectedJalan = null; 
                IsEnDaerah=false;
                IsEnBandar = false;
                IsEnJalan = false;
                PoskodList = App.Alamat.poskod.Where(x=>x.negeri_id==selectedNegeri.id).ToList();
                IsEnPoskod = true;
                // For example, load related Daerahs here
            }
        }
        private void OnPoskodChanged()
        {
            // Do whatever you need when the selection changes
            if (selectedPoskod != null)
            {
                
                BandarList.Clear();
                JalanList.Clear();
                selectedDaerah = null;
                selectedBandar = null;
                selectedJalan = null;
                IsEnBandar = false;
                IsEnJalan = false;
                DaerahList = App.Alamat.daerah.Where(x => x.poskod_id == selectedPoskod.id).ToList();
                IsEnDaerah = true;
                // For example, load related Daerahs here
            }
        }
        private void OnDaerahChanged()
        {
            // Do whatever you need when the selection changes
            if (selectedDaerah != null)
            {
                
                JalanList.Clear();
                selectedBandar = null;
                selectedJalan = null;
                IsEnJalan = false;
                BandarList = App.Alamat.bandar.Where(x => x.daerah_id == selectedDaerah.id).ToList();
                IsEnBandar = true;
                // For example, load related Daerahs here
            }
        }
        private void OnBandarChanged()
        {
            // Do whatever you need when the selection changes
            if (selectedBandar != null)
            {
                selectedJalan = null;
                JalanList = App.Alamat.jalan.Where(x => x.bandar_id == selectedBandar.id).ToList();
                IsEnJalan=true;
                // For example, load related Daerahs here
            }
        }
        private async Task refreshAll_()
        {
            IsBusy = true;
            common common = new common();
            LoadData loadReferences = new LoadData();
            App.DropDownItem = await loadReferences.GetDropDownAsync();
            App.MainData = await loadReferences.GetMainDataAsync();
            App.Alamat = await loadReferences.GetAlamatAsync();
            await common.SyncAlamat(App.MainData, App.Alamat);
            NegeriList = App.Alamat.negeri;
            PoskodList.Clear();
            DaerahList.Clear();
            BandarList.Clear();
            JalanList.Clear();
            selectedPoskod = null;
            selectedDaerah = null;
            selectedBandar = null;
            selectedJalan = null;
            WeakReferenceMessenger.Default.Send(new SummaryClearPinMessage());
            IsBusy = false;
        }
        private async Task OnPinClicked(Pin pin)
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
    }
}
