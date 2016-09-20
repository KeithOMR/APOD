using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APOD.DataModel
{
    /// <summary>
    /// Apod item data model.
    /// </summary>
    public class ApodDataItem
    {
        public ApodDataItem(string uniqueId, string title, string imagePath, string content, string mediaType)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.ImagePath = imagePath;
            this.Content = content;
            this.MediaType = mediaType;
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string ImagePath { get; private set; }
        public string Content { get; private set; }
        public string MediaType { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
