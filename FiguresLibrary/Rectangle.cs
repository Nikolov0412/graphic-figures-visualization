using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLibrary
{
    [Serializable]
   public class Rectangle : Shape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public override void Paint(IGraphics graphics)
        {
            var ColorBorder = Selected ? Color.Red : BorderColor;
            graphics.DrawRectangle(ColorBorder, FillColor, Location.X, Location.Y, Width, Height);

        }
        public override int Area => Width * Height;
       public override bool PointInShape(Point point)
        {
            return
                Location.X <= point.X && point.X <= Location.X + Width &&
                Location.Y <= point.Y && point.Y <= Location.Y + Height;
        }
        public override bool Intersect(Rectangle rectangle)
        {
            return
            Location.X < rectangle.Location.X + rectangle.Width && rectangle.Location.X < Location.X + Width &&
            Location.Y < rectangle.Location.Y + rectangle.Height && rectangle.Location.Y < Location.Y + Height;
        }
    }
}
