using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TemperatureMonitor.DAL.Context;
using TemperatureMonitor.Models;
using TemperatureMonitor.DAL.Objects;

namespace TemperatureMonitor.Controllers
{
    public class DataController : ApiController
    {
        private MonitorContext db = new MonitorContext();

        // GET: api/Data
        public IQueryable<TemperatureModel> GetTemperatures()
        {

            var temperatureList = db.Temperatures.ToList();

            IQueryable<TemperatureModel> temperatureModel = temperatureList.Select(tl => new TemperatureModel() { TemperatureId = tl.TemperatureId, Celcius = tl.Celcius, Time = tl.Time, Location = tl.Location }).AsQueryable();

            return temperatureModel;
        }

        // GET: api/Data/5
        [ResponseType(typeof(TemperatureModel))]
        public async Task<IHttpActionResult> GetTemperature(int id)
        {
            Temperature temperature = await db.Temperatures.FindAsync(id);

            TemperatureModel temperatureModel = new TemperatureModel() { TemperatureId = temperature.TemperatureId, Celcius = temperature.Celcius, Time = temperature.Time, Location = temperature.Location };

            if (temperature == null)
            {
                return NotFound();
            }

            return Ok(temperatureModel);
        }

        // PUT: api/Data/5
        [ResponseType(typeof(TemperatureModel))]
        public async Task<IHttpActionResult> PutTemperature(int id, TemperatureModel temperatureModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Temperature temperature = new Temperature() { TemperatureId = temperatureModel.TemperatureId, Celcius = temperatureModel.Celcius, Time = temperatureModel.Time, Location = temperatureModel.Location };

            if (id != temperature.TemperatureId)
            {
                return BadRequest();
            }

            db.Entry(temperature).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemperatureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(temperatureModel);
        }

        // POST: api/Data
        [ResponseType(typeof(TemperatureModel))]
        public async Task<IHttpActionResult> PostTemperature(TemperatureModel temperatureModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Temperature temperature = new Temperature() { Celcius = temperatureModel.Celcius, Time = temperatureModel.Time, Location = temperatureModel.Location };

            db.Temperatures.Add(temperature);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = temperature.TemperatureId }, temperature);
        }

        // DELETE: api/Data/5
        [ResponseType(typeof(TemperatureModel))]
        public async Task<IHttpActionResult> DeleteTemperature(int id)
        {
            Temperature temperature = await db.Temperatures.FindAsync(id);
            if (temperature == null)
            {
                return NotFound();
            }

            db.Temperatures.Remove(temperature);
            await db.SaveChangesAsync();

            TemperatureModel temperatureModel = new TemperatureModel() { TemperatureId = temperature.TemperatureId, Celcius = temperature.Celcius, Time = temperature.Time, Location = temperature.Location };

            return Ok(temperatureModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TemperatureExists(int id)
        {
            return db.Temperatures.Count(e => e.TemperatureId == id) > 0;
        }
    }
}