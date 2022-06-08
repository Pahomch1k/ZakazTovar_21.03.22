using System;
using static System.Console;
using System.Xml;
using System.Text;


namespace ZakazTovar
{
    class Program
    {
        static void Main(string[] args)
        {
            XML xml = new XML();

            int choise = 0;
            int flag = 0;

            while (flag == 0)
            {
                WriteLine("1. Добавть заказ\n2. Почитать заказ\n3. Выход");
                choise = Convert.ToInt32(ReadLine());

                switch (choise)
                {
                    case 1: xml.CreateXml(); break;
                    case 2: xml.ReadXml(); break; 
                    case 3: flag++; break;
                    default: WriteLine("try again"); break;
                }
            }
        }
    }

    class XML
    {
        static int Count = 1;
        string x { get; set; } 

        string[] Element = new string[5] { "Name", "Creator", "Harack", "Price", "InfoCredit" }; 

        public void CreateXml()
        {
            Write("Сколько товаров ?");
            int y;
            y = Convert.ToInt32(ReadLine());
            if (y < 1) Write("error");
            else
            {
                XmlTextWriter xmlwriter = new XmlTextWriter("../../Zakaz_" + Count + ".xml", Encoding.UTF8);
                xmlwriter.WriteStartDocument();

                // Formatting определяет способ форматирования выходных данных
                xmlwriter.Formatting = Formatting.Indented;
                xmlwriter.IndentChar = '\t';
                xmlwriter.Indentation = 1;

                xmlwriter.WriteStartElement("Zakaz");

                WriteLine("От кого заказ ?");
                x = ReadLine();
                xmlwriter.WriteStartElement(x);
                Count++; 
                 
                for (int i = 0; i < y; i++)
                {
                    xmlwriter.WriteStartElement("Tovar" + (i+1));

                    for (int i1 = 0; i1 < 5; i1++)
                    {
                        xmlwriter.WriteStartElement(Element[i1]);
                        Write(Element[i1] + "? ");
                        x = ReadLine();
                        xmlwriter.WriteString(x);
                        xmlwriter.WriteEndElement();
                    }
                    xmlwriter.WriteEndElement(); 
                } 
                xmlwriter.WriteEndElement();
                xmlwriter.WriteEndElement();

                WriteLine("Данные сохранены в XML-файл");
                xmlwriter.Close();
            } 
        }

        public void ReadXml()
        {
            Write("Номер заказа? ");
            int x;
            x = Convert.ToInt32(ReadLine());

            XmlTextReader reader = new XmlTextReader("../../Zakaz_" + x + ".xml");
            string str = null;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                    str += reader.Value + "\n";

                if (reader.NodeType == XmlNodeType.Element)
                    if (reader.HasAttributes)
                        while (reader.MoveToNextAttribute())
                            str += reader.Value + "\n";
            }
            WriteLine(str);
            reader.Close();
        } 
    }
}
