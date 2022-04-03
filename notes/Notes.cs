using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Notes
{

   public class Notes
    {      
            public string Title { get; set; }
            public string Text { get; set; }
            
            public int id_note { get; set; }


            public Notes(string title, string text,int id)
            {
                Title = title;
                Text = text;
                id_note = id;
            }
        
    }
}
