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

namespace Wpf_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Double Value = 0; // Lagrar decimal - men också int-värden ifall det kommer att behövas
        String Operand; // Avgör operationen
        Boolean OperandPressed;

        public MainWindow()
        {
            InitializeComponent();
            ClearAll();
        }

        public void ClearAll()
        {
            ShowNumber = 0;
            Result = 0;
            FirstNumber = 0;
            SecondNumber = 0;
            IsSecond = false;
            IsDecimal = false;
            DecimalValue = 1;
            Operand = string.Empty;
        }

        private double _showNumber;

        public double ShowNumber
        {
            get { return _showNumber; }
            set
            {
                _showNumber = value;
                Display.Content = ShowNumber.ToString();
            }
        }

        private double _firstNumber;

        public double FirstNumber //Första numret
        {
            get { return _firstNumber; }
            set { _firstNumber = value; }
        }

        public double SecondNumber { get; set; } // Andra numret


        public double Result { get; set; } // Resultatet sparas i en double

        public bool IsSecond { get; set; }

        public bool IsDecimal { get; set; }

        public int DecimalValue { get; set; }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void CE_Click(object sender, RoutedEventArgs e)
        {
            if (Operand != "")
            {
                if (SecondNumber > 0)
                {
                    SecondNumber = 0;
                    ShowNumber = SecondNumber;
                    IsDecimal = false;
                    DecimalValue = 1;
                }
                else
                {
                    Operand = "";
                    IsSecond = false;
                }
            }
            else
            {
                ClearAll();
            }
            Display.Content = "0";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button B = (Button)sender;

       
            if ((Display.Content.ToString() == "0") || (OperandPressed))
            {
                Display.Content = " ";
            }

            // Om decimalpunkt redan finns i displayen, lägg inte till ytterligare en decimalpunkt
            if (B.Content.ToString() == ",")
            {
                IsDecimal = true;
            }
            else
            {
                if (IsSecond)
                {
                    if (IsDecimal)
                    {
                        DecimalValue *= 10;
                        SecondNumber += double.Parse(B.Content.ToString()) / DecimalValue;
                    }
                    else
                    {
                        SecondNumber *= 10;
                        SecondNumber += double.Parse(B.Content.ToString());

                    }
                    ShowNumber = SecondNumber;
                }
                else
                {
                    if (IsDecimal)
                    {
                        DecimalValue *= 10;
                        FirstNumber += double.Parse(B.Content.ToString()) / DecimalValue;
                    }
                    else
                    {
                        FirstNumber *= 10;
                        FirstNumber += double.Parse(B.Content.ToString());

                    }

                    ShowNumber = FirstNumber;
                }


            }

            OperandPressed = false;
        }

        private void Button_Operand(object sender, RoutedEventArgs e)
        {
            Button B = (Button)sender;


            if (Operand == "")
            {
                Operand = B.Content.ToString();
                IsSecond = true;
                ShowNumber = SecondNumber;
                IsDecimal = false;
                DecimalValue = 1;
            }
            else
            {
                DoCalcuation();
                Operand = B.Content.ToString();
            }

        }
        public void DoCalcuation()
        {
            switch (Operand)
            {
                case "+":
                    Result = FirstNumber + SecondNumber;
                    ResetAfterCalculation();
                    break;
                case "-":
                    Result = FirstNumber - SecondNumber;
                    ResetAfterCalculation();
                    break;
                case "×":
                    Result = FirstNumber * SecondNumber;
                    ResetAfterCalculation();
                    break;
                case "÷":
                    if (SecondNumber == 0)
                    {
                        MessageBox.Show("Kan inte dela med 0");
                    }
                    else
                    {
                        Result = FirstNumber / SecondNumber;
                        ResetAfterCalculation();
                    }
                    break;
                default:
                    break;
            }

        }

        public void ResetAfterCalculation()
        {
            FirstNumber = Result;
            ShowNumber = Result;
            SecondNumber = 0;
        }
        private void Button_Equals(object sender, RoutedEventArgs e)
        {
            DoCalcuation();
            Operand = "";
        }
        private void Window_KeyDownPreview(object sender, KeyEventArgs e)
        {
            bool shift = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);
            if (shift == true && e.Key == Key.OemQuestion)
            {
                Multiply.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (shift == true && e.Key == Key.Oem7)
            {
                Divide.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }

            switch (e.Key)
            {
                case Key.D0:
                    Zero.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.D1:
                    One.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.D2:
                    Two.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.D3:
                    Three.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.D4:
                    Four.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.D5:
                    Five.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.D6:
                    Six.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.D7:
                    Seven.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.D8:
                    Eight.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.D9:
                    Nine.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.OemPlus:
                    Plus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.OemMinus:
                    Minus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.Multiply:
                    Multiply.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.Divide:
                    Divide.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.Enter:
                    Equals.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.OemComma:
                    Dot.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
            }
        }
    }
}