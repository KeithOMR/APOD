using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using APOD.DataModel;
using Newtonsoft.Json;

// The data model defined by this file serves as a representative example of a strongly-typed
// model.  The property names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs. If using this model, you might improve app 
// responsiveness by initiating the data loading task in the code behind for App.xaml when the app 
// is first launched.

namespace APOD.DataModel
{
    /// <summary>
    /// Creates a collection of groups and items with content read NASA's public API Atronomy Photo of the Day.
    /// ApodDataSource initializes with data read from the API.        
    /// </summary>
    public sealed class ApodDataSource
    {
        private HttpClient _httpClient;
        private HttpResponseMessage _response;

        private static ApodDataSource _apodDataSource = new ApodDataSource();

        private ObservableCollection<ApodDataGroup> _groups = new ObservableCollection<ApodDataGroup>();
        private const string DateQueryPrefix = "&date=";

        public ObservableCollection<ApodDataGroup> Groups
        {
            get { return this._groups; }
        }

        public static async Task<IEnumerable<ApodDataGroup>> GetGroupsAsync()
        {
            await _apodDataSource.GetApodDataAsync();

            return _apodDataSource.Groups;
        }

        public static async Task<ApodDataGroup> GetGroupAsync(string uniqueId)
        {
            await _apodDataSource.GetApodDataAsync();            
            var matches = _apodDataSource.Groups.Where((group) => group.UniqueId.Equals(uniqueId));
            var apodDataGroups = matches as ApodDataGroup[] ?? matches.ToArray();

            if (apodDataGroups.Any())
                return apodDataGroups.First();
            return null;
        }

        public static async Task<ApodDataItem> GetItemAsync(string uniqueId)
        {
            await _apodDataSource.GetApodDataAsync();            
            var matches = _apodDataSource.Groups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            var apodDataItems = matches as IList<ApodDataItem> ?? matches.ToList();
            if (apodDataItems.Any()) 
                return apodDataItems.First();
            return null;
        }

        private async Task GetApodDataAsync()
        {
            _httpClient = new HttpClient();
            _response = new HttpResponseMessage();

            if (this._groups.Count != 0)
                return;

            var dates = CreateListOfDates(DateTime.Today);

            ApodDataGroup group = new ApodDataGroup("Apod Collection", "Catalog", "MyImage", "A Collection of images from NASA's Astronomy Photo of The Day Archive");

            foreach (var date in dates)
            {
                Uri resourceUri = new Uri(Constants.ApodUrl + Constants.ApiKey + date);

                _response = await _httpClient.GetAsync(resourceUri);
                _response.EnsureSuccessStatusCode();
                var responseBodyAsText = await _response.Content.ReadAsStringAsync();

                var nasaApod = JsonConvert.DeserializeObject<NasaApodResponse>(responseBodyAsText);
                group.Items.Add(new ApodDataItem(nasaApod.Title, nasaApod.Title, nasaApod.ImagePath, nasaApod.Content, nasaApod.MediaType)); 
                
                //if (nasaApod.MediaType != "image")
                //{
                //    string html = @"<iframe width=""800"" height=""480"" src="""+nasaApod.ImagePath +@"""" +@" frameborder=""0"" allowfullscreen></iframe>";
                //    nasaApod.ImagePath = html;
                    
                //    group.Items.Add(new ApodDataItem(nasaApod.Title, nasaApod.Title, nasaApod.ImagePath, nasaApod.Content, nasaApod.MediaType));    
                //}                
            }
            this.Groups.Add(group);
        }

        private List<string> CreateListOfDates(DateTime today)
        {
            var dateList = new List<string>();

            for (int i = 0; i < Constants.ApodDataDays; i++)
            {
                var date = today.Subtract(TimeSpan.FromDays(i)).ToString("yyyy-MM-dd");
                dateList.Add(DateQueryPrefix + date);                
            }

            return dateList;
        }
    }
}