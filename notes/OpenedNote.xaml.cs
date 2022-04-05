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
    public sealed partial class OpenedNote : Page
    {
        private string titleNote;
        private int idnote;
        private string namefile;

        public OpenedNote()
        {
            this.InitializeComponent();
        }

        private void Button_Click_Listener(object sender, EventArgs e)
        {
            

            
                   
                    

             
        }

        private async void Saving()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile allnotes = await localFolder.GetFileAsync("allnotes.txt");
            StorageFile idfile = await localFolder.GetFileAsync("ids.txt");
            StorageFile file = await localFolder.GetFileAsync(namefile);

            string textTitle = TitleNote.Text;
            string textText = TextNote.Text;
            // получаем локальную папку 

            // создаем файл hello.txt
            string oldabc =idnote+titleNote;


            // await FileIO.AppendTextAsync(allnotes, idnote + textTitle + "\n");
            string abc = idnote + textTitle;
            string readline = await FileIO.ReadTextAsync(allnotes);
            

            readline = readline.Replace(oldabc, abc);
               
            await FileIO.WriteTextAsync(allnotes, readline);


            await FileIO.WriteTextAsync(file, "ID заметки: " + idnote + "\n");
            await FileIO.AppendTextAsync(file, "Заголовок заметки: " + textTitle);
            await FileIO.AppendTextAsync(file, "\n");
            await FileIO.AppendTextAsync(file, "Текст заметки: " + textText);
            try { await file.RenameAsync(idnote + textTitle+".txt"); }
            catch { }
           

            //await FileIO.WriteTextAsync(idfile, i.ToString());
        }

        private void Button_Click_Listener(object sender, RoutedEventArgs e)
        {
            
            int i;
            var btn = sender as Button;
            string sw = btn.Content.ToString();
            if (btn.Content.ToString() == "Save")
            {
                Saving();
                HomePage hp = new HomePage();
                
                //Frame.Navigate(typeof(HomePage));
                Frame.GoBack();
                //Frame.GoBack();
                
            }
            else if (btn.Content.ToString() == "Cancel")
            {   
                Frame.GoBack();
                //Frame.Navigate(typeof(HomePage));
            }
            else if (btn.Content.ToString() == "Delete")
            {
                DeleteNote();
                Frame.Navigate(typeof(HomePage));
            }
        }
        string title;
        string text;

        private async void DeleteNote() 
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile allnotes = await localFolder.GetFileAsync("allnotes.txt");
            StorageFile idfile = await localFolder.GetFileAsync("ids.txt");
            StorageFile file = await localFolder.GetFileAsync(namefile);

            string textTitle = TitleNote.Text;
            string textText = TextNote.Text;

            string oldabc = idnote + titleNote+"\n";

            string abc = idnote + textTitle;
            string readline = await FileIO.ReadTextAsync(allnotes);

            readline = readline.Replace(oldabc, "");

            await FileIO.WriteTextAsync(allnotes, readline);
            await file.DeleteAsync();
        }

        public async void Test()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            int foud = 0;
            int idote = 0;
            int ftitle = 0;
            //await new Windows.UI.Popups.MessageDialog(namefile).ShowAsync();
            int idnote = 1;
            
            StorageFile note = await localFolder.GetFileAsync(namefile);
            // Use reflection to get all the properties of the Colors class.
            List<string> notetext = (await FileIO.ReadLinesAsync(note)).ToList();

            notetext.IndexOf("Заголовок заметки: ");
            notetext.Remove("Текст заметки: ");

           
             string notet = await FileIO.ReadTextAsync(note);
            string title = await FileIO.ReadTextAsync(note);

           

            notet = notet.Replace("Заголовок заметки: ", "");
            notet = notet.Replace("\n", "");
            foud = notet.IndexOf("Текст заметки: ");
            notet = notet.Substring(foud + 14);

           
            title = title.Replace("Текст заметки:" + notet, "");
            title = title.Replace("\n", "");
            ftitle = title.IndexOf("Заголовок заметки: ");
            title = title.Substring(ftitle + 19);
           
                
            int ind = 0;
            notetext[ind] = notetext[ind].Replace("Заголовок заметки: ", "");
            TitleNote.Text = title;
            TextNote.Text = notet;
            idnote++;
            ind++;
                
            //}

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
                
            HomePage peredacha = (HomePage)e.Parameter;
            
            //idnote = peredacha.idnote;
            //TextNote.Text = peredacha.textNote;
            //TitleNote.Text = peredacha.titleNote;
           
            namefile = peredacha.textNote+".txt";
            titleNote = peredacha.titleNote;
            idnote = peredacha.idnote1;
            Test();


        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
