using SharpCompress.Common;
using SharpCompress.IO;
using SharpCompress.Readers;
using SharpCompress.Writers;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO.Compression;
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


namespace CM3D2.ModPacker
{
    /// <summary>
    /// Main.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ModPackerWindow : Window
    {
        public ModPackerWindow()
        {
            Console.OutputEncoding = Encoding.Unicode;

            InitializeComponent();

            FileItem fileItem = new FileItem();
            for (int i = 0; i < 100; ++i)
                fileItem.subFileItems.Add(new FileItem());

            (FindResource("FileItems") as FileItems).Add(fileItem);

            /*
            Tuple<string, CompressionLevel>[] args = new Tuple<string, CompressionLevel>[]
            {
                new Tuple<string, CompressionLevel>("Fastest",CompressionLevel.Fastest),
                new Tuple<string, CompressionLevel>("NoCompression", CompressionLevel.NoCompression),
                new Tuple<string, CompressionLevel>("Optimal", CompressionLevel.Optimal)
            };

            
            using (FileStream fileStreamToWrite = File.Open("ZipArchive.zip", FileMode.OpenOrCreate))
            {
                using (ZipArchive archive = new ZipArchive(fileStreamToWrite, ZipArchiveMode.Update))
                {
                    foreach (Tuple<string, CompressionLevel> arg in args)
                    {
                        ZipArchiveEntry entry = archive.GetEntry(arg.Item1);
                        if (entry == null)
                            entry = archive.CreateEntry(arg.Item1, arg.Item2);

                        Stream stream = entry.Open();
                        stream.Seek(0, SeekOrigin.End);

                        using (FileStream fileStreamToRead = File.Open(@"C:\Users\Rterg\OneDrive\Programming\VisualStudio\CM3D2\PngPlacement 5.0.1.zip", FileMode.Open))
                            fileStreamToRead.CopyTo(stream);
                    }
                }
            }
            */
        }

        private void Menu_Setting_Preferences(object sender, RoutedEventArgs e)
        {
            SettingWindow settingWindow = new SettingWindow();
            settingWindow.Owner = this;
            settingWindow.ShowDialog();
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            FileItem fileItem = e.NewValue as FileItem;
            fileListView.ItemsSource = fileItem.subFileItems;

            Console.WriteLine(e.OldValue + " : " + e.NewValue);

            fileItem.subFileItems.Add(new FileItem());
        }
    }
}
