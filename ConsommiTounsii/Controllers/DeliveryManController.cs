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
            var responseTask = Client.GetAsync("getall-DeliveryMan");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readJob = result.Content.ReadAsAsync<IList<DeliveryMan>>();
                readJob.Wait();
                var deliveryMen = readJob.Result;
                foreach(DeliveryMan dm in deliveryMen)
                {
                    dm.start_time = dm.StartTime.ToShortTimeString();
                    dm.end_time = dm.EndTime.ToShortTimeString();
                }
               
                return View(deliveryMen);
            }
            else
            {
                 
                ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
            }
            return View(Enumerable.Empty<DeliveryMan>());
        }

        // GET: DeliveryMan/Details/5
        public ActionResult Details(long id)
        {
            var responseTask = Client.GetAsync("getDeliveryman/"+id);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readJob = result.Content.ReadAsAsync<DeliveryMan>();
                readJob.Wait();
                DeliveryMan deliveryMen =(DeliveryMan) readJob.Result;

                deliveryMen.start_time = deliveryMen.StartTime.ToShortTimeString();
                deliveryMen.end_time = deliveryMen.EndTime.ToShortTimeString();
                

                return View(deliveryMen);
            }
            else
            {

                ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
            }
            return View(Enumerable.Empty<DeliveryMan>());
            ;
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
                deliveryman.end_time = "2018-03-01T" + deliveryman.end_time + ":00";
                deliveryman.start_time = "2018-03-01T" + deliveryman.start_time + ":00";
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
        public ActionResult Edit(long id)
        {
            var responseTask = Client.GetAsync("getDeliveryman/" + id);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readJob = result.Content.ReadAsAsync<DeliveryMan>();
                readJob.Wait();
                DeliveryMan deliveryMen = (DeliveryMan)readJob.Result;

                deliveryMen.start_time = deliveryMen.StartTime.ToShortTimeString();
                deliveryMen.end_time = deliveryMen.EndTime.ToShortTimeString();


                return View(deliveryMen);
            }
            else
            {

                ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
            }
            return View(Enumerable.Empty<DeliveryMan>());
            ;
        }

        // POST: DeliveryMan/Edit/5
        [HttpPost]
        public ActionResult Edit(long id, DeliveryMan deliveryman)
        {
            try
            {
                deliveryman.end_time = "2018-03-01T" + deliveryman.end_time + ":00";
                deliveryman.start_time = "2018-03-01T" + deliveryman.start_time + ":00";
                var postJob = Client.PutAsJsonAsync<DeliveryMan>("update-DeliveryMan", deliveryman);
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

        // GET: DeliveryMan/Delete/5
        public ActionResult Delete(int id)
        {
            var responseTask = Client.GetAsync("getDeliveryman/" + id);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readJob = result.Content.ReadAsAsync<DeliveryMan>();
                readJob.Wait();
                DeliveryMan deliveryMen = (DeliveryMan)readJob.Result;

                deliveryMen.start_time = deliveryMen.StartTime.ToShortTimeString();
                deliveryMen.end_time = deliveryMen.EndTime.ToShortTimeString();


                return View(deliveryMen);
            }
            else
            {

                ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
            }
            return View(Enumerable.Empty<DeliveryMan>());
            ;

        }

        // POST: DeliveryMan/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,DeliveryMan dm)
        {
            try
            {
                // TODO: Add delete logic here

                var deleteTask = Client.DeleteAsync("delete-DeliveryMan/" + id);

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
