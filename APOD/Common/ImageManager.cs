using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void SaveImage()
        {
            int i = 6;
        }
    }
}
