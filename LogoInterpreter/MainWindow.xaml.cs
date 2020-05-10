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
using System.IO;
using Microsoft.Win32;
using Microsoft.VisualBasic.CompilerServices;
using LogoInterpreter.Interpreter;

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
            Lexer lexer = new Lexer(new StringSource(CodeEditorTextBox.Text));

            Parser parser = new Parser(lexer);

            try
            {
                Program program = parser.Parse();
            }
            catch (LexerException ex)
            {
                ConsoleTextBox.Text += ex.Message;
            }
            catch (ParserException ex)
            {
                ConsoleTextBox.Text += ex.Message;
            }

            /*try
            {
                while (!(lexer.Token is EndOfTextToken))
                {
                    lexer.NextToken();
                    ConsoleTextBox.Text += lexer.Token.ToString();
                    ConsoleTextBox.Text += "\n";
                }
            }
            catch (LexerException ex)
            {
                ConsoleTextBox.Text += ex.Message;
            }*/


            /*Line line = new Line();
            line.Stroke = System.Windows.Media.Brushes.Black;

            line.X1 = 0;
            line.Y1 = 0;

            line.X2 = 100;
            line.Y2 = 100;

            MainCanvas.Children.Add(line);*/

        }
    }
}
