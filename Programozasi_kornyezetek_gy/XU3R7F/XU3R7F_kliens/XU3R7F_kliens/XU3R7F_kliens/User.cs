using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XU3R7F_kliens
{
    internal class User
    {
        private string name;
        private string token;

        public string Name { get { return name; } set { name = value; } }
        public string Token { get { return token; } set { token = value; } }

        public void Logout()
        {
            this.name = null;
            this.token = null;
        }
    }
}
