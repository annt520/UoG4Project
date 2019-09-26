using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment.Models;

namespace Assignment.Controllers
{
    public class PostTagsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PostTags
        public ActionResult Index()
        {
            var postTags = db.PostTags.Include(p => p.Post).Include(p => p.Tag);
            return View(postTags.ToList());
        }

        // GET: PostTags/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostTag postTag = db.PostTags.Find(id);
            if (postTag == null)
            {
                return HttpNotFound();
            }
            return View(postTag);
        }

        // GET: PostTags/Create
        public ActionResult Create()
        {
            ViewBag.PostID = new SelectList(db.Posts, "ID", "Image");
            ViewBag.TagId = new SelectList(db.Tags, "ID", "TagName");
            return View();
        }

        // POST: PostTags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PostID,TagId")] PostTag postTag)
        {
            if (ModelState.IsValid)
            {
                postTag.ID = Guid.NewGuid();
                db.PostTags.Add(postTag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PostID = new SelectList(db.Posts, "ID", "Image", postTag.PostID);
            ViewBag.TagId = new SelectList(db.Tags, "ID", "TagName", postTag.TagId);
            return View(postTag);
        }

        // GET: PostTags/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostTag postTag = db.PostTags.Find(id);
            if (postTag == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostID = new SelectList(db.Posts, "ID", "Image", postTag.PostID);
            ViewBag.TagId = new SelectList(db.Tags, "ID", "TagName", postTag.TagId);
            return View(postTag);
        }

        // POST: PostTags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PostID,TagId")] PostTag postTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PostID = new SelectList(db.Posts, "ID", "Image", postTag.PostID);
            ViewBag.TagId = new SelectList(db.Tags, "ID", "TagName", postTag.TagId);
            return View(postTag);
        }

        // GET: PostTags/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostTag postTag = db.PostTags.Find(id);
            if (postTag == null)
            {
                return HttpNotFound();
            }
            return View(postTag);
        }

        // POST: PostTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            PostTag postTag = db.PostTags.Find(id);
            db.PostTags.Remove(postTag);
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
