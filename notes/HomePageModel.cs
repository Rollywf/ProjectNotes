using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Notes
{
     class HomePageModel
    {
        public ObservableCollection<Note> Notes { get; set; }
        List<IconFavorite> iconf = new List<IconFavorite>();
        List<NotesTemp> NamedColors = new List<NotesTemp>();

        public async void LoadingNotesList(ObservableCollection<Note> Notes)
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
                    Notes.Add(new Note { Title = notetext[ind], Text = notet });
                    idnote++;
                    ind++;
                }
            }
            catch { }
        }

        public HomePageModel()
        {
            Notes = new ObservableCollection<Note>();
            LoadingNotesList(Notes);

            //LoadNotes();
        }

        public DelegateCommand<Note> RemoveCommand => new DelegateCommand<Note>((note) =>
        {
            Notes.Remove(note);
        }, (note) => note != null);


    }

}

