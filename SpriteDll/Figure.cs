using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSprite
{
    /// <summary>
    /// Промежуточная структура для сериализации типа Pen
    /// </summary>
    [Serializable]
    public struct Pens
    {
        public string Color;
        public int Width;

        public Pens(string color, int width)
        {
            this.Color = color;
            this.Width = width;
        }
    }

    /// <summary>
    /// Базовый класс для любой фигуры
    /// </summary>
    [Serializable]
    public class Figure
    {
        public Pens pen;   // Карандаш (цвет и толщина) фигуры
        public int X, Y;   // Координаты местоположения фигуры
        public int x, y;   // Параметры фигуры (вектор)

        public Figure(Pens pen, int X, int Y, int x, int y)
        {
            this.pen = pen;
            this.X = X;
            this.Y = Y;
            this.x = x;
            this.y = y;
        }

        public virtual void Draw(Graphics graphics) { }

        public virtual void Draw(Graphics graphics, int shiftX, int shiftY, float zoom) { }
    }
}
