using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Phonebook
{
    internal class Abonent
    {
        public string UserName
        { set; get; }

        public long UserPhoneNumber
        { set; get; }

        public Abonent(string name, long phonenumber)
        {
            UserName = name;
            UserPhoneNumber = phonenumber;
        }
    }
}