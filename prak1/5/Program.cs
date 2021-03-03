/*Создание zip архива, добавление туда файла, определение размера архива
Создать архив в форматер zip
Добавить файл в архив
Разархивировать файл и вывести данные о нем
Удалить файл и архив*/
using System;
using System.IO;
//using System.IO.Compression;
using  Ionic.Zip;

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
       Console.WriteLine("Файл был добавлен в архив.");

      //Разархивировать файл и вывести данные о нем
      zip.ExtractAll(@"test\more test\");
      Console.WriteLine("Разархивация прошла успешно:");
    }
  }
}
