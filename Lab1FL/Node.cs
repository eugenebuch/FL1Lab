using System;

namespace Lab1FL
{
    class Node
    {
        public int ID { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Thirdname { get; set; }
        public long Phone { get; set; }
        public string Country { get; set; }
        public DateTime Birthday { get; set; }
        public string Organization { get; set; }
        public string Position { get; set; }
        public string Other { get; set; }

        public override string ToString()
        {
            return $"\nID: {ID}\nФамилия: {Surname}\nИмя: {Name}\nОтчество: {Thirdname}\nДата рождения: {Birthday.ToString("d")}\n" +
                $"Телефон: {Phone}\nСтрана: {Country}\nОрганизация: {Organization}\nДолжность: {Position}\nПрочее: {Other}";
        }
    }
}
