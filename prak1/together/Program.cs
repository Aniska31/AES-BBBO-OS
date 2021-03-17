using System;
using System.IO;
using System.Collections;
using Ionic.Zip;
using System.Xml.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace together
{
  [Serializable]
  class Person
  {
    public string Name { get; set; }
    public string Age { get; set; }

    public Person(string name, string age)
    {
      Name = name;
      Age = age;
    }
    public Person() { }
  }
  class Program
  {
    /*Вывести информацию в консоль о логических дисках, именах, метке тома, размере типе файловой системы*/
    static void Disks()
    {
      {
        DriveInfo[] drives = DriveInfo.GetDrives();
        foreach (DriveInfo drive in drives)
        {
          Console.WriteLine($"Название: {drive.Name}");
          Console.WriteLine($"Тип: {drive.DriveType}");
          if (drive.IsReady)
          {
            Console.WriteLine($"Объем диска: {drive.TotalSize}");
            Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
            Console.WriteLine($"Метка: {drive.VolumeLabel}");
          }
          Console.WriteLine();
        }
        Console.ReadKey();
      }
    }

    /*2.	Работа  с файлами ( класс File, FileInfo, FileStream и другие)
a)	Создать файл
b)	Записать в файл строку
c)	Прочитать файл в консоль
d)	Удалить файл*/
    static void Files()
    {
      //Создать файл
      Console.WriteLine("Введите название файла(вместе с расширением):");
      string name = Console.ReadLine();
      FileStream fs = File.Create(name);
      Console.WriteLine("Файл был создан.\n");
      Console.ReadKey();

      //Записать в файл строку
      Console.WriteLine("Введите нужную информацию:");
      string text = Console.ReadLine();
      byte[] info = new UTF8Encoding(true).GetBytes(text);
      fs.Write(info, 0, info.Length);
      fs.Close();
      Console.WriteLine("Строка была записана в файл.\n");
      Console.ReadKey();

      //Прочитать файл в консоль
      Console.WriteLine("Сейчас будет считка информации:");
      StreamReader sr = File.OpenText(name);
      string s = "";
      while ((s = sr.ReadLine()) != null)
      {
        Console.WriteLine(s);
      }
      sr.Close();
      Console.WriteLine("Файл прочитан.\n");
      Console.ReadKey();

      //Удалить файл
      Console.WriteLine("Вы точно уверены, что хотите удалить? (1 - yes, 0 - no):");
      string answer = Console.ReadLine();
      if (answer != "0")
      {
        File.Delete(name);
        Console.WriteLine("Удаление произошло успешно.");
      }
      else
        Console.WriteLine("Удаление отменено");
      Console.ReadKey();
    }

    /*3.	Работа с форматом JSON
a)	Создать файл формате JSON  из редактора 
b)	Создать новый объект. Выполнить сериализацию объекта в формате JSON и записать в файл.
c)	Прочитать файл в консоль
d)	Удалить файл*/
    static async void JSON()
    {
      //Создать файл формате JSON  из редактора
      Console.WriteLine("Введите название файла для формата JSON:");
      string name = Console.ReadLine();
      FileStream fs = new FileStream($"{name}.json", FileMode.Create);
      Console.WriteLine("Файл был создан.\n");
      Console.ReadKey();

      //Создать новый объект. Выполнить сериализацию объекта в формате JSON и записать в файл
      Console.WriteLine("Дан шаблон работы.");
      Console.WriteLine("Введите имя пользователя:");
      string n = Console.ReadLine();
      Console.WriteLine("Введите возраст пользователя:");
      string a = Console.ReadLine();
      Person tom = new Person() { Name = n, Age = a };
      await JsonSerializer.SerializeAsync<Person>(fs, tom);
      Console.WriteLine("Данные были сохранены в файл.\n");
      Console.ReadKey();

      //Прочитать файл в консоль
      Console.WriteLine("Читка:");
      fs.Seek(0, SeekOrigin.Begin);
      Person restoredPerson = await JsonSerializer.DeserializeAsync<Person>(fs);
      Console.WriteLine($"Имя: {restoredPerson.Name}  Возраст: {restoredPerson.Age}");
      Console.WriteLine("Файл был прочитан.\n");
      Console.ReadKey();

      //Удалить файл
      fs.Close();
      Console.WriteLine("Вы точно уверены, что хотите удалить? (1 - yes, 0 - no):");
      string answer = Console.ReadLine();
      if (answer != "0")
      {
        File.Delete($"{name}.json");
        Console.WriteLine("Удаление произошло успешно.");
      }
      else
        Console.WriteLine("Удаление отменено");
      Console.ReadKey();
    }

    /*4.	Работа с форматом XML
a)	Создать файл формате XML  из редактора
b)	Записать в файл новые данные из консоли .
c)	Прочитать файл в консоль.
d)	Удалить файл.*/
    static void XML()
    {
      //Создать файл формате XML  из редактора
      Console.WriteLine("Введите название файла:");
      string doc_name = Console.ReadLine();
      XDocument market = new XDocument();
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

    /*Создание zip архива, добавление туда файла, определение размера архива
Создать архив в форматер zip
Добавить файл в архив
Разархивировать файл и вывести данные о нем
Удалить файл и архив*/
    static void ZIP()
    {
      //Создать архив в форматер zip
      DirectoryInfo dirInfo = new DirectoryInfo(@"test");
      if (!dirInfo.Exists)
      {
        dirInfo.Create();
      }
      Console.WriteLine("Введите название архива:");
      string nameZip = Console.ReadLine();
      nameZip += ".rar";
      ZipFile zip = new ZipFile(@"test\" + nameZip);
      zip.Save();
      Console.WriteLine("Архив был создан\n");
      Console.ReadKey();

      //Добавить файл в архив
      Console.WriteLine("Введите название файла,который хотите внести в архив:");
      string subzip = Console.ReadLine();
      FileStream fs = File.Create(@"test\" + subzip);
      fs.Close();
      zip.AddFile(@"test\" + subzip, "");
      zip.Save();
      Console.WriteLine("Файл был добавлен в архив.\n");
      Console.ReadKey();

      //Разархивировать файл и вывести данные о нем
      zip.ExtractAll(@"test\more test\");
      Console.WriteLine("Разархивация прошла успешно.");
      DirectoryInfo di = new DirectoryInfo(@"test\more test\");
      FileInfo[] fiArr = di.GetFiles();
      foreach (FileInfo f in fiArr)
        Console.WriteLine($"Разархивированный файл:\nИмя:{f.Name}\nРазмер:{f.Length}\nНахождение:{f.FullName}\n");
      Console.ReadKey();


      //Удалить файл и архив
      Console.WriteLine("Вы точно уверены, что хотите удалить? (1 - yes, 0 - no)");
      string answer = Console.ReadLine();
      if (answer != "0")
      {
        zip.Dispose();
        File.Delete(@"test\" + nameZip);
        File.Delete(@"test\" + subzip);
        File.Delete($@"test\more test\{subzip}");
        Console.WriteLine("Удаление произошло успешно.");
      }
      else
        Console.WriteLine("Удаление отменено");
      Console.ReadKey();
    }

    static int Menu()
    {
      Console.WriteLine
        (
        "1. Про диски\n" +
        "2. Про файлы\n" +
        "3. Про JSON\n" +
        "4. Про XML\n" +
        "5. Про Zip\n" +
        "0. Выход\n\n" +
        "Введите действие:"
        );
      int choise;
      string s;
      choise = -1;
      s = Console.ReadLine();
      while ((choise > 5) || (choise < 0))
      {
        while (!(Int32.TryParse(s, out choise)))
        {
          Console.WriteLine("Вы ввели не цифру или несуществующий пункт. Попробуйте еще раз:");
          s = Console.ReadLine();
        }
        choise=Int32.Parse(s);
      }
      return choise;
    }
    static void Main(string[] args)
    {
      bool check = true;
      while (check)
        switch(Menu())
        {
          case 1:
          {
              Disks();
              Console.Clear();
              break;
          }
          case 2:
          {
            Files();
            Console.Clear();
            break;
          }
          case 3:
          {
            JSON();
            Console.Clear();
            break;
          }
          case 4:
          {
            XML();
            Console.Clear();
            break;
          }
          case 5:
          {
            ZIP();
            Console.Clear();
            break;
          }
          case 0:
          {
              check = false;
              Console.WriteLine("Спасибо, до свидания!");
              break;
          }
        }

    }
  }
}
