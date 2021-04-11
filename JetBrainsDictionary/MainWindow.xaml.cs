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
            string exp = inputBox.Text;

            if (string.IsNullOrEmpty(exp))
            {
                MessageBox.Show("Введите что-нибудь в поле ввода");
                return;
            }

            Search = new FileDictionarySearch(Path, Checker);
            btn.IsEnabled = false;
            status.Visibility = Visibility.Hidden;

            IEnumerable<string> result = null;

            try
            {
                result = Search.Find(exp);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Не найден файл. Убедитесь, что вы выбрали существующий");
            }

            int total = await result.CountAsync();
            string[] output = await result.Take(2500).ToArrayAsync();
            outputBox.Text = string.Join(Environment.NewLine, output);
            status.Content = $"Показано {output.Length} результатов из {total}";

            status.Visibility = Visibility.Visible;
            btn.IsEnabled = true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            bool? result = dialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                Path = dialog.FileName;
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
