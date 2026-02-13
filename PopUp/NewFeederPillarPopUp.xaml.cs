using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using RekodLampuJalan.Models;
using RekodLampuJalan.PopUp.PopUpViewModel;
using RekodLampuJalan.Utils;

namespace RekodLampuJalan.PopUp;

public partial class NewFeederPillarPopUp : Popup
{
	NewFeederPillarPopUpViewModel viewModel;
	public NewFeederPillarPopUp(Location location, AlamatInsert alamat)
	{
        
        InitializeComponent();
		viewModel=new NewFeederPillarPopUpViewModel(location,alamat);
		this.BindingContext = viewModel;
        
    }
    public NewFeederPillarPopUp(FeederPillar feederPillar)
    {
        InitializeComponent();
        viewModel = new NewFeederPillarPopUpViewModel(feederPillar);
        this.BindingContext = viewModel;
    }
    private async void OnSaveClicked(object sender, EventArgs e)
	{
        string[] add1 = viewModel.address1Text.Split(",");
        string[] add2 = viewModel.address2Text.Split(",");
        string[] add3 = viewModel.address2Text.Split(",");
        bool flgAdd = true;
        foreach (string s in add1)
        {
            if (s.Trim() == "" || s == null)
            {
                flgAdd = false;
            }
        }
        foreach (string s in add2)
        {
            if (s.Trim() == "" || s == null)
            {
                flgAdd = false;
            }
        }
        foreach (string s in add3)
        {
            if (s.Trim() == "" || s == null)
            {
                flgAdd = false;
            }
        }
        if (viewModel.feederPillar.no_meter == "" || viewModel.feederPillar.no_meter == null)
        {
            await Application.Current.MainPage.DisplayAlert("Data Tidak Mencukupi", "Sila lengkapkan no meter", "OK");
        }
        else if (!flgAdd)
        {
            await Application.Current.MainPage.DisplayAlert("Data Tidak Mencukupi", "Sila lengkapkan alamat", "OK");
        }
        else
        {
            FeederPillarPopUpData feederPillarPopUp = new FeederPillarPopUpData();
            feederPillarPopUp.IsUpdate = viewModel.IsUpdate;
            feederPillarPopUp.IsDelete = false;
            feederPillarPopUp.feederPillar = viewModel.feederPillar;
            this.Close(feederPillarPopUp);
        }

	}
    private async void OnCancelClicked(object sender, EventArgs e)
    {
        this.Close();
    }
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        FeederPillarPopUpData feederPillarPopUp = new FeederPillarPopUpData();
        feederPillarPopUp.IsUpdate = viewModel.IsUpdate;
        feederPillarPopUp.IsDelete = true;
        feederPillarPopUp.feederPillar = viewModel.feederPillar;
        this.Close(feederPillarPopUp);
    }
}