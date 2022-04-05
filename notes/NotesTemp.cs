using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Notes
{

   public class NotesTemp : INotifyPropertyChanged
    {      private bool _isFavorite = false;
            public string Title { get; set; }
            public string Text { get; set; }
            
            public int id_note { get; set; }
        
        public bool IsFavorite
        {
            get
            {
                return _isFavorite;
            }
            set
            {
                _isFavorite = value;
                OnPropertyChanged("IsFavorite");
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(String property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public NotesTemp(string title, string text,int id,bool favorit)
            {
                Title = title;
                Text = text;
                id_note = id;
            IsFavorite = favorit;
            }
        
    }
}
