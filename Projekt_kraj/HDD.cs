using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_kraj
{
    class HDD
    {
        string size;
        string name;
      

        public double SizeDouble
        {
            get { return ToDoubleGigaByte(); }            
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Size
        {
            get { return ToGigaByte(); }
            set { size = value; }
        }
        public HDD()
        {
            
        }
        private string ToGigaByte()
        {
            double size_ = double.Parse(this.size);
            double sizeGB = size_ * 0.000000001;

            return Math.Round(sizeGB).ToString()+" GB";
        }

        private double ToDoubleGigaByte()
        {
            double size_ = double.Parse(this.size);
            double sizeGB = size_ * 0.000000001;

            return sizeGB;
            
        }


    }
}
