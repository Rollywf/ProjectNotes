using Microsoft.VisualStudio.PlatformUI;
using notes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Notes
{


    public sealed partial class HomePage : Page
    {
        public ObservableCollection<Note> Notes { get; set; }
        List<IconFavorite> iconf = new List<IconFavorite>();
        List<NotesTemp> NamedColors = new List<NotesTemp>();


        public static readonly DependencyProperty NoteObjectProperty =
       DependencyProperty.Register(
           "PodcastObject",
           typeof(NoteObject),
           typeof(HomePage),
           new PropertyMetadata(null));

        public NoteObject NoteObject
        {
            get { return (NoteObject)GetValue(NoteObjectProperty); }
            set { SetValue(NoteObjectProperty, value); }
        }



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

        public async void LoadingNotesList()
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

                    StorageFile note = await localFolder.GetFileAsync(file + ".txt");
                    List<string> notetext = (await FileIO.ReadLinesAsync(note)).ToList();
                    notetext.Remove("ID заметки: " + idnote);
                    notetext.Remove("Favorite: False");
                    notetext.Remove("Favorite: True");    
                    //notetext.IndexOf("Заголовок заметки: ");
                    notetext.Remove("Текст заметки: ");
                    //notetext.Remove("Заголовок заметки:");
                    //found = notetext.IndexOf(":");     
                    //Console.WriteLine("   {0}", file.Substring(found + 1));
                    // notetext = notetext.Substring(found+1);
                    string notet = await FileIO.ReadTextAsync(note);
                    // notetext = notetext.Replace("Заголовок заметки:", "");
                    notet = notet.Replace("Заголовок заметки: ", "");
                    foud = notet.IndexOf("Текст заметки: ");
                    //Console.WriteLine("   {0}", file.Substring(found + 1));
                    notet = notet.Substring(foud + 14);
                    int ind = 0;
                    bool fav = false;
                    //notetext.Remove("Заголовок заметки: ");
                    notetext[ind] = notetext[ind].Replace("Заголовок заметки: ", "");
                    Notes.Add(new Note {Title= notetext[ind], Text=notet });
                    idnote++;
                    ind++;
                }
            }
            catch { }
        }

        public HomePage()
        {    Notes = new ObservableCollection<Note>();
            LoadingNotesList();
          
            this.InitializeComponent();
           
            //LoadNotes();
        }


        private void butadd_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddPage));
        }


        public async void LoadNotes()
        {//
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile allnotes = await localFolder.GetFileAsync("allnotes.txt");
                List<string> notename = (await FileIO.ReadLinesAsync(allnotes)).ToList();
                int foud = 0;
                int idnote = 1;

                foreach (string file in notename)
                {

                    StorageFile note = await localFolder.GetFileAsync(file + ".txt");
                    // Use reflection to get all the properties of the Colors class.
                    List<string> notetext = (await FileIO.ReadLinesAsync(note)).ToList();
                    // notetext = notetext.Replace("Заголовок заметки:", "");
                    // delettext = notetext.IndexOf("Текст заметки:");
                    //notetext=notetext.Replace("Текст заметки:", "");
                    //await new Windows.UI.Popups.MessageDialog("id заметки "+idnote).ShowAsync();
                    notetext.Remove("ID заметки: " + idnote);
                    try 
                    { 
                    notetext.Remove("Favorite: False");
                    }
                    catch 
                    {
                        notetext.Remove("Favorite: True");
                    }

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
                    NamedColors.Add(new NotesTemp(notetext[ind], notet, idnote, fav));
               
                    idnote++;
                    ind++;
                }
                //GridView abc = new GridView();
                //Grid gridforone = new Grid();
                //gridforone.Height = 100;
                //gridforone.Width = 400;
                //abc.Items.Add(gridforone);

                //TextBlock tb = new TextBlock();
                //tb.Text = NamedColors[0].Title;
                //tb.Tapped += nameNote_Tapped;

                //TextBlock tt = new TextBlock();
                //tt.Text = NamedColors[1].Text;


                //Grid.SetColumn(tb, 0);
                //Grid.SetColumn(tt, 1);

                //Grid.SetRow(tb, 0);
                //Grid.SetRow(tt, 1);

                //Grid.SetColumn(abc, 1);  // Grid.Column = 0
                //Grid.SetRow(abc, 1);  // Grid.Row= 0
                //gridforone.Children.Add(tb);
                //gridforone.Children.Add(tt);
                //G1.Children.Add(abc);

                //await new Windows.UI.Popups.MessageDialog(NamedColors.ToString()).ShowAsync();
                
                //GVNotes.ItemsSource = NamedColors;
                ListOfNotes.ItemsSource = NamedColors;
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
        

        private void Border_Tapped(object sender, TappedRoutedEventArgs e)
        {   //(sender as Border).
            //Frame.Navigate(typeof(OpenedNote),nameNote);
            //Frame.Navigate(typeof(OpenedNote));
        }

        private void downbtn_Click(object sender, RoutedEventArgs e)
        {
            //if (colorsGridView.Visibility == Visibility.Visible)
            //{

            //  //  i = colorsGridView.Height;
            //   // colorsGridView.Height = 0;
            //    colorsGridView.Visibility = Visibility.Collapsed;
            //    upbtn.Visibility = Visibility.Visible;
            //    downbtn.Visibility = Visibility.Collapsed;

            //}
            //else
            //{
            //    //colorsGridView.Height = i;
            //    colorsGridView.Visibility = Visibility.Visible;
            //    upbtn.Visibility = Visibility.Collapsed;
            //    downbtn.Visibility = Visibility.Visible;
            //}
        }

        private async void Favoriting()
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
                    if (favortext.Contains("Favorite: False") == true) 
                    {
                        notetext.Remove("ID заметки: " + result);
                        notetext.Remove("Favorite: False");
                        notetext.IndexOf("Заголовок заметки: ");
                        notetext.Remove("Текст заметки:");
                        string notet = await FileIO.ReadTextAsync(note);
                        notet = notet.Replace("Заголовок заметки:", "");
                        foud = notet.IndexOf("Текст заметки:");
                        notet = notet.Substring(foud + 14);
                        int ind = 0;
                        bool fav = true;
                        notetext[ind] = notetext[ind].Replace("Заголовок заметки: ", "");
                        idnote++;
                        ind++;
                        await FileIO.WriteTextAsync(note, "ID заметки: " + result + "\n");
                        await FileIO.AppendTextAsync(note, "Favorite: " + fav + "\n");
                        await FileIO.AppendTextAsync(note, "Заголовок заметки: " + notetext[ind]);
                        await FileIO.AppendTextAsync(note, "\n");
                        await FileIO.AppendTextAsync(note, "Текст заметки: " + notet);
                        NamedColors.Add(new NotesTemp(notetext[ind],notet,result,fav));
                    }
                    else { }

                }
                ListOfNotes.ItemsSource = NamedColors;
            }
            catch { }
        }

        public void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem root = (sender as MenuFlyoutItem);
            if (root.Text.ToString() == "Favorite") { 
                root.Text = "Unfavorite";
                Favoriting();
                

            }
            else if (root.Text.ToString()== "Unfavorite")
            {
                root.Text = "Favorite";
            }

        }
        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void downbtn1_Click(object sender, RoutedEventArgs e)
        {
            //if (GridViewNotifications.Visibility == Visibility.Visible)
            //{

            //    //  i = colorsGridView.Height;
            //    // colorsGridView.Height = 0;
            //    GridViewNotifications.Visibility = Visibility.Collapsed;
            //    upbtn1.Visibility = Visibility.Visible;
            //    downbtn1.Visibility = Visibility.Collapsed;

            //}
            //else
            //{
            //    //colorsGridView.Height = i;
            //    GridViewNotifications.Visibility = Visibility.Visible;
            //    Show();
            //    upbtn1.Visibility = Visibility.Collapsed;
            //    downbtn1.Visibility = Visibility.Visible;
            //}
        }

        private void butadd_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Favorite_Loaded(object sender, RoutedEventArgs e)
        {  
           MenuFlyoutItem sefdsdf = (sender as MenuFlyoutItem);
        }
        internal int idnote1;
        internal string textNote;
        internal string titleNote;

        private async void nameNote_Tapped(object sender, TappedRoutedEventArgs e)
        {   
           string abc = (sender as TextBlock).Text;
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile allnotes = await localFolder.GetFileAsync("allnotes.txt");
            titleNote = abc;
                List<string> notename = (await FileIO.ReadLinesAsync(allnotes)).ToList();
            int idnote = 1;
            foreach(string file in notename) {
                if (file.Contains(idnote + abc) == true)
                {

                    textNote = idnote + abc;
                    idnote1 = idnote;

                }
                else {

                    if (file.Contains(idnote + abc) ==false)
                    {
                        idnote++;

                    }
                    if (file.Contains(idnote + abc) == true)
                        {
                            textNote = idnote + abc;
                            idnote1 = idnote;

                        }

                }
            }


           
            Frame.Navigate(typeof(OpenedNote), this);
            
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           
        }

        private void textNote_Loaded(object sender, RoutedEventArgs e)
        {   
         
        }

        private void textNote_Loaded_1(object sender, RoutedEventArgs e)
        {
          
        }

        private void Root_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void StackPanel_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            
            // var favoriteCommand = Application.Current.Resources["favoriteCommand"] as ICommand;
            //favoriteCommand.Execute(NotesTemp);
        }

        private async void TextBlockTitle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MenuFlyout menfav = new MenuFlyout();
            MenuFlyoutItem Favorite = new MenuFlyoutItem();
            Favorite.Text = "Favorite";
            Favorite.Click +=MenuFlyoutItem_Click;

            MenuFlyoutItem_Click( Favorite, e);
            string a = (sender as TextBlock).Text;

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
                    if (favortext.Contains(a) == true)
                    {
                        notetext.Remove("ID заметки: " + result);
                        notetext.Remove("Favorite: False");
                        notetext.IndexOf("Заголовок заметки: ");
                        notetext.Remove("Текст заметки:");
                        string notet = await FileIO.ReadTextAsync(note);
                        notet = notet.Replace("Заголовок заметки:", "");
                        foud = notet.IndexOf("Текст заметки:");
                        notet = notet.Substring(foud + 14);
                        int ind = 0;
                        bool fav = true;
                        notetext[ind] = notetext[ind].Replace("Заголовок заметки: ", "");
                        idnote++;
                        ind++;
                        await FileIO.WriteTextAsync(note, "ID заметки: " + result + "\n");
                        await FileIO.AppendTextAsync(note, "Favorite: " + fav + "\n");
                        await FileIO.AppendTextAsync(note, "Заголовок заметки: " + notetext[ind]);
                        await FileIO.AppendTextAsync(note, "\n");
                        await FileIO.AppendTextAsync(note, "Текст заметки: " + notet);
                        NamedColors.Add(new NotesTemp(notetext[ind], notet, result, fav));
                    }
                    else { }

                }
                ListOfNotes.ItemsSource = NamedColors;
            }
            catch { }















        }

        private void Root_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
