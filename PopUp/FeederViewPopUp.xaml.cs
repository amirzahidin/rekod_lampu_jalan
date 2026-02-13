using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using RekodLampuJalan.Models;
using RekodLampuJalan.PopUp.PopUpViewModel;
using System.Diagnostics.Metrics;

namespace RekodLampuJalan.PopUp
{
    public partial class FeederViewPopUp : Popup
    {
        FeederViewPopUpViewModel viewModel;
        public FeederViewPopUp(FeederPillar feederPillar)
        {
         //   WeakReferenceMessenger.Default.Send(new MapViewBusyMessage(true));
            InitializeComponent();
            viewModel = new FeederViewPopUpViewModel(feederPillar);
            this.BindingContext = viewModel;
      //     WeakReferenceMessenger.Default.Send(new MapViewBusyMessage(false));
            //if (attchment != "" && attchment != null)
            //{
            //    AttachedImage.IsVisible = true;
            //    AttachedImage.Source = ImageSource.FromUri(new Uri("https://vertbyte.com/rekod_lampu_jalan/uploads/" + attchment));
            //    this.Size = new Size(340, 300);
            //}
            //else
            //{

            //}
        }
        private async void OnCloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

