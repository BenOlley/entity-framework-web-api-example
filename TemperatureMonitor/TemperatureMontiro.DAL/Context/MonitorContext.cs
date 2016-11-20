using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemperatureMonitor.DAL.Objects;

namespace TemperatureMonitor.DAL.Context
{
    public class MonitorContext : DbContext
    {
        public MonitorContext() : base("name=MonitorConnection")
        {

        }

        public DbSet<Temperature> Temperatures { get; set; }
    }
}
