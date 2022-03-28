using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        public OpenedNote()
        {
            this.InitializeComponent();
        }

        private void Button_Click_Listener(object sender, EventArgs e)
        {
            

            
                   
                    

             
        }

        private void Button_Click_Listener(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            string sw = btn.Content.ToString();
            if (btn.Content.ToString() == "Save")
            {

            }
            else if (btn.Content.ToString() == "Cancel")
            {
                Frame.Navigate(typeof(HomePage));
            }
            else if (btn.Content.ToString() == "Delete")
            {

            }
        }
    }
}
