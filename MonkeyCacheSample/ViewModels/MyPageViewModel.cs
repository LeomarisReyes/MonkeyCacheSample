using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using MonkeyCache.FileStore;
using MonkeyCacheSample.Models;
using MonkeyCacheSample.Services;
using Newtonsoft.Json;
using Refit;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;

namespace MonkeyCacheSample.ViewModels
{
    public class MyPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Fuel> fuel { get; set; }
        public HttpResponseMessage response;
        public string BaseURL = "http://www.apidashboard.somee.com"; 

        public MyPageViewModel()
        {
            Barrel.ApplicationId = "MonkeyCacheSample";
            Task.Run(() => GetResponse());
        }
        async Task<IEnumerable<Fuel>> GetResponse()
        {
            if(Connectivity.NetworkAccess != NetworkAccess.Internet && !Barrel.Current.IsExpired(key: BaseURL))
            {
                 fuel = new ObservableCollection<Fuel>(Barrel.Current.Get<IEnumerable<Fuel>>(key: BaseURL));
                 await PopupNavigation.Instance.PushAsync(new Views.PopUpWarning());
            }

            var apiResponse = RestService.For<IFuelsApi>(BaseURL); 
            response = await apiResponse.GetFuels();

            if (response.IsSuccessStatusCode)
            {
                var JD = JsonConvert.DeserializeObject<List<Fuel>>(await response.Content.ReadAsStringAsync());
                fuel = new ObservableCollection<Fuel>(JD); 
                Barrel.Current.Add(key: BaseURL, data: fuel, expireIn: TimeSpan.FromDays(2));
            } 
            return null;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}


