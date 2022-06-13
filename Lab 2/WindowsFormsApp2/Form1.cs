using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        double A, B, K, a;
        double x, y, y_2;

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                return;
            }
            else if ((e.KeyChar == '.' && !string.IsNullOrEmpty(textBox.Text) && !textBox.Text.Contains(".") && textBox.Text != "-"))
            {
                return;
            }
            e.Handled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            chart1.SaveImage("C:\\Users\\User\\Pictures\\chart.docx", ChartImageFormat.Png);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            chart2.SaveImage("C:\\Users\\User\\Pictures\\mychart2.png", ChartImageFormat.Png);
            chart3.SaveImage("C:\\Users\\User\\Pictures\\mychart3.png", ChartImageFormat.Png);
        }

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Clear();
                chart2.Series[0].Points.Clear();
                chart2.Series[1].Points.Clear();
                chart3.Series[0].Points.Clear();
                chart3.Series[1].Points.Clear();
                if (string.IsNullOrWhiteSpace(textBox4.Text)
                    || string.IsNullOrWhiteSpace(textBox5.Text)
                    || string.IsNullOrWhiteSpace(textBox6.Text))
                {
                    MessageBox.Show("Ви не заповнили всі поля", "Помилка");
                }
                A = double.Parse(textBox6.Text);
                B = double.Parse(textBox5.Text);
                K = double.Parse(textBox4.Text);
                if (string.IsNullOrWhiteSpace(textBox7.Text))
                {
                    a = 1;
                }
                else
                {
                    a = double.Parse(textBox7.Text);
                }
                x = A;
                int index = 0;
                while (x < B)
                {                  
                    y = Math.Pow(Math.Tan(x), 3) * a;
                    y_2 = a / Math.Pow(Math.Cos(x), 3);
                    if (y_2 > A && y_2 < B)
                    {
                        chart3.Series[0].Points.AddXY(y_2, x);
                    }
                    if (y > A && y < B)
                    {
                        chart3.Series[1].Points.AddXY(x, y);
                    }
                    chart2.Series[0].Points.AddXY(x, y / y_2);
                    if ((y_2/y) > A  && (y_2/y) < B)
                    {
                        chart2.Series[1].Points.AddXY(y_2 / y, x);
                    }
                    if (!Double.IsNaN(y) || !Double.IsNaN(y_2))
                    {
                        dataGridView2.Rows.Add(y / y_2, y_2 / y, y, y_2, x);
                        dataGridView2.Rows[index].HeaderCell.Value = (index).ToString();
                    }
                    index++;
                    x += (B - A) / K;
                }         
            }
            catch { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender; 
            if (char.IsDigit(e.KeyChar)||e.KeyChar == (char)Keys.Back)
            {
                return;
            }
            else if ((e.KeyChar == '.' && !string.IsNullOrEmpty(textBox.Text) && !textBox.Text.Contains(".") && textBox.Text != "-"))
            {
                return;
            }
            else if ((e.KeyChar == '-' && string.IsNullOrEmpty(textBox.Text) && !textBox.Text.Contains("-")))
            {
                return;
            }
            e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                chart1.Series[0].Points.Clear();
                if (string.IsNullOrWhiteSpace(textBox1.Text) 
                    || string.IsNullOrWhiteSpace(textBox2.Text) 
                    || string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Ви не заповнили всі поля", "Помилка");
                }
                A = double.Parse(textBox1.Text);
                B = double.Parse(textBox2.Text);
                K = double.Parse(textBox3.Text);
                x = A;
                int index = 0;
                while (x < B)
                {
                    y = Math.Acos((1-x)/(1-2*x));
                    chart1.Series[0].Points.AddXY(x, y);
                    if (!Double.IsNaN(y))
                    {
                        dataGridView1.Rows.Add(y, x);
                        dataGridView1.Rows[index].HeaderCell.Value = (++index).ToString();
                    }
                    x += (B - A)/K;
                }
            }
            catch { }
        }
    }
}
