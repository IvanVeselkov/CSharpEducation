using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.StaffInformation
{
  class Staff:IComparer<Employee>
  {
    public List<Employee> StaffMembers { get; set; }

    private static Staff instance;
 
    /// <summary>
    /// Добавление нового сотрудника.
    /// </summary>
    /// <param name="name">Имя сотрудника.</param>
    /// <param name="salaryPerHour">Часовая оплата.</param>
    /// <param name="hoursOfWork">Ставка.</param>
    /// <returns>
    /// true: если удалось добавить.
    /// false: если не удолось добавить.
    /// </returns>
    public bool AddEmployee(string name,double salaryPerHour,int hoursOfWork)
    {
      return AddEmployee(new Employee(name, salaryPerHour, hoursOfWork));
    }

    /// <summary>
    /// Добавление нового сотрудника.
    /// </summary>
    /// <param name="newEmployee">Новый сотрудник</param>
    /// <returns>true: если удалось добавить.
    /// false: если не удолось добавить.</returns>
    public bool AddEmployee(Employee newEmployee)
    {
      try
      {
        StaffMembers.Add(newEmployee);
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    /// <summary>
    /// Изменение имени сотрудника.
    /// </summary>
    /// <param name="employeeName">Имя сотрудника.</param>
    /// <param name="newName">Новое имя.</param>
    /// <returns>true: если удалось изменить.
    /// false: если не удолось изменить.</returns>
    public bool ChangeEmployeeName(string employeeName, string newName)
    {
      return ChangeEmployeeName(new Employee(employeeName, 0, 0), newName);
    }

    /// <summary>
    /// Изменение имени сотрудника.
    /// </summary>
    /// <param name="employee">Сотрудник.</param>
    /// <param name="newName">Новое имя.</param>
    /// <returns>true:если удалось изменить.
    /// false: если не удолось изменить.</returns>
    public bool ChangeEmployeeName(Employee employee,string newName)
    {
      try
      {
        Employee ourEmployee = FindEmployee(employee.FullName);
        ourEmployee.FullName = newName;
        return true;
      }
      catch(Exception)
      {
        return false;
      }
    }

    /// <summary>
    /// Изменение часовой оплаты сотрудника.
    /// </summary>
    /// <param name="employee">Сотрудник.</param>
    /// <param name="newSalary">Новая часовая оплата.</param>
    /// <returns>true:если удалось изменить.
    /// false: если не удолось изменить.</returns>
    public bool ChangeEmployeeSalaryPerHours(Employee employee, double newSalary)
    {
      try
      {
        Employee ourEmployee = FindEmployee(employee.FullName);
        ourEmployee.SalaryPerHour = newSalary;
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    /// <summary>
    /// Изменение часовой оплаты сотрудника.
    /// </summary>
    /// <param name="employeeName">Имя сотруднка.</param>
    /// <param name="newSalary">Новая часовая оплата.</param>
    /// <returns>true:если удалось изменить.
    /// false: если не удолось изменить.</returns>
    public bool ChangeEmployeeSalaryPerHours(string employeeName, double newSalary)
    {
      return ChangeEmployeeSalaryPerHours(new Employee(employeeName, 0, 0), newSalary);
    }

    /// <summary>
    /// Изменение часовой ставки сотрудника. 
    /// </summary>
    /// <param name="employeeName">Имя сотруднка.</param>
    /// <param name="newWorkingHours">Часовая ставка.</param>
    /// <returns>true:если удалось изменить.
    /// false: если не удолось изменить.</returns>
    public bool ChangeEmployeeWorkingHours(string employeeName, int newWorkingHours)
    {
      return ChangeEmployeeWorkingHours(new Employee(employeeName, 0, 0), newWorkingHours);
    }

    /// <summary>
    /// Изменение часовой ставки сотрудника. 
    /// </summary>
    /// <param name="employee">Сотрудник.</param>
    /// <param name="newWorkingHours">Часовая ставка.</param>
    /// <returns>true:если удалось изменить.
    /// false: если не удолось изменить.</returns>
    public bool ChangeEmployeeWorkingHours(Employee employee, int newWorkingHours)
    {
      try
      {
        Employee ourEmployee = FindEmployee(employee.FullName);
        ourEmployee.WorkingHours = newWorkingHours;
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }
    
    /// <summary>
    /// Удаление сотрудника из базы организации.
    /// </summary>
    /// <param name="employee">Входные данные сотрудника</param>
    /// <returns>Успешность удаления.</returns>
    public bool RevomeEmployee(Employee employee)
    {
      Employee e = FindEmployee(employee.FullName);
      return StaffMembers.Remove(e);
    }

    /// <summary>
    /// Поиск работника по имени.
    /// </summary>
    /// <param name="name">Имя работника.</param>
    /// <returns>Сотрудник из списка staff.</returns>
    public Employee FindEmployee(string name)
    {
      Employee fEmployee = new Employee(name, -1, -1);
      for(int i=0;i<StaffMembers.Count;i++)
      {
        if(Compare(fEmployee,StaffMembers[i])==0)
        {
          fEmployee = StaffMembers[i];
          break;
        }
      }
      if(fEmployee.WorkingHours != -1)
        return fEmployee;
      else
        throw new Exception();
    }

    public int Compare(Employee x, Employee y)
    {
      if (x != null && y!=null)
      {
        return x.CompareTo(y);
      }
      throw new NotImplementedException();
    }

    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    public static Staff getInstance()
    {
      if (instance == null)
        instance = new Staff();
      return instance;
    }

    private Staff()
    {
      StaffMembers = new List<Employee>();
    }

    
  }
}
