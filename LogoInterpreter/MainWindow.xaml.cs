﻿using LogoInterpreter.Interpreter;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

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

            MainCanvas.Children.Clear();
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Clear canvas and console
                MainCanvas.Children.Clear();

                Lexer lexer = new Lexer(new StringSource(CodeEditorTextBox.Text));
                Parser parser = new Parser(lexer);

                Program program = parser.Parse();

                ExecutorVisitor executeProgram = new ExecutorVisitor(MainCanvas);
                executeProgram.Visit(program);
            }
            catch (LexerException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (ParserException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (ExecutorException ex)
            {
                MessageBox.Show(ex.Message);
            }
            /*catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }
    }    
}
