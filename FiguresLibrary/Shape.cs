using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLibrary
{
    [Serializable]
   public abstract class Shape
    {
        private string types = "";
        public Point Location { get; set; }
        public Color FillColor { get; set; }
        public Color BorderColor { get; set; }
      public string Type {
            get => types;
          
            set
            {
                if (value == "Rectangle" || value == "Triangle" || value == "Circle")
                {
                    types = value;
                }
                else
                    return ;
            } 
        }
      
        public bool Selected { get; set; }
        public abstract int Area { get; }
        public abstract void Paint(IGraphics graphics);
        public abstract bool PointInShape(Point p);
        public abstract bool Intersect(Rectangle rectangle);


    }
}
