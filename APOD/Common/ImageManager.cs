using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using APOD.DataModel;

namespace APOD.Common
{
    public class ImageManager
    {
        private readonly ApodDataItem _item;
        RelayCommand _saveImageAsBackground;

        public ImageManager(ApodDataItem dataItem)
        {
            this._item = dataItem;
        }

        public RelayCommand SaveAsBackground
        {
            get
            {
                return _saveImageAsBackground ?? (_saveImageAsBackground = new RelayCommand(SaveImage));
            }
            set
            {
                _saveImageAsBackground = value;
            }
        }

        private async void SaveImage()
        {
            string fileName = Path.GetFileName(_item.ImagePath);
            var httpClient = new HttpClient();
            HttpResponseMessage message = await httpClient.GetAsync(_item.ImagePath);

            StorageFolder myfolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await myfolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            byte[] file = await message.Content.ReadAsByteArrayAsync();

            await FileIO.WriteBytesAsync(sampleFile, file);
            var files = await myfolder.GetFilesAsync();  
        }
    }
}
