using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using DataSprite;

namespace SpriteSoft
{
    public partial class FormSprite : Form
    {
        int widthForm, heightForm;     // размеры основной формы
        int stepShift;                 // шаг смещения изображения
        int stepNet;                   // шаг сетки
        Sprite spriteNet;   // спрайт сетки

        Graph graph;
        Sprite sprite;

        DataSprite.Pens currPens;       // Текущий карандаш
        Figure currFigure;   // Текущая фигура

        bool flagLine;       // Флаг, для контроля рисования ломанной линии

        public FormSprite()
        {
            InitializeComponent();

            widthForm = this.Size.Width;
            heightForm = this.Size.Height;
            stepNet = 10;
            stepShift = 1;

            toolStripLabelXW.Text = "xW=" + this.Size.Width;
            toolStripLabelYW.Text = "yW=" + this.Size.Height;

            toolStripComboBoxWidthPen.SelectedIndex = 2;
            toolStripComboBoxStepNet.SelectedIndex = 0;
            toolStripComboBoxShift.SelectedIndex = 0;

            graph = new Graph(picture.Width, picture.Height);
            sprite = new Sprite();
            currPens = new DataSprite.Pens(ColorTranslator.ToHtml(Color.Black), 3);
            currFigure = new MyLine(currPens, 1, 1, 1, 1);

            flagLine = false;

            picture.Image = graph.GetBitmap;
        }

        private void RefreshBitMap()
        {
            if (toolStripButtonNet.Checked == true)
            {
                graph.Draw(spriteNet);
            }

            graph.Draw(sprite);
            picture.Image = graph.GetBitmap;
        }
        
        private void picture_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)           // Когда нажимаем левую кнопку мыши
            {
                if(toolStripButtonLine.Checked)         // Если выбрана обычная линия
                {
                    currFigure = new MyLine(currPens, e.X, e.Y, 1, 1);  // То создаем новый объект линии
                }
                else
                if (toolStripButtonBrokenLine.Checked)   // Если выбрана ломанная линия
                {
                    if(flagLine)                         // То проверяем, начинаем или продолжаем рисовать ломаную
                    {
                        sprite.AddFigure(currFigure);    // Если продолжаем, то добавляем в список линию
                        RefreshBitMap();
                    }

                    currFigure = new MyLine(currPens, e.X, e.Y, 1, 1);  //  И создаем новую линию
                    flagLine = true;
                }
                else
                if (toolStripButtonRectangle.Checked)     // Если выбран прямоугольник
                {
                    currFigure = new MyRectangle(currPens, e.X, e.Y, 1, 1);
                }
                else
                if (toolStripButtonEllipse.Checked)       // Если выбран эллипс
                {
                    currFigure = new MyEllipse(currPens, e.X, e.Y, 1, 1);
                }
            }

