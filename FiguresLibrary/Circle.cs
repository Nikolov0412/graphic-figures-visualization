using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLibrary
{
    [Serializable]
    public class Circle : Shape
    {

        public int Radius { get; set; }

        public override void Paint(IGraphics graphics)
        {
            var ColorBorder = Selected ? Color.Red : BorderColor;
            graphics.DrawCircle(ColorBorder, FillColor, Location.X, Location.Y,Radius);
        }
        public override int Area => Convert.ToInt32((3.14) * Radius * Radius);



        public override bool PointInShape(Point point)
        {
            return Math.Pow((point.X - Location.X), 2) + Math.Pow((point.Y - Location.Y), 2) <= Math.Pow(Radius,2);
        }
        public override bool Intersect(Rectangle rectangle)
        {
            return
            Location.X < rectangle.Location.X + rectangle.Width && rectangle.Location.X < Location.X + Radius &&
            Location.Y < rectangle.Location.Y + rectangle.Height && rectangle.Location.Y < Location.Y + Radius;
        }

    }

}
