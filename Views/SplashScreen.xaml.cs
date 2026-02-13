using RekodLampuJalan.Utils;

namespace RekodLampuJalan.Views;

public partial class SplashScreen : ContentPage
{
	public SplashScreen()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        bool flginternet= await CheckInternetConnectionAsync();
        if(flginternet)
        {
            common common = new common();
            LoadData loadReferences = new LoadData();
            App.DropDownItem = await loadReferences.GetDropDownAsync();
            App.MainData = await loadReferences.GetMainDataAsync();
            App.Alamat = await loadReferences.GetAlamatAsync();
            await common.SyncAlamat(App.MainData, App.Alamat);
            await Shell.Current.GoToAsync("///MapsView");
        }

        //await Shell.Current.GoToAsync("///SummaryPage");
    }

/* Unmerged change from project 'RekodLampuJalan (net9.0-windows10.0.19041.0)'
Before:
    private bool CheckInternetConnection()
    {
After:
    private bool CheckInternetConnectionAsync()
    {
*/
    private async Task<bool> CheckInternetConnectionAsync()
    {
        var current = Connectivity.Current.NetworkAccess;

        if (current != NetworkAccess.Internet)
        {
            await Application.Current.MainPage.DisplayAlert(
                "No Internet",
                "No internet connection available. The app will now close.",
                "Exit");


#if ANDROID
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
#endif
            
        }
        return true;
    }
}