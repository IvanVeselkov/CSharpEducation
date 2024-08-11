﻿using System;

namespace Task3.Phonebook
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Phonebook phonebook = Phonebook.getInstance();

            Console.WriteLine("Телефонная книга.\n" +
                "Выберите действие:");
            bool programmWork = true;
            do
            {
                Console.WriteLine("Количество контактов: " +
                phonebook.Contacts.Count +
                ".\n");
                int action = 0;
                Console.Write("1) Создать абонента.\n" +
                    "2) Найти абонента используя имя.\n" +
                    "3) Найти абонента используя номер.\n" +
                    "4) Удалить абонента\n" +
                    "5) Закрыть программу\n" +
                    "Ввод: ");
                if (int.TryParse(Console.ReadLine(), out action))
                {
                    switch (action)
                    {
                        case 1:
                            Console.Write("Создание абонента.\n");
                            Console.Write("Введите номер абонента:");
                            long phoneAbonent = long.Parse(Console.ReadLine());
                            Console.Write("Введите имя абонента:");
                            string nameAbonent = Console.ReadLine();

                            Abonent a = new Abonent(nameAbonent, phoneAbonent);
                            phonebook.AddAbonent(a);
                            break;

                        case 2:
                            Console.Write("Поиск абонента по имени.\n");
                            Console.Write("Введите имя абонента:");
                            string nameAbonent1 = Console.ReadLine();
                            phonebook.GetAbonent(nameAbonent1);
                            break;

                        case 3:
                            Console.Write("Поиск абонента по номеру.\n");
                            Console.Write("Введите номер абонента:");
                            long phoneAbonent1 = long.Parse(Console.ReadLine());
                            phonebook.GetAbonent(phoneAbonent1);
                            break;

                        case 4:
                            Console.Write("Удаление абонента.\n");
                            Console.Write("Введите имя абонента:");
                            string nameAbonent2 = Console.ReadLine();
                            phonebook.RemoveAbonent(nameAbonent2);
                            break;

                        default:
                            programmWork = false;
                            break;
                    }
                }
                Console.WriteLine();
            } while (programmWork);
            Console.WriteLine("Завершение работы программы.");
        }
    }
}