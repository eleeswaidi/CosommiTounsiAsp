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
    public class DeliveryManController : Controller
    {

        HttpClient Client;
        public DeliveryManController()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8083/ConsomiTounsi/servlet/");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        // GET: DeliveryMan
        public ActionResult Index()
        {
            return View();
        }

        // GET: DeliveryMan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DeliveryMan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeliveryMan/Create
        [HttpPost]
        public ActionResult Create(DeliveryMan deliveryman)
        {
            try
            {
                var postJob = Client.PostAsJsonAsync<DeliveryMan>("add-DeliveryMan", deliveryman);
                postJob.Wait();

                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.message = "problème d'ajout";
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: DeliveryMan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DeliveryMan/Edit/5
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

        // GET: DeliveryMan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DeliveryMan/Delete/5
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
    }
}
