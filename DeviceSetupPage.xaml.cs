namespace MauiApp2;
using SQLite;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;

public partial class DeviceSetupPage : ContentPage
{
	public DeviceSetupPage()
	{
		InitializeComponent();
	}

    private async void OnSendCredentialsClicked(object sender, EventArgs e)
    {
        string ssid = ssidEntry.Text?.Trim();
        string password = passwordEntry.Text?.Trim();

        if (string.IsNullOrEmpty(ssid) || string.IsNullOrEmpty(password))
        {
            statusLabel.Text = "Lütfen tüm alanlarý doldurun.";
            return;
        }

        try
        {
            var client = new HttpClient();
            var content = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("ssid", ssid),
            new KeyValuePair<string, string>("password", password)
        });

            // Wi-Fi baðlantýsýný saðlayan endpoint'e isteði gönder
            var response = await client.PostAsync("http://192.168.4.1/wifi", content);
            var contentString = await response.Content.ReadAsStringAsync();
            statusLabel.Text = $"StatusCode: {(int)response.StatusCode} ({response.StatusCode})";

            if (response.IsSuccessStatusCode)
            {
                
                
                
                statusLabel.TextColor = Colors.Green;
                statusLabel.Text = "Bilgiler gönderildi. Cihaz yeniden baþlatýlacak.";
            }
            else
            {
                statusLabel.TextColor = Colors.Red;
                statusLabel.Text = "Gönderim baþarýsýz. Cihaz açýk mý?";
            }
        }
        catch (Exception ex)
        {
            statusLabel.TextColor = Colors.Red;
            statusLabel.Text = $"Hata: {ex.Message}";
        }
    }

   

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var client = new HttpClient();
        // ESP32 IP adresini almak için baþka bir GET isteði gönderiyoruz
        var ipResponse = await client.GetStringAsync("http://kilit.local/get-ip");


        //// Gelen IP'yi kaydediyoruz
        DataBaseHelper dbHelper = new DataBaseHelper();
        dbHelper.AddDevice(new Device { IpAddress = ipResponse });
        statusLabel.Text = $"Ýp adresi alýndý {ipResponse}";
    }
}