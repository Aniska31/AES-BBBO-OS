/*3.	Работа с форматом JSON
a)	Создать файл формате JSON  из редактора 
b)	Создать новый объект. Выполнить сериализацию объекта в формате JSON и записать в файл.
c)	Прочитать файл в консоль
d)	Удалить файл*/

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _3
{
  [Serializable]
    class Person
    {
        public string Name { get; set; }
         public string Age { get; set; }
 
        public Person(string name, string age)
        {
          Name = name;
          Age = age;
        }
       public Person() { }
    }

  class Program
  {
    static async Task Main(string[] args)
    {
      //BinaryFormatter formatter = new BinaryFormatter();
      Console.WriteLine("Введите название файла для формата JSON:");
      string name = Console.ReadLine();
      // FileStream fs = File.Create(name);
      FileStream fs = new FileStream($"{name}.json", FileMode.Create);

      Console.WriteLine("Дан шаблон работы.");
      Console.WriteLine("Введите имя пользователя:");
      string n = Console.ReadLine();
      Console.WriteLine("Введите возраст пользователя:");
      string a = Console.ReadLine();
      //Person tom = new Person(n,a);
      Person tom = new Person() { Name = n, Age = a };
      await JsonSerializer.SerializeAsync<Person>(fs, tom);
      Console.WriteLine("Данные были сохранены в файл.");

      fs.Seek(0, SeekOrigin.Begin);
      /*Person deserilizePeople = (Person)formatter.Deserialize(fs);
      //await JsonSerializer.DeserializeAsync<Person>(fs);
      Console.WriteLine($"Имя: {deserilizePeople.Name}  Возраст: {deserilizePeople.Age}");
      Console.WriteLine("Файл был прочитан");*/
      Person restoredPerson = await JsonSerializer.DeserializeAsync<Person>(fs);
      Console.WriteLine($"Имя: {restoredPerson.Name}  Возраст: {restoredPerson.Age}");
      Console.WriteLine("Файл был прочитан");

      fs.Close();
      Console.WriteLine("Вы точно уверены, что хотите удалить? (1 - yes, 0 - no)");
      string answer = Console.ReadLine();
      if (answer != "0")
      {
        File.Delete($"{name}.json");
        Console.WriteLine("Удаление произошло успешно.");
      }
      else
        Console.WriteLine("Удаление отменено");
    }
  }
}
