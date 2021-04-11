using JetBrainsDictionary.Extensions;
using JetBrainsDictionary.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace JetBrainsDictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string Path { get; set; } = "../../../words.txt";
        IMatchChecker Checker { get; set; } = new StrictMatchChecker();
        IDictionarySearch Search { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string exp = inputBox.Text;                 //Выражение для поиска

            if (string.IsNullOrEmpty(exp))              //Не пустое
            {
                MessageBox.Show("Введите что-нибудь в поле ввода");
                return;
            }

            Search = new FileDictionarySearch(Path, Checker);               //Создаем поиск в файле с заданными параметрами
            btn.IsEnabled = false;                                          //Отключаем кнопку поиска
            status.Visibility = Visibility.Hidden;                          //Скрываем статусное сообщение

            IEnumerable<string> result = null;                              //Результаты

            try
            {
                result = Search.Find(exp);                                  //Попытка поиска
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Не найден файл. Убедитесь, что вы выбрали существующий");
                btn.IsEnabled = true;
                return;
            }

            int total = await result.CountAsync();                                  //Всего результатов
            string[] output = await result.Take(5000).ToArrayAsync();               //Взять не больше 5000
            outputBox.Text = string.Join(Environment.NewLine, output);              //Положить каждый по одному на строку
            status.Content = $"Показано {output.Length} результатов из {total}";    //Вывести количество найденных и выведенных на экран

            status.Visibility = Visibility.Visible;                                 //Статус видимый
            btn.IsEnabled = true;                                                   //Кнопку снова можно нажать
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();                           //Диалог выбора файла
            bool? result = dialog.ShowDialog();

            if (result.HasValue && result.Value)                                    //Если файл выбран
            {
                Path = dialog.FileName;                                             //Прописываем путь
            }
        }

        private void continuousSearch_Checked(object sender, RoutedEventArgs e)
        {
            Checker = new ContinuousMatchChecker();
        }

        private void continuousSearch_Unchecked(object sender, RoutedEventArgs e)
        {
            Checker = new StrictMatchChecker();
        }
    }
}
