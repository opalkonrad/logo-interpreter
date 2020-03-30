﻿using System;
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
using System.IO;
using Microsoft.Win32;

namespace LogoInterpreter
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

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            CodeEditorTextBox.Text = "";
            ConsoleTextBox.Text = "";
            MainCanvas.Children.Clear();
        }

        private void File_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            // Filter - only *.txt files
            openFile.Filter = "Text|*.txt";

            if (openFile.ShowDialog() == true)
            {
                CodeEditorTextBox.Text = File.ReadAllText(openFile.FileName);
            }
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            Line line = new Line();
            line.Stroke = System.Windows.Media.Brushes.Black;

            line.X1 = 0;
            line.Y1 = 0;

            line.X2 = 100;
            line.Y2 = 100;

            MainCanvas.Children.Add(line);
        }
    }
}
