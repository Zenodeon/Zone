using System;
using System.IO;
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
using Ookii.Dialogs.Wpf;
using Zone.View;

namespace Zone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ZoneWindow : Window
    {
        public ZoneWindow()
        {
            DLog.Instantiate();

            InitializeComponent();

            new ThumbnailExtactorManager().Instantiate();

        }

        private void DO(object sender, RoutedEventArgs e)
        {
            Zoner zoner = new Zoner();
            zoner.ApplyZoneMetadata();
        }

        private void OpenDialog(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog fileDialog = new VistaFolderBrowserDialog();

            bool? success = fileDialog.ShowDialog();
            bool selected = success == null ? false : success.Value;
            if (selected)
                LoadDirectory(fileDialog.SelectedPath);
        }

        private void LoadDirectory(string directoryPath)
        {
            DLog.Log("SelectedPath : " + directoryPath);

            int count = 0;
            foreach (string path in Directory.GetFiles(directoryPath))
            {
                FileItem fileItem = new FileItem(count);
                count++;

                CFileInfo fileInfo = new CFileInfo(path);
                fileItem.Configure(fileInfo);

                fileView.AddFileItem(fileItem);
            }
        }

        #region UI Interface

        private void OnBarDrag(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MaxWindow(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
        }

        private void MinWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        #endregion  
    }
}
