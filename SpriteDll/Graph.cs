using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSprite
{
    /// <summary>
    /// Реализует отрисовку спрайта
    /// </summary>
    public class Graph
    {
        Bitmap bmp;
        Graphics graphics;

        public Graph(int width, int height)
        {
            bmp = new Bitmap(width, height);
            graphics = Graphics.FromImage(bmp);
            Clear();
        }

        /// <summary>
        /// Возвращает BitMap спрайта
        /// </summary>
        public Bitmap GetBitmap { get { return bmp; } }

        /// <summary>
        /// Очищает изображение пустым цветом
        /// </summary>
        public void Clear()
        {
            graphics.Clear(Color.Empty);
        }

        /// <summary>
        /// Очищает изображение заданным цветом
        /// </summary>
        /// <param name="color">Цвет очистки</param>
        public void Clear(Color color)
        {
            graphics.Clear(color);
        }

        /// <summary>
        /// Рисует фигуру
        /// </summary>
        /// <param name="figure">Фигура</param>
        public void Draw(Figure figure)
        {
            figure.Draw(graphics);            
        }
        
        public void Draw(Figure figure, int shiftX, int shiftY, float zoom)
        {
            figure.Draw(graphics, shiftX, shiftY, zoom);
        }

        /// <summary>
        /// Рисует спрайт
        /// </summary>
        /// <param name="sprite">Спрайт</param>
        public void Draw(Sprite sprite)
        {
            foreach(Figure figure in sprite.figures)
            {
                Draw(figure);
            }
        }

        public void Draw(Sprite sprite, int shiftX, int shiftY, float zoom)
        {
            foreach(Figure figure in sprite.figures)
            {
                Draw(figure, shiftX, shiftY, zoom);
            }
        }
    }
}
