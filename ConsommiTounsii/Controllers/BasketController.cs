using ConsommiTounsii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ConsommiTounsii.Controllers
{
    public class BasketController : Controller
    {
        // GET: order
        public ActionResult Index()
        {
            IEnumerable<Basket> basket = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8084/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("retrieve-basket");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Basket>>();
                    readJob.Wait();
                    basket = (IEnumerable<Basket>)readJob.Result;

                }
                else
                {
                    basket = Enumerable.Empty<Basket>();
                    ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
                }
            }
            return View(basket);

        }
        //validate basket

        public ActionResult Validatebasket(int id)
        {
            Basket basket = null;

            

            return View(basket);
        }

        
        //validatebasket
        [HttpPost]
        public ActionResult Validatebasket( string tp, Basket basket,int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8084/ConsomiTounsi/servlet/");
                var putTask = client.PutAsJsonAsync<Basket>("ValidateBasket/" + id.ToString() + "/" + basket.client.user_id.ToString() + "/" + basket.type_paiement.ToString(), basket);
                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)

                    return RedirectToAction("Index");
                return View(basket);

            }
        }


        //Reduction basket

        public ActionResult Reduction(int id)
        {
            Basket basket = null;

           
            return View(basket);
        }


        //Reductionbasket
        [HttpPost]
        public ActionResult Reduction(Basket basket, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8084/ConsomiTounsi/servlet/");
                var putTask = client.PutAsJsonAsync<Basket>("Reduction/" + basket.id_basket.ToString() , basket);
                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)

                    return RedirectToAction("Index");
                return View(basket);

            }
        }
    }



}