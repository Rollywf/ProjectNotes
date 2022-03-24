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
    public sealed partial class AddPage : Page
    {
        public AddPage()
        {
            this.InitializeComponent();
        }
        int i;
        private async void Button_Click(object sender, RoutedEventArgs e)
        {   
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile idfile = await localFolder.GetFileAsync("ids.txt");
            StorageFile allnotes = await localFolder.GetFileAsync("allnotes.txt");
            i =Convert.ToInt32(await FileIO.ReadTextAsync(idfile));
            i++;
            string textZ = TBZ.Text;
            string text = TBT.Text;
            // получаем локальную папку 
           
            // создаем файл hello.txt
            StorageFile helloFile = await localFolder.CreateFileAsync(i+textZ+".txt",
                                                CreationCollisionOption.ReplaceExisting);

            await FileIO.AppendTextAsync(allnotes,i+textZ+"\n");
            await FileIO.AppendTextAsync(helloFile,"Заголовок заметки:"+textZ);
            await FileIO.AppendTextAsync(helloFile, "\n");
            await FileIO.AppendTextAsync(helloFile, "" + text);
            
            await FileIO.WriteTextAsync(idfile, i.ToString());

            await new Windows.UI.Popups.MessageDialog("Файл создан и сохранен").ShowAsync();
        
        
        }

        private void Button_Click1(object sender,RoutedEventArgs e)
        {
            string textZ = TBZ.Text;
            string text = TBT.Text;
            StreamReader sri = new StreamReader("ids.txt");
            i = Convert.ToInt32(sri.ReadToEnd());
            sri.Close();
            i++;
            StreamWriter swid = new StreamWriter("ids.txt");
            StreamWriter sw = new StreamWriter(i+textZ+".txt");
            StreamWriter swan = new StreamWriter("allnotes.txt");

            swan.WriteLine(i + textZ);
            sw.Write("Заголовок заметки:" + textZ+"\n"+"Текст заметки:"+text);
            swid.Write(i);

            swid.Close();
            swan.Close();
            sw.Close();
            new Windows.UI.Popups.MessageDialog("Заметка Создана").ShowAsync();
        }
    }
}
