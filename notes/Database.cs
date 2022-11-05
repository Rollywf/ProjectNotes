using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    class Database
    {
        public static IEnumerable<Note> GetNotes()
        {
            return new Note[]
            {
                new Note
                {
                    Title="АБСВ",
                    Text = "abc"
                },
                new Note
                {
                    Title="BBBММ",
                    Text="bbb"
                },
                new Note
                {
                    Title="CCCЫЫ",
                    Text="ccc"
                }
            };
        }
    }
}
