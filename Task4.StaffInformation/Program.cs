using System;

/// <summary>
/// Создайте консольное приложение, которое позволит управлять данными сотрудников. 
/// Программа должна обеспечивать функционал добавления, обновления, 
/// получения информации о работниках и расчета их зарплаты.
/// </summary>
namespace Task4.StaffInformation
{
  class Program
  {
    /// <summary>
    /// Вывод информации о сотрудниках в консоль
    /// </summary>
    /// <param name="s">Штат компании</param>
    public static void GetAllStaffMembers(Staff s)
    {
      Console.WriteLine("Список сотрудников:");
      foreach(var i in s.StaffMembers)
      {
        Console.WriteLine(string.Format("Полное имя: {0}|| Заработная плата: {1} || Часовая ставка: {2} || Часы: {3}", i.FullName, i.Salary,i.SalaryPerHour,i.WorkingHours));
      }
    }

    /// <summary>
    /// Нахождение информации о сотруднике организации и вывод информации в консоль
    /// </summary>
    /// <param name="s">Штат компании</param>
    public static void FindEmployeeInStaffAndGetInConsole(Staff s)
    {
      Console.WriteLine("Информация о сотруднике:\n" +
        "Введите имя сотрудника: ");
      string enterName = Console.ReadLine();
      Employee findedEmployee = s.FindEmployee(enterName);
      if(findedEmployee is null)
      {
        Console.WriteLine("Такого сотрудника не существует.\n");
      }else
      {
        Console.WriteLine(string.Format("Полное имя: {0}|| Заработная плата: {1} || Часовая ставка: {2} || Часы: {3}", findedEmployee.FullName, findedEmployee.Salary, findedEmployee.SalaryPerHour, findedEmployee.WorkingHours));
      }
    }

    /// <summary>
    /// Обновление информации о сотруднике.
    /// </summary>
    /// <param name="s">Штат компании</param>
    public static void UpdateInformationAboutEmployee(Staff s)
    {
      Console.WriteLine("Обновление информации о сотруднике:\n" +
        "Введите имя сотрудника: ");
      string enterName = Console.ReadLine();
      Employee findedEmployee = s.FindEmployee(enterName);
      bool subprogramState = true;
      do
      {
        Console.WriteLine("Какую информацию хотите изменить:\n" +
          "1) Имя сотрудника\n" +
          "2) Изменение часовой оплаты\n" +
          "3) Изменения количества рабочих часов\n" +
          "4) Выйти\n ");
        string symbol = Console.ReadLine();

        switch (symbol)
        {
          case "1":
            Console.WriteLine("Введите новое имя сотрудника: ");
            string enterNewName = Console.ReadLine();
            s.ChangeEmployeeName(findedEmployee.FullName, enterNewName);
            break;
          case "2":
            Console.WriteLine("Введите новую часовую оплату сотрудника: ");
            float enterNewSalary = float.Parse(Console.ReadLine());
            s.ChangeEmployeeSalaryPerHours(findedEmployee.FullName, enterNewSalary);
            break;
          case "3":
            Console.WriteLine("Введите новое количество рабочих часов сотрудника: ");
            int enterNewWorkHours = int.Parse(Console.ReadLine());
            s.ChangeEmployeeWorkingHours(findedEmployee.FullName, enterNewWorkHours);
            break;
          default:
            subprogramState = false;
            break;
        }
      } while (subprogramState);
    }

    /// <summary>
    /// Добавление нового сотрудника в штат компанииы.
    /// </summary>
    /// <param name="s">Штат компании</param>
    public static void AddNewEmployeeInStaff(Staff s)
    {
      Console.WriteLine("Добавление нового сотрудника:\n" +
        "Введите имя сотрудника: ");
      string enterName = Console.ReadLine();
      Employee findedEmployee = s.FindEmployee(enterName);
      if (findedEmployee is null)
      {
        Console.WriteLine("Введите часовую оплату сотрудника: ");
        float enterNewSalary = float.Parse(Console.ReadLine());
        Console.WriteLine("Введите количество рабочих часов сотрудника: ");
        int enterNewWorkHours = int.Parse(Console.ReadLine());
        s.AddEmployee(enterName, enterNewSalary, enterNewWorkHours);
      }
      else
      {
        Console.WriteLine("Сотрудник с таким именем уже работает в компании.");
      }
    }

    /// <summary>
    /// Удаление сотрудника из штата.
    /// </summary>
    /// <param name="s">Штат компании.</param>
    public static void RemoveEmployeeFromStaff(Staff s)
    {
      Console.WriteLine("Удаление сотрудника:\n" +
        "Введите имя сотрудника: ");
      string enterName = Console.ReadLine();
      Employee findedEmployee = s.FindEmployee(enterName);
      if (findedEmployee is null)
      {
        Console.WriteLine("Такого сотрудника не существует.\n");
      }
      else
      {
        bool removeState = s.RevomeEmployee(findedEmployee);
        if(removeState)
        {
          Console.WriteLine("Удаление прошло успещно.");
        }
        else
        {
          Console.WriteLine("Удаление не выполнено.");
        }
      }
    }

    static void Main(string[] args)
    {
      Staff staff = Staff.getInstance();
      Employee Tim = new Employee("Tim Hook", 5, 10);
      staff.AddEmployee("Tim Hook", 5, 10);
      GetAllStaffMembers(staff);
      staff.RevomeEmployee(new Employee("Tim Hook", 0, 0));
      GetAllStaffMembers(staff);
      bool programmState = true;
      Console.WriteLine("---StaffInformation---");
      
      do
      {
        Console.WriteLine("Выберите: \n" +
          "1) Список сотрудников\n" +
          "2) Найти информацию о сотруднике" +
          "3) Обновить информацию о сотруднике\n" +
          "4) Добавить сотрудника\n" +
          "5) Удалить сотрудника\n" +
          "6) Закрыть программу\n");
        string symbol = Console.ReadLine();
        
        switch(symbol)
        {
          case "1":
            GetAllStaffMembers(staff);
            break;
          case "2":
            FindEmployeeInStaffAndGetInConsole(staff);
            break;
          case "3":
            UpdateInformationAboutEmployee(staff);
            break;
          case "4":
            AddNewEmployeeInStaff(staff);
            break;
          case "5":
            RemoveEmployeeFromStaff(staff);
            break;
          default:
            break;
        }
      } while (programmState);
    }
  }
}
