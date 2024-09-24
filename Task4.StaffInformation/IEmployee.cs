using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.StaffInformation
{
  interface IEmployee
  {
    public string FullName { get; set; }
    public double Salary { get; }
    public double SalaryPerHour { get; set; }

    public int WorkingHours { get; set; }

    public double CalculateBonus { get; }
  }
}
