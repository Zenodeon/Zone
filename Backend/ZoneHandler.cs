using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using Zone.Database;

namespace Zone.Backend
{
    public class ZoneHandler
    {
        private const string DataFolder = @"\ZoneData";
        private const string ThumbnailFolder = @"\Thumbnails";
        private const string DatabaseName = @"\ZoneDatabase";

        private string dataFolderPath;

        private string thumbnailFolderPath => $"{dataFolderPath}{ThumbnailFolder}";
        private string databasePath => $"{dataFolderPath}{DatabaseName}";

        public DatabaseHandler database { get; private set; }

        public ZoneHandler(string path)
        {
            dataFolderPath = $"{path}{DataFolder}";

            if (!Directory.Exists(dataFolderPath))
            {
                DirectoryInfo directoryInfo = Directory.CreateDirectory(dataFolderPath);
                directoryInfo.Attributes |= FileAttributes.Hidden;
            }

            database = new DatabaseHandler(databasePath);
        }

        public void GetThumbnail(string fileID, string filePath, Action<BitmapImage> callback)
        {
            string thumbnailPath = $@"{thumbnailFolderPath}\{fileID}";

            if (File.Exists(thumbnailPath))
            {
                Task.Run(() => callback.Invoke(LoadThumbnail(thumbnailPath)));
            }
            else
                ThumbnailExtactorManager._instance.GetThumbnail(filePath, callback);
        }

        private BitmapImage LoadThumbnail(string path)
        {
            BitmapImage image = new BitmapImage();

            using FileStream thumbnailFile = File.Open(path, FileMode.Open, FileAccess.Read);
            MemoryStream thumbnailMemory = new MemoryStream();

            thumbnailFile.CopyTo(thumbnailMemory);

            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = thumbnailMemory;
            image.EndInit();
            image.Freeze();

            return image;
        }
    }
}
