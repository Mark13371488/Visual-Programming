using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vp_lab_6
{
    public partial class Form1 : Form
    {
        private Matrix A;
        private Matrix B;
        List<TextBox> textBoxesA = new List<TextBox>();
        List<TextBox> textBoxesB = new List<TextBox>();
        private readonly int startY = 156;
        private readonly int startX = 144;
        //private int endY = 936;
        //private int endX = 864;
        private readonly int width = 26;
        private readonly int height = 27;

        public Form1()
        {

            A = new Matrix();
            B = new Matrix();
            InitializeComponent();
            KeyPreview = true;
        }

        private void MatrixA_ValueChanged(object sender, EventArgs e)
        {
            if (ARows.Value != 0 
                && AColumns.Value != 0 
                && ARows.Value <= 6 
                && AColumns.Value <= 6)
            {
                ValidateBox(ref textBoxesA);
                for (int i=0; i<ARows.Value; i++)
                {
                    for(int j=0; j<AColumns.Value; j++)
                    {
                        int x = startX + 60 * j;
                        int y = startY + 60 * i;
                        TextBox textBox = new TextBox
                        {
                            Name = $"a{i}{j}",
                            Location = new Point(x,y),
                            Size = new Size(width,height),
                        };
                        textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
                        this.Controls.Add(textBox);                   
                        textBoxesA.Add(textBox);
                    }
                }
            }
        }
        private void ValidateBox(ref List<TextBox> textBoxes)
        {
            if (textBoxes != null) 
            {
                foreach (TextBox textBox in textBoxes)
                    Controls.Remove(textBox);
            }
            textBoxes = new List<TextBox>();
        }

        private void MatrixB_ValueChanged(object sender, EventArgs e)
        {
            
            if (BRows.Value != 0 
                && BColumns.Value != 0 
                && BRows.Value <= 6 
                && BColumns.Value <= 6)
            {
                ValidateBox(ref textBoxesB);
                for (int i = 0; i < BRows.Value; i++)
                {
                    for (int j = 0; j < BColumns.Value; j++)
                    {
                        int x = (startX + 644) + 60 * j;
                        int y = startY + 60 * i;
                        TextBox textBox = new TextBox
                        {
                            Name = $"b{i}{j}",
                            Location = new Point(x, y),
                            Size = new Size(width, height),
                        };
                        textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
                        this.Controls.Add(textBox);
                        textBoxesB.Add(textBox);
                    }
                }
            }
        }
        private bool ValidateA()
        {
            if (textBoxesA.Count == 0) return false;
            foreach (var t in textBoxesA)
            {
                if (string.IsNullOrEmpty(t.Text))
                    return false;
            }
            return true;
        }
        private bool ValidateB()
        {
            if(textBoxesB.Count == 0) return false;
            foreach (var t in textBoxesB)
            {
                if (string.IsNullOrEmpty(t.Text))
                    return false;
            }
            return true;
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if(ValidateA() && ValidateB())
            {
                A = AddElementsToMatrix((ARows, AColumns), textBoxesA);
                B = AddElementsToMatrix((BRows, BColumns), textBoxesB);
                try
                {
                    var res = MatrixOperations.Add(A, B);
                    new Form2(res, Actions.Add).ShowDialog();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Помилка");
                }
                
            }
            else
            {
                MessageBox.Show("Дані не введено!", "Помилка");
            }
        }
        private Matrix AddElementsToMatrix((NumericUpDown r, NumericUpDown c) dim, List<TextBox> textBoxes)
        {
            double[,] mtr = new double[(int)dim.r.Value, (int)dim.c.Value];
            int index = 0;
            for(int i = 0; i < dim.r.Value; i++)
            {
                for(int j = 0; j < dim.c.Value; j++)
                {
                    mtr[i, j] = double.Parse(textBoxes[index++].Text);
                }
            }
            return new Matrix(mtr);
        }

        private void SubBtn_Click(object sender, EventArgs e)
        {
            if (ValidateA() && ValidateB())
            {
                A = AddElementsToMatrix((ARows, AColumns), textBoxesA);
                B = AddElementsToMatrix((BRows, BColumns), textBoxesB);
                try
                {
                    var res = MatrixOperations.Substract(A, B);
                    new Form2(res, Actions.Sub).ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Помилка");
                }

            }
            else
            {
                MessageBox.Show("Дані не введено!", "Помилка");
            }
        }

        private void MultABBtn_Click(object sender, EventArgs e)
        {
            if (ValidateA() && ValidateB())
            {
                A = AddElementsToMatrix((ARows, AColumns), textBoxesA);
                B = AddElementsToMatrix((BRows, BColumns), textBoxesB);
                try
                {
                    var res = MatrixOperations.Multiple(A, B);
                    new Form2(res, Actions.MultForward).ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Помилка");
                }

            }
            else
            {
                MessageBox.Show("Дані не введено!", "Помилка");
            }
        }

        private void MultBABtn_Click(object sender, EventArgs e)
        {
            if (ValidateA() && ValidateB())
            {
                A = AddElementsToMatrix((ARows, AColumns), textBoxesA);
                B = AddElementsToMatrix((BRows, BColumns), textBoxesB);
                try
                {
                    var res = MatrixOperations.Multiple(B, A);
                    new Form2(res, Actions.MultBackward).ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Помилка");
                }

            }
            else
            {
                MessageBox.Show("Дані не введено!", "Помилка");
            }
        }

        private void TraceABtn_Click(object sender, EventArgs e)
        {
            if (ValidateA())
            {
                A = AddElementsToMatrix((ARows, AColumns), textBoxesA);
               
                try
                {
                    A.IsSquare();
                    new Form2(A, Actions.TraceA).ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Помилка");
                }
            }
            else
            {
                MessageBox.Show("Дані не введено!", "Помилка");
            }
        }

        private void TraceBBtn_Click(object sender, EventArgs e)
        {
            if (ValidateB())
            {
                B = AddElementsToMatrix((BRows, BColumns), textBoxesB);
                
                try
                {
                    B.IsSquare();
                    new Form2(B, Actions.TraceB).ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Помилка");
                }
            }
            else
            {
                MessageBox.Show("Дані не введено!", "Помилка");
            }
        }
        private void textBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (sender != null)
            {
                TextBox tb = (TextBox)sender;
                if (Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                {
                    return;
                }
                else if (!tb.Text.Contains(",") && (!string.IsNullOrEmpty(tb.Text) && e.KeyChar == ','))
                {
                    return;
                }
                e.Handled = true;
            }
        }
    }
}
