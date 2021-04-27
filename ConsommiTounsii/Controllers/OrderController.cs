using ConsommiTounsii.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.Mime;
using System.Web.Mvc;
using System.Windows.Forms;

namespace ConsommiTounsii.Controllers
{
    public class OrderController : Controller
    {
        

        public System.Net.Mail.AttachmentCollection Attachments { get; }


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

        public ActionResult PDF()
        {
            
            IEnumerable<Orders> o = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));

                client.BaseAddress = new Uri("http://localhost:8084/ConsomiTounsi/servlet/");

                var responseTask = client.GetAsync("orders/export/pdf");
                responseTask.Wait();

                var result = responseTask.Result;
             /*   if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Orders>>();
                    readJob.Wait();
                    o = readJob.Result;
                }*/
            }

            
            return Index();


        }

        public ActionResult Email()
        {

            IEnumerable<Orders> o = null;
            using (var client = new HttpClient())
            {
              
                client.BaseAddress = new Uri("http://localhost:8084/ConsomiTounsi/servlet/");

                var responseTask = client.GetAsync("send-mail-attachment");
                responseTask.Wait();

              
            }


            return View(o);


        }


        public ActionResult removefrombasket(int id)
        {
            Basket basket = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8084/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("retrieve-basket/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Basket>();
                    readTask.Wait();

                    basket = readTask.Result;
                }
            }
            return View(basket);
        }


        
        [HttpPost]
        public ActionResult removefrombasket(Orders order, int id, Basket basket)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8084/ConsomiTounsi/servlet/");
                var putTask = client.PutAsJsonAsync<Orders>("removeOrderfromBasket/" + basket.id_basket.ToString() + "/" + id.ToString(), order);

                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)

                    return RedirectToAction("Index");
                return View(order);

            }
        }

        #region elee_delivery

        public ActionResult getMyOrders()
        {
            int id= 4;
            IEnumerable<Orders> orders = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8083/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("getMyOrders/"+id);
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

        public ActionResult getnotaffctedorders()
        {
            IEnumerable<Orders> orders = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8083/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("getNotAffectedOrders");
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

        public JsonResult affectorder(string idorder,string dateorder)
        {
            string retour = "";
            using (var client = new HttpClient())
            {
                var dt= DateTime.Parse(dateorder);
                client.BaseAddress = new Uri("http://localhost:8083/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("affecteddelivery/"+long.Parse(idorder)+"/"+dateorder);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsStringAsync();
                    retour = readJob.Result;
                    readJob.Wait();


                }
                else
                {
                    retour= "Server error occured. Please contact admin for help!";
                }
            }


            return Json(retour, JsonRequestBehavior.AllowGet);
        }

        public JsonResult refreshorders()
        {
            IEnumerable<Orders> orders = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8083/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("getNotAffectedOrders");
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
                     }
            }

            return Json(orders, JsonRequestBehavior.AllowGet);

        }

        #endregion
    }
}