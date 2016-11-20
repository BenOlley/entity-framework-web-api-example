using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary.WebApiClient;
using System.Threading.Tasks;

namespace TemperatureMonitorWebApiClient.Controllers
{
    public class TemperaturesController : Controller
    {
        // GET: Temperatures
        public async Task<ActionResult> Index()
        {
            var temperatureObjects = await TemperatureClient.GetTemperatures();
            return View(temperatureObjects);
        }

        // GET: Temperatures/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var temperatureObject = await TemperatureClient.GetTemperature(id);
            return View(temperatureObject);
        }

        // GET: Temperatures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Temperatures/Create
        [HttpPost]
        public async Task<ActionResult> Create(TemperatureModel temperatureModel)
        {
            try
            {
                var temperatureObject = await TemperatureClient.PostTemperature(temperatureModel);

                if (temperatureObject != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(temperatureObject);
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Temperatures/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var temperatureObject = await TemperatureClient.GetTemperature(id);
            return View(temperatureObject);
        }

        // POST: Temperatures/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(TemperatureModel temperatureModel)
        {
            try
            {
                var temperatureObject = await TemperatureClient.PutTemperature(temperatureModel.TemperatureId, temperatureModel);

                if (temperatureObject != null)
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    return View(temperatureObject);
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Temperatures/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var temperatureObject = await TemperatureClient.GetTemperature(id);
            return View(temperatureObject);
        }

        // POST: Temperatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var temperatureObject = await TemperatureClient.DeleteTemperature(id);

                if (temperatureObject != null)
                {
                    return RedirectToAction("Index");

                } else
                {
                    return View(temperatureObject);
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
