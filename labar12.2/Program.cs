using labar12._2;
using library;


namespace labar12._2
{
    internal class Program
    {
        static void Main()
        {
            // Создание хештаблицы
            MyHashtable<zAircraft, Airplane>? htable = new MyHashtable<zAircraft, Airplane>();
            Item<zAircraft, Airplane> item = null;
            bool exit = false;
            do
            {
                Console.WriteLine("\nВыберите пункт меню из списка:");
                Console.WriteLine("1. Сформировать хештаблицу и заполнить ее рандомными значениями.");
                Console.WriteLine("2. Распечатать полученую хештаблицу.");
                Console.WriteLine("3. Выполнить поиск по ключу.");
                Console.WriteLine("4. Удалить найденный элемент.");
                Console.WriteLine("5. Добавить в таблицу рандомные значения.");
                Console.WriteLine("6. Выход.");
                int number = IntManualInput(1, 6);

                switch (number)
                {
                    case 1:
                        htable = CreateHashTable(); // Создание хештаблицы
                        break;
                    case 2:
                        htable.Print(); // Вывод элементов хештаблицы
                        break;
                    case 3:
                        item = SearchItem(htable); // Поиск элемента
                        break;
                    case 4:
                        if (item == null)
                        {
                            Console.WriteLine("Элемент для удаления не найден. Сначала выполните поиск.");
                        }
                        else
                        {
                            DeletePoints(htable, item.Key); // Поиск и удаление элемента
                            item = null;
                        }
                        break;
                    case 5:
                        AddPoints(htable); // Добаление элементов в хештаблицу
                        break;
                    case 6:
                        exit = true; // Выход из программы
                        break;
                }
            } while (!exit);
        }
        static int IntManualInput(int min, int max)
        {
            bool ok;
            int number;
            do
            {
                ok = int.TryParse(Console.ReadLine(), out number);
                if (!ok)
                {
                    Console.WriteLine("Некорректный ввод, попробуйте еще раз");
                }
                else if (number < min || number > max)
                {
                    Console.WriteLine($"Число находится вне диапазона {min} и {max}, попробуйте еще раз");
                    ok = false;
                }
            } while (!ok);

            return number;
        }

        static MyHashtable<zAircraft, Airplane> CreateHashTable()
        {
            Console.WriteLine("Введите длину хештаблицы от 1 до 100.");
            int len = IntManualInput(1, 100);
            Console.WriteLine($"Введите количество элементов хештаблицы от 1 до 100.");
            MyHashtable<zAircraft, Airplane> newhtable = new MyHashtable<zAircraft, Airplane>(len);
            int count = IntManualInput(1, 100);
            for (int i = 0; i < count; i++)
            {
                zAircraft aircraft = new();
                aircraft.RandomInit();
                Airplane airplane = new Airplane();
                airplane.RandomInit();
                newhtable.AddData(aircraft, airplane);
            }
            return newhtable;
        }
        static void AddPoints(MyHashtable<zAircraft, Airplane> htable)
        {
            if (htable.Count == 0)
            {
                Console.WriteLine("Хештаблица пустая");
                return;
            }
            Console.WriteLine("Введите количество элементов для добавления");
            int count = IntManualInput(1, 100);
            bool flag;
            for (int i = 0; i < count; i++)
            {
                flag = false;
                while (!flag)
                {
                    zAircraft mi = new();
                    mi.RandomInit();
                    Airplane airplane = new Airplane();
                    airplane.RandomInit();
                    flag = htable.AddData(mi, airplane);
                }
            }
            Console.WriteLine("Элементы добавлены успешно");
        }
        static Item<zAircraft, Airplane> SearchItem(MyHashtable<zAircraft, Airplane> htable)
        {
            if (htable.Count == 0)
            {
                Console.WriteLine("Хештаблица пустая");
                return default;
            }
            Console.WriteLine("Введите id ключа для удаления");
            int id = IntManualInput(0, int.MaxValue);
            Console.WriteLine("Введите модель ключа для удаления");
            string model = Console.ReadLine();
            Console.WriteLine("Введите год ключа для удаления");
            int year = IntManualInput(0, int.MaxValue);
            Console.WriteLine("Введите тип двигателя ключа для удаления");
            string engine = Console.ReadLine();
            Console.WriteLine("Введите количество членов экипажа ключа для удаления");
            int members = IntManualInput(0, int.MaxValue);

            Item<zAircraft, Airplane> item = htable.FindKeyByData(new zAircraft(model, year, engine, members, id));
            if (item == null)
            {
                Console.WriteLine("Элемент не найден");
                return default;
            }
            else
            {
                Console.WriteLine($"Найден элемент с значением {item.Value}");
                return item;
            }
        }
        static void DeletePoints(MyHashtable<zAircraft, Airplane> htable, zAircraft key)
        {
            if (htable.Count == 0 || htable == null)
            {
                Console.WriteLine("Хештаблица пустая");
            }
            else if (key == null)
            {
                Console.WriteLine("Элемент для удаления не определен.");
            }
            else
            {
                htable.RemoveData(key);
                Console.WriteLine($"Элемент успешно удален");
            }
        }
    }
}