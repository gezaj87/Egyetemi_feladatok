using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_Feladat_Server
{
    internal class User
    {
        private string name;
        private string passwd;
        private int money;

        public string Name
        {
            get { return name; }
        }

        public int Money
        {
            get { return money; }
            set { money = value; }
        }

        public User(string name, string passwd, int money)
        {
            this.name = name;
            this.passwd = passwd;
            this.money = money;
        }
    }
}
