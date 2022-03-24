using notes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class HomePage : Page
    {
        public  HomePage()
        {
            this.InitializeComponent();
        }


        private void butadd_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddPage));

        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
           Show(); 
            
        }

        public async void Show()
        {
            string s;
            StreamReader f = new StreamReader(@"C:\Users\rrd20\AppData\Local\Packages\027aec2b-44be-4a64-aacd-ac5238164b5a_4bqe7bzmx95w0\LocalState\allnotes.txt");
            while (!f.EndOfStream)
            {
               s = f.ReadLine();
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile idfile = await localFolder.GetFileAsync("ids.txt");
                StorageFile allnotes = await localFolder.GetFileAsync("allnotes.txt");
                string notename = s;
                //string[] notename = (string[])await FileIO.ReadLinesAsync(allnotes);               
                int i = Convert.ToInt32(await FileIO.ReadTextAsync(idfile));
                await new Windows.UI.Popups.MessageDialog(notename.ToString()).ShowAsync();

                //for(int g = 0; g <= i; g++) {
                
                StorageFile note = await localFolder.GetFileAsync(notename + ".txt");
                string notetext = await FileIO.ReadTextAsync(note);              
                notetext = notetext.Replace("Заголовок заметки:", "");
                notetext = notetext.Replace("Текст заметки:", "");

                GV1.Items.Add(notetext);//}

            }
            f.Close();

           // StorageFolder localFolder1 = ApplicationData.Current.LocalFolder;
            //StorageFile allnotes1 = await localFolder1.GetFileAsync("allnotes.txt");
           

        }

        private void LB1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
