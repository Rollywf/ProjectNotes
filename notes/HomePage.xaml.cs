using notes;
using System;
using System.Collections;
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
        private DependencyProperty dp;

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
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile idfile = await localFolder.GetFileAsync("ids.txt");
                StorageFile allnotes = await localFolder.GetFileAsync("allnotes.txt");
                List <string> notename = (await FileIO.ReadLinesAsync(allnotes)).ToList(); ;             
                int i = Convert.ToInt32(await FileIO.ReadTextAsync(idfile));
                //await new Windows.UI.Popups.MessageDialog(notename[4]).ShowAsync();
                foreach(string file in notename)
                {
                StorageFile note = await localFolder.GetFileAsync(file + ".txt");
                string notetext = await FileIO.ReadTextAsync(note);
                notetext = notetext.Replace("Заголовок заметки:", "");
                notetext = notetext.Replace("Текст заметки:", "");
                Button test = new Button();
                test.Content = notetext;
                double sizeHeight = 60;
                double sizeWidth = 150;
                test.Height = sizeHeight;
                test.Width = sizeWidth;
                GV1.Items.Add(test);


                }
                //for(int g = 0; g <= i-1; g++) {
                
                //StorageFile note = await localFolder.GetFileAsync(notename[g] + ".txt");
                //string notetext = await FileIO.ReadTextAsync(note);              
                //notetext = notetext.Replace("Заголовок заметки:", "");
                //notetext = notetext.Replace("Текст заметки:", "");

                //GV1.Items.Add(notetext);}
        }

        private void LB1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
