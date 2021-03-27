using System;
using System.Threading;
using System.Collections.Generic;

namespace prak3
{

  class Program
  {
    static List<Int32> goods = new List<Int32>();//  массив производителей
    static List<Int32> uses = new List<Int32>();//массив  потребителей
    static bool check = true;
    static void Seller(object i)
    {
      bool sleeping = false;
      int number = (int)i;
      int value;
      Random rnd = new Random();
      while (check)
        if (goods.Count >= 100)
        {
          Console.WriteLine($"Продавец {number} спит.");
          Thread.Sleep(200);
          sleeping = true;
        }
        else
          if ((goods.Count <= 80)&&(sleeping))
          {
            sleeping = false;
            value = rnd.Next(1, 100);
            goods.Add(value);
            Console.WriteLine($"Продавец {number} произвел число {value}");
            Thread.Sleep(0);
          }
          else
            {
              value = rnd.Next(1, 100);
              goods.Add(value);
              Console.WriteLine($"Продавец {number} произвел число {value}");
              Thread.Sleep(0);
            }
      Console.WriteLine($"Продавец {number} ушел на покой.");
    }
    static void Buyer(object j)
    {
      int value;
      int number = (int)j;
      while ((goods.Count != 0) || (check))
        if (goods.Count == 0)
        {
          Console.WriteLine($"Покупатель {number} спит.");
          Thread.Sleep(500);
        }
        else
        {
          value = goods[0];
          uses.Add(value);
          goods.RemoveAt(0);
          Console.WriteLine($"Покупатель {number} извлек число {value}");
          Thread.Sleep(0);
        }
      Console.WriteLine($"Покупатель {number} ушел на отдых.");
    }
    static void Main()
    {
      char for_check;
      List<Thread> sell = new List<Thread>();
      List<Thread> buy = new List<Thread>();
      for (int i = 0; i < 3; i++)
      {
        sell.Add(new Thread(new ParameterizedThreadStart(Seller)));
        sell[i].Start(i);
      }
      for (int j = 0; j < 2; j++)
      {
        buy.Add(new Thread(new ParameterizedThreadStart(Buyer)));
        buy[j].Start(j);
      }
      while (check)
      {
        for_check = Console.ReadKey().KeyChar;
        if (for_check == 'q')
          check = false;
      }
    }
  }
}