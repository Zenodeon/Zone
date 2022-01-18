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
using WpfAnimatedGif;

namespace Zone.View
{
    /// <summary>
    /// Interaction logic for FileItem.xaml
    /// </summary>
    public partial class FileItem : UserControl
    {
        public ContentPresenter frame { get; set; }
        public int id { get; set; }

        public FileItem(int id)
        {
            InitializeComponent();

            frame = new ContentPresenter();
            this.id = id;

            frame.Content = this;
        }

        public void Configure(CFileInfo cFileInfo)
        {
            ThumbnailExtactorManager._instance.GetThumbnailPreview(cFileInfo.filePath,
                thumbnail);
        }

        public void thumbnail(BitmapImage image)
        {
            DLog.Log("thumbnail");
            Dispatcher.BeginInvoke(() => ImageBehavior.SetAnimatedSource(filePreview, image), DispatcherPriority.Normal);
        }
    }
}
