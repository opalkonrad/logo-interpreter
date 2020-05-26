using System.Windows;
using System.IO;
using Microsoft.Win32;
using LogoInterpreter.Interpreter;
using System.Windows.Controls;
using System;
using System.Windows.Media;

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
            try
            {
                // Clear canvas
                MainCanvas.Children.Clear();

                Lexer lexer = new Lexer(new StringSource(CodeEditorTextBox.Text));
                Parser parser = new Parser(lexer);

                Program program = parser.Parse();

                ExecutorVisitor executeProgram = new ExecutorVisitor(MainCanvas);
                executeProgram.Visit(program);
            }
            catch (LexerException ex)
            {
                ConsoleTextBox.Text += ex.Message;
            }
            catch (ParserException ex)
            {
                ConsoleTextBox.Text += ex.Message;
            }
            catch (ExecutorException ex)
            {
                ConsoleTextBox.Text += ex.Message;
            }
            catch (Exception ex)
            {
                ConsoleTextBox.Text += ex.Message;
            }
        }
    }    
}
