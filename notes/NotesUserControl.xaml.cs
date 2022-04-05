using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
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

// Документацию по шаблону элемента "Пользовательский элемент управления" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234236

namespace Notes
{
    public sealed partial class NotesUserControl : UserControl
    {
       
    public static readonly DependencyProperty NotesTempProperty =
        DependencyProperty.Register(
            "NotesTemp",
            typeof(NotesTemp),
            typeof(NotesUserControl),
            new PropertyMetadata(null));

        public NotesTemp NotesTemp
        {   
            get { return (NotesTemp)GetValue(NotesTempProperty); }
            set { SetValue(NotesTempProperty, value); }
        }

        public NotesUserControl()
        {
            this.InitializeComponent(); 

            // TODO: We will add event handlers here.
        }
        
    }
}

