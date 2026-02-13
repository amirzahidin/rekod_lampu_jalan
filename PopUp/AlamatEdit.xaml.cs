using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Devices.Sensors;
using RekodLampuJalan.PopUp.PopUpViewModel;
using RekodLampuJalan.Utils;

namespace RekodLampuJalan.PopUp;

public partial class AlamatEdit : Popup
{
	AlamatEditViewModel viewModel;
	public AlamatEdit( List<AlamatInsert> listAlamat, AlamatInsert alamatInsert)
	{
		InitializeComponent();
        viewModel=new AlamatEditViewModel(listAlamat, alamatInsert);
        this.BindingContext = viewModel;
     //   this.Opened += AlamatEdit_OpenedAsync;
		

    }
    private async void AlamatEdit_OpenedAsync(object? sender, CommunityToolkit.Maui.Core.PopupOpenedEventArgs e)
    {

    }
    private async void OnSaveClicked(object sender, EventArgs e)
    {
        //AlamatInsert lss = viewModel.alamat;
        //AlamatInsert insert = new AlamatInsert();
        //insert.nama_jalan = Jalan.SelectedItem.ToString();
        //insert.bandar = Bandar.SelectedItem.ToString();
        //insert.daerah = Daerah.SelectedItem.ToString();
        //insert.poskod = Poskod.SelectedItem.ToString();
        //insert.negeri = Negeri.SelectedItem.ToString();
        if(Jalan.SelectedItem==null||Jalan.SelectedItem.ToString() == "")
        {
            await Application.Current.MainPage.DisplayAlert("Data Tidak Mencukupi", "Sila pilih nama jalan", "OK");
        }
        else if(Bandar.SelectedItem == null||Bandar.SelectedItem.ToString() == "")
        {
            await Application.Current.MainPage.DisplayAlert("Data Tidak Mencukupi", "Sila pilih bandar", "OK");
        }
        else if(Daerah.SelectedItem == null || Daerah.SelectedItem.ToString() == "")
        {
            await Application.Current.MainPage.DisplayAlert("Data Tidak Mencukupi", "Sila pilih daerah", "OK");
        }
        else if(Poskod.SelectedItem == null||Poskod.SelectedItem.ToString() == "")
        {
            await Application.Current.MainPage.DisplayAlert("Data Tidak Mencukupi", "Sila pilih poskod", "OK");
        }
        else if(Negeri.SelectedItem == null || Negeri.SelectedItem.ToString() == "")
        {
            await Application.Current.MainPage.DisplayAlert("Data Tidak Mencukupi", "Sila pilih negeri", "OK");
        }
        else
        {
            this.Close(viewModel.alamat);
        }
    
    }
    private async void OnCancelClicked(object sender, EventArgs e)
    {
        this.Close();
    }
}