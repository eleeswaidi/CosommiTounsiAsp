using Consomitounsi.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Consomitounsi.WebService.Controllers
{
    public class PublicityController : Controller
    {
        // GET: Publicity
        public ActionResult Index()
        {
            IEnumerable<Publicity> pubs = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("retrieve-all-publicity");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Publicity>>();
                    readJob.Wait();
                    pubs = readJob.Result;
                }
                else
                {
                    pubs = Enumerable.Empty<Publicity>();
                    ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
                }
            }
            return View(pubs);
        }

        public ActionResult Details(int id)
        {
            Publicity pubs = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("retrieve-publicity/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Publicity>();
                    readTask.Wait();

                    pubs = readTask.Result;
                }
            }
            return View(pubs);
        }

        // Post : Publicity
        public ActionResult Create()
        {
            IEnumerable<Product> products = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/");
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
        public ActionResult Create(Publicity pub)
        {
            string idprod = Request.Form["productList"].ToString();
            Product p = new Product();
            p.id_prod = Convert.ToInt32(idprod);
            pub.product = p;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/add-publicity");
                var postJob = client.PostAsJsonAsync<Publicity>("add-publicity", pub);
                postJob.Wait();

                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    return Redirect("/Publicity/Index");
                }
                ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
                return View(pub);
            }

        }

        //Delete a publicity
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/");
                var deleteTask = client.DeleteAsync("remove-publicity/" + id.ToString());

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return Redirect("/Publicity/Index");
                }
                return Redirect("/Publicity/Index");
            }
        }

        //create an action for getting data by id  for edit form
        public ActionResult Edit(int id)
        {
            IEnumerable<Product> products = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/");
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

            Publicity pub = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("retrieve-publicity/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Publicity>();
                    readTask.Wait();

                    pub = readTask.Result;
                }
            }
            return View(pub);
        }

        //craete post  method to update the data
        [HttpPost]
        public ActionResult Edit(Publicity pub)
        {
            Product products = null;
            long id = pub.product.id_prod;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("retrieve-product/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();

                    products = readTask.Result;
                }
            }

            pub.product = products;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/modify-publicity");
                var putTask = client.PutAsJsonAsync<Publicity>("modify-publicity", pub);
                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)

                    return Redirect("/Publicity/Index");
                return View(pub);

            }
        }
    }
}