using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2
{
    public interface IWifiService
    {
        Task<List<string>> GetAvailableSsidsAsync();
    }

}
