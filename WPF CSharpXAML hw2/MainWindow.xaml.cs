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
using System.Windows.Threading;

namespace WPF_CSharpXAML_hw2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        Random random;
        string symbols;
        double WindowSize;
        bool startedTimer;
        bool lowerLetters;
        bool startText;
        public int chars { get; set; }
        public int seconds { get; set; }
        public int Speed
        {
            get { return Int32.Parse(speedTextBlock.Text); }
            set { speedTextBlock.Text = value.ToString(); }
        }
        public int Fails
        {
            get { return Int32.Parse(failsTextBlock.Text); }
            set { failsTextBlock.Text = value.ToString(); }
        }
        public int Difficulty
        {
            get { return Int32.Parse(difficultyTextBlock.Text); }
            set { difficultyTextBlock.Text = value.ToString(); }
        }


        public MainWindow()
        {
            InitializeComponent();
            WindowSize = this.Height + this.Width;
            timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            random = new Random();
            startedTimer = false;
            lowerLetters = true;
            startText = true;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            seconds++;
            Speed = Convert.ToInt32(((Convert.ToDouble(chars) / seconds)) * 60);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (Grid grid in buttonsGrid.Children)
            {
                foreach (Button button in (grid as Grid).Children)
                {
                    if ((button as Button).Name == e.Key.ToString())
                    {
                        switch ((button as Button).Name)
                        {
                            case "Capital":
                                (button as Button).IsEnabled = !(button as Button).IsEnabled;
                                if (lowerLetters)
                                { ToUpLetters(); }
                                else
                                { ToDownLetters(); }
                                lowerLetters = !lowerLetters;
                                return;
                            case "LeftShift":
                            case "RightShift":
                                if (Capital.IsEnabled)
                                {
                                    ToUpLetters();
                                    lowerLetters = false;
                                }
                                else
                                {
                                    ToDownLetters();
                                    lowerLetters = true;
                                }
                                ToUpSymbols();
                                break;
                            case "Back":
                                (button as Button).IsEnabled = false;
                                if (startButton.IsEnabled)
                                {
                                    if ((taskLabel.Content as String).Length > 0)
                                    {
                                        taskLabel.Content = taskLabel.Content.ToString().Remove((taskLabel.Content as String).Length - 1, 1);
                                    }
                                    return;
                                }
                                if ((userLabel.Content as String).Length > 0)
                                {
                                    userLabel.Content = userLabel.Content.ToString().Remove((userLabel.Content as String).Length - 1, 1);
                                }
                                return;
                        }
                        (button as Button).IsEnabled = false;
                        char key;
                        switch ((button as Button).Name)
                        {
                            case "LeftShift":
                            case "RightShift":
                            case "Tab":
                            case "LeftCtrl":
                            case "RightCtrl":
                            case "LWin":
                            case "RWin":
                            case "Return":
                                return;
                            case "Space":
                                key = ' ';
                                break;
                            default:
                                key = (button as Button).Content.ToString().ElementAt(0);
                                break;
                        }
                        if (startButton.IsEnabled == false)
                        {
                            if (startedTimer) { timer.Start(); startedTimer = false; }
                            if (taskLabel.Content.ToString()[userLabel.Content.ToString().Length] == key)
                            {
                                chars++;
                                userLabel.Content += key.ToString();
                            }
                            else
                            {
                                Fails++;
                            }
                            if (taskLabel.Content.ToString().Length == userLabel.Content.ToString().Length)
                            {
                                Stop();
                                MessageBox.Show($"Results:" +
                                    $"\nSpeed: {Speed} chars/min" +
                                    $"\nFails: {Fails}" +
                                    $"\nDifficulty: {Difficulty}" +
                                    $"\nCase Sensitive: " + (checkBox.IsChecked == true ? "checked" : "unchecked"), "Result", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            if (startText)
                            {
                                userLabel.Content = String.Empty;
                                startText = false;
                            }
                            taskLabel.Content += key.ToString();
                        }
                        return;
                    }
                    else if ((button as Button).Name == e.SystemKey.ToString())
                    {
                        (button as Button).IsEnabled = false;
                        return;
                    }
                }
            }
        }
        private void ToUpLetters()
        {
            Q.Content = "Q";
            W.Content = "W";
            E.Content = "E";
            R.Content = "R";
            T.Content = "T";
            Y.Content = "Y";
            U.Content = "U";
            I.Content = "I";
            O.Content = "O";
            P.Content = "P";
            A.Content = "A";
            S.Content = "S";
            D.Content = "D";
            F.Content = "F";
            G.Content = "G";
            H.Content = "H";
            J.Content = "J";
            K.Content = "K";
            L.Content = "L";
            Z.Content = "Z";
            X.Content = "X";
            C.Content = "C";
            V.Content = "V";
            B.Content = "B";
            N.Content = "N";
            M.Content = "M";
        }
        private void ToUpSymbols()
        {
            Oem3.Content = "~";
            D1.Content = "!";
            D2.Content = "@";
            D3.Content = "#";
            D4.Content = "$";
            D5.Content = "%";
            D6.Content = "^";
            D7.Content = "&";
            D8.Content = "*";
            D9.Content = "(";
            D0.Content = ")";
            OemMinus.Content = '_';
            OemPlus.Content = '+';
            OemOpenBrackets.Content = "{";
            Oem6.Content = "}";
            Oem5.Content = "|";
            Oem1.Content = ":";
            OemQuotes.Content = "\"";
            OemComma.Content = "<";
            OemPeriod.Content = ">";
            OemQuestion.Content = "?";
        }
        private void ToDownLetters()
        {
            Q.Content = "q";
            W.Content = "w";
            E.Content = "e";
            R.Content = "r";
            T.Content = "t";
            Y.Content = "y";
            U.Content = "u";
            I.Content = "i";
            O.Content = "o";
            P.Content = "p";
            A.Content = "a";
            S.Content = "s";
            D.Content = "d";
            F.Content = "f";
            G.Content = "g";
            H.Content = "h";
            J.Content = "j";
            K.Content = "k";
            L.Content = "l";
            Z.Content = "z";
            X.Content = "x";
            C.Content = "c";
            V.Content = "v";
            B.Content = "b";
            N.Content = "n";
            M.Content = "m";
        }
        private void ToDownSymbols()
        {
            Oem3.Content = "`";
            D1.Content = "1";
            D2.Content = "2";
            D3.Content = "3";
            D4.Content = "4";
            D5.Content = "5";
            D6.Content = "6";
            D7.Content = "7";
            D8.Content = "8";
            D9.Content = "9";
            D0.Content = "0";
            OemMinus.Content = '-';
            OemPlus.Content = '=';
            OemOpenBrackets.Content = "[";
            Oem6.Content = "]";
            Oem5.Content = "\\";
            Oem1.Content = ";";
            OemQuotes.Content = "'";
            OemComma.Content = ",";
            OemPeriod.Content = ".";
            OemQuestion.Content = "/";
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            foreach (Grid grid in buttonsGrid.Children)
            {
                foreach (Button button in (grid as Grid).Children)
                {
                    if ((button as Button).Name == e.Key.ToString())
                    {
                        if ((button as Button).Name == "Capital")
                        { return; }
                        else if ((button as Button).Name == "LeftShift" || (button as Button).Name == "RightShift")
                        {
                            if (Capital.IsEnabled)
                            {
                                ToDownLetters();
                                lowerLetters = true;
                            }
                            else
                            {
                                ToUpLetters();
                                lowerLetters = false;
                            }
                            ToDownSymbols();
                        }
                        (button as Button).IsEnabled = true;
                        return;
                    }
                    else if ((button as Button).Name == e.SystemKey.ToString())
                    {
                        (button as Button).IsEnabled = true;
                        return;
                    }
                }
            }
        }

        /*На основе изменения размера окна - изменяет размер текстовых полей окна
        Based on window resizing - resizes the window text fields*/
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double procent = (this.Width + this.Height) / WindowSize;
            foreach (Grid childGrid in generalGrid.Children)
            {
                foreach (var child in childGrid.Children)
                {
                    if (child is Button)
                    {
                        (child as Button).FontSize *= procent;
                    }
                    else if (child is TextBlock)
                    {
                        (child as TextBlock).FontSize *= procent;
                    }
                    else if (child is Label)
                    {
                        (child as Label).FontSize *= procent;
                    }
                    else if (child is Grid)
                    {
                        foreach (Button button in (child as Grid).Children)
                        {
                            (button as Button).FontSize *= procent;
                        }
                    }
                    else if (child is CheckBox)
                    {
                        (child as CheckBox).FontSize *= procent;
                    }
                }
            }
            WindowSize = (this.Width + this.Height);
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            startButton.IsEnabled = false;
            stopButton.IsEnabled = true;
            Speed = 0;
            Fails = 0;
            chars = 0;
            seconds = 0;
            userLabel.Content = String.Empty;
            taskLabel.Content = GenerateTasklabel();
            startedTimer = true;
        }

        private string GenerateTasklabel()
        {
            string result = String.Empty;
            if (checkBox.IsChecked == true)
            {
                symbols = "`1234567890-=qwertyuiop[]\\asdfghjkl;'zxcvbnm,./~!@#$%^&*()+QWERTYUIOP{}ASDFGHJKL:\"ZXCVBNM<>?";
            }
            else
            {
                symbols = "`1234567890-=qwertyuiop[]\\asdfghjkl;'zxcvbnm,./";
            }
            for (int i = 0; i < diffSlider.Value; i++)
            {
                int lengthSentence = random.Next(3, 5);
                for (int k = 0; k < 2; k++)
                {
                    if (result != String.Empty) { result += ' '; }
                    for (int j = 0; j < lengthSentence; j++)
                    {
                        result += symbols[random.Next(0, symbols.Length)];
                    }
                }
            }
            return result;
        }

        private void diffSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Difficulty = Convert.ToInt32(e.NewValue);
            diffSlider.Value = Convert.ToInt32(e.NewValue);
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            Stop();
        }
        private void Stop()
        {
            stopButton.IsEnabled = false;
            timer.Stop();
            startButton.IsEnabled = true;
            taskLabel.Content = String.Empty;
            startText = true;
            userLabel.Content = "Hello, Click Start to start!";
        }
    }
}
