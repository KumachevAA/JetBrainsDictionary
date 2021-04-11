using JetBrainsDictionary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JetBrainsDictionary.Extensions;
using System.IO;
using Microsoft.Win32;

namespace JetBrainsDictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string Path { get; set; }

        IDictionarySearch Search = new FileDictionarySearch("../../../words.txt");

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
            string[] output = await result.Take(5000).ToArrayAsync();
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
                Search = new FileDictionarySearch(dialog.FileName);
            }
        }
    }
}
