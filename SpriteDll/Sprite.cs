using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSprite
{
    /// <summary>
    /// Реализует свойства и поля спрайта
    /// </summary>
    [Serializable]
    public class Sprite
    {
        public string fonColor = string.Empty;
        public List<Figure> figures;

        public Sprite()
        {
            this.figures = new List<Figure>();
        }

        public Sprite(List<Figure> figures)
        {
            this.figures = figures;
        }

        /// <summary>
        /// Добавляет фигуру в спрайт
        /// </summary>
        /// <param name="figure"></param>
        public void AddFigure(Figure figure)
        {
            figures.Add(figure);
        }

        /// <summary>
        /// Удаляет все фигуры из спрайта
        /// </summary>
        public void Clear()
        {
            fonColor = string.Empty;
            figures.Clear();
        }

        /// <summary>
        /// Удаляет посленюю фигуру из спрайта
        /// </summary>
        public void Undo()
        {
            if (figures.Count > 0)
            {
                figures.RemoveAt(figures.Count - 1);
            }            
        }
    }
}
