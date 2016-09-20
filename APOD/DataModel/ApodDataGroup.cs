using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APOD.DataModel
{
    /// <summary>
    /// Apod group data model.
    /// </summary>
    public class ApodDataGroup
    {
        public ApodDataGroup(String uniqueId, String title, String imagePath, String description)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Description = description;
            this.ImagePath = imagePath;
            this.Items = new ObservableCollection<ApodDataItem>();
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public ObservableCollection<ApodDataItem> Items { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
