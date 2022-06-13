using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Config _config = null;
        public double n1 = 0;
        public double res = 0;
        public string operation = string.Empty;
        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            _config = Config.GetConfig();
            Init();
        }
        private void Init()
        {
            if (!_config.IsNullProp())
            {
                textBox1.Text = _config.Num2;
                textBox2.Text = _config.Num1;
                n1 = (double)_config.N1;
                operation = _config.Operation;
            }
        }
        //AC(All Clear)
        private void button19_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            textBox2.Text = string.Empty;
            operation = string.Empty;
        }
        //change sign
        private void button18_Click(object sender, EventArgs e)
        {
                double result, number;
                number = double.Parse(textBox1.Text);
                result = -number;
                textBox1.Text = result.ToString();
        }
        //
        public void pidr()
        {
            try
            {
                switch (operation[0])
                {
                    case '+':
                        {
                            res = n1 + double.Parse(textBox1.Text);
                            textBox1.Text = $"{res}";
                            break;
                        }
                    case '-':
                        {
                            res = n1 - double.Parse(textBox1.Text);
                            textBox1.Text = $"{res}";
                            break;
                        }
                    case '*':
                        {
                            res = n1 * double.Parse(textBox1.Text);
                            textBox1.Text = $"{res}";
                            break;
                        }
                    case '/':
                        {
                            res = n1 / double.Parse(textBox1.Text);
                            textBox1.Text = $"{res}";
                            break;
                        }
                    case '^':
                        {
                            res = Math.Pow(n1, double.Parse(textBox1.Text));
                            textBox1.Text = $"{res}";
                            break;
                        }
                }
            }
            catch{ }
            operation = string.Empty;
        }
        private void button15_Click(object sender, EventArgs e)
        {
            if (operation.Length == 1)
            {
                textBox2.Text += textBox1.Text + '=';
                pidr();
            }
        }


        private void button11_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (textBox1.Text != string.Empty && !textBox1.Text.Contains(","))
            {
                textBox1.Text += button.Text;
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (textBox1.Text == "0")
            {
                textBox1.Text = button.Text;
            }
            else
            {
                textBox1.Text += button.Text;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Click_1(object sender, EventArgs e)
        {
            if (operation.Length == 1)
            {
                pidr();
            }
            try
            {
                n1 = double.Parse(textBox1.Text);
                Button button = (Button)sender;
                operation = button.Text;
                textBox2.Text = textBox1.Text + button.Text;
                textBox1.Text = "0";
            }
            catch { }
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'b':
                    button15.PerformClick();
                    break;
                case '1':
                    Form1_Click(button1, null);
                    break;
                case '2':
                    Form1_Click(button2, null);
                    break;
                case '3':
                    Form1_Click(button3, null);
                    break;
                case '4':
                    Form1_Click(button4, null);
                    break;
                case '5':
                    Form1_Click(button5, null);
                    break;
                case '6':
                    Form1_Click(button6, null);
                    break;
                case '7':
                    Form1_Click(button7, null);
                    break;
                case '8':
                    Form1_Click(button8, null);
                    break;
                case '9':
                    Form1_Click(button9, null);
                    break;
                case '0':
                    Form1_Click(button10, null);
                    break;
                case '+':
                    Form1_Click_1(button14, null);
                    break;
                case '-':
                    Form1_Click_1(button13, null);
                    break;
                case '/':
                    Form1_Click_1(button16, null);
                    break;
                case '*':
                    Form1_Click_1(button12, null);
                    break;
                case ',':
                    button11.PerformClick();
                    break;
                case '^':
                    Form1_Click_1(button17, null);
                    break;
                case (char)Keys.Back:
                    button19.PerformClick();
                    break;
                case '_':
                    button18.PerformClick();
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _config.Num2 = textBox1.Text;
            _config.Num1 = textBox2.Text;
            _config.N1 = n1;
            _config.Operation = operation;
            _config.Save();
        }
    }
    
}
//private void textBox1_KeyDown(object sender, KeyEventArgs e)
//{
//    switch (e.KeyCode)
//    {
//        case Keys.Enter:
//            button15.PerformClick();
//            break;
//        case Keys.NumPad1:
//            Form1_Click(button1, null);
//            break;
//        case Keys.NumPad2:
//            Form1_Click(button2, null);
//            break;
//        case Keys.NumPad3:
//            Form1_Click(button3, null);
//            break;
//        case Keys.NumPad4:
//            Form1_Click(button4, null);
//            break;
//        case Keys.NumPad5:
//            Form1_Click(button5, null);
//            break;
//        case Keys.NumPad6:
//            Form1_Click(button6, null);
//            break;
//        case Keys.NumPad7:
//            Form1_Click(button7, null);
//            break;
//        case Keys.NumPad8:
//            Form1_Click(button8, null);
//            break;
//        case Keys.NumPad9:
//            Form1_Click(button9, null);
//            break;
//        case Keys.NumPad0:
//            Form1_Click(button10, null);
//            break;
//        case Keys.Add:
//            Form1_Click_1(button14, null);
//            break;
//        case Keys.Subtract:
//            Form1_Click_1(button13, null);
//            break;
//        case Keys.Divide:
//            Form1_Click_1(button16, null);
//            break;
//        case Keys.Multiply:
//            Form1_Click_1(button12, null);
//            break;
//        case Keys.Decimal:
//            button11.PerformClick();
//            break;
//        case Keys.Control:
//            Form1_Click_1(button17, null);
//            break;
//        default:
//            e.Handled = true;
//            break;
//    }
//}

//підрахунок
//                string[] arr = textBox1.Text.Split('+', '-', '*', '/', '^');
//if (arr.Length == 3)
//{
//    arr[0] = $"-{arr[1]}";
//    arr[1] = arr[2];
//}
//if (operation.Length != 0 && arr[1] != "")
//{
//    switch (operation[0])
//    {
//        case '+':
//            {
//                res = double.Parse(arr[0]) + double.Parse(arr[1]);
//                textBox1.Text = $"{res}";
//                break;
//            }
//        case '-':
//            {
//                res = double.Parse(arr[0]) - double.Parse(arr[1]);
//                textBox1.Text = $"{res}";
//                break;
//            }
//        case '*':
//            {
//                res = double.Parse(arr[0]) * double.Parse(arr[1]);
//                textBox1.Text = $"{res}";
//                break;
//            }
//        case '/':
//            {
//                res = double.Parse(arr[0]) / double.Parse(arr[1]);
//                textBox1.Text = $"{res}";
//                break;
//            }
//        case '^':
//            {
//                res = Math.Pow(double.Parse(arr[0]), double.Parse(arr[1]));
//                textBox1.Text = $"{res}";
//                break;
//            }
//    }
//    operation = string.Empty;
//}
