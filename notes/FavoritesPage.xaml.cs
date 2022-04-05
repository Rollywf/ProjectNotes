using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
        List<NotesTemp> listNotes = new List<NotesTemp>();

         public static readonly DependencyProperty NotesProperty =
       DependencyProperty.Register(
           "NotesTemp",
           typeof(NotesTemp),
           typeof(HomePage),
           new PropertyMetadata(null));

        public NotesTemp NotesTemp
        {
            get { return (NotesTemp)GetValue(NotesProperty); }
            set { SetValue(NotesProperty, value); }
        }
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
                        int idnote = 1;

                foreach (string file in notename)
                {
                    int result;
                    string resultString = string.Join(string.Empty, Regex.Matches(file, @"\d+").OfType<Match>().Select(m => m.Value));
                    int.TryParse(resultString, out result);
                    //await new Windows.UI.Popups.MessageDialog(result.ToString()).ShowAsync();
                    StorageFile note = await localFolder.GetFileAsync(file + ".txt");
                    // Use reflection to get all the properties of the Colors class.
                    List<string> notetext = (await FileIO.ReadLinesAsync(note)).ToList();
                    // notetext = notetext.Replace("Заголовок заметки:", "");
                    // delettext = notetext.IndexOf("Текст заметки:");
                    //notetext=notetext.Replace("Текст заметки:", "");
                    string favortext = await FileIO.ReadTextAsync(note);
                    if (favortext.Contains("Favorite: True") == true)
                    {
                        notetext.Remove("ID заметки: " + result);
                        notetext.Remove("Favorite: True");
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
                        bool fav = false;
                        notetext[ind] = notetext[ind].Replace("Заголовок заметки: ", "");
                        listNotes.Add(new NotesTemp(notetext[ind], notet, idnote, fav));

                        idnote++;
                        ind++; 
                        
                    }
                    else {}
                   
                    }ListOfNotes1.ItemsSource = listNotes;
                  }
                    catch { }




           
        }


        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
          

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
            //if (colorsGridView.Visibility == Visibility.Visible)
            //{

            //    i = colorsGridView.Height;
            //    colorsGridView.Height = 0;
            //    colorsGridView.Visibility = Visibility.Collapsed;


            //}
            //else
            //{
            //    colorsGridView.Height = i;
            //    colorsGridView.Visibility = Visibility.Visible;
            //}
        }

        private void Border_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(OpenedNote));
        }
        private async void Unfavoriting() 
        {
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile allnotes = await localFolder.GetFileAsync("allnotes.txt");
                List<string> notename = (await FileIO.ReadLinesAsync(allnotes)).ToList();
                int foud = 0;
                int idnote = 1;

                foreach (string file in notename)
                {
                    int result;
                    string resultString = string.Join(string.Empty, Regex.Matches(file, @"\d+").OfType<Match>().Select(m => m.Value));
                    int.TryParse(resultString, out result);
                    StorageFile note = await localFolder.GetFileAsync(file + ".txt");
                    List<string> notetext = (await FileIO.ReadLinesAsync(note)).ToList();

                    string favortext = await FileIO.ReadTextAsync(note);
                    if (favortext.Contains("Favorite: True") == true)
                    {
                        notetext.Remove("ID заметки: " + result);
                        notetext.Remove("Favorite: True");
                        notetext.IndexOf("Заголовок заметки: ");
                        notetext.Remove("Текст заметки:");
                       
                        string notet = await FileIO.ReadTextAsync(note);
                        notet = notet.Replace("Заголовок заметки:", "");
                        foud = notet.IndexOf("Текст заметки:");
                        notet = notet.Substring(foud + 14);
                        int ind = 0;
                        bool fav = false;
                        notetext[ind] = notetext[ind].Replace("Заголовок заметки: ", "");
                        idnote++;
                        ind++;
                        await FileIO.WriteTextAsync(note, "ID заметки: " + result + "\n");
                        await FileIO.AppendTextAsync(note, "Favorite: " + fav + "\n");
                        await FileIO.AppendTextAsync(note, "Заголовок заметки: " + notetext[ind]);
                        await FileIO.AppendTextAsync(note, "\n");
                        await FileIO.AppendTextAsync(note, "Текст заметки: " + notet);
                    }
                    else { }

                }
                ListOfNotes1.ItemsSource = listNotes;
            }
            catch { }
        }
        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem root = (sender as MenuFlyoutItem);
            if (root.Text.ToString() == "Unfavorite")
            {
                Unfavoriting();
   
            }
        }

        private void colorsGridView_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void StackPanel_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            MenuFlyout menfav = new MenuFlyout();
            MenuFlyoutItem Favorite = new MenuFlyoutItem();
            Favorite.Text = "Unfavorite";
            Favorite.Click += MenuFlyoutItem_Click;
            // var favoriteCommand = Application.Current.Resources["favoriteCommand"] as ICommand;
            //favoriteCommand.Execute(NotesTemp);
        }

        private void Favorite_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
