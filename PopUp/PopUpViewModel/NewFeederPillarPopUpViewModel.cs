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
    public class NewFeederPillarPopUpViewModel : ViewModelBase
    {
        private FeederPillar _feederPillar;
        public FeederPillar feederPillar
        {
            get { return _feederPillar; }
            set
            {
                if (value != _feederPillar)
                {
                    _feederPillar = value;
                    OnPropertyChanged("feederPillar");
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
        public ICommand OnUbahaAlamatClicked { get { return new Command(async () => await UbahaAlamatClicked_()); } }
        PopupService _popupService;
        private Location _location;
        private AlamatInsert alamat;
        public NewFeederPillarPopUpViewModel(Location location, AlamatInsert alamat_)
        {
            feederPillar=new FeederPillar();
            _popupService=new PopupService();
            IsUpdate = false;
            _location = location;
            feederPillar.alamatInsert = alamat_;
            alamat = alamat_;
            latittudeText = "Lattitude: " + location.Latitude.ToString("F4"); ;
            longitudeText = "Longitude: " + location.Longitude.ToString("F4"); ;
            address1Text = alamat_.nama_jalan + ", " + alamat_.bandar;
            address2Text = alamat_.poskod + " " + alamat_.daerah;
            address3Text = alamat_.negeri;
        }
        public NewFeederPillarPopUpViewModel(FeederPillar feederPillar_)
        {
            feederPillar = feederPillar_;
            _popupService = new PopupService();
            string[] existImageName = feederPillar.attachment.Split(',');
            for (int i = 0; i < existImageName.Count(); i++)
            {
                if (existImageName[i] != "" && existImageName[i] != null)
                {
                    AttachmentImage attachment = new AttachmentImage();
                    attachment.fileName = ConstantValue.feederPillarAttachmentList[i];
                    attachment.filePath = ConstantValue.ImageUrl + existImageName[i];
                    attachment.newItem = false;
                    feederPillar.attachmentFile.Add(attachment);
                }
            }
            alamat = new AlamatInsert
            {
                nama_jalan = feederPillar.alamatFull.jalan.nama,
                bandar = feederPillar.alamatFull.bandar.nama,
                poskod = feederPillar.alamatFull.poskod.nama,
                daerah = feederPillar.alamatFull.daerah.nama,
                negeri = feederPillar.alamatFull.negeri.nama
            };
            IsUpdate = true;
            latittudeText = "Lattitude: " + feederPillar.lattitude.ToString("F4"); ;
            longitudeText = "Longitude: " + feederPillar.longitude.ToString("F4"); ;
            address1Text = feederPillar.alamatFull.jalan.nama + ", " + feederPillar.alamatFull.bandar.nama;
            address2Text = feederPillar.alamatFull.poskod.nama + " " + feederPillar.alamatFull.daerah.nama;
            address3Text = feederPillar.alamatFull.negeri.nama;
            _location = new Location(feederPillar.lattitude, feederPillar.longitude);
        }
        private async Task OnAttachmentImageClicked_(string parameterName)
        {
           // IsBusy = true;
            if (feederPillar.attachmentFile.Where(x => x.fileName == parameterName).Count() == 0)
            {
                var result = await _popupService.ShowImageFromFilePopUpAsync(null);
                if (result.FilePath != null)
                {
                    AttachmentImage attachment = new AttachmentImage();
                    attachment.fileName = parameterName;
                    attachment.filePath = result.FilePath;
                    attachment.newItem = true;
                    feederPillar.attachmentFile.Add(attachment);
                }
            }
            else
            {
                AttachmentImage attachment = feederPillar.attachmentFile.Where(x => x.fileName == parameterName).First();
                if (attachment.filePath != null)
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
                }
            }
           // IsBusy = false;
        }
        private async Task UbahaAlamatClicked_()
        {
            IsBusy = true;
            LocationFunction locationFunction = new LocationFunction();
            //alamat_ = new AlamatInsert();
            List<AlamatInsert> listAlamat = await locationFunction.GetAddressList(_location);
            AlamatInsert alamatnew = await _popupService.ShowAlamatEditPopUpAsync(listAlamat, alamat);
            if (alamatnew != null)
            {
                feederPillar.alamatInsert = alamatnew;
                address1Text = alamatnew.nama_jalan + ", " + alamatnew.bandar;
                address2Text = alamatnew.poskod + " " + alamatnew.daerah;
                address3Text = alamatnew.negeri;
                alamat = alamatnew;
            }

            IsBusy = false;
        }
    }
}
