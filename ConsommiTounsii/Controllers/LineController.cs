using ConsommiTounsii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ConsommiTounsii.Controllers
{
    public class LineController : Controller
    {
        // GET: Line
        public ActionResult Index()
        {
            IEnumerable<Line> lines = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/api/auth/");
                var responseTask = client.GetAsync("retrieve-all-lines");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Line>>();
                    readJob.Wait();
                    lines = readJob.Result;
                }
                else
                {
                    lines = Enumerable.Empty<Line>();
                    ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
                }
            }
            return View(lines);
        }

        public ActionResult Details(int id)
        {
            Line lines = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/api/auth/");
                var responseTask = client.GetAsync("retrieve-line/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Line>();
                    readTask.Wait();

                    lines = readTask.Result;
                }
            }
            return View(lines);
        }

        // Post : Publicity
        public ActionResult Create()
        {
            IEnumerable<LineManager> linesmanager = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/api/auth/");
                var responseTask = client.GetAsync("retrieve-all-linemanagers");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<LineManager>>();
                    readJob.Wait();
                    linesmanager = readJob.Result;
                }
            }

            ViewBag.LineManagerList = new SelectList(linesmanager, "userId", "username");

            return View();
        }

        [HttpPost]
        public ActionResult Create(Line line)
        {
            string LineManagerId = Request.Form["LineManagerList"].ToString();
            LineManager c = new LineManager();
            c.user_id = Convert.ToInt32(LineManagerId);
            line.lineManager = c;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/api/auth/add-line");
                var postJob = client.PostAsJsonAsync<Line>("add-line", line);
                postJob.Wait();

                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
                return View(line);
            }

        }

        //Delete a publicity
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/api/auth/");
                var deleteTask = client.DeleteAsync("remove-line/" + id.ToString());

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
            IEnumerable<LineManager> lineManagers = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/api/auth/");
                var responseTask = client.GetAsync("retrieve-all-linemanagers");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<LineManager>>();
                    readJob.Wait();
                    lineManagers = readJob.Result;
                }
            }

            ViewBag.categoryList = new SelectList(lineManagers, "userId", "username");

            Line line = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/api/auth/");
                var responseTask = client.GetAsync("retrieve-line/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Line>();
                    readTask.Wait();

                    line = readTask.Result;
                }
            }
            return View(line);
        }

        //craete post  method to update the data
        [HttpPost]
        public ActionResult Edit(Line line)
        {
            LineManager categorys = null;
            long id = line.lineManager.user_id;
            Console.WriteLine(id);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/api/auth/modify-line");
                var putTask = client.PutAsJsonAsync<Line>("modify-line", line);
                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)

                    return RedirectToAction("Index");
                return View(line);

            }
        }
    }
}
