using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XamlAnimatedGif;
using System.IO;
using Zone.FileInterface;

namespace Zone.Component.FileItemCmpt
{
    /// <summary>
    /// Interaction logic for FileItem.xaml
    /// </summary>
    public partial class FileItem : UserControl
    {
        public ContentPresenter frame { get; set; }
        public int id { get; set; }

        public CFileInfo info { get; private set; }

        private bool thumbnailPreviewSet = false;

        ZoneMetadata metadata;

        public FileItem(int id)
        {
            InitializeComponent();

            frame = new ContentPresenter();
            this.id = id;

            frame.Content = this;
        }

        public void dummyConfigure(CFileInfo cFileInfo)
        {
            info = cFileInfo;
            fileNameBlock.Text = info.fileName;
        }

        public void Configure(CFileInfo cFileInfo)
        {
            info = cFileInfo;
            fileNameBlock.Text = info.fileName;

            Task.Run(() =>
            {
                metadata = ZoneLink.Link(info.filePath);
                ZoneWindow._instance.zone.GetThumbnail(metadata.fileID, info.filePath, SetThumbnail);
                //ThumbnailExtactorManager._instance.GetThumbnail(fileInfo.filePath, SetThumbnail);
                //ThumbnailExtactorManager._instance.GetThumbnailPreview(fileInfo.filePath, SetThumbnailPreview);
            });
        }

        private void SetThumbnail(BitmapImage thumbnail)
        {
            Dispatcher.BeginInvoke(() => filePreview.Source = thumbnail, DispatcherPriority.Normal);
        }

        private void SetThumbnailFromStream(MemoryStream thumbnailStream)
        {
            if (!thumbnailPreviewSet)
                Dispatcher.BeginInvoke(() => AnimationBehavior.SetSourceStream(filePreview, thumbnailStream), DispatcherPriority.Normal);
        }

        private void SetThumbnailPreview(MemoryStream thumbnailStream)
        {
            Dispatcher.BeginInvoke(() => AnimationBehavior.SetSourceStream(filePreview, thumbnailStream), DispatcherPriority.Normal);
            thumbnailPreviewSet = true;
        }
    }
}
