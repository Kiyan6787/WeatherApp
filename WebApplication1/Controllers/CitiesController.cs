using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CitiesController : Controller
    {
        private ApplicationDBContext _context = new ApplicationDBContext();

        // GET: Cities
        public ActionResult Index()
        {
            return View(_context.Cities.ToList());
        }


        // GET: Cities/Create
        public ActionResult Create()
        {
            var cities = new Cities();
            return View(cities);
        }

        // GET: Cities/WeatherBox
        public ActionResult WeatherBox()
        {
            return View();
        }

        // Gets the cities from the DB to display in the drop down menu.
        [HttpGet]
        public ActionResult CityDropdownMenu()
        {
            var cities = _context.Cities.ToList();

            return Json(cities, JsonRequestBehavior.AllowGet); // Return JSON data
        }

        // Method for displaying the weather details such as temp, humidity, etc., on the page.
        [HttpGet]
        public ActionResult WeatherDetails(int cityId)
        {
            var city = _context.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city != null)
            {
                var weatherDetails = new
                {
                    Temperature = $"{city.temperature} C",
                    Description = city.weatherType,
                    Humidity = $"{city.Humidity} %",
                    WindSpeed = $"{city.WindSpeed} km/h"
                };

                return Json(weatherDetails, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Error = "City not found" }, JsonRequestBehavior.AllowGet);
            }
        }

        //Method to display data on the kendo grid
        public ActionResult Select([DataSourceRequest] DataSourceRequest request)
        {
            var data = _context.Cities.ToList();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        //Method for the excel and pdf exports.
        [HttpPost]
        public ActionResult Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        // POST: Cities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,cityName,temperature,weatherType,Humidity,WindSpeed")] Cities cities)
        {
            if (ModelState.IsValid)
            {
                _context.Cities.Add(cities);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            return View(cities);
        }




    }
}
