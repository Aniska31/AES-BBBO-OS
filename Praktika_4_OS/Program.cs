using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Threading;

namespace praktika4
{
  class Program
  {
    static List<Thread> func = new List<Thread>();
    static int kvant = 5000;
    static bool zero, first;//проверка спят ли потоки
    static bool working = true;
    private static ManualResetEvent mre_zero = new ManualResetEvent(false);
    private static ManualResetEvent mre_first = new ManualResetEvent(true);
    private static ManualResetEvent mre_second = new ManualResetEvent(true);
    static Stopwatch timer_0 = new Stopwatch();
    static Stopwatch timer_1 = new Stopwatch();
    static Stopwatch timer_2 = new Stopwatch();
    static void WordCount()//просто переборка слова из 7 слов
    {// работало около 30секунд, sum=578207808

      uint sum = 0;
      string text;
      char[] symbols = new char[7];
      //for (int i = 65; i < 82; i++)//случайная последовательность, нужно лишь чтобы дольше работало
      for (int j = 65; j <= 82; j++)
        for (int k = 65; k <= 82; k++)
          for (int l = 65; l <= 82; l++)
            for (int m = 65; m <= 82; m++)
              for (int n = 65; n <= 82; n++)
              {

                for (int q = 65; q <= 82; q++)
                {
                  mre_first.WaitOne();
                  mre_second.WaitOne();
                  //Console.WriteLine("0 работает");
                  sum++;
                  //symbols[0] = Convert.ToChar(i);
                  symbols[1] = Convert.ToChar(j);
                  symbols[2] = Convert.ToChar(k);
                  symbols[3] = Convert.ToChar(l);
                  symbols[4] = Convert.ToChar(m);
                  symbols[5] = Convert.ToChar(n);
                  symbols[6] = Convert.ToChar(q);
                  text = new string(symbols);
                }
              }
      Console.WriteLine(sum);
    }
    static void NumberCount()//просто переборка чисел из 9 цифр
    {//работает около 60сек, sum=900kk
     //mre_first.WaitOne();

      //Thread.Sleep(Timeout.Infinite);
      uint sum = 0;
      string text;
      char[] symbols = new char[9];
      //for (int i = 49; i <= 57; i++)//опять же просто числа, 48 это 0, а 57 это 9
      // for (int j = 48; j <= 57; j++)
      for (int k = 48; k <= 57; k++)
        for (int l = 48; l <= 57; l++)
          for (int m = 48; m <= 57; m++)
            for (int p = 48; p <= 57; p++)
              for (int s = 48; s <= 57; s++)
                for (int t = 48; t <= 57; t++)
                  for (int q = 48; q <= 57; q++)
                  { mre_zero.WaitOne();
                    mre_second.WaitOne();
                    //Console.WriteLine("1 работает");
                    sum++;
                    //symbols[0] = Convert.ToChar(i);
                    //symbols[1] = Convert.ToChar(j);
                    symbols[2] = Convert.ToChar(k);
                    symbols[3] = Convert.ToChar(l);
                    symbols[4] = Convert.ToChar(m);
                    symbols[5] = Convert.ToChar(p);
                    symbols[6] = Convert.ToChar(s);
                    symbols[7] = Convert.ToChar(t);
                    symbols[8] = Convert.ToChar(q);
                    text = new string(symbols);
                  }
      Console.WriteLine(sum);
    }
    static void Fibonacci()
    {//время около 40сек, a=14553936698768595515
     //mre_second.WaitOne();

      //Thread.Sleep(Timeout.Infinite);
      ulong a = 0;//первый и последний член
      ulong b = 1;//второй и последующий член
      ulong tmp;//хранилище
      ulong n = 10000000;
      for (ulong i = 0; i < n; i++)
      {
        mre_zero.WaitOne();
        mre_first.WaitOne();
        //Console.WriteLine("2 работает");
        tmp = a;
        a = b;
        b += tmp;
      }
      Console.WriteLine(a);
    }

