using System;
using System.Collections.Generic;

namespace pls
{
  class Program
  {
    const int count = 8;//будет 8 ячейки памяти, содержащая 8кб
    static int[] massive = new int[count];//массив ячеек
    const int value = 8 * 1024;//вес одной ячейки, т.е. 8кб или 8*1024 байт
    static int sum;//значение общей свободной памяти
    static List<Tuple<Int32, Int32>> works = new List<Tuple<Int32, Int32>>();//item 1-process,item 2 - position from first byte
    static List<Tuple<Int32, Int32>> works_replica = new List<Tuple<Int32, Int32>>();//item 1-process,item 2 - position from first byte, нужен с особенностями работы с кортежами.
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
    static void Getworks()
    {
      for (int i = 0; i < works.Count; i++)
        Console.WriteLine($"{i} процесс занимает {works[i].Item1} байт и находится начиная с {works[i].Item2} байта.");
    }
    static void BeautiWrite()
    {
      Getmassive();
      Console.WriteLine(); Getsum();
      Getworks();
    }
    static void Addtask()
    {
      int full;//номер первой ячейки, доступной для записи.
      int process;//значение занимаемой памяти, в байтах
      full = 0;
      BeautiWrite();
      Console.WriteLine("Введите занимаемую память от 0 до 65535 байт:");
      while (true)//проверка на правильность
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
      for (int i = full; i < count; i++)//заполняем ячейки
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
      BeautiWrite();
    }
    static void Deletetask()
    {
      works_replica.Clear();
      int pos;//начальная ячейка процесса
      int pro;//общее число байт процесса
      int chose;
      BeautiWrite();
      Console.WriteLine("Какой процесс хотели бы удалить?:");
      while (true)//проверка на правильность
      {
        chose = Int32.Parse(Console.ReadLine());
        if (!((chose >= 0) && (chose < works.Count)))
          Console.WriteLine("Вы ввели несуществующую задачу, повторите еще раз:");
        else
          break;
      }
      pos = works[chose].Item2 / value;
      pro = works[chose].Item1;
      for (int i = 0; i < chose; i++)//изменения в процессах
        works_replica.Add(works[i]);
      for (int i = chose + 1; i < works.Count; i++)
        works_replica.Add(new Tuple<Int32, Int32>(works[i].Item1, works[i].Item2 - pro));
      works.Clear();
      for (int i = 0; i < works_replica.Count; i++)
        works.Add(new Tuple<Int32, Int32>(works_replica[i].Item1, works_replica[i].Item2));
      for (int i = count - 1; i >= pos; i--)//изменяем ячейки памяти
      {
        if (massive[i] == value)//итак пустая
          continue;
        if (pro > massive[i])//встретилась заполненная ячейка
        {
          pro = pro - massive[i];
          massive[i] = value;
        }
        if (pro <= massive[i])//та ячейка, в которой хранится первоначальный байт
        {
          massive[i] = massive[i] + pro;
          pro = 0;
        }
        Console.WriteLine("Полученный результат:");
        BeautiWrite();
      }
    }
    static void Main()
    {
      rnd = new Random();
      for (int i = 0; i < count; i++)
        massive[i] = value;//в данном случае значение ячейки эквивалентно свободному пространству.
      Console.WriteLine("Все ячейки очищены.Готово к работе.");
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
              Deletetask();
              break;
            }
          case 3:
            {
              Console.Clear();
              BeautiWrite();
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
