using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_kraj
{
    class Memory
    {
        string capacity;

        public string Capacity
        {
            get { return ToGigaByte(capacity); }
        }
        string speed;

        public string Speed
        {
            get { return speed + " Hz"; }
        }

        public Memory(string capacity, string speed)
        {
            this.capacity = capacity;
            this.speed = speed;
        }

        string ToGigaByte(string size)
        {
            double size_ = double.Parse(size);
            double sizeGB = size_/ (1024);
            double sizeGB2 = sizeGB / (1024);
            double sizeGB3 = sizeGB2 / (1024);
            

            return Math.Round(sizeGB3).ToString() + " GB";
        }

    }
}
