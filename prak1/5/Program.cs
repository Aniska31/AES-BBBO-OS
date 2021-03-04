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

       //Добавить файл в архив
       Console.WriteLine("Введите название файла,который хотите внести в архив:");
       string subzip = Console.ReadLine();
       File.Open(@"test\"+subzip, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
       zip.AddFile(@"test\" + subzip,"");
       zip.Save();
       Console.WriteLine("Файл был добавлен в архив.\n");

      //Разархивировать файл и вывести данные о нем
      zip.ExtractAll(@"test\more test\");
      Console.WriteLine("Разархивация прошла успешно.");
      DirectoryInfo di = new DirectoryInfo(@"test\more test\");
      FileInfo[] fiArr = di.GetFiles();
      foreach (FileInfo f in fiArr)
        Console.WriteLine($"Разархивированный файл:\nИмя:{f.Name}\nРазмер:{f.Length}\nНахождение:{f.FullName}");


      //Удалить файл и архив

    }
  }
}
