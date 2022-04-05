using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace FiguresLibrary
{
    public interface IGraphics
    {


        void DrawRectangle(Color colorBorder, Color colorFill, int x, int y, int width, int height);
        void DrawCircle(Color colorBorder, Color colorFill, int x, int y, int radius);

        void DrawTriangle(Color colorBorder, Color colorFill, int x, int y, Point[] points,int height,int Base);
    }
}
