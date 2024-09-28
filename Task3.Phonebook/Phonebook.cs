using System;
using System.Collections.Generic;
using System.IO;


namespace Task3.Phonebook
{
  /// <summary>
  /// Телефонная книга.
  /// </summary>
  internal sealed class Phonebook
  {
    #region Поля и свойства
    public List<Abonent> Contacts { get; }
    private string path;

    private static Phonebook? instance;
    #endregion

    #region Методы

    /// <summary>
    /// Формирование базы контактов из файла.
    /// </summary>
    private void ReadFromFile()
    {
      Contacts.Clear();
      

      if (File.Exists(path))
      {
        using (StreamReader sr = File.OpenText(path))
        {
          string s;
          while ((s = sr.ReadLine()) != null)
          {
            Contacts.Add(MakeAbonentFromLine(s));
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
    /// Полная перезапись базы с номерами.
    /// </summary>
    private void OverwritingTheDatabase()
    {
      File.WriteAllText(path, String.Empty);
      using (StreamWriter sw = File.CreateText(path))
      {
        foreach (var i in Contacts)
        {
          sw.WriteLine(MakeLineFromAbonent(i));
        }
        sw.Close();
      }
    }

    /// <summary>
    /// Добавление абонента в базу.
    /// </summary>
    /// <param name="contact">Входной абонент</param>
    private void SavePhoneNumberIntoDataBase(Abonent contact)
    {
      string path = @"D:\ProgrammingShit\Repository\CSharpEducation\Task3.Phonebook\phonebook.txt";
      using (StreamWriter sw = File.CreateText(path))
      {
        sw.WriteLine(MakeLineFromAbonent(contact));
      }
    }

    /// <summary>
    /// Обрабока входящей строки и формирование объекта Abonent.
    /// </summary>
    /// <param name="line">Входная строка</param>
    /// <returns>Выходной абонент</returns>
    public Abonent MakeAbonentFromLine(string line)
    {
      string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
      Abonent newContact = new Abonent(parts[0], long.Parse(parts[1]));
      return newContact;
    }

    /// <summary>
    /// Обрабока входящего абонента и формирование строки.
    /// </summary>
    /// <param name="line">Входной абонент</param>
    /// <returns>Выходная строка</returns>
    public string MakeLineFromAbonent(Abonent contact)
    {
      return string.Format("{0} {1}", contact.Name, contact.PhoneNumber);
    }

    /// <summary>
    /// Добавление номера в телефонную книгу.
    /// </summary>
    /// <param name="contact">Входной абонент</param>
    public void AddAbonent(Abonent contact)
    {
      if (!TryFindAbonentInfo(contact,out _))
      {
        Contacts.Add(contact);
        Console.WriteLine("Абонент добавлен успешно.\n");
        SavePhoneNumberIntoDataBase(contact);
      }
      else
      {
        Console.WriteLine("Абонент с таким именем уже есть в базе.\n");
      }
    }

    /// <summary>
    /// Вывод информации по номеру.
    /// </summary>
    /// <param name="number">Входной номер телефона</param>
    public Abonent GetAbonent(long number)
    {
      Abonent startAbonent = new Abonent("", number);
      Abonent result;
      if (TryFindAbonentInfo(startAbonent, out result))
      {
        Console.WriteLine("Найден абонент: \n");
        Console.WriteLine(MakeLineFromAbonent(result));
        return result;
      }
      Console.WriteLine("Нет такого абонента");
      return null;
    }

    /// <summary>
    /// Вывод информации по имени.
    /// </summary>
    /// <param name="contact">Входное имя абонента</param>
    public Abonent GetAbonent(string name)
    {
      Abonent startAbonent = new Abonent(name, 0);
      Abonent result;
      if (TryFindAbonentInfo(startAbonent, out result))
      {
        Console.WriteLine("Найден абонент: \n");
        Console.WriteLine(MakeLineFromAbonent(result));
        return result;
      }
      Console.WriteLine("Нет такого абонента");
      return null;
    }

    /// <summary>
    /// Удаление контакта из базы.
    /// </summary>
    /// <param name="contant">Входной абонент</par>
    public void RemoveAbonent(Abonent contant)
    {
      Contacts.Remove(contant);
      OverwritingTheDatabase();
      Console.WriteLine("Абонент удален успешно.\n");
    }

    /// <summary>
    /// Удаление контакта из базы.
    /// </summary>
    /// <param name="name">Входное имя абонента</param>
    public void RemoveAbonent(string name)
    {
      Abonent r = GetAbonent(name);
      if (r != null)
      {
        RemoveAbonent(r);
      }
    }

    /// <summary>
    /// Проверка наличия номера в телефонной книге, ели есть то сохраняет в resultContact.
    /// Проверка по имени нужна для базы.
    /// Условие что все имена должны быть уникальны.
    /// </summary>
    ///  <param name="contact">Входной абонент</param>
    ///  <param name="resultContact">Найденный абонент</param>
    /// <returns>
    /// True: Такой номер есть.
    /// False: Такого номера нет.
    /// </returns>
    private bool TryFindAbonentInfo(Abonent contact, out Abonent resultContact)
    {
      bool find = false;
      resultContact = null;
      for (int i = 0; i < Contacts.Count; i++)
      {
        if (Contacts[i].PhoneNumber == contact.PhoneNumber)
        {
          find = true;
          resultContact = Contacts[i];
          break;
        }
        if (Contacts[i].Name == contact.Name)
        {
          find = true;
          resultContact = Contacts[i];
          break;
        }
      }
      return find;
    }


    /// <summary>
    /// Сингтун стаф.
    /// </summary>
    /// <returns>Выходная телефонная книжка</returns>
    public static Phonebook getInstance()
    {
      if (instance == null)
        instance = new Phonebook();
      return instance;
    }
    #endregion;

    #region Конструкторы
    private Phonebook()
    {
      Contacts = new List<Abonent>();
      var exePath = AppDomain.CurrentDomain.BaseDirectory;
      path = Path.Combine(exePath, "phonebook.txt");
      ReadFromFile();
    }

    #endregion
  }
}