using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureMonitor.DAL.Objects
{
    public class Temperature
    {
        public Temperature()
        {

        }

        public int TemperatureId { get; set; }
        public DateTime Time { get; set; }
        public double Celcius { get; set; }
        //TODO : Refactor this to it's own Location object.
        public string Location { get; set; }

        //Navigation Property
        //public Location Location { get; set; }

    }
}
