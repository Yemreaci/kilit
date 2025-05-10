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
            statusLabel.Text = "L�tfen t�m alanlar� doldurun.";
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

            // Wi-Fi ba�lant�s�n� sa�layan endpoint'e iste�i g�nder
            var response = await client.PostAsync("http://192.168.4.1/wifi", content);
            var contentString = await response.Content.ReadAsStringAsync();
            statusLabel.Text = $"StatusCode: {(int)response.StatusCode} ({response.StatusCode})";

            if (response.IsSuccessStatusCode)
            {
                
                
                
                statusLabel.TextColor = Colors.Green;
                statusLabel.Text = "Bilgiler g�nderildi. Cihaz yeniden ba�lat�lacak.";
            }
            else
            {
                statusLabel.TextColor = Colors.Red;
                statusLabel.Text = "G�nderim ba�ar�s�z. Cihaz a��k m�?";
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
        // ESP32 IP adresini almak i�in ba�ka bir GET iste�i g�nderiyoruz
        var ipResponse = await client.GetStringAsync("http://kilit.local/get-ip");


        //// Gelen IP'yi kaydediyoruz
        DataBaseHelper dbHelper = new DataBaseHelper();
        dbHelper.AddDevice(new Device { IpAddress = ipResponse });
        statusLabel.Text = $"�p adresi al�nd� {ipResponse}";
    }
}