using RekodLampuJalan.Utils;
using RekodLampuJalan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RekodLampuJalan.PopUp.PopUpViewModel
{
    public class AlamatEditViewModel: ViewModelBase
    {
        private List<AlamatInsert> _alamatList;
        public List<AlamatInsert> alamatList
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
        private List<string> _namaJalanList =new List<string> ();
        public List<string> namaJalanList
        {
            get { return _namaJalanList; }
            set
            {
                if (value != _namaJalanList)
                {
                    _namaJalanList = value;
                    OnPropertyChanged("namaJalanList");
                }
            }
        }
        private List<string> _bandarList = new List<string>();
        public List<string> bandarList
        {
            get { return _bandarList; }
            set
            {
                if (value != _bandarList)
                {
                    _bandarList = value;
                    OnPropertyChanged("bandarList");
                }
            }
        }
        private List<string> _daerahList = new List<string>();
        public List<string> daerahList
        {
            get { return _daerahList; }
            set
            {
                if (value != _daerahList)
                {
                    _daerahList = value;
                    OnPropertyChanged("daerahList");
                }
            }
        }
        private List<string> _poskodList = new List<string>();
        public List<string> poskodList
        {
            get { return _poskodList; }
            set
            {
                if (value != _poskodList)
                {
                    _poskodList = value;
                    OnPropertyChanged("poskodList");
                }
            }
        }
        private List<string> _negeriList = new List<string>();
        public List<string> negeriList
        {
            get { return _negeriList; }
            set
            {
                if (value != _negeriList)
                {
                    _negeriList = value;
                    OnPropertyChanged("negeriList");
                }
            }
        }
        private AlamatInsert _alamat =new AlamatInsert();
        public AlamatInsert alamat
        {
            get { return _alamat; }
            set
            {
                if (value != _alamat)
                {
                    _alamat = value;
                    OnPropertyChanged("alamat");
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
        LocationFunction locationFunction;
        public AlamatEditViewModel(List<AlamatInsert> _listAlamat,AlamatInsert alamatInsert)
        {
            alamat= alamatInsert;
            locationFunction =new LocationFunction();
            IsBusy = true;
            foreach (var address in _listAlamat)
            {
                if(address.nama_jalan!=null&& !namaJalanList.Contains(address.nama_jalan))
                {
                    namaJalanList.Add(address.nama_jalan);
                }
                if (address.bandar != null && !bandarList.Contains(address.bandar))
                {
                    bandarList.Add(address.bandar);
                }
                if (address.daerah != null && !daerahList.Contains(address.daerah))
                {
                    daerahList.Add(address.daerah);
                }
                if (address.poskod != null && !poskodList.Contains(address.poskod))
                {
                    poskodList.Add(address.poskod);
                }
                if (address.negeri != null && !negeriList.Contains(address.negeri))
                {
                    negeriList.Add(address.negeri);
                }
                
            }
            
            IsBusy = false;
            
           // alamatList =await locationFunction.GetAddressList(location);
        }
        public async Task InitAsync(Location location)
        {
            //namaJalanList=new List<string>();
            //bandarList=new List<string>();
            //daerahList=new List<string>();
            //poskodList=new List<string>();
            //negeriList=new List<string>();

            alamatList = await locationFunction.GetAddressList(location);
            foreach (var address in alamatList)
            {
                namaJalanList.Add(address.nama_jalan);
                bandarList.Add(address.bandar);
                daerahList.Add(address.daerah);
                poskodList.Add(address.poskod);
                negeriList.Add(address.negeri);
            }
            
            // Raise PropertyChanged if needed
        }
    }
}
