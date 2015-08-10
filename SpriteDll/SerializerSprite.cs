using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace DataSprite
{
    public class SerializerSprite
    {
        /// <summary>
        /// Сериализует спрайт в xml файл
        /// </summary>
        /// <param name="figures">Список фигур</param>
        /// <param name="filename">Имя файла</param>
        static public void SaveToXml(List<Figure> figures, string filename)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(Sprite));

            using (StreamWriter sw = new StreamWriter(filename))
            {
                xsSubmit.Serialize(sw, figures);
            }
        }

        /// <summary>
        /// Десериализует спрайт из xml файла
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <returns></returns>
        static public List<Figure> LoadFromXml(string filename)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(Sprite));

            using (StreamReader sr = new StreamReader(filename))
            {
                List<Figure> figures = (List<Figure>)xsSubmit.Deserialize(sr);
                return figures;
            }
        }

        /// <summary>
        /// Сереализует спрайт в бинарный файл
        /// </summary>
        /// <param name="figures">Список фигур</param>
        /// <param name="filename">Имя файла</param>
        static public void SaveToBit(List<Figure> figures, string filename)
        {
            BinaryFormatter xsSubmit = new BinaryFormatter();

            using(FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                xsSubmit.Serialize(fs, figures);
            }
        }

        /// <summary>
        /// Десериализует спрайт из бинарного файла
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <returns></returns>
        static public List<Figure> LoadFromBit(string filename)
        {
            BinaryFormatter xsSubmit = new BinaryFormatter();

            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                List<Figure> figures = (List<Figure>)xsSubmit.Deserialize(fs);
                return figures;
            }
        }
    }
}
