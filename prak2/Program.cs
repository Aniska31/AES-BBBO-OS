using System;
using System.IO;
using System.Threading.Tasks;

namespace prak2
{
  class Program
  {
    static async Task Main(string[] args)
    {
      Console.WriteLine
      (
        "1. Вписать SHA вручную.\n" +
        "2. Достать SHA из файла.\n" +
        "Выполните действие:"
      );
      int choise;
      string s;
      choise = -1;
      while ((choise > 2) || (choise < 1))
      {
        s = Console.ReadLine();
        while (!(Int32.TryParse(s, out choise)))
        {
          Console.WriteLine("Вы ввели не цифру. Попробуйте еще раз:");
          s = Console.ReadLine();
        }
        choise = Int32.Parse(s);
        if ((choise > 2) || (choise < 1))
          Console.WriteLine("Вы ввели несуществующий пункт. Попробуйте еще раз:");
      }

      string name = "SHA.txt";
      if(!(File.Exists(name)))
        using (FileStream fstream = new FileStream(name, FileMode.Create))
        {
          string[] SHA = { "1115dd800feaacefdf481f1f9070374a2a81e27880f187396db67958b207cbad", Environment.NewLine, "3a7bd3e2360a3d29eea436fcfb7e44c735d117c42d1c1835420b6b9942dd4f1b", Environment.NewLine, "74e1bb62f8dabb8125a58852b63bdf6eaef667cb56ac7f7cdba6d7305c50a22f" };
            foreach (string text in SHA)
            {
              byte[] array = System.Text.Encoding.Default.GetBytes(text);
              // асинхронная запись массива байтов в файл
              await fstream.WriteAsync(array, 0, array.Length);
            }
        }

    }
  }
}
