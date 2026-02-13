using CommunityToolkit.Maui.Views;
using RekodLampuJalan.Models;

namespace RekodLampuJalan.PopUp;

public partial class FlyCablePopUp :Popup
{
	public FlyCablePopUp(FlyCable flyCable)
	{

		InitializeComponent();
        jarakCable.Text = flyCable.jarak.ToString("F2") + " m";
        startLat.Text = flyCable.start_lattitude.ToString("F4");
        startLong.Text = flyCable.start_longitude.ToString("F4");
        endLat.Text = flyCable.end_lattitude.ToString("F4");
        EndLong.Text = flyCable.end_longitude.ToString("F4");
        if(App.login)
        {
          DeleteButton.IsVisible = true;
        }
        else
        {
            DeleteButton.IsVisible = false;
        }
    }
    private async void OnCloseClicked(object sender, EventArgs e)
    {
        this.Close();
    }
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        this.Close("Delete");
    }

}