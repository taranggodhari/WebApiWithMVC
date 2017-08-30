using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApiWithMVC.Data;

namespace WebApiWithMVC.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private Entities db = new Entities();
        CarEntities ce = new CarEntities();
        // GET: Car
        public ActionResult Index()
        {
            //var carEntities = db.CarEntities.Include(c => c.AspNetUsers);
           
            ce.UserId = User.Identity.GetUserId();
            var carEntities = db.CarEntities;
            return View(carEntities.ToList());
        }
        // GET: Car/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarEntities carEntities = db.CarEntities.Find(id);
            if (carEntities == null)
            {
                return HttpNotFound();
            }
            return View(carEntities);
        }

        // GET: Car/Create
        public ActionResult Create()
        {
            //ViewBag.UserId = new SelectList(User.Identity.GetUserId(), "Id", "Email");
            return View();
        }

        // POST: Car/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ModelNumber,UserId")] CarEntities carEntities)
        {
            if (ModelState.IsValid)
            {
                db.CarEntities.Add(carEntities);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(User.Identity.GetUserId(), "Id", "Email", carEntities.UserId);
            return View(carEntities);
        }

        // GET: Car/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarEntities carEntities = db.CarEntities.Find(id);
            if (carEntities == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(User.Identity.GetUserId(), "Id", "Email", carEntities.UserId);
            return View(carEntities);
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ModelNumber,UserId")] CarEntities carEntities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carEntities).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(User.Identity.GetUserId(), "Id", "Email", carEntities.UserId);
            return View(carEntities);
        }

        // GET: Car/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarEntities carEntities = db.CarEntities.Find(id);
            if (carEntities == null)
            {
                return HttpNotFound();
            }
            return View(carEntities);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarEntities carEntities = db.CarEntities.Find(id);
            db.CarEntities.Remove(carEntities);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
