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
using Ookii.Dialogs.Wpf;
using Zone.View;
using Zone.Database;
using Zone.Backend;
using Zone.Component.FileItemCmpt;
using Zone.ThumbnailExtractor;

namespace Zone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ZoneWindow : Window
    {
        public static ZoneWindow _instance;

        public ZoneHandler zone { get; private set; }

        public ZoneWindow()
        {
            DLog.Instantiate();

            InitializeComponent();

            _instance = this;

            new ThumbnailExtactorManager().Instantiate();
        }

        private void DO(object sender, RoutedEventArgs e)
        {
            //FileInterface.Helper.ZoneMetadataHelper.GenerateMetadata(ZoneLink.uriPath + ZoneLink.media);
        }

        private void DO2(object sender, RoutedEventArgs e)
        {
            //for (int i = 0; i < 100; i++)
            //    DatabaseHandler.activeDatabase.testad();

            //DLog.Log("Done");
        }

        private void OpenDialog(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog fileDialog = new VistaFolderBrowserDialog();
            fileDialog.Multiselect = false;

            bool? success = fileDialog.ShowDialog();
            bool selected = success == null ? false : success.Value;
            //if (selected)
            //    LoadDirectory(fileDialog.SelectedPath);

            if (selected)
            {
                zone = new ZoneHandler(fileDialog.SelectedPath);
                DisplayZoneFiles();
            }
        }

        private void DisplayZoneFiles()
        {
            zone.LoadZone();

                foreach (FileItem item in zone.fileItems)
                    fileView.AddFileItem(item);
        }

        #region UI Interface

        private void OnBarDrag(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            if (zone != null)
                zone.Close();
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
