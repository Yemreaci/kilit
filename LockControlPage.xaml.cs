namespace MauiApp2;
using System.Net.Http;
public partial class LockControlPage : ContentPage
{
    private Device _device;
    public LockControlPage(Device device)
    {
        InitializeComponent();
        _device = device;
    }

    private async void OpenLockButton_Clicked(object sender, EventArgs e)
{
    try
    {
        var client = new HttpClient();
        var response = await client.GetAsync("http://"+_device.IpAddress+"/lock?state=open");

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("Baþarýlý", "Kilit açýldý!", "Tamam");
        }
        else
        {
            await DisplayAlert("Hata", "Kilit açma baþarýsýz!", "Tamam");
        }
    }
    catch (Exception ex)
    {
        await DisplayAlert("Baðlantý Hatasý", ex.Message, "Tamam");
    }
}

private async void CloseLockButton_Clicked(object sender, EventArgs e)
{
    try
    {
        var client = new HttpClient();
        var response = await client.GetAsync("http://"+_device.IpAddress+"/lock?state=close");

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("Baþarýlý", "Kilit kapatýldý!", "Tamam");
        }
        else
        {
            await DisplayAlert("Hata", "Kilit kapatma baþarýsýz!", "Tamam");
        }
    }
    catch (Exception ex)
    {
        await DisplayAlert("Baðlantý Hatasý", ex.Message, "Tamam");
    }
}

}
