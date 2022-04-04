using notes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Notes
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class NotifPage : Page
    {
        List<Notes> NamedColors = new List<Notes>();
        
        public NotifPage()
        {
            this.InitializeComponent();
            Abc();
    }


        

        private void butadd_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddPage));
        }

        public async void Abc() {
             try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile allnotif = await localFolder.GetFileAsync("allnotif.txt");
                List<string> notename = (await FileIO.ReadLinesAsync(allnotif)).ToList();
                int foud = 0;
           
                foreach (string file in notename)
                {
                    StorageFile note = await localFolder.GetFileAsync(file + ".txt");
                    List<string> notetext = (await FileIO.ReadLinesAsync(note)).ToList();

                    notetext.IndexOf("Заголовок Уведомления: ");
                    notetext.Remove("Текст Уведомления: ");
                    string notet = await FileIO.ReadTextAsync(note);
                    notet = notet.Replace("Заголовок Уведомления:", "");
                    foud = notet.IndexOf("Текст Уведомления: ");
                    notet = notet.Substring(foud + 18);

                    int ind = 0;
                    notetext[ind] = notetext[ind].Replace("Заголовок Уведомления: ", "");
                    NamedColors.Add(new Notes(notetext[ind], notet, ind));
                    ind++;


                }

                colorsGridView.ItemsSource = NamedColors;
            }
            catch { }
        }


        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //Show(); 

        }

        private void colorsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(OpenedNote));
        }

        private void colorsGridView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(OpenedNote));
        }

        private void Border_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }


        private void Grid_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void Rectangle_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }


    }
}
