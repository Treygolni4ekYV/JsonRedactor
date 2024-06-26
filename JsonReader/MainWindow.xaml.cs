﻿using JsonRedactor;
using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JsonReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenDir_click(object sender, RoutedEventArgs e)
        {
            var folderDialog = new OpenFolderDialog();
            
            if (folderDialog.ShowDialog() == true)
            {
                JsonRedactorWindow redactor = new();
                redactor.Owner = this;
                redactor.Closed += (s, e) => this.Close();
                redactor.Show();
                this.Hide();
            }
        }
    }
}