using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_kraj
{
    class GraphicCard
    {
        string name;

        public string Name
        {
            get { return name; }
        }

        public GraphicCard(string name)
        {
            this.name = name;
        }
    }
}