    static void ControlThread_zero()
    {
      char for_check;
      while (func[0].IsAlive || func[1].IsAlive || func[2].IsAlive)//окончание работы при помощи нажатия q
      {
        for_check = Console.ReadKey().KeyChar;
        if (for_check == 'p')//оставовить или восстановить поток
          if (working)
          {
            working = false;
            continue;

          }
          else
          {
            working = true;
            continue;
          }
        if ((for_check == 'q') && ((int)func[0].Priority < 4))// увеличить приоритет потока
        {
          func[0].Priority++;
          continue;
        }
        if ((for_check == 'w') && ((int)func[0].Priority > 0))// уменьшить приоритет потока
        {
          func[0].Priority--;
          continue;
        }
      }
    }
    static void Main()
    {
      func.Add(new Thread(WordCount)); func[0].Name = "WordCount";
      func.Add(new Thread(NumberCount)); func[1].Name = "NumberCount";
      func.Add(new Thread(Fibonacci)); func[2].Name = "Fibonacci";
      for (int i = 0; i < 3; i++)
        func[i].Start();
      zero = false; first = true;
      timer_0.Start();
      Thread.Sleep(kvant);
      while (func[0].IsAlive || func[1].IsAlive || func[2].IsAlive)//проверка на существование потоков
      {
        if (((int)func[0].Priority == 2))//потоки равны
        {
          if (working)//должен ли работать 0ой поток
            if (!zero)//0 засыпает, зарабатывает 1
            {
              zero = true;
              mre_zero.Set();
              timer_0.Stop();
              mre_first.Reset();
              timer_1.Start();
              first = false;
              Console.WriteLine("0 заснул, а 1 заработал");
              Console.WriteLine($"{timer_0.ElapsedMilliseconds}");
              Thread.Sleep(kvant);
              continue;
            }
            else if (!first)//1 засыпает, 2 зарабатывает
            {

              first = true;
              mre_first.Set();
              timer_1.Stop();
              mre_second.Reset();
              timer_2.Start();
              Console.WriteLine("1 заснул, а 2 заработал");
              Console.WriteLine($"{timer_1.ElapsedMilliseconds}");
              Thread.Sleep(kvant);
              continue;
            }
            else//2 засыпает, 0 зарабатывает
            {

              mre_second.Set();
              timer_2.Stop();
              mre_zero.Reset();
              timer_0.Start();
              zero = false;
              Console.WriteLine("2 заснул, а 0 заработал");
              Console.WriteLine($"{timer_2.ElapsedMilliseconds}");
              Thread.Sleep(kvant);
              continue;
            }
          else//потоки равны, но 0 не должен работать искусственно
        if (!first)//1 засыпает, 2 зарабатывает
          {

            first = true;
            mre_first.Set();
            timer_1.Stop();
            mre_second.Reset();
            timer_2.Start();
            Console.WriteLine("1 заснул, а 2 заработал");
            Console.WriteLine($"{timer_1.ElapsedMilliseconds}");
            Thread.Sleep(kvant);
            continue;
          }
          else//2 засыпает, 1 зарабатывает
          {

            mre_second.Set();
            timer_2.Stop();
            mre_first.Reset();
            timer_1.Start();
            first = false;
            Console.WriteLine("2 заснул, а 1 заработал");
            Console.WriteLine($"{timer_2.ElapsedMilliseconds}");
            Thread.Sleep(kvant);
            continue;
          }
        }
        if ((int)func[0].Priority >2)//поток 0 значимее, он работает один
        {
          if (working)//должен ли работать 0ой поток
          {
            mre_first.Reset();
            timer_0.Start();
            Console.WriteLine("работает только 0");
            Console.WriteLine($"{timer_0.ElapsedMilliseconds}");
            Thread.Sleep(kvant);
            continue;
          }
          else//никто не работает
          {
            Console.WriteLine("никто не работает, т.к. 0 выделены все ресурсы, а он отдыхает");
            Thread.Sleep(kvant);
            continue;
          }
        }
        if ((int)func[0].Priority < 2)//поток 0 менее важен, работают только 1 и 2
        {
          if (!first)//1 засыпает, 2 зарабатывает
          {

            first = true;
            mre_first.Set();
            timer_1.Stop();
            mre_second.Reset();
            timer_2.Start();
            Console.WriteLine("1 заснул, а 2 заработал");
            Console.WriteLine($"{timer_1.ElapsedMilliseconds}");
            Thread.Sleep(kvant);
            continue;
          }
          else//2 засыпает, 1 зарабатывает
          {

            mre_second.Set();
            timer_2.Stop();
            mre_first.Reset();
            timer_1.Start();
            first = false;
            Console.WriteLine("2 заснул, а 1 заработал");
            Console.WriteLine($"{timer_2.ElapsedMilliseconds}");
            Thread.Sleep(kvant);
            continue;
          }
        }
      }
    }
  }
}
