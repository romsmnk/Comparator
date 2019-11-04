using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using CompareLibrary;
using System.Windows.Forms;

namespace CompareDirectories
{
    public partial class MainWindow : Window
    {
        Compare compare;
        ObservableCollection<ComparedFile> comaredFirstDir = new ObservableCollection<ComparedFile>();
        ObservableCollection<ComparedFile> comaredSecondDir = new ObservableCollection<ComparedFile>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            firstDirTxtBox.Text = dialog.SelectedPath;
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            secondDirTxtBox.Text = dialog.SelectedPath;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            compare = new Compare(@firstDirTxtBox.Text, @secondDirTxtBox.Text);

            comaredFirstDir.Clear();
            foreach (ComparedFile item in compare.firstDir)
                comaredFirstDir.Add(item);
            this.dataGridFirstDir.ItemsSource = comaredFirstDir;

            comaredSecondDir.Clear();
            foreach (ComparedFile item in compare.secondDir)
                comaredSecondDir.Add(item);
            this.dataGridSecondDir.ItemsSource = comaredSecondDir;
        }
    }
}
