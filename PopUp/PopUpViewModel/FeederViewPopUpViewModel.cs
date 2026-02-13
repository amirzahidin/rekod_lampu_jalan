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

    public class FeederViewPopUpViewModel : ViewModelBase
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
        public FeederViewPopUpViewModel(FeederPillar feederPillar_)
        {
            feederPillar= feederPillar_;
            feederPillar.attachmentFile = new List<AttachmentImage>();
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
            _popupService = new PopupService();
            latittudeText = "Lattitude: " + feederPillar.lattitude.ToString("F4"); ;
            longitudeText = "Longitude: " + feederPillar.longitude.ToString("F4"); ;
            address1Text = feederPillar.alamatFull.jalan.nama + ", " + feederPillar.alamatFull.bandar.nama;
            address2Text = feederPillar.alamatFull.poskod.nama + " " + feederPillar.alamatFull.daerah.nama;
            address3Text = feederPillar.alamatFull.negeri.nama;
        }
        private async Task OnAttachmentImageClicked_(string parameterName)
        {
            if (feederPillar.attachmentFile.Where(x => x.fileName == parameterName).Count() == 0)
            {
                var result = await _popupService.ShowImageFromFilePopUpAsync(null);
            }
            else
            {
                AttachmentImage attachment = feederPillar.attachmentFile.Where(x => x.fileName == parameterName).First();
                if (attachment.filePath != null)
                {
                    var result = await _popupService.ShowImageFromFilePopUpAsync(attachment.filePath);

                    // mapPin.attachmentFile.Add(attachment);
                }
            }

        }
    }
}
