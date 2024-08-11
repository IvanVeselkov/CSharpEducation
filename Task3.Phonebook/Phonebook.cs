using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Должна быть реализована CRUD функциональность:
///     Должен уметь принимать от пользователя номер и имя телефона.+
///     Сохранять номер в файле phonebook.txt. (При завершении программы либо при добавлении).+
///     Вычитывать из файла сохранённые номера. (При старте программы).+
///     Удалять номера.+
///     Получать абонента по номеру телефона.+ Надо проверить
///     Получать номер телефона по имени абонента.+ Надо проверить
///     Обращение к Phonebook должно быть как к классу-одиночке SingleTon.+
///     Внутри должна быть коллекция с абонентами.+
///     Для обращения с абонентами нужно завести класс Abonent. С полями «номер телефона», «имя».+
///     Не дать заносить уже записанного абонента.
/// </summary>
namespace Task3.Phonebook
{
    internal class Abonent
    {
        private string userName;

        public string UserName
        { set { userName = value; } get => userName; }

        private long userPhoneNumber;

        public long UserPhoneNumber
        { set { userPhoneNumber = value; } get => userPhoneNumber; }

        public Abonent(string userNAME = "", long userNUMBER = 0)
        {
            UserName = userNAME;
            UserPhoneNumber = userNUMBER;
        }
    }

    internal sealed class Phonebook
    {
        private List<Abonent> contacts = new List<Abonent>();
        public List<Abonent> Contacts { get => contacts; }
        private static Phonebook? instance;

        public static Phonebook getInstance()
        {
            if (instance == null)
                instance = new Phonebook();
            return instance;
        }

        private Phonebook()
        {
            ReadFromFile();
        }

        /// <summary>
        /// Формирование базы контактов из файла
        /// </summary>
        private void ReadFromFile()
        {
            contacts.Clear();
            string path = @"D:\ProgrammingShit\Repository\CSharpEducation\Task3.Phonebook\phonebook.txt";
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        contacts.Add(MakeAbonentFromLine(s));
                    }
                    sr.Close();
                }
            }
            else
            {
                Console.WriteLine("Не могу прочесть файл");
            }
        }

        /// <summary>
        /// Полная перезапись базы с номерами
        /// </summary>
        public void OverwritingTheDatabase()
        {
            string path = @"D:\ProgrammingShit\Repository\CSharpEducation\Task3.Phonebook\phonebook.txt";
            File.WriteAllText(path, String.Empty);
            using (StreamWriter sw = File.CreateText(path))
            {
                foreach (var i in contacts)
                {
                    sw.WriteLine(MakeLineFromAbonent(i));
                }
                sw.Close();
            }
        }

        /// <summary>
        /// Добавление абонента в базу
        /// </summary>
        /// <param name="contact"></param>
        private void SavePhoneNumberIntoDataBase(Abonent contact)
        {
            string path = @"D:\ProgrammingShit\Repository\CSharpEducation\Task3.Phonebook\phonebook.txt";
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(MakeLineFromAbonent(contact));
            }
        }

        /// <summary>
        /// Обрабока входящей строки и формирование объекта Contact
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public Abonent MakeAbonentFromLine(string line)
        {
            string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Abonent newContact = new Abonent(parts[0], long.Parse(parts[1]));
            return newContact;
        }

        /// <summary>
        /// Обрабока входящего контакта и формирование строки
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public string MakeLineFromAbonent(Abonent contact)
        {
            return string.Format("{0} {1}", contact.UserName, contact.UserPhoneNumber);
        }

        /// <summary>
        /// Добавление номера в телефонную книгу
        /// </summary>
        public void AddAbonent(Abonent a)
        {
            if (!FindAbonentInfo(a))
            {
                contacts.Add(a);
                Console.WriteLine("Абонент добавлен успешно.\n");
                SavePhoneNumberIntoDataBase(a);
            }
            else
            {
                Console.WriteLine("Абонент с таким именем уже есть в базе.\n");
            }
        }

        /// <summary>
        /// Вывод информации по номеру
        /// </summary>
        public Abonent GetAbonent(long number)
        {
            Abonent startAbonent = new Abonent("", number);
            Abonent result = new Abonent();
            if (FindAbonentInfo(startAbonent, out result))
            {
                Console.WriteLine("Найден абонент: \n");
                Console.WriteLine(MakeLineFromAbonent(result));
                return result;
            }
            Console.WriteLine("Нет такого абонента");
            return null;
        }

        /// <summary>
        /// Вывод информации по имени
        /// </summary>
        public Abonent GetAbonent(string name)
        {
            Abonent startAbonent = new Abonent(name, 0);
            Abonent result = new Abonent();
            if (FindAbonentInfo(startAbonent, out result))
            {
                Console.WriteLine("Найден абонент: \n");
                Console.WriteLine(MakeLineFromAbonent(result));
                return result;
            }
            Console.WriteLine("Нет такого абонента");
            return null;
        }

        /// <summary>
        /// Удаление контакта из базы
        /// </summary>
        /// <param name="a">Абонент</par>
        public void RemoveAbonent(Abonent a)
        {
            contacts.Remove(a);
            OverwritingTheDatabase();
            Console.WriteLine("Абонент удален успешно.\n");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name">Имя абонента</param>
        public void RemoveAbonent(string name)
        {
            Abonent r = GetAbonent(name);
            if (r != null)
            {
                contacts.Remove(r);
                OverwritingTheDatabase();
                Console.WriteLine("Абонент удален успешно.\n");
            }
        }

        /// <summary>
        /// Проверка наличия номера в телефонной книге, ели есть то сохраняет в b
        /// </summary>
        /// <returns>
        /// True: Такой номер есть
        /// False: Такого номера нет
        /// </returns>
        private bool FindAbonentInfo(Abonent a, out Abonent b)
        {
            bool find = false;
            b = null;
            for (int i = 0; i < contacts.Count; i++)
            {
                if (contacts[i].UserName == a.UserName)//
                {
                    find = true;
                    b = contacts[i];
                    break;
                }
                if (contacts[i].UserPhoneNumber == a.UserPhoneNumber)//
                {
                    find = true;
                    b = contacts[i];
                    break;
                }
            }
            return find;
        }

        private bool FindAbonentInfo(Abonent a)
        {
            Abonent b = new Abonent();
            return FindAbonentInfo(a, out b);
        }
    }
}