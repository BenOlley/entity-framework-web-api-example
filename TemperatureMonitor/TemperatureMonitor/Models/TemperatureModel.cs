using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TemperatureMonitor.Models
{
    public class TemperatureModel
    {
        [Key]
        public int TemperatureId { get; set; }
        public DateTime Time { get; set; }
        public double Celcius { get; set; }
        public string Location { get; set; }
    }
}