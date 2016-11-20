using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using TemperatureMonitor.ClassLibrary;
using TemperatureMonitor.DAL.Context;
using TemperatureMonitor.DAL.Objects;
using TemperatureMonitor.Models;

namespace TemperatureMonitor.Controllers
{
    public class TemperaturesController : Controller
    {
        private MonitorContext db = new MonitorContext();

        // GET: Temperatures
        public ActionResult Index()
        {
            var temperatureList = db.Temperatures.ToList();

            IEnumerable<TemperatureModel> temperatureModel = temperatureList.Select(tl => new TemperatureModel() {TemperatureId = tl.TemperatureId ,Celcius = tl.Celcius, Time = tl.Time, Location = tl.Location });
            
            return View(temperatureModel);
        }

        // GET: Temperatures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Temperature temperature = db.Temperatures.Find(id);

            if (temperature == null)
            {
                return HttpNotFound();
            }

            TemperatureModel temperatureModel = new TemperatureModel() { TemperatureId = temperature.TemperatureId, Celcius = temperature.Celcius, Time = temperature.Time, Location = temperature.Location };

            return View(temperatureModel);
        }

        // GET: Temperatures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Temperatures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TemperatureId,Time,Celcius,Location")] TemperatureModel temperatureModel)
        {
            if (ModelState.IsValid)
            {
                Temperature temperature = new Temperature() { Celcius = temperatureModel.Celcius, Time = temperatureModel.Time, Location = temperatureModel.Location};

                db.Temperatures.Add(temperature);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(temperatureModel);
        }

        // GET: Temperatures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temperature temperature = db.Temperatures.Find(id);
            if (temperature == null)
            {
                return HttpNotFound();
            }

            TemperatureModel temperatureModel = new TemperatureModel() { TemperatureId = temperature.TemperatureId, Celcius = temperature.Celcius, Time = temperature.Time, Location = temperature.Location };

            return View(temperatureModel);
        }

        // POST: Temperatures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TemperatureId,Time,Celcius,Location")] TemperatureModel temperatureModel)
        {
            if (ModelState.IsValid)
            {
                Temperature temperature = new Temperature() { TemperatureId = temperatureModel.TemperatureId, Celcius = temperatureModel.Celcius, Time = temperatureModel.Time, Location = temperatureModel.Location };
                db.Entry(temperature).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(temperatureModel);
        }

        // GET: Temperatures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temperature temperature = db.Temperatures.Find(id);
            if (temperature == null)
            {
                return HttpNotFound();
            }
            TemperatureModel temperatureModel = new TemperatureModel() { TemperatureId = temperature.TemperatureId, Celcius = temperature.Celcius, Time = temperature.Time, Location = temperature.Location };
            return View(temperatureModel);
        }

        // POST: Temperatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Temperature temperature = db.Temperatures.Find(id);
            db.Temperatures.Remove(temperature);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public FileResult DownloadXML(string FileName)
        {

            XMLHelperClass helper = new XMLHelperClass();

            var xmldoc = helper.DownloadXML(db);

            using (MemoryStream ms = new MemoryStream()){

                xmldoc.Save(ms);
                return File(new MemoryStream(ms.ToArray()), "text/xml", FileName);
            }
        }

        public ActionResult UploadXML()
        {
            return View();
        }


        [HttpPost]
        public ActionResult UploadXML(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0 && file.ContentType == "text/xml")
            {
                string streamContents;
                using (var sr = new StreamReader(file.InputStream))
                {
                    streamContents = sr.ReadToEnd();
                }
                var document = XDocument.Parse(streamContents);



                var currenTemperatureIDList = db.Temperatures.Select(m => m.TemperatureId).ToList();

                // TODO: work with the document here

                try {
                    foreach (var measurement in document.Root.Elements())
                    {
                        //null check
                        if (measurement.Attribute("id").Value != null)
                        {
                            var measurementInt = Convert.ToInt16(measurement.Attribute("id").Value);

                            //check if the ID doesn't already exist.
                            if (!currenTemperatureIDList.Contains(measurementInt))
                            {
                                Temperature temperature = new Temperature() { TemperatureId = measurementInt };

                                //Could loop or directly access elements.
                                foreach (var data in measurement.Elements())
                                {
                                    if (data.Name == "Celcius")
                                    {
                                        temperature.Celcius = Convert.ToDouble(data.Value);
                                    }
                                    else if (data.Name == "Time")
                                    {
                                        temperature.Time = Convert.ToDateTime(data.Value);
                                    }
                                    else if (data.Name == "Location")
                                    {
                                        temperature.Location = data.Value;
                                    }
                                }
                                db.Temperatures.Add(temperature);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // you could handle the exception here also.
                    throw new HttpException(500, "Internal Server error");
                }

                //If there are no issues, save.
                db.SaveChanges();

            }
            return View();
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
