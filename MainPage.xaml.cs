using SQLite;
namespace MauiApp2
{
    public partial class MainPage : ContentPage
    {
        private DataBaseHelper dbHelper;

        public MainPage()
        {
            InitializeComponent();
            dbHelper = new DataBaseHelper();
            LoadDevices();
        }
        private void LoadDevices()
        {
            var devices = dbHelper.GetAllDevices();
            devicesListView.ItemsSource = devices;
        }

        private async void cihazekleBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeviceSetupPage());
        }

        private async void Kontrol_Clicked(object sender, EventArgs e)
        {
           // await Navigation.PushAsync(new LockControlPage());
        }
        private async void OnDeviceTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                var selectedDevice = e.Item as Device;
                await Navigation.PushAsync(new LockControlPage(selectedDevice));
            }
        }
    }

}
