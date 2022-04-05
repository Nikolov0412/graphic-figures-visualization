using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLibrary
{
    [Serializable]
    public class Triangle : Shape
    {

        public int Height { get; set; }
        public int Base { get; set; }

        private double A;
        private double B;
        private double C;
        private double S;
        public override int Area => Convert.ToInt32(Math.Sqrt(S * (S - A) * (S - B) * (S - C)));

        private Point[] points = new Point[3];


        public override void Paint(IGraphics graphics)
        {

            A = Math.Sqrt(Math.Pow((points[1].X - points[0].X), 2) + Math.Pow((points[1].Y - points[0].Y), 2));
            B = Math.Sqrt(Math.Pow((points[2].X - points[1].X), 2) + Math.Pow((points[2].Y - points[1].Y), 2));
            C = Math.Sqrt(Math.Pow((points[2].X - points[0].X), 2) + Math.Pow((points[2].Y - points[0].Y), 2));
            S = (A + B + C) / 2;

            var ColorBorder = Selected ? Color.Red : BorderColor;

            graphics.DrawTriangle(ColorBorder, FillColor, Location.X, Location.Y, points, Height, Base);

        }

        public override bool PointInShape(Point p)
        {
            var s = (points[0].X - points[2].X) * (p.Y - points[2].Y) - (points[0].Y - points[2].Y) * (p.X - points[2].X);
            var t = (points[1].X - points[0].X) * (p.Y - points[0].Y) - (points[1].Y - points[0].Y) * (p.X - points[0].X);
            if ((s < 0) != (t < 0) && s != 0 && t != 0)
                return false;

            var d = (points[2].X - points[1].X) * (p.Y - points[1].Y) - (points[2].Y - points[1].Y) * (p.X - points[1].X);
            return d == 0 || (d < 0) == (s + t <= 0);
        }
        public override bool Intersect(Rectangle rectangle)
        {
            return
            Location.X < rectangle.Location.X + rectangle.Width && rectangle.Location.X < Location.X + Base &&
            Location.Y < rectangle.Location.Y + rectangle.Height && rectangle.Location.Y < Location.Y + Height;
        }

       
    }
}
