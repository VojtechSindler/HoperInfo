using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_kraj
{
    class NetworkAdapter
    {
        string productName;

        public string ProductName
        {
            get { return productName; }
        }

        string speed;

        public string Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        string macAddress;

        public string MacAddress
        {
            get { return macAddress; }
            set { macAddress = value; }
        }
        string physicalAdapter;

        public string PhysicalAdapter
        {
            get { return physicalAdapter; }
        }

        public NetworkAdapter(string productName, string physicalAdapter)
        {
            this.productName = productName;
            this.physicalAdapter = physicalAdapter;
        }
    }
}
