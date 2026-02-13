using RekodLampuJalan.Models;
using RekodLampuJalan.Services;
using RekodLampuJalan.Utils;
using RekodLampuJalan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RekodLampuJalan.PopUp.PopUpViewModel
{
    public class ClickPinViewPopUpViewModel : ViewModelBase
    {
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
        private string _JenisItem;
        public string JenisItem
        {
            get { return _JenisItem; }
            set
            {
                if (value != _JenisItem)
                {
                    _JenisItem = value;
                    OnPropertyChanged("JenisItem");
                }
            }
        }
        private string _JenisLampu;
        public string JenisLampu
        {
            get { return _JenisLampu; }
            set
            {
                if (value != _JenisLampu)
                {
                    _JenisLampu = value;
                    OnPropertyChanged("JenisLampu");
                }
            }
        }
        private string _TinggiTiang;
        public string TinggiTiang
        {
            get { return _TinggiTiang; }
            set
            {
                if (value != _TinggiTiang)
                {
                    _TinggiTiang = value;
                    OnPropertyChanged("TinggiTiang");
                }
            }
        }
        private string _MaterialTiang;
        public string MaterialTiang
        {
            get { return _MaterialTiang; }
            set
            {
                if (value != _MaterialTiang)
                {
                    _MaterialTiang = value;
                    OnPropertyChanged("MaterialTiang");
                }
            }
        }
        private string _JenisPintuPenyelenggaraan;
        public string JenisPintuPenyelenggaraan
        {
            get { return _JenisPintuPenyelenggaraan; }
            set
            {
                if (value != _JenisPintuPenyelenggaraan)
                {
                    _JenisPintuPenyelenggaraan = value;
                    OnPropertyChanged("JenisPintuPenyelenggaraan");
                }
            }
        }
        private string _SaizPintuPenyelenggaraan;
        public string SaizPintuPenyelenggaraan
        {
            get { return _SaizPintuPenyelenggaraan; }
            set
            {
                if (value != _SaizPintuPenyelenggaraan)
                {
                    _SaizPintuPenyelenggaraan = value;
                    OnPropertyChanged("SaizPintuPenyelenggaraan");
                }
            }
        }
        private string _PanjangLenganLampu;
        public string PanjangLenganLampu
        {
            get { return _PanjangLenganLampu; }
            set
            {
                if (value != _PanjangLenganLampu)
                {
                    _PanjangLenganLampu = value;
                    OnPropertyChanged("PanjangLenganLampu");
                }
            }
        }
        private string _JenisTiang;
        public string JenisTiang
        {
            get { return _JenisTiang; }
            set
            {
                if (value != _JenisTiang)
                {
                    _JenisTiang = value;
                    OnPropertyChanged("JenisTiang");
                }
            }
        }
        private string _JenamaLampu;
        public string JenamaLampu
        {
            get { return _JenamaLampu; }
            set
            {
                if (value != _JenamaLampu)
                {
                    _JenamaLampu = value;
                    OnPropertyChanged("JenamaLampu");
                }
            }
        }
        private string _JenamaTiang;
        public string JenamaTiang
        {
            get { return _JenamaTiang; }
            set
            {
                if (value != _JenamaTiang)
                {
                    _JenamaTiang = value;
                    OnPropertyChanged("JenamaTiang");
                }
            }
        }
        private string _Komen;
        public string Komen
        {
            get { return _Komen; }
            set
            {
                if (value != _Komen)
                {
                    _Komen = value;
                    OnPropertyChanged("Komen");
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
        public ICommand OnAttachmentImageClicked { get { return new Command<string>(async (parameterName) => await OnAttachmentImageClicked_(parameterName)); } }
        PopupService _popupService;
        public ClickPinViewPopUpViewModel(MapPin _mapPin)
        {
            mapPin = _mapPin;
            JenisItem = App.DropDownItem.Jenis_Item.FirstOrDefault(x => x.Id == mapPin.jenis_item)?.Item ?? "";
            JenisLampu = App.DropDownItem.Jenis_Lampu.FirstOrDefault(x => x.Id == mapPin.jenis_lampu)?.Item ?? "";
            TinggiTiang = App.DropDownItem.Tinggi_Tiang.FirstOrDefault(x => x.Id == mapPin.tinggi_tiang)?.Item.ToString() ?? "";
            MaterialTiang = App.DropDownItem.Material_Tiang.FirstOrDefault(x => x.Id == mapPin.material_tiang)?.Item ?? "";
            JenisPintuPenyelenggaraan = App.DropDownItem.Jenis_Pintu_Penyelenggaraan.FirstOrDefault(x => x.Id == mapPin.jenis_pintu_penyelenggaraan)?.Item ?? "";
            SaizPintuPenyelenggaraan = App.DropDownItem.Saiz_Pintu_Penyelenggaraan.FirstOrDefault(x => x.Id == mapPin.saiz_pintu_penyelenggaraan)?.Item ?? "";
            PanjangLenganLampu = App.DropDownItem.Panjang_Lengan_Lampu.FirstOrDefault(x => x.Id == mapPin.panjang_lengan_lampu)?.Item.ToString() ?? "";
            JenisTiang = App.DropDownItem.Jenis_Tiang.FirstOrDefault(x => x.Id == mapPin.jenis_tiang)?.Item ?? "";
            JenamaLampu = App.DropDownItem.Jenama_Lampu.FirstOrDefault(x => x.Id == mapPin.jenama_lampu)?.Item ?? "";
            JenamaTiang = App.DropDownItem.Jenama_Tiang.FirstOrDefault(x => x.Id == mapPin.jenama_tiang)?.Item ?? "";
            Komen = mapPin.komen;
            mapPin.attachmentFile = new List<AttachmentImage>();
            string[] existImageName = mapPin.attachment.Split(',');
            for (int i = 0; i < existImageName.Count(); i++)
            {
                if (existImageName[i] != "" && existImageName[i] != null)
                {
                    AttachmentImage attachment = new AttachmentImage();
                    attachment.fileName = ConstantValue.mapPinAttachmentList[i];
                    attachment.filePath = ConstantValue.ImageUrl + existImageName[i];
                    attachment.newItem = false;
                    mapPin.attachmentFile.Add(attachment);
                }
            }
            _popupService=new PopupService();
            latittudeText = "Lattitude: " + mapPin.lattitude.ToString("F4"); ;
            longitudeText = "Longitude: " + mapPin.longitude.ToString("F4"); ;
            address1Text = mapPin.alamatFull.jalan.nama + ", " + mapPin.alamatFull.bandar.nama;
            address2Text = mapPin.alamatFull.poskod.nama + " " + mapPin.alamatFull.daerah.nama;
            address3Text = mapPin.alamatFull.negeri.nama;
        }
        private async Task OnAttachmentImageClicked_(string parameterName)
        {
            if (mapPin.attachmentFile.Where(x => x.fileName == parameterName).Count() == 0)
            {
                var result = await _popupService.ShowImageFromFilePopUpAsync(null);
            }
            else
            {
                AttachmentImage attachment = mapPin.attachmentFile.Where(x => x.fileName == parameterName).First();
                if (attachment.filePath != null)
                {
                    var result = await _popupService.ShowImageFromFilePopUpAsync(attachment.filePath);

                    // mapPin.attachmentFile.Add(attachment);
                }
            }

        }
    }
}
