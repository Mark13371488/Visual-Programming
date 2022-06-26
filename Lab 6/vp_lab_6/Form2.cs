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
    public partial class Form2 : Form
    {
        private Matrix result;
        private Actions action;
        private int startX = 50;
        private int startY = 200;
        private int width = 50;
        private int height = 20;

        public Form2(Matrix res, Actions action)
        {
            result = res;
            this.action = action;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            switch (action)
            {
                case Actions.Add:
                    trace.Text = "A + B";
                    GenerateResult();
                    break;
                case Actions.Sub:
                    trace.Text = "A - B";
                    GenerateResult();
                    break;
                case Actions.MultForward:
                    trace.Text = "A * B";
                    GenerateResult();
                    break;
                case Actions.MultBackward:
                    trace.Text = "B * A";
                    GenerateResult();
                    break;
                case Actions.TraceA:
                    trace.Text = "Trace (A):";
                    label2.Text = MatrixOperations.Trace(result).ToString();
                    break;
                case Actions.TraceB:
                    trace.Text = "Trace (B):";
                    label2.Text = MatrixOperations.Trace(result).ToString();
                    break;
            }
        }
        private void GenerateResult()
        {
            for(int i = 0; i < result.Rows; i++)
            {
                for(int j= 0; j< result.Columns; j++)
                {
                    int x = startX + 60 * j;
                    int y = startY + 60 * i;
                    Label label = new Label()
                    {
                        Name = $"label{i}{j}",
                        Text = result[i, j].ToString(),
                        Location = new Point(x, y),
                        Size = new Size(width, height),
                    };
                    Controls.Add(label);
                }
            }
        }
    }
}
