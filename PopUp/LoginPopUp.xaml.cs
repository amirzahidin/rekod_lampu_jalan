using CommunityToolkit.Maui.Views;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RekodLampuJalan.Utils;
using CommunityToolkit.Mvvm.Messaging;
using RekodLampuJalan.Models;

namespace RekodLampuJalan.PopUp
{
    public partial class LoginPopUp : Popup
    {
        public LoginPopUp()
        {
            InitializeComponent();
        }
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            loadingGrid.IsVisible=true;
            loadingSpinner.IsRunning=true;
            //WeakReferenceMessenger.Default.Send(new MapViewBusyMessage(true));
            CommonApi apiservice=new CommonApi();
            var result = await apiservice.LoginAsync(userName.Text, passwordEntry.Text);
            var jsonResponse = JsonConvert.DeserializeObject<LoginResponse>(result);
            string ApiMessage = jsonResponse.message;
            if(ApiMessage== "Invalid password")
            {
                await Application.Current.MainPage.DisplayAlert("Login Failed", "Invalid password", "OK");
            }
            else if(ApiMessage == "User not found")
            {
                await Application.Current.MainPage.DisplayAlert("Login Failed", "User not found", "OK");
            }
            else if(ApiMessage == "Login successful")
            {
               // WeakReferenceMessenger.Default.Send(new MapViewBusyMessage(true));
               App.userName = userName.Text;
                App.userId= int.Parse(jsonResponse.user.id);
                Close(ApiMessage);
               
            }
            loadingGrid.IsVisible = false;
            loadingSpinner.IsRunning = false;
        }
        private async void OnCancelClicked(object sender, EventArgs e)
        {
            this.Close();
        }
        private void OnTogglePasswordClicked(object sender, EventArgs e)
        {
            passwordEntry.IsPassword = !passwordEntry.IsPassword;

            // swap icon depending on state
            togglePasswordButton.Source = passwordEntry.IsPassword ? "show.png" : "hide.png";
        }
    }
}

