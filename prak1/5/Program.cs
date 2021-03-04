/*Создание zip архива, добавление туда файла, определение размера архива
Создать архив в форматер zip
Добавить файл в архив
Разархивировать файл и вывести данные о нем
Удалить файл и архив*/
using System;
using System.IO;
using System.Collections;
using Ionic.Zip;

namespace _5
{
  class Program
  {
    static void Main(string[] args)
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
       ZipFile zip = new ZipFile(@"test\"+ nameZip);
       zip.Save();
       Console.WriteLine("Архив был создан\n");
       Console.ReadKey();

      //Добавить файл в архив
      Console.WriteLine("Введите название файла,который хотите внести в архив:");
       string subzip = Console.ReadLine();
       FileStream fs= File.Create(@"test\" + subzip);
       fs.Close();
       zip.AddFile(@"test\" + subzip,"");
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
  }
}
