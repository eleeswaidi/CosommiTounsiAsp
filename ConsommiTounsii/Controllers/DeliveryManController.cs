using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsommiTounsii.Controllers
{
    public class DeliveryManController : Controller
    {
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
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
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
