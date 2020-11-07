using System;
using System.Collections.Generic;

namespace Lab1FL
{
    class Program
    {
        private static NodeHandler handler;

        static void Main(string[] args)
        {
            OpenNodebook();
        }

        private static void OpenNodebook()
        {
            handler = new NodeHandler();
            while (true)
            {
                Console.WriteLine("\nМеню программы:\n1. Создать запись\n2. Найти и вывести запись\n3. Изменить запись" +
                    "\n4. Удалить запись\n5. Вывести все записи кратко\n 0 - ВЫХОД\n");

                var menu = new Action[] { () => CreateNode(), () => ReadNode(), () => EditNode(),
                    () => DeleteNode(), () => ShowAllNodes() };

                try
                {
                    var option = int.Parse(Console.ReadLine());
                    if (option == 0) return;
                    menu[--option]();
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine($"Ошибка, аргумент оказался пустым. Ошибка: {ex.Message}");
                }
                catch (OverflowException ex)
                {
                    Console.WriteLine($"Ошибка переполнения аргумента. Ошибка: {ex.Message}");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Ошибка форматирования, неверный формат данных. Ошибка: {ex.Message}");
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine($"Несуществующий пункт меню/индекс записи. Ошибка: {ex.Message}");
                }
                catch
                {
                    Console.WriteLine("Получена неизвестная ошибка. Перезапуск меню...");
                }
            }
        }

        static string Inputter(string name)
        {
            try
            {
                Console.Write($"{name}: ");
                var output = Console.ReadLine();
                if (new List<string> { "Фамилия*", "Имя*", "Телефон*", "Страна*" }.Contains(name) 
                    && !handler.IsRequiredNotNull(output))
                {
                    throw new FormatException();
                }
                if (!string.IsNullOrEmpty(output)) return output;
            } catch
            {
                throw;
            }
            return "Не указано";
        }

        static void CreateNode()
        {
            try
            {
                Console.WriteLine("Обязательные поля помечены звёздочкой *");
                handler.Create(new Node()
                {
                    ID = (handler.GetList() as List<Node>).Count,
                    Surname = Inputter("Фамилия*"),
                    Name = Inputter("Имя*"),
                    Thirdname = Inputter("Отчество"),
                    Birthday = handler.DateParse(Inputter("Дата рождения (DD.MM.YYYY)")),
                    Phone = long.Parse(Inputter("Телефон*")),
                    Country = Inputter("Страна*"),
                    Organization = Inputter("Организация"),
                    Position = Inputter("Должность"),
                    Other = Inputter("Другое")
                });
            } catch
            {
                throw;
            }
        }

        static void ReadNode()
        {
            try
            {
                Console.WriteLine($"\nВведите id записи: ");
                var id = int.Parse(Console.ReadLine());
                Console.WriteLine(handler.GetItem(id));
            } catch
            {
                throw;
            }
        }

        static void EditNode()
        {
            try
            {
                Console.WriteLine("\nВведите id записи");
                handler.Update(handler.GetItem(int.Parse(Console.ReadLine())));
            } catch
            {
                throw;
            }
        }

        static void DeleteNode()
        {
            try
            {
                Console.WriteLine("\nВведите id записи:");
                handler.Delete(int.Parse(Console.ReadLine()));
            } catch
            {
                throw;
            }
        }

        static void ShowAllNodes()
        {
            try
            {
                var list = handler.GetList();
                if (list == null) throw new ArgumentNullException();
                foreach (var item in handler.GetList())
                {
                    Console.WriteLine($"\nID: {item.ID}\nФамилия: {item.Surname}\nИмя: {item.Name}\nТелефон: {item.Phone}");
                }
            } catch
            {
                throw;
            }
        }
    }
}
