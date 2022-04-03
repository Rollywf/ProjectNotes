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
    public sealed partial class FavoritesPage : Page
    {
        List<Notes> listNotes = new List<Notes>();
        public FavoritesPage()
        {
            this.InitializeComponent();
            Abc();
        }

        public async void Abc()
        {
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile allnotes = await localFolder.GetFileAsync("allnotes.txt");
                List<string> notename = (await FileIO.ReadLinesAsync(allnotes)).ToList();
                int foud = 0;

                foreach (string file in notename)
                {
                    StorageFile note = await localFolder.GetFileAsync(file + ".txt");
                    // Use reflection to get all the properties of the Colors class.
                    List<string> notetext = (await FileIO.ReadLinesAsync(note)).ToList();
                    // notetext = notetext.Replace("Заголовок заметки:", "");
                    // delettext = notetext.IndexOf("Текст заметки:");
                    //notetext=notetext.Replace("Текст заметки:", "");
                    notetext.IndexOf("Заголовок заметки: ");
                    notetext.Remove("Текст заметки:");
                    //notetext.Remove("Заголовок заметки:");
                    //found = notetext.IndexOf(":");     
                    //Console.WriteLine("   {0}", file.Substring(found + 1));
                    // notetext = notetext.Substring(found+1);
                    
                    string notet = await FileIO.ReadTextAsync(note);
                    // notetext = notetext.Replace("Заголовок заметки:", "");
                    notet = notet.Replace("Заголовок заметки:", "");
                    foud = notet.IndexOf("Текст заметки:");
                    //Console.WriteLine("   {0}", file.Substring(found + 1));
                    notet = notet.Substring(foud + 14);

                    int ind = 0;
                    notetext[ind] = notetext[ind].Replace("Заголовок заметки: ", "");
                    listNotes.Add(new Notes(notetext[ind], notet,ind));
                    ind++;


                }
                colorsGridView.ItemsSource = listNotes;
            }
            catch { }
        }


        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Show();

        }

        public async void Show()
        {
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile idfile = await localFolder.GetFileAsync("ids.txt");
                StorageFile allnotes = await localFolder.GetFileAsync("allnotes.txt");
                List<string> notename = (await FileIO.ReadLinesAsync(allnotes)).ToList(); ;
                int i = Convert.ToInt32(await FileIO.ReadTextAsync(idfile));
                //await new Windows.UI.Popups.MessageDialog(notename[4]).ShowAsync();
                foreach (string file in notename)
                {
                    StorageFile note = await localFolder.GetFileAsync(file + ".txt");
                    string notetext = await FileIO.ReadTextAsync(note);
                    notetext = notetext.Replace("Заголовок заметки:", "");
                    notetext = notetext.Replace("Текст заметки:", "");
                    Button test = new Button();
                    test.Content = notetext;

                    //test.Template = btTemplate;
                    //tb1.Text = notetext;
                    double sizeHeight = 60;
                    double sizeWidth = 150;
                    test.Height = sizeHeight;
                    test.Width = sizeWidth;
                    //colorsListView.Items.Add(test);


                }
                //for(int g = 0; g <= i-1; g++) {

                //StorageFile note = await localFolder.GetFileAsync(notename[g] + ".txt");
                //string notetext = await FileIO.ReadTextAsync(note);              
                //notetext = notetext.Replace("Заголовок заметки:", "");
                //notetext = notetext.Replace("Текст заметки:", "");

                //GV1.Items.Add(notetext);}
            }
            catch
            {

            }
        }

        private void colorsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(OpenedNote));
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
        public double i;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (colorsGridView.Visibility == Visibility.Visible)
            {

                i = colorsGridView.Height;
                colorsGridView.Height = 0;
                colorsGridView.Visibility = Visibility.Collapsed;


            }
            else
            {
                colorsGridView.Height = i;
                colorsGridView.Visibility = Visibility.Visible;
            }
        }

        private void Border_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(OpenedNote));
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
        }

        private void colorsGridView_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
