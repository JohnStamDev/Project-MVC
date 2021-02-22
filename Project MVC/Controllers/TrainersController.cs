using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_MVC.DAL;
using Project_MVC.Models;
using PagedList;

namespace Project_MVC.Controllers
{
    public class TrainersController : Controller
    {
        readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Trainers
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "";
            ViewBag.LastNameSortParm = sortOrder == "lastname" ? "lastname_desc" : "lastname";
            ViewBag.SubjectSortParm = sortOrder == "subject" ? "subject_desc" : "subject";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var trainers = from s in db.Trainers
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                trainers = trainers.Where(s => s.FirstName.Contains(searchString)
                                       || s.LastName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "firstname_desc":
                    trainers = trainers.OrderByDescending(s => s.FirstName);
                    break;
                case "lastname":
                    trainers = trainers.OrderBy(s => s.LastName);
                    break;
                case "lastname_desc":
                    trainers = trainers.OrderByDescending(s => s.LastName);
                    break;
                case "subject":
                    trainers = trainers.OrderBy(s => s.Subject);
                    break;
                case "subject_desc":
                    trainers = trainers.OrderByDescending(s => s.Subject);
                    break;
                default:
                    trainers = trainers.OrderBy(s => s.FirstName);
                    break;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(trainers.ToPagedList(pageNumber, pageSize));
        }

        // GET: Trainers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // GET: Trainers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trainers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Subject")] Trainer trainer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Trainers.Add(trainer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, " + "and if the problem persists see your system administrator.");
            }
            return View(trainer);
        }

        // GET: Trainers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // POST: Trainers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var trainerToUpdate = db.Trainers.Find(id);
            if (TryUpdateModel(trainerToUpdate,"",new string[] { "FirstName,LastName,Subject" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch(DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem " + "persists, see your system administrator.");
                }
            }
            return View(trainerToUpdate);
        }

        // GET: Trainers/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists " + "see your system administrator.";
            }
            Trainer trainer = db.Trainers.Find(id);
            if(trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // POST: Trainers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Trainer trainer = db.Trainers.Find(id);
                db.Trainers.Remove(trainer);
                db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
