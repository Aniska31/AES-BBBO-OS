using System;
using System.IO;
using System.Threading;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace prak2
{
  class Program
  {
    static string[] passwords_all = { "", "", "" };
    static int Menu()
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
      return choise;
    }

    static string[] SHA_read()
    {
      switch (Menu())
      {
        case 1:
        {
            string[] SHA = { "", "", "" };
            for (int i = 0; i < 3; i++)
            {
              Console.WriteLine($"Введите SHA {i+1}:");
              SHA[i] = Console.ReadLine();
            }
            return SHA;
        }
        case 2:
        { 
          string name = "SHA.txt";
          if (!(File.Exists(name)))
          {
            FileStream fstream = new FileStream(name, FileMode.Create);
            string[] SHA_file = {
              "1115dd800feaacefdf481f1f9070374a2a81e27880f187396db67958b207cbad", Environment.NewLine,
              "3a7bd3e2360a3d29eea436fcfb7e44c735d117c42d1c1835420b6b9942dd4f1b", Environment.NewLine,
              "74e1bb62f8dabb8125a58852b63bdf6eaef667cb56ac7f7cdba6d7305c50a22f" };
            foreach (string text in SHA_file)
            {
              byte[] array_file = System.Text.Encoding.Default.GetBytes(text);
              fstream.Write(array_file, 0, array_file.Length);
            }
            fstream.Close();
          }
          FileStream fs = new FileStream(name, FileMode.Open);
          string[] SHA = { "", "", "" };
          string copy;
          byte[] array = new byte[66 * 3];// 64 - sha, 1 \n
          fs.Read(array, 0, array.Length);
          copy = System.Text.Encoding.Default.GetString(array);
          for (int i = 0; i < 3; i++)
            SHA[i] = copy.Substring(66 * i, 64);
          return SHA;
        }
        default:
        {
            string[] SHA = { "error" };
            Console.WriteLine("Ошибка.");
            return SHA;
        }
      }
    }

    public static string GetHash(HashAlgorithm hashAlgorithm, string input)
    {
      byte[] data = hashAlgorithm.ComputeHash(System.Text.Encoding.ASCII.GetBytes(input));
      var sBuilder = new StringBuilder();
      for (int i = 0; i < data.Length; i++)
      {
        sBuilder.Append(data[i].ToString("x2"));
      }
      return sBuilder.ToString();
    }

    public static void GetPassword(object obj)
     {
      Count c = (Count)obj;
      string[] SHA_all = c.SHA_all;
      SHA256 SHA = SHA256.Create();
      string hash;
      string password;
      char[] symbols = new char[5];
      //for (int i = 97; (i <= 122); i++) //97 a, 122 z
      for (int i = 97+c.number*c.tries; (i <= 122)&&(i< 97 + c.number*c.tries+c.tries); i++)
        for (int j = 97; (j <= 122); j++)
          for (int k = 97; (k <= 122); k++)
            for (int l = 97; (l <= 122); l++)
              for (int m = 97; (m <= 122); m++)
              {
                symbols[0] = Convert.ToChar(i);
                symbols[1] = Convert.ToChar(j);
                symbols[2] = Convert.ToChar(k);
                symbols[3] = Convert.ToChar(l);
                symbols[4] = Convert.ToChar(m);
                password = new string(symbols);
                hash = GetHash(SHA, password);
                Console.WriteLine(password);//
                for (int h = 0; h < 3; h++)
                  if (SHA_all[h] == hash)
                    //Console.WriteLine(password);
                    passwords_all[h] = password;
                Thread.Sleep(0);
              }
     }

    public static void WritePassword()
    {
      Console.ReadKey();
      Console.WriteLine("Полученные пароли:");
      for (int f = 0; f < 3; f++)
        Console.WriteLine(passwords_all[f]);
      Console.WriteLine("Вот и все!");
      Console.ReadKey();
    }

    public class Count
    {
      public int number;
      public int tries;
      public string[] SHA_all;
      public Count() { }
    }

    static void Main()
    {
      List<Thread> threads = new List<Thread>();
      List<Count> c_th = new List<Count>();
      string[] SHA_all;
      SHA_all = SHA_read();
      int count;
      Console.WriteLine("Введите количество потоков(до 26):");
      count = Int32.Parse(Console.ReadLine());
      if (count >= 13)
      {
        for (int a = 0; a < count; a++)
        {
          threads.Add(new Thread(new ParameterizedThreadStart(GetPassword)));
          c_th.Add(new Count());
          c_th[a].SHA_all = SHA_all;
          c_th[a].number = a;
          if (a < 26 - count)
            c_th[a].tries = 2;
          else
            c_th[a].tries = 1;
          threads[a].Start(c_th[a]);
        }
        /*for (int a = 0; a < 26 - count; a++)
          threads[a].Start(c_th[a]);
        for(int a=26-count; a<count ;a++)
          threads[a].Start(c_th[a]);*/
      }
      /*Thread th = new Thread(new ThreadStart(WritePassword));
      th.Priority = 0;
      th.Start();*/
      WritePassword();
    }
  }
}
