
using Microsoft.Maui.Devices.Sensors;
using RekodLampuJalan.Models;
using RekodLampuJalan.Services;
using RekodLampuJalan.Utils;
using RekodLampuJalan.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RekodLampuJalan.PopUp.PopUpViewModel
{

    public class NewPinPopUpViewModel : ViewModelBase
    {
        
        PopupService _popupService;
        private DropDownItem _dropDownItems;
        public DropDownItem DropDownItems
        {
            get { return _dropDownItems; }
            set
            {
                if (value != _dropDownItems)
                {
                    _dropDownItems = value;
                    OnPropertyChanged("DropDownItems");
                }
            }
        }
        //private DropDownSelectedItem _dropDownSelectedItem;
        //public DropDownSelectedItem dropDownSelectedItem
        //{
        //    get { return _dropDownSelectedItem; }
        //    set
        //    {
        //        if (value != _dropDownSelectedItem)
        //        {
        //            _dropDownSelectedItem = value;
        //            OnPropertyChanged("dropDownSelectedItem");
        //        }
        //    }
        //}
        private DropDownStringItem _dropDownStringItem;
        public DropDownStringItem dropDownStringItem
        {
            get { return _dropDownStringItem; }
            set
            {
                if (value != _dropDownStringItem)
                {
                    _dropDownStringItem = value;
                    OnPropertyChanged("dropDownStringItem");
                }
            }
        }
        private string _jenisItem;
        public string JenisItem
        {
            get { return _jenisItem; }
            set
            {
                if (value != _jenisItem)
                {
                    _jenisItem = value;
                    OnPropertyChanged("JenisItem");
                }
            }
        }
        private bool _isUpdate;
        public bool IsUpdate
        {
            get { return _isUpdate; }
            set
            {
                if (value != _isUpdate)
                {
                    _isUpdate = value;
                    OnPropertyChanged("IsUpdate");
                }
            }
        }
        private MapPin _mapPin;
        public MapPin mapPin
        {
            get { return _mapPin; }
            set
            {
                if (value != _mapPin)
                {
                    _mapPin = value;
                    OnPropertyChanged("mapPin");
                }
            }
        }
        private string _latittudeText;
        public string latittudeText
        {
            get { return _latittudeText; }
            set
            {
                if (value != _latittudeText)
                {
                    _latittudeText = value;
                    OnPropertyChanged("latittudeText");
                }
            }
        }
        private string _longitudeText;
        public string longitudeText
        {
            get { return _longitudeText; }
            set
            {
                if (value != _longitudeText)
                {
                    _longitudeText = value;
                    OnPropertyChanged("longitudeText");
                }
            }
        }
        private string _address1Text;
        public string address1Text
        {
            get { return _address1Text; }
            set
            {
                if (value != _address1Text)
                {
                    _address1Text = value;
                    OnPropertyChanged("address1Text");
                }
            }
        }
        private string _address2Text;
        public string address2Text
        {
            get { return _address2Text; }
            set
            {
                if (value != _address2Text)
                {
                    _address2Text = value;
                    OnPropertyChanged("address2Text");
                }
            }
        }
        private string _address3Text;
        public string address3Text
        {
            get { return _address3Text; }
            set
            {
                if (value != _address3Text)
                {
                    _address3Text = value;
                    OnPropertyChanged("address3Text");
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
        //private List<AttachmentImage> _attachmentFile;
        //public List<AttachmentImage> attachmentFile
        //{
        //    get { return _attachmentFile; }
        //    set
        //    {
        //        if (value != _attachmentFile)
        //        {
        //            _attachmentFile = value;
        //            OnPropertyChanged("attachmentFile");
        //        }
        //    }
        //}
        public ICommand OnAttachmentImageClicked { get { return new Command<string>(async (parameterName) => await OnAttachmentImageClicked_(parameterName)); } }
        public ICommand OnUbahaAlamatClicked { get { return new Command(async () => await UbahaAlamatClicked_()); } }

        private Location _location;
        private AlamatInsert alamat;
        public NewPinPopUpViewModel(int id,Location location,AlamatInsert alamat_)
        {
            mapPin=new MapPin();
            DropDownItems = App.DropDownItem;
            mapPin.jenis_item = id;
            JenisItem = DropDownItems.Jenis_Item.Where(x=>x.Id==id).First().Item;
            //mapPin.jenis_item = id;
            _popupService =new PopupService();
            mapPin.attachmentFile = new List<AttachmentImage>();
            _location=location;
            IsUpdate =false;
            mapPin.alamatInsert=alamat_;
            alamat = alamat_;
            latittudeText = "Lattitude: " + location.Latitude.ToString("F4"); ;
            longitudeText="Longitude: "+location.Longitude.ToString("F4"); ;
            address1Text = alamat_.nama_jalan + ", " + alamat_.bandar;
            address2Text =alamat_.poskod+" "+alamat_.daerah;
            address3Text = alamat_.negeri;
           // dropDownSelectedItem=new DropDownSelectedItem();
            dropDownStringItem=new DropDownStringItem();
        }
        public NewPinPopUpViewModel(MapPin mapPin_)
        {
            DropDownItems = App.DropDownItem;
            mapPin = mapPin_;
            JenisItem = DropDownItems.Jenis_Item.Where(x => x.Id == mapPin.jenis_item).First().Item;
            dropDownStringItem = new DropDownStringItem();
            dropDownStringItem.Jenis_Lampu = DropDownItems.Jenis_Lampu.Where(x=>x.Id== mapPin.jenis_lampu).FirstOrDefault().Item;
            dropDownStringItem.Tinggi_Tiang = DropDownItems.Tinggi_Tiang.Where(x => x.Id == mapPin.tinggi_tiang).FirstOrDefault().Item.Value;
            dropDownStringItem.Material_Tiang = DropDownItems.Material_Tiang.Where(x => x.Id == mapPin.material_tiang).FirstOrDefault().Item;
            dropDownStringItem.Jenis_Pintu_Penyelenggaraan= DropDownItems.Jenis_Pintu_Penyelenggaraan.Where(x => x.Id == mapPin.jenis_pintu_penyelenggaraan).FirstOrDefault().Item;
            dropDownStringItem.Saiz_Pintu_Penyelenggaraan = DropDownItems.Saiz_Pintu_Penyelenggaraan.Where(x => x.Id == mapPin.saiz_pintu_penyelenggaraan).FirstOrDefault().Item;
            dropDownStringItem.Panjang_Lengan_Lampu = DropDownItems.Panjang_Lengan_Lampu.Where(x => x.Id == mapPin.panjang_lengan_lampu).FirstOrDefault().Item.Value;
            dropDownStringItem.Jenis_Tiang = DropDownItems.Jenis_Tiang.Where(x => x.Id == mapPin.jenis_tiang).FirstOrDefault().Item;
            dropDownStringItem.Jenama_Lampu = DropDownItems.Jenama_Lampu.Where(x => x.Id == mapPin.jenama_lampu).FirstOrDefault().Item;
            dropDownStringItem.Jenama_Tiang = DropDownItems.Jenama_Tiang.Where(x => x.Id == mapPin.jenama_tiang).FirstOrDefault().Item;
            _popupService = new PopupService();
            mapPin.attachmentFile = new List<AttachmentImage>();
            string[] existImageName=mapPin.attachment.Split(',');
            for(int i=0;i< existImageName.Count();i++)
            {
                if(existImageName[i]!=""&& existImageName[i] != null)
                {
                    AttachmentImage attachment = new AttachmentImage();
                    attachment.fileName = ConstantValue.mapPinAttachmentList[i];
                    attachment.filePath = ConstantValue.ImageUrl+ existImageName[i];
                    attachment.newItem = false;
                    mapPin.attachmentFile.Add(attachment);
                }
            }
            alamat = new AlamatInsert
            {
                nama_jalan = mapPin.alamatFull.jalan.nama,
                bandar = mapPin.alamatFull.bandar.nama,
                poskod = mapPin.alamatFull.poskod.nama,
                daerah = mapPin.alamatFull.daerah.nama,
                negeri = mapPin.alamatFull.negeri.nama
            };
            IsUpdate=true;
            latittudeText = "Lattitude: " + mapPin.lattitude.ToString("F4"); ;
            longitudeText = "Longitude: " + mapPin.longitude.ToString("F4"); ;
            address1Text = mapPin.alamatFull.jalan.nama + ", " + mapPin.alamatFull.bandar.nama;
            address2Text = mapPin.alamatFull.poskod.nama + " " + mapPin.alamatFull.daerah.nama;
            address3Text = mapPin.alamatFull.negeri.nama;
            _location=new Location(mapPin.lattitude, mapPin.longitude);
        }

        private async Task OnAttachmentImageClicked_(string parameterName)
        {
           // IsBusy=true;
            if (mapPin.attachmentFile.Where(x=>x.fileName==parameterName).Count()==0)
            {
                var result = await _popupService.ShowImageFromFilePopUpAsync(null);
                if (result.FilePath != null)
                {
                    AttachmentImage attachment = new AttachmentImage();
                    attachment.fileName = parameterName;
                    attachment.filePath = result.FilePath;
                    attachment.newItem = true;
                    mapPin.attachmentFile.Add(attachment);
                }
            }
            else
            {
                AttachmentImage attachment=  mapPin.attachmentFile.Where(x => x.fileName == parameterName).First();
                if(attachment.filePath != null)
                {
                    var result = await _popupService.ShowImageFromFilePopUpAsync(attachment.filePath);
                    if (result.FilePath != null && result.FilePath != attachment.filePath)
                    {
                        attachment.filePath = result.FilePath;
                        attachment.newItem = true;
                    }
                    else
                    {

                    }
                   // mapPin.attachmentFile.Add(attachment);
                }
            }
            //IsBusy=false;
        }
        private async Task UbahaAlamatClicked_()
        {
            IsBusy=true;
            LocationFunction locationFunction=new LocationFunction();
            //alamat_ = new AlamatInsert();
            List<AlamatInsert> listAlamat= await locationFunction.GetAddressList(_location);
            AlamatInsert alamatnew = await _popupService.ShowAlamatEditPopUpAsync(listAlamat, alamat);
            if(alamatnew!=null)
            {
                mapPin.alamatInsert=alamatnew;
                address1Text = alamatnew.nama_jalan + ", " + alamatnew.bandar;
                address2Text = alamatnew.poskod + " " + alamatnew.daerah;
                address3Text = alamatnew.negeri;
                alamat = alamatnew;
            }

            IsBusy=false;
        }
    }

}
