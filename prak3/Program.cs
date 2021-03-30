using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;

namespace prak3
{

  class Program
  {
    static List<Int32> goods = new List<Int32>();//  массив производителей
    static List<Int32> uses = new List<Int32>();//массив  потребителей
    static List<Int32> control = new List<Int32>();//дубликат производителя
    static bool check = true;
    //static string S_txt = "Sellers.txt"; static StreamWriter S;
    //static string B_txt= "Buyers.txt"; static StreamWriter B;
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
          sleeping = true;
          Thread.Sleep(1200);
          continue;
        }
        else
          if ((goods.Count <= 80)&&(sleeping))
          {
            Console.WriteLine($"Продавец {number} проснулся.");
            sleeping = false;
            value = rnd.Next(1, 100);
            goods.Add(value);
            control.Add(value);  
            //S.WriteLine(value);
            Console.WriteLine($"Продавец {number} произвел число {value}");
            //Thread.Sleep(0);
            continue;
          }
          else
            {
              value = rnd.Next(1, 100);
              goods.Add(value);
               control.Add(value);   
             // S.WriteLine(value);
              Console.WriteLine($"Продавец {number} произвел число {value}");
              //Thread.Sleep(0);
              continue;
            }
      Console.WriteLine($"Продавец {number} ушел на покой.");
    }
    static void Buyer(object j)
    {
      int value;
      int number = (int)j;
      bool sleeping = false;
      while ((goods.Count != 0) || (check))
        if (goods.Count == 0)
        {
          Console.WriteLine($"Покупатель {number} спит.");
          sleeping = true;
          Thread.Sleep(500);
          continue;
        }
        else
          if (sleeping)
        {
          Console.WriteLine($"Покупатель {number} проснулся.");
          sleeping = false;
          value = goods[0];
          uses.Add(value);
          goods.RemoveAt(0);
          //B.WriteLine(value);
          Console.WriteLine($"Покупатель {number} извлек число {value}");
         //Thread.Sleep(0);
          continue;
        }
        else
        {
          value = goods[0];
          uses.Add(value);
          goods.RemoveAt(0);
          //B.WriteLine(value);
          Console.WriteLine($"Покупатель {number} извлек число {value}");
          //Thread.Sleep(0);
          continue;
        }
      Console.WriteLine($"Покупатель {number} ушел на отдых.");
    }
    static void Main()
    {
      char for_check;
      bool check_alive = true;
      List<Thread> sell = new List<Thread>();
      List<Thread> buy = new List<Thread>();
      //S = new StreamWriter(S_txt, false, System.Text.Encoding.Default);
      //B = new StreamWriter(B_txt, false, System.Text.Encoding.Default);
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
      while (check)//окончание работы при помощи нажатия q
      {
        for_check = Console.ReadKey().KeyChar;
        if (for_check == 'q')
          check = false;
      }
      while (check_alive)//работают ли потоки
        if (!(sell[0].IsAlive || sell[1].IsAlive || sell[2].IsAlive || buy[0].IsAlive || buy[1].IsAlive))
        {
          check_alive = false;
          uses.Sort();
          control.Sort();
          bool correct = true;
          if (uses.Count == control.Count)//проверка на целостность
          {
            for (int i = 0; i < uses.Count; i++)
              if (uses[i] != control[i])
              {
                Console.WriteLine("Есть расхождения. Что-то пошло не так.");
                correct = false;
                break;
              }
            if (correct)
              Console.WriteLine("Вроде бы все работает");
          }
          else
            Console.WriteLine("В них даже количество элементов не совпадает. Явно что-то не то.");
        }
    }
  }
}