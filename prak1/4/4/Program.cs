/*4.	Работа с форматом XML
a)	Создать файл формате XML  из редактора
b)	Записать в файл новые данные из консоли .
c)	Прочитать файл в консоль.
d)	Удалить файл.
*/
using System;
using System.IO;
using System.Xml.Linq;

namespace _4
{
  class Program
  {
    static void Main(string[] args)
    {
      //Создать файл формате XML  из редактора
      Console.WriteLine("Введите название файла:");
      string doc_name = Console.ReadLine();
      XDocument market  = new XDocument();
      XElement things = new XElement("things");
      XElement frut = new XElement("fruit");
      XElement veg = new XElement("veg");
      Console.ReadKey();

      //Записать в файл новые данные из консоли
      Console.WriteLine("\nВведите название фрукта:");
      string f_name = Console.ReadLine();
      XAttribute frutNameAttr = new XAttribute("name", f_name);
      Console.WriteLine("Введите название страны-импортера:");
      string f_coun = Console.ReadLine();
      XElement frutCountryElem = new XElement("country", f_coun);
      Console.WriteLine("Введите цену за килограмм:");
      string f_price = Console.ReadLine();
      XElement frutPriceElem = new XElement("price", f_price);
      frut.Add(frutNameAttr);
      frut.Add(frutCountryElem);
      frut.Add(frutPriceElem);
      Console.WriteLine("Введите название овоща:");
      string v_name = Console.ReadLine();
      XAttribute vegNameAttr = new XAttribute("name", v_name);
      Console.WriteLine("Введите название страны-импортера:");
      string v_coun = Console.ReadLine();
      XElement vegCountryElem = new XElement("country", v_coun);
      Console.WriteLine("Введите цену за килограмм:");
      string v_price = Console.ReadLine();
      XElement vegPriceElem = new XElement("price", v_price);
      veg.Add(vegNameAttr);
      veg.Add(vegCountryElem);
      veg.Add(vegPriceElem);
      things.Add(frut);
      things.Add(veg);
      market.Add(things);
      market.Save(doc_name);
      Console.ReadKey();

      //Прочитать файл в консоль
      XDocument xdoc = XDocument.Load(doc_name);
      Console.WriteLine("\nВывод фруктов:");
      foreach (XElement thingElement in xdoc.Element("things").Elements("fruit"))
      {
        XAttribute nameAttribute = thingElement.Attribute("name");
        XElement companyElement = thingElement.Element("country");
        XElement priceElement = thingElement.Element("price");

        if (nameAttribute != null && companyElement != null && priceElement != null)
        {
          Console.WriteLine($"Фрукт: {nameAttribute.Value}");
          Console.WriteLine($"Страна-импортер: {companyElement.Value}");
          Console.WriteLine($"Цена: {priceElement.Value}");
        }
      }
      Console.WriteLine("Вывод овощей:");
      foreach (XElement thingElement in xdoc.Element("things").Elements("veg"))
      {
        XAttribute nameAttribute = thingElement.Attribute("name");
        XElement companyElement = thingElement.Element("country");
        XElement priceElement = thingElement.Element("price");

        if (nameAttribute != null && companyElement != null && priceElement != null)
        {
          Console.WriteLine($"Овощ: {nameAttribute.Value}");
          Console.WriteLine($"Страна-импортер: {companyElement.Value}");
          Console.WriteLine($"Цена: {priceElement.Value}");
        }
      }
      Console.ReadKey();

      //Удалить файл
      Console.WriteLine("\nВы точно уверены, что хотите удалить? (1 - yes, 0 - no)");
      string answer = Console.ReadLine();
      if (answer != "0")
      {
        File.Delete(doc_name);
        Console.WriteLine("Удаление произошло успешно.");
      }
      else
        Console.WriteLine("Удаление отменено");
      Console.ReadKey();
    }
  }
}
