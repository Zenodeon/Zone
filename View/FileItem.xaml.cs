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
using DebugLogger.Wpf;
using XamlAnimatedGif;
using System.IO;

namespace Zone.View
{
    /// <summary>
    /// Interaction logic for FileItem.xaml
    /// </summary>
    public partial class FileItem : UserControl
    {
        public ContentPresenter frame { get; set; }
        public int id { get; set; }

        private bool previewSet = false;

        private CFileInfo fileInfo;

        public FileItem(int id)
        {
            InitializeComponent();

            frame = new ContentPresenter();
            this.id = id;

            frame.Content = this;
        }

        public void Configure(CFileInfo cFileInfo)
        {
            fileInfo = cFileInfo;
            fileNameBlock.Text = fileInfo.fileName;

            ThumbnailExtactorManager._instance.GetThumbnail(fileInfo.filePath, SetThumbnail);
            //ThumbnailExtactorManager._instance.GetThumbnailPreview(fileInfo.filePath, SetThumbnailPreview);
        }

        private void SetThumbnail(MemoryStream thumbnailStream)
        {
            if (!previewSet)
                Dispatcher.BeginInvoke(() => AnimationBehavior.SetSourceStream(filePreview, thumbnailStream), DispatcherPriority.Normal);
        }

        private void SetThumbnailPreview(MemoryStream thumbnailStream)
        {
            Dispatcher.BeginInvoke(() => AnimationBehavior.SetSourceStream(filePreview, thumbnailStream), DispatcherPriority.Normal);
            previewSet = true;
        }
    }
}
