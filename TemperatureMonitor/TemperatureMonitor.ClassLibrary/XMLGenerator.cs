using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TemperatureMonitor.DAL.Context;
using TemperatureMonitor.DAL.Objects;

namespace TemperatureMonitor.ClassLibrary
{
    public class XMLHelperClass
    {
        public XDocument DownloadXML(MonitorContext db)
        {
            var temperatureList = db.Temperatures.ToList();

            XDocument xmldoc = new XDocument(new XElement("Temperatures"));

            foreach (Temperature temperature in temperatureList)
            {
                xmldoc.Root.Add(
                    new XElement("Measurement", new XAttribute("id", temperature.TemperatureId),
                        new XElement("Celcius", temperature.Celcius),
                        new XElement("Time", temperature.Time.ToString()),
                        new XElement("Location", temperature.Location)
                        )
                );
            }
            return xmldoc;
        }
    }
}
