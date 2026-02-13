using RekodLampuJalan.Models;

namespace RekodLampuJalan
{
    public partial class App : Application
    {
        public static DropDownItem? DropDownItem { get; set; }
        public static MainData? MainData { get; set; }
        public static Alamat? Alamat { get; set; }
        public static bool login {  get; set; } = false;
        public static string userName { get; set; } = "";
        public static int userId { get; set; } = 0;
        public App()
        {
            InitializeComponent();
            //Application.Current.UserAppTheme = AppTheme.Light;
            //  MainPage = new AppShell();
            //   Shell.Current.GoToAsync("//SplashScreen");
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}