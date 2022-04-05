using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using FiguresLibrary;
using System.Windows.Forms;
using Rectangle = FiguresLibrary.Rectangle;
using Shape = FiguresLibrary.Shape;
using Circle = FiguresLibrary.Circle;
using Triangle = FiguresLibrary.Triangle;
 
namespace Graphic_Figures
{
    public partial class FormMain : Form, IGraphics
    {
        private List<Shape> _shapes = new List<Shape>();
        private Shape shape;
        private Point _mouseCaptureLocation;
        private Rectangle _frame;
        private Graphics _graphics;
        private bool _isMouseDown;
        private bool _save;
        public FormMain()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        public void DrawRectangle(Color colorBorder, Color colorFill, int x, int y, int width, int height)
        {
            if (_graphics != null)
            {
                using (var brush = new SolidBrush(colorFill))
                    _graphics.FillRectangle(brush, x, y, width, height);
                using (var pen = new Pen(colorBorder))
                    _graphics.DrawRectangle(pen, x, y, width, height);
            }
        }
        public void DrawCircle(Color colorBorder, Color colorFill, int x, int y, int radius)
        {
            if (_graphics != null)
            {
                using (var brush = new SolidBrush(colorFill))
                    _graphics.FillEllipse(brush, x - radius, y - radius, radius + radius, radius + radius);

                using (var pen = new Pen(colorBorder))
                    _graphics.DrawEllipse(pen, x - radius, y - radius, radius + radius, radius + radius);
            }
        }

        public void DrawTriangle(Color colorBorder, Color colorFill, int x, int y, Point[] points,int height,int Base)
        {
            points[0].X = x;
            points[0].Y = y + Base;
            points[1].X = x + Base;
            points[1].Y = y - height;
            points[2].X = x + height;
            points[2].Y = y + Base;

            if (_graphics != null)
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(100, colorFill)))
                {
                     
                    _graphics.FillPolygon(brush, points.ToArray());
                    using (Pen pen = new Pen(colorBorder))
                        _graphics.DrawPolygon(pen, points);
                }
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            _graphics = e.Graphics;

            foreach (var s in _shapes)
                s.Paint(this);


            _frame?.Paint(this);
            _graphics = null;

        }
        private void FormMain_MouseUp(object sender, MouseEventArgs e)
        {
            _isMouseDown = false;
            if(e.Button == MouseButtons.Right && _frame != null)
            {
                var rand = new Random();
 
                if (shapesSelector.SelectedItem == null)
                    return;

                switch (shapesSelector.SelectedItem.ToString())
                {
                    case "Circle":
                        shape = new Circle()
                        {
                            Location = e.Location,
                            BorderColor = Color.Black,
                            FillColor = Color.FromArgb(100, Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255))),
                            Radius = 100,
                            Type = "Circle"

                        };
                        _shapes.Add(shape);
                        break;
                    case "Rectangle":
                        shape = new Rectangle()
                        {
                            Location = e.Location,
                            BorderColor = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255)),
                            FillColor = Color.FromArgb(100, Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255))),
                            Width =150,
                            Height=250,
                            Type = "Rectangle"
                        };
                        _shapes.Add(shape);
                        break;

                    case "Triangle":
                        shape = new Triangle()
                        {
                            Location = e.Location,
                            BorderColor = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255)),
                            FillColor = Color.FromArgb(100, Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255))),
                            Height = 200,
                            Base = 100,
                            Type="Triangle"
                        };
                        _shapes.Add(shape);
                        break;
                }

                int area = 0;
                foreach (var shape in _shapes)
                {
                    shape.Selected = false;
                    area += shape.Area;
                }
                
                toolStripStatusArea.Text = area.ToString();

            }
            _frame = null;
            Invalidate();
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseCaptureLocation = e.Location;
            _isMouseDown = true;
            _frame = new Rectangle()
            {
                Location = e.Location,
                BorderColor = Color.LightGray

            };
            foreach (var selected in _shapes)
            {
                if (selected.PointInShape(e.Location))
                {
                    selected.Selected = true;
                    SelectedLabel.Text = selected.Selected.ToString();
                    break;
                }
                else
                {
                    selected.Selected = false;
                    SelectedLabel.Text = selected.Selected.ToString();
                }

                
            }

            Invalidate();
        }

        private void FormMain_DoubleClick(object sender, EventArgs e)
        {
            foreach (var shape in _shapes)
            {
                if (shape.Selected)
                {
                    var fp = new FormEdit();
                    fp.Shape = (Shape)shape;
                    fp.ShowDialog();

                    Invalidate();
                    break;
                }
            }
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (_frame == null)
                return;
            _frame.Location = new Point
            {
                X = Math.Min(_mouseCaptureLocation.X, e.Location.X),
                Y = Math.Min(_mouseCaptureLocation.Y, e.Location.Y),
            };
            _frame.Width = Math.Abs(_mouseCaptureLocation.X - e.Location.X);
            _frame.Height = Math.Abs(_mouseCaptureLocation.Y - e.Location.Y);

            if (e.Button == MouseButtons.Left)
                foreach (var shape in _shapes)

                    shape.Selected = shape.Intersect(_frame);


            Invalidate();
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode != Keys.Delete)
                return;
            var area = 0;
            for (int r = _shapes.Count() - 1; r >= 0; r--)
            {
                if (_shapes[r].Selected)
                {
                    _shapes.RemoveAt(r);
                }
                else
                {
                    area += _shapes[r].Area;
                }
            }

            toolStripStatusArea.Text = area.ToString();
            Invalidate();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            var fsd = new FormSaveDialog();
            if (fsd.ShowDialog() == DialogResult.OK)
            {
                _save = fsd.Save;
                if (_save == true)
                {
                    var formatter = new BinaryFormatter();
                using (var stream = new FileStream("data", FileMode.Create))
                {
                    formatter.Serialize(stream, _shapes);
                }
                }
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (!File.Exists("data"))
                return;

            var formatter = new BinaryFormatter();
            using (var stream = new FileStream("data", FileMode.Open))
            {
                _shapes = (List<Shape>)formatter.Deserialize(stream);
            }
        }
    }
}
