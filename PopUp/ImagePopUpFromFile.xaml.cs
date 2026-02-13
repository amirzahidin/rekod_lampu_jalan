using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using RekodLampuJalan.Models;

namespace RekodLampuJalan.PopUp;

public partial class ImagePopUpFromFile : Popup
{
    private TaskCompletionSource<ImagePopupResult> _taskCompletionSource = new();

    private string? _imagePath;
    public ImagePopUpFromFile(string? imagePath = null)
	{
      //  WeakReferenceMessenger.Default.Send(new MapViewBusyMessage(true));
        InitializeComponent();
        _imagePath = imagePath;
        if(!string.IsNullOrEmpty(imagePath)&&imagePath.Contains("https://"))
        {
            PreviewImageBorder.IsVisible = true;
            tittleName.Text = "Attachment Preview";
            PreviewImage.Source = imagePath;
        }
        else if (!string.IsNullOrEmpty(imagePath))
        {
            PreviewImageBorder.IsVisible = true;
            tittleName.Text = "Attachment Preview";
            PreviewImage.Source = ImageSource.FromFile(imagePath);
        }
        else
        {
            PreviewImageBorder.IsVisible = false;
            tittleName.Text = "No Image.";
        }
        if(App.login)
        {
            if(PreviewImageBorder.IsVisible)
            {
                this.Size = new Microsoft.Maui.Graphics.Size(400, 630);
            }
            else
            {
                this.Size = new Microsoft.Maui.Graphics.Size(400, 320);
            }
            saveButton.IsVisible = true;
            uploadButton.IsVisible = true;
        }
        else
        {
            if (PreviewImageBorder.IsVisible)
            {
                this.Size = new Microsoft.Maui.Graphics.Size(400, 530);
            }
            else
            {
                this.Size = new Microsoft.Maui.Graphics.Size(400, 220);
            }
            saveButton.IsVisible = false;
            uploadButton.IsVisible = false;
        }
   //     WeakReferenceMessenger.Default.Send(new MapViewBusyMessage(true));
    }
   
    private async void OnPickFileClicked(object sender, EventArgs e)
    {
        loadingGrid.IsVisible = true;
        loadingSpinner.IsRunning = true;
        if (await RequestStoragePermissionAsync())
        {
            var photo = await MediaPicker.PickPhotoAsync();
            if (photo != null)
            {
                _imagePath = photo.FullPath;
                var stream = await photo.OpenReadAsync();           
                PreviewImage.Source = ImageSource.FromStream(() => stream);
                PreviewImageBorder.IsVisible = true;
                tittleName.Text = "Attachment Preview";
            }
        }
        if (PreviewImageBorder.IsVisible)
        {
            this.Size = new Microsoft.Maui.Graphics.Size(400, 630);
        }
        else
        {
            this.Size = new Microsoft.Maui.Graphics.Size(400, 320);
        }
        loadingGrid.IsVisible = false;
        loadingSpinner.IsRunning = false;
    }

    private async void OnTakePhotoClicked(object sender, EventArgs e)
    {
        loadingGrid.IsVisible = true;
        loadingSpinner.IsRunning = true;
        if (await RequestCameraPermissionAsync())
        {
            var photo = await MediaPicker.CapturePhotoAsync();
            if (photo != null)
            {
                _imagePath = photo.FullPath;
                var stream = await photo.OpenReadAsync();
                PreviewImage.Source = ImageSource.FromStream(() => stream);
                PreviewImageBorder.IsVisible = true;
                tittleName.Text = "Attachment Preview";

            }
        }
        if (PreviewImageBorder.IsVisible)
        {
            this.Size = new Microsoft.Maui.Graphics.Size(400, 630);
        }
        else
        {
            this.Size = new Microsoft.Maui.Graphics.Size(400, 320);
        }
        loadingGrid.IsVisible = false;
        loadingSpinner.IsRunning = false;
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
        ClosePopup();
    }
    private void OnCloseClicked(object sender, EventArgs e)
    {
        this.Close();
    }
    private void ClosePopup()
    {
        _taskCompletionSource.TrySetResult(new ImagePopupResult
        {
            FilePath = _imagePath,
         
        });

        Close();
    }

    public Task<ImagePopupResult> WaitForResultAsync() => _taskCompletionSource.Task;

    private async Task<bool> RequestCameraPermissionAsync()
    {
#if ANDROID
        var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.Camera>();
        }
        return status == PermissionStatus.Granted;
#else
            return true;
#endif
    }

    private async Task<bool> RequestStoragePermissionAsync()
    {
#if ANDROID
        var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.StorageRead>();
        }
        return status == PermissionStatus.Granted;
#else
            return true;
#endif
    }
}
