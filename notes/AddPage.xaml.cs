using Microsoft.Toolkit.Uwp.Notifications;
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
            i = Convert.ToInt32(await FileIO.ReadTextAsync(idfile));
            i++;
            string textZ = TBZ.Text;
            string text = TBT.Text;
            // получаем локальную папку 

            // создаем файл hello.txt
            StorageFile helloFile = await localFolder.CreateFileAsync(i + textZ + ".txt",
                                                CreationCollisionOption.ReplaceExisting);

            await FileIO.AppendTextAsync(allnotes, i + textZ + "\n");
            await FileIO.AppendTextAsync(helloFile, "ID заметки: " + i +"\n");
            await FileIO.AppendTextAsync(helloFile, "Заголовок заметки: " + textZ);
            await FileIO.AppendTextAsync(helloFile, "\n");
            await FileIO.AppendTextAsync(helloFile, "Текст заметки: " + text);

            await FileIO.WriteTextAsync(idfile, i.ToString());

            //await new Windows.UI.Popups.MessageDialog("Файл создан и сохранен").ShowAsync();


        }

        
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {  int hours = Convert.ToInt32(TP.Time.Hours);
          int  minuts = Convert.ToInt32(TP.Time.Minutes);
            int years = Convert.ToInt32(CDP.Date.Value.Year);
            int months = Convert.ToInt32(CDP.Date.Value.Month);
            int days = Convert.ToInt32(CDP.Date.Value.Day);
            DateTime datenotif = new DateTime(year: years, month: months, day: days, hour: hours, minute: minuts, second:0);
            //string a = hour.SelectedValue.ToString();
            string textZ = TBZ.Text;
            string text = TBT.Text;
            new ToastContentBuilder()
            .AddArgument("action", "viewItemsDueToday")
            .AddText(textZ)
            .AddText(text)
            .AddText(DateTime.Now.ToString())
            .Schedule(datenotif, toast =>
            {
                toast.Tag = "1";
            });

            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile idfile = await localFolder.GetFileAsync("idsnotif.txt");
            StorageFile allnotes = await localFolder.GetFileAsync("allnotif.txt");
            i = Convert.ToInt32(await FileIO.ReadTextAsync(idfile));
            i++;
            // получаем локальную папку 

            // создаем файл hello.txt
            StorageFile helloFile = await localFolder.CreateFileAsync(i + textZ + ".txt",
                                                CreationCollisionOption.ReplaceExisting);

            await FileIO.AppendTextAsync(allnotes, i + textZ + "\n");
            await FileIO.AppendTextAsync(helloFile, "Заголовок Уведомления: " + textZ);
            await FileIO.AppendTextAsync(helloFile, "\n");
            await FileIO.AppendTextAsync(helloFile, "Текст Уведомления: " + text);

            await FileIO.WriteTextAsync(idfile, i.ToString());

            await new Windows.UI.Popups.MessageDialog("Уведомление создано и сохранено").ShowAsync();

        }

        private void TS_Toggled(object sender, RoutedEventArgs e)
        {
            if (TS.IsOn == true)
            {
                CDP.Visibility = Visibility.Visible;
               // TimePickerMy.Visibility = Visibility.Visible;
                TP.Visibility = Visibility.Visible;
                ButtonNotif.Visibility = Visibility.Visible;
                ButZapis.Visibility = Visibility.Collapsed;
                TBlockZ.Text = "Заголовок Уведомления";
                TBlockT.Text = "Текст Уведомления";
            }
            else
            {
                CDP.Visibility = Visibility.Collapsed;
                //TimePickerMy.Visibility = Visibility.Collapsed;
                TP.Visibility = Visibility.Collapsed;
                ButtonNotif.Visibility = Visibility.Collapsed;
                ButZapis.Visibility = Visibility.Visible;

                TBlockZ.Text = "Заголовок Заметки";
                TBlockT.Text = "Текст Заметки";
            }
        }

        private void hour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

    }
}