            if(e.Button==MouseButtons.Right && flagLine)   // Когда нажимаем правую кнопку мыши
            {
                flagLine = false;                          // Прекращаем рисовать ломаные линии
                graph.Clear();
                RefreshBitMap();                           // Перерисовываем
            }
        }

        private void picture_MouseMove(object sender, MouseEventArgs e)  // При движении мышки
        {
            if (flagLine)                                                // Если продолжаем рисовать ломанную линию (при этом не нажата ни одна кнопка мыши)
            {
                if (toolStripButtonFonImage.Checked)
                {
                    graph.Clear();
                    RefreshBitMap();
                }
                else
                {
                    currFigure.pen.Color = ColorTranslator.ToHtml(picture.BackColor);  // Устанавливаем у фигуры карандаш цветом фона 
                    graph.Draw(currFigure);                                // Рисуем текущую фигуру цветом фона (стираем)
                }

                currFigure.x = e.X - currFigure.X;                         // Изменяем параметры текущей фигуры
                currFigure.y = e.Y - currFigure.Y;

                currFigure.pen.Color = currPens.Color;                     // Устанавливаем цвет у фгуры на цвет текущего карандаша
                graph.Draw(currFigure);                                    // Рисуем фигуру с новыми параметрами

                RefreshBitMap();                                           // Обновляем BitMap
            }

            if(e.Button == MouseButtons.Left)
            {
                if (toolStripButtonLine.Checked || toolStripButtonRectangle.Checked || toolStripButtonEllipse.Checked)
                {
                    if (toolStripButtonFonImage.Checked)
                    {
                        graph.Clear();
                        RefreshBitMap();
                    }
                    else
                    {
                        currFigure.pen.Color = ColorTranslator.ToHtml(picture.BackColor);  // Устанавливаем у фигуры карандаш цветом фона 
                        graph.Draw(currFigure);                                   // Рисуем текущую фигуру цветом фона (стираем)
                    }

                    currFigure.x = e.X - currFigure.X;    // Вычисляем вектор
                    currFigure.y = e.Y - currFigure.Y;
                    
                    currFigure.pen.Color = currPens.Color;
                    graph.Draw(currFigure);
                }

                RefreshBitMap();
            }

            toolStripLabelXM.Text = "xM=" + e.X.ToString();
            toolStripLabelYM.Text = "yM=" + e.Y.ToString();
        }

        private void picture_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if (toolStripButtonLine.Checked || toolStripButtonRectangle.Checked || toolStripButtonEllipse.Checked)
                {
                    sprite.AddFigure(currFigure);
                }
                
                RefreshBitMap();
            }
        }        

        private void toolStripButtonColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                toolStripButtonColor.BackColor = colorDialog.Color;
                currPens.Color = ColorTranslator.ToHtml(colorDialog.Color);
            }
        }

        private void toolStripButtonClear_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Очистка экрана! Вы уверены?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(result == DialogResult.Yes)
            {
                flagLine = false;

                graph.Clear();
                sprite.Clear();
                RefreshBitMap();
            }
        }

        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {
            sprite.Undo();
            graph.Clear();
            RefreshBitMap();
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                graph.Clear();
                sprite.figures = SerializerSprite.LoadFromBit(openFileDialog.FileName);
                RefreshBitMap();
            }            
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SerializerSprite.SaveToBit(sprite.figures, saveFileDialog.FileName);
            }
        }

        private void IsFlagLine()
        {
            if (flagLine)
            {
                flagLine = false;
                graph.Clear();
                RefreshBitMap();
            }
        }

        private void toolStripButtonLine_Click(object sender, EventArgs e)
        {
            IsFlagLine();

            toolStripButtonLine.Checked = true;
            toolStripButtonBrokenLine.Checked = false;
            toolStripButtonEllipse.Checked = false;
            toolStripButtonRectangle.Checked = false;
        }       

        private void toolStripButtonBrokenLine_Click(object sender, EventArgs e)
        {
            toolStripButtonLine.Checked = false;
            toolStripButtonBrokenLine.Checked = true;
            toolStripButtonEllipse.Checked = false;
            toolStripButtonRectangle.Checked = false;
        }

        private void toolStripButtonRectangle_Click(object sender, EventArgs e)
        {
            IsFlagLine();

            toolStripButtonLine.Checked = false;
            toolStripButtonBrokenLine.Checked = false;
            toolStripButtonEllipse.Checked = false;
            toolStripButtonRectangle.Checked = true;
        }

        private void toolStripButtonEllipse_Click(object sender, EventArgs e)
        {
            IsFlagLine();

            toolStripButtonLine.Checked = false;
            toolStripButtonBrokenLine.Checked = false;
            toolStripButtonEllipse.Checked = true;
            toolStripButtonRectangle.Checked = false;
        }

        private void toolStripComboBoxWidthPen_DropDownClosed(object sender, EventArgs e)
        {
            currPens.Width = Convert.ToInt32(toolStripComboBoxWidthPen.SelectedItem.ToString());
        }

        private void toolStripButtonFonImage_Click(object sender, EventArgs e)
        {
            if (toolStripButtonFonImage.Checked && openFileDialog.ShowDialog() == DialogResult.OK)
            {
                picture.BackgroundImage = Image.FromFile(openFileDialog.FileName);
            }
            else
            {
                picture.BackgroundImage = null;
            }
        }

        private void FormSprite_SizeChanged(object sender, EventArgs e)
        {
            int widthP = this.Size.Width - widthForm + picture.Size.Width;
            int heightP = this.Size.Height - heightForm + picture.Size.Height;

            if (widthP <= 0) widthP = 1;
            if (heightP <= 0) heightP = 1;

            picture.Size = new Size(widthP, heightP);

            widthForm = this.Size.Width;
            heightForm = this.Size.Height;

            toolStripLabelXW.Text = "xW=" + widthP.ToString();
            toolStripLabelYW.Text = "yW=" + heightP.ToString();

            graph = new Graph(widthP, heightP);
            RefreshBitMap();            
        }

        private void toolStripButtonNet_Click(object sender, EventArgs e)
        {
            if(toolStripButtonNet.Checked == true)
            {
                spriteNet = new Sprite();
                for(int x = stepNet; x < picture.Size.Width; x += stepNet)
                {
                    spriteNet.AddFigure(new MyLine(currPens, x, 0, 0, picture.Size.Height));
                }

                for (int y = stepNet; y < picture.Size.Height; y += stepNet)
                {
                    spriteNet.AddFigure(new MyLine(currPens, 0, y, picture.Size.Width, 0));
                }
            }

            graph.Clear();
            RefreshBitMap();
        }

        private void toolStripComboBoxStepNet_DropDownClosed(object sender, EventArgs e)
        {
            stepNet = Convert.ToInt32(toolStripComboBoxStepNet.SelectedItem.ToString());
        }

        private void toolStripComboBoxShift_DropDownClosed(object sender, EventArgs e)
        {
            stepShift = Convert.ToInt32(toolStripComboBoxShift.SelectedItem.ToString());
        }

        private void toolStripButtonLeft_Click(object sender, EventArgs e)
        {
            if (sprite.figures.Select(x => x.X).Min() - stepShift > 0)  // Если крайняя левая точка изображения больше нуля
            {
                foreach(Figure figure in sprite.figures)    // Проходим по всем фигурам и меняем положение на шаг смещения
                    figure.X -= stepShift;

                graph.Clear();
                RefreshBitMap();
            }            
        }

        private void toolStripButtonRight_Click(object sender, EventArgs e)
        {
            if (sprite.figures.Select(x => x.X).Max() + stepShift < picture.Size.Width)  // Если крайняя правая точка изображения меньше ширины pictureBox
            {
                foreach (Figure figure in sprite.figures)    // Проходим по всем фигурам и меняем положение на шаг смещения
                    figure.X += stepShift;

                graph.Clear();
                RefreshBitMap();
            }
            
        }

        private void toolStripButtonUp_Click(object sender, EventArgs e)
        {
            if (sprite.figures.Select(x => x.Y).Min() - stepShift > 0)  // Если крайняя верхняя точка больше нуля
            {
                foreach (Figure figure in sprite.figures)    // Проходим по всем фигурам и меняем положение на шаг смещения
                    figure.Y -= stepShift;

                graph.Clear();
                RefreshBitMap();
            }          
        }

        private void toolStripButtonDown_Click(object sender, EventArgs e)
        {
            if (sprite.figures.Select(x => x.Y).Max() + stepShift < picture.Size.Height)  // Если крайняя нижняя точка изображения меньше высоты pictureBox
            {
                foreach (Figure figure in sprite.figures)    // Проходим по всем фигурам и меняем положение на шаг смещения
                    figure.Y += stepShift;

                graph.Clear();
                RefreshBitMap();
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа предназначена для рисования спрайтов и сохранения их в специальном формате. Для последующего использования этих спрайтов в минииграх.");
        }            
    }
}
