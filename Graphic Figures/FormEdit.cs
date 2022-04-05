using FiguresLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rectangle = FiguresLibrary.Rectangle;

namespace Graphic_Figures
{
    public partial class FormEdit : Form
    {
        public Shape Shape { get; set; }
        private Circle Circle{ get; set; }
        private Triangle Triangle { get; set; }
        private Rectangle Rectangle { get; set; }

        public Color Color => buttonColor.BackColor;

        public FormEdit()
        {
            InitializeComponent();
        }

        private void FormEdit_Load(object sender, EventArgs e)
        {

            if (Shape.Type == "Circle")
            {
                Circle = (Circle)Shape;
                textBoxRadius.Text = Circle.Radius.ToString();
                textBoxBase.Enabled = false;
                textBoxWidth.Enabled = false;
                textBoxHeight.Enabled = false;
                buttonColor.BackColor = Circle.FillColor;
            }
            else if (Shape.Type == "Rectangle")
            {
                textBoxBase.Enabled = false;
                textBoxRadius.Enabled = false;
                textBoxWidth.Enabled = true;
                textBoxHeight.Enabled = true;
                Rectangle = (Rectangle)Shape;
                textBoxWidth.Text = Rectangle.Width.ToString();
                textBoxHeight.Text = Rectangle.Height.ToString();
                buttonColor.BackColor = Rectangle.FillColor;
            }
            else if (Shape.Type == "Triangle")
            {
                textBoxBase.Enabled = true;
                textBoxRadius.Enabled = false;
                textBoxWidth.Enabled = false;
                textBoxHeight.Enabled = true;
                Triangle = (Triangle)Shape;
                textBoxHeight.Text = Triangle.Height.ToString();
                textBoxBase.Text = Triangle.Base.ToString();
            }
             
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (Rectangle != null)
            {
                Rectangle.Width = int.Parse(textBoxWidth.Text);
                Rectangle.Height = int.Parse(textBoxHeight.Text);
                Rectangle.FillColor = Color.FromArgb(100, buttonColor.BackColor);
            }
            else if (Triangle != null)
            {
                Triangle.Base = int.Parse(textBoxBase.Text);
                Triangle.Height = int.Parse(textBoxHeight.Text);
                 Triangle.FillColor= Color.FromArgb(100, buttonColor.BackColor);
            }
            else if (Circle != null)
            {
                Circle.Radius = int.Parse(textBoxRadius.Text);
               Circle.FillColor = Color.FromArgb(100, buttonColor.BackColor);

            }
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            var cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
                buttonColor.BackColor = cd.Color;
        }
    }
    }
