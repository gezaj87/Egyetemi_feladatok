using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Stream_Server
{
    internal class Product
    {
        //Minden tárgynak van - kod (string) - neve (string) - ára (egész szám) - felrakójának login neve (string) - vásárló login neve (string, csak ha már megvásárolta valaki)
        private string id;
        private string name;
        private int price;
        private string user;

        public Product(string id, string name, int price, string user)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.user = user;
        }

        public string Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public int Price
        {
            get { return price; }
        }

        public string User
        {
            get { return user; }
            set { user = value; }
        }
    
    }
}
