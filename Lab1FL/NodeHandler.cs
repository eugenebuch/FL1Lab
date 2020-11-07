using System;
using System.Collections.Generic;

namespace Lab1FL
{
    class NodeHandler : INodeRepository<Node>
    {
        private List<Node> nodes;

        public NodeHandler()
        {
            nodes = new List<Node>();
        }

        public IEnumerable<Node> GetList()
        {
            return nodes;
        }

        public void Create(Node item)
        {
            nodes.Add(item);
        }

        public Node GetItem(int id)
        {
            try
            {
                return nodes.Find(x => x.ID == id);
            } catch (ArgumentNullException ex)
            {
                Console.WriteLine("Запись не найдена.");
                return null;
            }
        }

        public void Update(Node item)
        {
            try
            {
                if (item == null) throw new ArgumentNullException();
                object[] parameters = { item.Surname, item.Name, item.Thirdname, 
                    item.Birthday.ToString($"{item.Birthday.Day}.{item.Birthday.Month}.{item.Birthday.Year}"),
                    item.Phone, item.Country, item.Organization, item.Position, item.Other };

                Console.WriteLine("Поля для изменения:\n1. Фамилия\n2. Имя\n3. Отчество\n4. Дата рождения\n" +
                    "5. Телефон\n6. Страна\n7. Организация\n8. Должность\n9. Другое");
                Console.WriteLine("\nВведите номер поля, которое хотите изменить:");
                var option = int.Parse(Console.ReadLine()) - 1;
                if (option == -1) return;

                Console.WriteLine($"Старое значение: {parameters[option]}");
                Console.WriteLine("Новое значение:");
                parameters[option] = Console.ReadLine();

                if (!IsRequiredNotNull(parameters[option])) throw new ArgumentNullException();

                item = new Node()
                {
                    ID = item.ID,
                    Surname = (string)parameters[0],
                    Name = (string)parameters[1],
                    Thirdname = (string)parameters[2],
                    Phone = long.Parse(parameters[4].ToString()),
                    Country = (string)parameters[5],
                    Birthday = DateParse((string)parameters[3]),
                    Organization = (string)parameters[6],
                    Position = (string)parameters[7],
                    Other = (string)parameters[8]
                };
                nodes[item.ID] = item;

                Console.WriteLine("Запись успешно изменена");
            } catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Ошибка, передан пустой аргумент в обязательное поле. Ошибка: {ex.Message}");
            } catch (OverflowException ex)
            {
                Console.WriteLine($"Ошибка переполнения аргумента. Ошибка: {ex.Message}");
            } catch (FormatException ex)
            {
                Console.WriteLine($"Ошибка форматирования, неверный формат данных. Ошибка: {ex.Message}");
            } catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Ошибка индексирования. Ошибка: {ex.Message}");
            }
        }

        public void Delete(int id)
        {
            try
            {
                var node = nodes.Find(x => x.ID == id);
                if (node == null) throw new ArgumentNullException();
                nodes.Remove(node);
                for (int i = 0; i < nodes.Count; i++)
                {
                    nodes[i].ID = i;
                }
                Console.WriteLine("Запись успешо удалена");
            } catch (ArgumentNullException ex)
            {
                Console.WriteLine("Запись не найдена.");
            }
        }

        public DateTime DateParse(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input) || input == "Не указано")
                {
                    Console.WriteLine("Установлена дата по умолчанию.");
                    return new DateTime();
                }
                var splitted = input.Split('.');

                return new DateTime(int.Parse(splitted[2]),
                    int.Parse(splitted[1]), int.Parse(splitted[0]));
            } catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Ошибка преобразования даты, установлена дата по умолчанию. Ошибка: {ex.Message}");
                return new DateTime();
            }
        }

        public bool IsRequiredNotNull(object param)
        {
            if (long.TryParse((string)param, out long intparam) 
                && (intparam < Math.Pow(10, 10) || intparam >= Math.Pow(10, 11)))
            {
                return false;
            }
            else if (param is string strparam && string.IsNullOrEmpty(strparam))
            {
                return false;
            }

            return true;
        }
    }
}
