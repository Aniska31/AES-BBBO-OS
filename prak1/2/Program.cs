/*2.	Работа  с файлами ( класс File, FileInfo, FileStream и другие)
a)	Создать файл
b)	Записать в файл строку
c)	Прочитать файл в консоль
d)	Удалить файл*/

using System;
using System.IO;
using System.Text;

namespace _2
{
  class Program
  {
    static void Main(string[] args)
    {
      //Создать файл
      Console.WriteLine("Введите название файла(вместе с расширением):");
      string name = Console.ReadLine();
      FileStream fs=File.Create(name);
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
  }
}
