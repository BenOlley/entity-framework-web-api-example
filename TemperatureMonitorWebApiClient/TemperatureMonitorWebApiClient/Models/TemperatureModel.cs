using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TemperatureMonitorWebApiClient.Models
{
    public class TemperatureModel
    {
        public int TemperatureId { get; set; }
        public DateTime Time { get; set; }
        public double Celcius { get; set; }
        public string Location { get; set; }
    }
}