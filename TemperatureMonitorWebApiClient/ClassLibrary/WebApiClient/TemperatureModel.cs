﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.WebApiClient
{
    public class TemperatureModel
    {
        public int TemperatureId { get; set; }
        public DateTime Time { get; set; }
        public double Celcius { get; set; }
        public string Location { get; set; }
    }
}