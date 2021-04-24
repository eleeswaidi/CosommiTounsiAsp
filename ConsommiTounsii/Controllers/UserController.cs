using ConsommiTounsii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ConsommiTounsii.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<User> users = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/api/auth/");
                var responseTask = client.GetAsync("retrieve-all-users");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<User>>();
                    readJob.Wait();
                    users = readJob.Result;

                }
                else
                {
                    users = Enumerable.Empty<User>();
                    ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
                }
            }
            return View(users);
        }


        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/api/auth/signup");
                var postJob = client.PostAsJsonAsync<User>("signup", user);
                postJob.Wait();

                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
                // ViewBag.id_cat = new SelectList(cats, "id_cat", "name_cat");
                return View(user);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(User u)
        {
            string Baseurl = "http://localhost:8086/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("api/auth/login");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(u);
        }


        public ActionResult Details(int id)
        {
            User users = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/api/auth/");
                var responseTask = client.GetAsync("retrieve-user/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<User>();
                    readTask.Wait();

                    users = readTask.Result;
                }
            }
            return View(users);
        }
    }
}
