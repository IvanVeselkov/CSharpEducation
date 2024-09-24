using System.Collections.Generic;
using System.Collections;
using System;

namespace Task4.StaffInformation
{
  internal class Employee:IEmployee, IComparable<Employee>
  {
    public string FullName { get; set; }
    public double Salary { get => this.SalaryPerHour * this.WorkingHours * 4; }

    public double SalaryPerHour { get; set; }
    
    public int WorkingHours { get; set; }
    public virtual double CalculateBonus => Salary / 10;

    public int CompareTo(Employee other)
    {
      if(other.FullName!=null)
      {
        return this.FullName.CompareTo(other.FullName);
      }
      throw new NotImplementedException();
    }

    /// <summary>
    /// Конструктор класса сотрудник.
    /// </summary>
    /// <param name="name">Полное имя сотрудника (ФИО).</param>
    /// <param name="salaryPerHour">Оплата сотрудника за час работы.</param>
    /// <param name="workingHours">Недельная ставка (в часах).</param>
    public Employee(string name,double salaryPerHour,int workingHours)
    {
      FullName = name;
      SalaryPerHour = salaryPerHour;
      WorkingHours = workingHours;
    }

    
  }
}