namespace Task3.Phonebook
{
  /// <summary>
  /// Класс абонент содержащий информацию о абоненте из телефонной книги.
  /// </summary>
  internal class Abonent
  {
    #region Поля и Свойства
    public string Name { set; get; }

    public long PhoneNumber { set; get; }
    #endregion

    #region Конструкторы
    public Abonent(string name, long phonenumber)
    {
      Name = name;
      PhoneNumber = phonenumber;
    }
    #endregion
  }
}