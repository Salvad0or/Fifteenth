using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindowLibrary.Models
{
    /// <summary>
    /// Шаблон клиента
    /// </summary>
    public class Clients
    {
        public string Name { get; set; }

        public string SurName { get; set; }

        public int Phone { get; set; }

        public int Balance { get; set; }

        public string Passport { get; set; }

        public string Comments { get; set; }

        public Clients(string name, string surName, int phone, string passport, int balance, string comments = null)
        {
            Name = name;
            SurName = surName;
            Phone = phone;
            Passport = passport;
            Balance = balance;
            Comments = comments;
        }

        public Clients()
        {

        }
    }
}
