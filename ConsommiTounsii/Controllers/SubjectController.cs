using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ConsommiTounsii.Models;

namespace ConsommiTounsii.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Subject
        public ActionResult Index()
        {
            IEnumerable<Subject> subjects = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("retrieve-all-subjects");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Subject>>();
                    readJob.Wait();
                    subjects = readJob.Result;
                    return View(subjects);
                }
                else
                {
                    subjects = Enumerable.Empty<Subject>();
                    ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
                }
            }
            return View(subjects);
        }

        [HttpPost]
        public ActionResult Index(String searchString)
        {
            IEnumerable<Subject> subjects = null;
            if (!String.IsNullOrEmpty(searchString))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/");
                    var responseTask = client.GetAsync("search-subject-by-similarity/" + searchString);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readJob = result.Content.ReadAsAsync<IList<Subject>>();
                        readJob.Wait();
                        subjects = readJob.Result;
                        return View(subjects);
                    }
                }
            }
            else
            {
                Index();
            }

            return View(subjects);
        }

        // GET: Subject/Details/5
        public ActionResult Details(int id)
        {
            Subject subject = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("retrieve-subject/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Subject>();
                    readTask.Wait();

                    subject = readTask.Result;
                }
            }

            List<Comment> commentsList = subject.comments;
            ViewBag.CommList = commentsList;
            ViewBag.idSub = subject.id_subject;

            return View(subject);
        }


        // GET: Subject/Create
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

        // POST: Subject/Create
        [HttpPost]
        public ActionResult Create(Subject subject)
        {
            string idProduit = Request.Form["productList"].ToString();
            Product p = new Product();
            p.id_prod = Convert.ToInt32(idProduit);
            subject.product = p;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/add-subject");
                var postJob = client.PostAsJsonAsync<Subject>("add-subject", subject);
                postJob.Wait();

                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
                return View(subject);
            }
        }

        // GET: Subject/Edit/5
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

            Subject subject = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("retrieve-subject/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Subject>();
                    readTask.Wait();

                    subject = readTask.Result;
                }
            }
            ViewBag.productList = new SelectList(products, "id_prod", "name_prod");

            return View(subject);
        }

        // POST: Subject/Edit/5
        [HttpPost]
        public ActionResult Edit(Subject subject)
        {
            Product product = null;
            long id = subject.product.id_prod;
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

                    product = readTask.Result;
                }
            }

            subject.product = product;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/modify-subject");
                var putTask = client.PutAsJsonAsync<Subject>("modify-subject", subject);
                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                return View(subject);

            }
        }

        // GET: Subject/Delete/5
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/");
                var deleteTask = client.DeleteAsync("remove-subject/" + id.ToString());

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
        }

        // POST: Subject/Delete/5
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

        public ActionResult MakeComment(String id)
        {

            Subject subject = null;
            Console.WriteLine("aaaa");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("retrieve-subject/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Subject>();
                    readTask.Wait();

                    subject = readTask.Result;
                }
            }

            List<Comment> commentsList = subject.comments;
            ViewBag.CommList = commentsList;

            return View(subject);
        }

        [HttpPost]
        public ActionResult MakeComment(int subId, String comment)
        {
            Subject subject = new Subject();
            subject.id_subject = subId;

            Comment cmnt = new Comment();
            cmnt.subject = subject;
            cmnt.desc_comment = comment;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/add-comment");
                var postJob = client.PostAsJsonAsync<Comment>("add-comment", cmnt);
                postJob.Wait();

                var postResult = postJob.Result;
                /*if (postResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");*/

            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/ConsomiTounsi/servlet/");
                var responseTask = client.GetAsync("retrieve-subject/" + subId.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Subject>();
                    readTask.Wait();

                    subject = readTask.Result;
                }
            }

            List<Comment> commentsList = subject.comments;
            ViewBag.CommList = commentsList;
            ViewBag.idSub = subject.id_subject;
            return View(subject);

        }
    }
}
