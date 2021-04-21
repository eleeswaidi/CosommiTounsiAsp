using ConsommiTounsii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace ConsommiTounsii.Controllers
{
    public class OrderController : Controller
    {
        //private static Client client;


       

        // GET: order
        public ActionResult Index()
        {
          

            IEnumerable<Orders> orders = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8084/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("retrieve-all-orders");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Orders>>();
                    readJob.Wait();
                    orders = (IEnumerable<Orders>)readJob.Result;

                }
                else
                {
                    orders = Enumerable.Empty<Orders>();
                    ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
                }
            }
            
            return View(orders);

        }
        // Post : order

        public ActionResult Create()
        {
            List<Orders> o = new List<Orders>();
            IEnumerable<Product> products = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8084/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("retrieve-all-products");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Product>>();
                    readJob.Wait();
                    products = readJob.Result;
                }
            }

            ViewBag.productList = new SelectList(products, "id_prod", "name_prod");
            return View();


        }
        [HttpPost]
        public ActionResult Create(Orders o)
        {
            string idprod = Request.Form["productList"].ToString();
            Product product = null;
         
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8084/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("retrieve-product/" + idprod);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();

                    product = readTask.Result;
                }
            }

            o.product = product;
           
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8084/ConsomiTounsi/servlet/add-orders");
                var postJob = client.PostAsJsonAsync<Orders>("add-orders",o);

                postJob.Wait();

                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");

                return View(o);
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8084/ConsomiTounsi/servlet/");
                var putTask = client.PutAsJsonAsync<Orders>("affectOrderToBasket/" + o.basket.id_basket.ToString() + "/" + o.id_order.ToString(), o);

                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)

                    return RedirectToAction("Index");
                return View(o);

            }

        }

        //Delete order
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8084/ConsomiTounsi/servlet/");
                var deleteTask = client.DeleteAsync("remove-orders/" + id.ToString());

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
        }

        //create an action for getting data by id  for edit form
        public ActionResult Edit(int id)
        {
            Orders orders = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8084/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("retrieve-orders/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Orders>();
                    readTask.Wait();

                    orders = readTask.Result;
                }
            }
            return View(orders);
        }

        //create post  method to update the data
        [HttpPost]
        public ActionResult Edit(int id,Orders order)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8084/ConsomiTounsi/servlet/");
                var putTask = client.PutAsJsonAsync<Orders>("modify-orders/"+id.ToString()+"/"+order.quantity.ToString(), order);
                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)

                { return RedirectToAction("Index"); }

                return View(order);

            }
           
        }


        public ActionResult Affect(int id)
        {
            Orders orders = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8084/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("retrieve-orders/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Orders>();
                    readTask.Wait();

                    orders = readTask.Result;
                }
            }
            return View(orders);
        }


        //affect order to basket
        [HttpPost]
        public ActionResult Affect(Orders order,int id,Basket basket)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8084/ConsomiTounsi/servlet/");
                var putTask = client.PutAsJsonAsync<Orders>("affectOrderToBasket/" + basket.id_basket.ToString() + "/" +id.ToString(), order);
              
                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)

                    return RedirectToAction("Index");
                return View(order);

            }
        }

 
    }
}