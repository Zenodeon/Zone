using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;

namespace Zone.ThumbnailExtractor
{
    public static class ThumbnailHelper
    {
        public static BitmapImage CreateBitmapImage(MemoryStream memoryStream)
        {
            BitmapImage image = new BitmapImage();

            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = memoryStream;
            image.EndInit();
            image.Freeze();

            return image;
        }
    }
}
