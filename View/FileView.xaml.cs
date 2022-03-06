using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using WpfToolkit.Controls;
using Zone.Component.FileItemCmpt;

namespace Zone.View
{
    /// <summary>
    /// Interaction logic for FileView.xaml
    /// </summary>
    public partial class FileView : UserControl
    {
        //private WrapPanel filePanel { get; set; }

        ActiveFileItemList<ContentPresenter> activeFiles = new ActiveFileItemList<ContentPresenter>();

        public FileView()
        {
            InitializeComponent();

            fileControl.ApplyTemplate();
            //filePanel = (WrapPanel)fileControl.Template.FindName("filePanel", fileControl);

            activeFiles.Initialize();

            fileControl.ItemsSource = activeFiles;
        }

        public void AddFileItem(FileItem fileItem)
        {
            activeFiles.Add(fileItem);
        }

        private void FileControl_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //if (autoScroll & e.Delta > 0)
            //    autoScroll = false;

            //logViewer.ScrollToVerticalOffset(logViewer.VerticalOffset - (e.Delta / 6));

            e.Handled = true;
        }
    }
}
