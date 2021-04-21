using ConsommiTounsii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ConsommiTounsii.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(String searchString)
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
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        products = products.Where(m => m.name_prod.Contains(searchString)).ToList();
                    }
                    return View(products);
                }
                else
                {
                    products = Enumerable.Empty<Product>();
                    ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
                }
            }
            return View(products);
        }


        public ActionResult Details(int id)
        {
            Product products = null;

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
            return View(products);
        }

        // Post : Product
        public ActionResult Create()
        {
            IEnumerable<Category> categorys = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("retrieve-all-categorys");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Category>>();
                    readJob.Wait();
                    categorys = readJob.Result;
                }
            }

            ViewBag.categoryList = new SelectList(categorys, "id_cat", "name_cat");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product prod)
        {
            string idCat = Request.Form["categoryList"].ToString();
            Category c = new Category();
            c.id_cat = Convert.ToInt32(idCat);
            prod.category = c;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/add-product");
                var postJob = client.PostAsJsonAsync<Product>("add-product", prod);
                postJob.Wait();

                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
                // ViewBag.id_cat = new SelectList(cats, "id_cat", "name_cat");
                return View(prod);
            }

        }

        //Delete a product
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/");
                var deleteTask = client.DeleteAsync("remove-product/" + id.ToString());

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
            IEnumerable<Category> categorys = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("retrieve-all-categorys");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Category>>();
                    readJob.Wait();
                    categorys = readJob.Result;
                }
            }

            ViewBag.categoryList = new SelectList(categorys, "id_cat", "name_cat");

            Product products = null;
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
            return View(products);
        }

        //craete post  method to update the data
        [HttpPost]
        public ActionResult Edit(Product product)
        {

            Category categorys = null;
            long id = product.category.id_cat;
            Console.WriteLine(id);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("retrieve-category/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Category>();
                    readTask.Wait();

                    categorys = readTask.Result;
                }
            }

            product.category = categorys;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/modify-product");
                var putTask = client.PutAsJsonAsync<Product>("modify-product", product);
                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)

                    return RedirectToAction("Index");
                return View(product);

            }
        }

    }
}