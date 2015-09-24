using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace Elliptic_Curves
{
    public partial class Form1 : Form
    {
        Complex cx = new Complex();
        Complex cy = new Complex();
        Graphics gX = null;
        Graphics gY = null;
        Point CenterPointX = new Point(0, 0);
        Point CenterPointY = new Point(0, 0);
        float SCALE = 5;

        public Form1()
        {
            InitializeComponent();

        }

        private void panelOriCrd_MouseMove(object sender, MouseEventArgs e)
        {
            textBoxOriCrd.Text = e.Location.ToString();
            cx = new Complex((double)(e.X - CenterPointX.X) / 10, -(double)(e.Y - CenterPointX.Y) / 10);
            cy = Complex.Sqrt(Complex.Pow(cx, 3) + Complex.Multiply((double)numericUpDownFormula2.Value, cx) + (double)numericUpDownFormula4.Value);

            if (e.Button == MouseButtons.Left)
            {
                gX.FillRectangle(Brushes.Azure, e.X, e.Y, 2, 2);
                gY.FillRectangle(Brushes.Azure, SCALE * (float)cy.Real + CenterPointY.X, SCALE * (float)cy.Imaginary + CenterPointY.Y, 2, 2);
                gY.FillRectangle(Brushes.Orange, -SCALE * (float)cy.Real + CenterPointY.X, -SCALE * (float)cy.Imaginary + CenterPointY.Y, 2, 2);
            }
            textBoxOriX.Text = cx.Imaginary < 0 ? cx.Real.ToString() + cx.Imaginary.ToString() + "i" : cx.Real + "+" + cx.Imaginary + "i";
            textBoxOutputY.Text = cy.Real.ToString("F2") + "+" + cy.Imaginary.ToString("F2") + "i";
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            gX.Clear(panelOriCrd.BackColor);
            gX.FillRectangle(Brushes.Red, CenterPointX.X, CenterPointX.Y, 2, 2);
            gY.Clear(panelOutputY.BackColor);
            gY.FillRectangle(Brushes.Red, CenterPointY.X, CenterPointY.Y, 2, 2);
        }


        private void Form1_Shown(object sender, EventArgs e)
        {
            gX = panelOriCrd.CreateGraphics();
            gY = panelOutputY.CreateGraphics();
            MouseEventArgs mea = new MouseEventArgs(System.Windows.Forms.MouseButtons.Right, 1, panelOriCrd.Width / 2, panelOriCrd.Height / 2, 0);
            panelOriCrd_MouseClick(null, mea);
            mea = new MouseEventArgs(System.Windows.Forms.MouseButtons.Right, 1, panelOutputY.Width / 2, panelOutputY.Height / 2, 0);
            panelOutputY_MouseClick(null, mea);
        }

        private void panelOriCrd_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                CenterPointX = e.Location;
                gX.Clear(panelOriCrd.BackColor);
                gX.FillRectangle(Brushes.Red, e.X, e.Y, 2, 2);
            }
        }

        private void panelOutputY_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                CenterPointY = e.Location;
                gY.Clear(panelOriCrd.BackColor);
                gY.FillRectangle(Brushes.Red, e.X, e.Y, 2, 2);
            }
        }

        private void numericUpDownScale_ValueChanged(object sender, EventArgs e)
        {
            SCALE = (float)numericUpDownScale.Value;
        }

    }
}
