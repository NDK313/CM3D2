using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System;
using System.Collections.ObjectModel;

namespace CM3D2.ModPacker
{
    public class FileItems : ObservableCollection<FileItem> { }

    public class FileItem
    {
        public string fileName;

        public ImageSource FileIcon { set; get; }
        public string FileName
        {
            set
            {
                fileName = value;
            }
            get
            {
                return fileName;
            }
        }
        public string FilePath { set; get; }

        public ObservableCollection<FileItem> subFileItems { set; get; }

        public FileItem()
        {
            //File.Create(System.IO.Path.GetTempPath() + @"\" + "file");




            FileName = "AAaaaAAB" + new Random().Next();


            Icon sysicon = System.Drawing.Icon.ExtractAssociatedIcon(@"C:\Users\Rterg\OneDrive\Programming\VisualStudio\CM3D2\CM3D2.ModPacker\CM3D2.ModPacker.csproj");
            BitmapSource bmpSrc = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(sysicon.Handle, System.Windows.Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            sysicon.Dispose();

            this.FileIcon = bmpSrc;


            this.subFileItems = new ObservableCollection<FileItem>();
        }
    }
}
