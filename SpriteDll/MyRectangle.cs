using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSprite
{
    /// <summary>
    /// Создает прямоугольник
    /// </summary>
    [Serializable]
    public class MyRectangle : Figure
    {
        public MyRectangle(Pens pen, int X, int Y, int x, int y)
            : base(pen, X, Y, x, y)
        {
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(new Pen(ColorTranslator.FromHtml(pen.Color), pen.Width), X, Y, x, y);
        }

        public override void Draw(Graphics graphics, int shiftX, int shiftY, float zoom)
        {
            graphics.DrawRectangle(new Pen(ColorTranslator.FromHtml(pen.Color), pen.Width), X + shiftX, Y + shiftY, x * zoom, y * zoom);
        }
    }
}
