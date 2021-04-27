using ConsommiTounsii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace ConsommiTounsii.Controllers
{
    public class DeliveryController : Controller
    {
        HttpClient Client;
        public DeliveryController()
        {
             Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8083/ConsomiTounsi/servlet/");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
        }

        // GET: Delivery
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: Delivery/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: Delivery/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Delivery/Create
        [HttpPost]
        public ActionResult Create(Delivery delivery)
        {
            try
            {
                HttpResponseMessage response = Client.GetAsync("api/employes").Result;
              //  if (response)
                    // TODO: Add insert logic here

                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Delivery/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Delivery/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Delivery/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Delivery/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult FollowDelivery(long id)
        {
            var responseTask = Client.GetAsync("FollowingOrder/" + id);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readJob = result.Content.ReadAsStringAsync();
                readJob.Wait();

                ViewBag.resultat = readJob.Result;
                return View(new Delivery());

            }
            else
            {

                ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
            }
            return View(new Delivery());
            
        }
    }
}
