using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class NoteObject : INotifyPropertyChanged
    {
        // The title of the podcast
        public String Title { get; set; }

        // The podcast's description
        public String Text { get; set; }

        // Describes if the user has set this podcast as a favorite
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
        private bool _isFavorite = false;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(String property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
