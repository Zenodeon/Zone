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
using Unosquare.FFME;
using Zone.Component.FileItemCmpt;

namespace Zone.View.EditView
{
    /// <summary>
    /// Interaction logic for FileSelectionPreviewView.xaml
    /// </summary>
    public partial class FileSelectionPreviewView : UserControl
    {
        public FileSelectionPreviewView()
        {
            InitializeComponent();
        }

        public async void PreviewFile(FileItem file)
        {
            //DLog.Log("Play Call");
            //DLog.Log(file.info.filePath);
            //await displayElement.Open(new Uri(file.info.filePath));
            //await displayElement.Play();
        }
    }
}
