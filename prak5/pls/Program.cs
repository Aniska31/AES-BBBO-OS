using System;
using System.Collections.Generic;

namespace pls
{
  class Program
  {
    const int count = 8;//будет 8 ячейки памяти, содержащая 8кб
    static int[] massive = new int[count];//массив ячеек
    const int value = 8*1024;//вес одной ячейки, т.е. 8кб или 8*1024 байт
    static int full;//номер первой ячейки, доступной для записи.
    static int sum;//значение общей свободной памяти
    static int process;//значение занимаемой памяти, в байтах
    static List<Tuple<Int32,Int32>> works = new List<Tuple<Int32, Int32>>();//item 1-process,item 2 - position from first byte
    static Random rnd;
    static int Menu()
    {
      Console.WriteLine
        (
        "1. Добавить задачу\n" +
        "2. Удалить задачу\n" +
        "3. Посмотреть количество свободной памяти\n" +
        "0. Выход\n\n" +
        "Введите действие:"
        );
      int choise;
      string s;
      choise = -1;
      while ((choise > 3) || (choise < 0))
      {
        s = Console.ReadLine();
        while (!(Int32.TryParse(s, out choise)))
        {
          Console.WriteLine("Вы ввели не цифру. Попробуйте еще раз:");
          s = Console.ReadLine();
        }
        choise = Int32.Parse(s);
        if ((choise > 3) || (choise < 0))
          Console.WriteLine("Вы ввели несуществующий пункт. Попробуйте еще раз:");
      }
      return choise;
    }
    static void Getsum()
    {
      sum = 0;
      for (int i = 0; i < count; i++)
        sum = sum + massive[i];
      Console.WriteLine($"Количество свободной памяти:{sum}");
    }
    static void Getmassive()
    {
      for (int i = 0; i < count; i++)
        Console.Write($"{i}:{massive[i]}, ");
    }
    static void Addtask()
    {
      full = 0;
      Getsum();
      Console.WriteLine("Введите занимаемую память от 0 до 65535 байт:");
      while(true)//проверка на правильность
      {
        process = Int32.Parse(Console.ReadLine());
        if (!((process > 0) && (process < 65535)))
          Console.WriteLine("Вы ввели невыполнимую задачу, повторите еще раз:");
        else
          if (process < sum)
            break;
          else
            Console.WriteLine("Вы ввели  задачу,которую в данный момент нельзя решить, повторите еще раз:");
      }
      for (int i = 0; i < count; i++)//запоминаем будущую заполняемую ячейку
        if (massive[i] == 0)
          full++;
      works.Add(new Tuple<Int32, Int32>(process, count * value - sum));//добавили процесс в список
      for(int i=full;i<count;i++)//заполняем ячейки
        if (process > massive[i])
        {
          process = process - massive[i];
          massive[i] = 0;
          full++;
        }
        else
        {
          massive[i] = massive[i] - process;
          process = 0;
          break;
        }
      Console.WriteLine("Полученный результат:");
      Getmassive();
      Console.WriteLine();Getsum();
    }
    static void Main()
    {
      rnd = new Random();
      for (int i = 0; i < count; i++)
        massive[i] = value;//в данном случае значение ячейки эквивалентно свободному пространству.
      Console.WriteLine("Все ячейки очищены.");
      Getmassive();
      Console.WriteLine(); Getsum();
      bool check = true;
      while (check)
        switch (Menu())
        {
          case 1:
            {
              Console.Clear();
              Addtask();
              break;
            }
          case 2:
            {
              Console.Clear();
              //Files();
              break;
            }
          case 3:
            {
              Console.Clear();
              Getsum();
              break;
            }
          case 0:
            {
              check = false;
              Console.WriteLine("Спасибо, до свидания!");
              break;
            }
        }
    }
  }
}
