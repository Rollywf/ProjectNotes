using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Notes
{

    class Notes
    {
        public Notes(string NoteZ, string NoteT)
        {
            Name = NoteZ;
            Not1 = NoteT;
        }

        public string Name { get; set; }

        public string Not1 { get; set; }

    }
}
