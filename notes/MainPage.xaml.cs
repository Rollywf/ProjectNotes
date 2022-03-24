using Notes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace notes
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
   //#FF1F1F1F
            Color BackGroundFTB = 0xFF1F1F1F.ToColor();
            var t = ApplicationView.GetForCurrentView().TitleBar;
            t.BackgroundColor = BackGroundFTB;
            t.ForegroundColor = Colors.White;
            t.ButtonBackgroundColor = BackGroundFTB;
            t.ButtonForegroundColor = Colors.White;

            ABC();

        }

        public async void ABC()
        {
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                // создаем файл hello.txt
                StorageFile storageFile = await localFolder.GetFileAsync("ids.txt");
                StorageFile notes = await localFolder.GetFileAsync("allnotes.txt");

               

            }
            catch
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile idfile = await localFolder.CreateFileAsync("ids.txt",CreationCollisionOption.ReplaceExisting);
                StorageFile notes = await localFolder.CreateFileAsync("allnotes.txt", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(idfile, "0");
            }
        }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(HomePage));
        }
     
        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            { 
                
            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;

                switch (item.Tag.ToString())
                {
                    case "Home":
                        ContentFrame.Navigate(typeof(HomePage));
                        
                        break;
                    case "Favorite":
                        ContentFrame.Navigate(typeof(FavoritesPage));
                        break;
                   
                    
                
                }

            }
        }
    }

 

    static class ExtensionMethods
    {
        public static Color ToColor(this uint argb)
        {
            return Color.FromArgb((byte)((argb & -16777216) >> 0x18),
                                  (byte)((argb & 0xff0000) >> 0x10),
                                  (byte)((argb & 0xff00) >> 8),
                                  (byte)(argb & 0xff));
        }
    }
}
