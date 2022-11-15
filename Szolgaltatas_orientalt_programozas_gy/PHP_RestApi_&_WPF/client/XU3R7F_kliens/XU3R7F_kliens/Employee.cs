using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XU3R7F_kliens
{
    internal class Employee
    {
        private int id;
        private string name;
        private string email;
        private string department;
        private int salary;
        private bool admin;

        public int Id { get { return id; } }
        public string Name { get { return name; } }
        public string Email { get { return email; } }
        public string Department { get { return department; } }
        public string Salary { get { return salary.ToString("C0", CultureInfo.CurrentCulture); } }
        public bool Admin { get { return admin; } }


        public Employee(int id, string name, string email, string department, int salary, int isAdmin)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.department = department;
            this.salary = salary;
            this.admin = isAdmin == 0 ? false : true;
        }


        public override string ToString()
        {
            return $"Name: {Name}\n";
        }
    }
}
