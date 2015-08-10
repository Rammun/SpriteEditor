using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSprite
{
    /// <summary>
    /// Создает линию
    /// </summary>
    [Serializable]
    public class MyLine : Figure
    {
        public MyLine(Pens pen, int X, int Y, int x, int y)
            : base(pen, X, Y, x, y)
        {
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawLine(new Pen(ColorTranslator.FromHtml(pen.Color), pen.Width), X, Y, X + x, Y + y);
        }

        public override void Draw(Graphics graphics, int shiftX, int shiftY, float zoom)
        {
            graphics.DrawLine(new Pen(ColorTranslator.FromHtml(pen.Color), pen.Width), X * zoom + shiftX, Y * zoom + shiftY, X * zoom + shiftX + x * zoom, Y * zoom + shiftY + y * zoom);
        }
    }

}
