using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_kraj
{
    class Printer
    {
        string caption;

        public string Caption
        {
            get { return caption; }
        }
        string isNetwork;

        public string IsNetwork
        {
            get { return isNetwork; }
        }
        public Printer (string caption, string isNetwork)
        {
            this.caption = caption;
            this.isNetwork = isNetwork;
        }

    }
}
