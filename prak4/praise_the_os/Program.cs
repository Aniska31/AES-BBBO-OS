using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Threading;

namespace praise_the_os
{
  class Program
  {
    static List<Thread> func = new List<Thread>();
    static int kvant = 7000;
    static bool zero,first,second;//проверка спят ли потоки
    static void WordCount()//просто переборка слова из 7 слов
    {// работало около 30секунд, sum=578207808
      uint sum = 0;
      string text;
      char[] symbols = new char[7];
      for (int i = 65; i < 82; i++)//случайная последовательность, нужно лишь чтобы дольше работало
        for (int j = 65; j <= 82; j++)
          for (int k = 65; k <= 82; k++)
            for (int l = 65; l <= 82; l++)
              for (int m = 65; m <= 82; m++)
                for (int n = 65; n <= 82; n++)
                  for (int q = 65; q <= 82; q++)
                  {
                    sum++;
                    symbols[0] = Convert.ToChar(i);
                    symbols[1] = Convert.ToChar(j);
                    symbols[2] = Convert.ToChar(k);
                    symbols[3] = Convert.ToChar(l);
                    symbols[4] = Convert.ToChar(m);
                    symbols[5] = Convert.ToChar(n);
                    symbols[6] = Convert.ToChar(q);
                    text = new string(symbols);
                  }
      Console.WriteLine(sum);
    }
    static void NumberCount()//просто переборка чисел из 9 цифр
    {//работает около 60сек, sum=900kk
      Thread.Sleep(Timeout.Infinite);
      uint sum = 0;
      string text;
      char[] symbols = new char[9];
      for(int i=49;i<=57;i++)//опять же просто числа, 48 это 0, а 57 это 9
        for (int j = 48; j <= 57; j++)
          for (int k = 48; k <= 57; k++)
            for (int l = 48; l <= 57; l++)
              for (int m = 48; m <= 57; m++)
                for (int p = 48; p <= 57; p++)
                  for (int s = 48; s <= 57; s++)
                    for (int t = 48; t <= 57; t++)
                      for (int q = 48; q <= 57; q++)
                        {
                          sum++;
                          symbols[0] = Convert.ToChar(i);
                          symbols[1] = Convert.ToChar(j);
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
      Thread.Sleep(Timeout.Infinite);
      ulong a = 0;//первый и последний член
      ulong b = 1;//второй и последующий член
      ulong tmp;//хранилище
      ulong n = 10000000000;
      for (ulong i = 0; i < n; i++)
      {
        tmp = a;
        a = b;
        b += tmp;
      }
      Console.WriteLine(a);
    }

    /*static void Factorial()//слишком быстро,меньше секунды работает
    {
      int n = 5;
      var factorial = new BigInteger(1);
      for (int i = 1; i <= n; i++)
        factorial *= i;
      Console.WriteLine(factorial);
    }*/
    [Obsolete]
    static void Main()
    {
      /*Stopwatch timer = new Stopwatch();
      timer.Start();
      WordCount();
      //NumberCount();
      //Fibonacci();
      Factorial();
      Console.WriteLine($"время:{timer.ElapsedMilliseconds}");*/
      func.Add(new Thread(WordCount)); func[0].Name = "WordCount";
      func.Add(new Thread(NumberCount)); func[1].Name = "NumberCount";
      func.Add(new Thread(Fibonacci)); func[2].Name = "Fibonacci";
      for(int i=0;i<3;i++)
        func[i].Start();
      zero = false; first = true; second = true;
      Stopwatch timer = new Stopwatch();
      timer.Start();
      while (func[0].IsAlive|| func[1].IsAlive|| func[2].IsAlive)
      {
        if(timer.ElapsedMilliseconds>kvant)//секундомер на 7 секунд
          if(!zero)
          {
            zero = true;
            func[0].Suspend();
            func[1].Interrupt();
            first = false;
            timer.Restart();
          }
          else
            if(!first)
            {
              first = true;
              func[1].Suspend();
              func[2].Interrupt();
              second = false;
              timer.Restart();
            }
            else
            {
              second = true;
              func[2].Suspend();
              func[0].Interrupt();
              zero = false;
              timer.Restart();
            }
      }
    }
  }
}
