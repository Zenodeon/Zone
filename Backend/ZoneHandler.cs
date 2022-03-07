using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using Zone.Component.FileItemCmpt;
using Zone.Database;
using Zone.ThumbnailExtractor;

namespace Zone.Backend
{
    public class ZoneHandler
    {
        private const string DataFolder = @"\ZoneData";
        private const string ThumbnailFolder = @"\Thumbnails";
        private const string DatabaseName = @"\ZoneDatabase";

        private string zonePath;

        private string dataFolderPath
        {
            get
            {
                string path = $"{zonePath}{DataFolder}";
                if (!Directory.Exists(path))
                {
                    DirectoryInfo directoryInfo = Directory.CreateDirectory(path);
                    directoryInfo.Attributes |= FileAttributes.Hidden;
                }
                return path;
            }
        }
        private string thumbnailFolderPath
        {
            get
            {
                string path = $"{dataFolderPath}{ThumbnailFolder}";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }
        private string databasePath => $"{dataFolderPath}{DatabaseName}";

        public DatabaseHandler database { get; private set; }

        public List<FileItem> fileItems = new List<FileItem>();

        public ZoneHandler(string path)
        {
            zonePath = path;

            database = new DatabaseHandler(databasePath);
        }

        public void Close()
        {
            if (DatabaseHandler.activeDatabase != null)
                DatabaseHandler.activeDatabase.CloseDB();
        }

        public void LoadZone()
        {
            int count = 0;
            foreach (string path in Directory.EnumerateFiles(zonePath))
            {
                FileItem fileItem = new FileItem(count);
                count++;

                CFileInfo fileInfo = new CFileInfo(path);
                fileItem.Configure(fileInfo);

                fileItems.Add(fileItem);
            }
        }

        #region Thumbnail

        public void GetThumbnail(string fileID, string filePath, Action<BitmapImage> callback)
        {
            string thumbnailPath = $@"{thumbnailFolderPath}\{fileID}";

            //Trys to load thumbnail from cachefile, else falls back to creating thumbnail from file.
            if (File.Exists(thumbnailPath))
                if (TryLoadThumbnailFromFile(thumbnailPath, out BitmapImage thumbnail))
                {
                    callback.Invoke(thumbnail);
                    return;
                }

            ThumbnailExtactorManager._instance.GetThumbnail(filePath, (MemoryStream mStream) =>
            {
                Task.Run(() =>
                {
                    callback.Invoke(ThumbnailHelper.CreateBitmapImage(mStream));
                    SaveThumbnail(fileID, mStream);
                });
            });
        }

        private void SaveThumbnail(string fileID, MemoryStream thumbnailMStream)
        {
            string thumbnailPath = $@"{thumbnailFolderPath}\{fileID}";

            byte[] thumbnailData = thumbnailMStream.ToArray();
            File.WriteAllBytes(thumbnailPath, thumbnailData);
        }

        private bool TryLoadThumbnailFromFile(string path, out BitmapImage thumbnail)
        {
            using FileStream thumbnailFile = File.Open(path, FileMode.Open, FileAccess.Read);
            MemoryStream thumbnailMemory = new MemoryStream();

            thumbnailFile.CopyTo(thumbnailMemory);

            try
            {
                thumbnail = ThumbnailHelper.CreateBitmapImage(thumbnailMemory);
                return true;
            }
            catch
            {
                thumbnail = null;
                return false;
            }
        }

        #endregion
    }
}
