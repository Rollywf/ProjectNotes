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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Xamarin.Forms.Platform.UWP;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Notes
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        List<IconFavorite> iconf = new List<IconFavorite>();
        List<Notes> NamedColors = new List<Notes>();

        public HomePage()
        {
            this.InitializeComponent();
            LoadNotes();
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
                    NamedColors.Add(new Notes(notetext[ind], notet, idnote));
                    
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



                 colorsGridView.ItemsSource = NamedColors;
            }
            catch { }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Show();

        }





        public async void Show()
        {
            try {
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
        

        private void Border_Tapped(object sender, TappedRoutedEventArgs e)
        {   //(sender as Border).
            //Frame.Navigate(typeof(OpenedNote),nameNote);
            //Frame.Navigate(typeof(OpenedNote));
        }

        private void downbtn_Click(object sender, RoutedEventArgs e)
        {
            if (colorsGridView.Visibility == Visibility.Visible)
            {

              //  i = colorsGridView.Height;
               // colorsGridView.Height = 0;
                colorsGridView.Visibility = Visibility.Collapsed;
                upbtn.Visibility = Visibility.Visible;
                downbtn.Visibility = Visibility.Collapsed;

            }
            else
            {
                //colorsGridView.Height = i;
                colorsGridView.Visibility = Visibility.Visible;
                upbtn.Visibility = Visibility.Collapsed;
                downbtn.Visibility = Visibility.Visible;
            }
        }

        public void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem root = (sender as MenuFlyoutItem);
            // textForF = "Unfavorite";
            if (root.Text.ToString() == "Favorite") { 
                root.Text = "Unfavorite";
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
            //(sender as Button).;
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
                // await new Windows.UI.Popups.MessageDialog(notename[4]).ShowAsync();
                if (file.Contains(idnote + abc) == true)
                {

                    textNote = idnote + abc;
                    idnote1 = idnote;

                }
                else {
                    //while (file.Contains(idnote + abc) == false)
                    //{
                    if (file.Contains(idnote + abc) ==false)
                    {
                        idnote++;

                    }
                    if (file.Contains(idnote + abc) == true)
                        {
                            textNote = idnote + abc;
                            idnote1 = idnote;

                        }
                            //idnote++;
                       
                   // }
                }
            }


            //    int idnote = 0;
            //titleNote=NamedColors[0].Title;
            //textNote = NamedColors[idnote].Text;
            //idnote++;




            //StorageFile idfile = await localFolder.GetFileAsync("ids.txt");
            //StorageFile files = await localFolder.GetFileAsync("");

            //int id = 1;
            //for (int i = 0; i < id;id++) 
            //{
            // }  

            //titleNote = NamedColors[0].Title;
            // textNote = id + titleNote +".txt";
            //await new Windows.UI.Popups.MessageDialog(textNote).ShowAsync();
            //idnote = NamedColors[1-idnote].id_note;
            //Frame.Navigate(typeof(OpenedNote), titleNote);
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
    }
}
