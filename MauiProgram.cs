using Maui.GoogleMaps.Clustering.Hosting;
using Microsoft.Extensions.Logging;
using Maui.GoogleMaps.Hosting;
using CommunityToolkit.Maui;
using RekodLampuJalan.Services;

namespace RekodLampuJalan
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
#if ANDROID
            var platformConfig = new Maui.GoogleMaps.Android.PlatformConfig
            {
                BitmapDescriptorFactory = new CachingNativeBitmapDescriptorFactory()
            };

            builder.UseGoogleMaps(platformConfig);
#elif IOS
            var platformConfig = new Maui.GoogleMaps.iOS.PlatformConfig
            {
                ImageFactory = new CachingImageFactory()
            };

            builder.UseGoogleMaps(Variables.GOOGLE_MAPS_IOS_API_KEY, platformConfig);
#endif
            builder.UseGoogleMapsClustering();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IPopupService, Services.PopupService>();
            return builder.Build();
        }
    }
}
