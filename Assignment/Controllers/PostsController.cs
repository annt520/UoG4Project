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
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Category).Include(x=> x.PostTags);
            var tags = db.Tags.Include(p => p.TagName);
            return View(posts.ToList());
        }

        public ActionResult PostEditDelete()
        {
            var posts = db.Posts.Include(p => p.Category).Include(x => x.PostTags);
            var tags = db.Tags.Include(p => p.TagName);
            return View(posts.ToList());

        }

        // GET: Posts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Where(x => x.ID == id).Include(x => x.PostTags).Include(x => x.Category).First();
            //adding viewer count
            post.ViewCount = post.ViewCount + 1;
            db.SaveChanges();
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        [Authorize (Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName");
            ViewBag.TagID = new SelectList(db.Tags, "ID", "TagName");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post, HttpPostedFileBase fileupload, List<string>TagId)
        {
            if (ModelState.IsValid)
            {

                if (fileupload != null)
                {
                    string ImageName = System.IO.Path.GetFileName(fileupload.FileName);
                    string physicalPath = Server.MapPath("~/Imgs/" + ImageName);
                    fileupload.SaveAs(physicalPath);

                    post.Image = ImageName;
                }


                post.ID = Guid.NewGuid();
                db.Posts.Add(post);
                db.SaveChanges();

                foreach (var tag in TagId)
                {
                    PostTag pt = new PostTag
                    {
                        ID = Guid.NewGuid(),
                        PostID = post.ID,
                        TagId = new Guid(tag)
                    };
                    db.PostTags.Add(pt);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", post.CategoryID);
            ViewBag.TagID = new SelectList(db.Tags, "ID", "TagName");
            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            PostTag pt = db.PostTags.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", post.CategoryID);
            ViewBag.TagID = new SelectList(db.Tags, "ID", "TagName");
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post, HttpPostedFileBase fileupload, List<string> TagId)
        {
            if (ModelState.IsValid)
            {


                if (fileupload != null)
                {
                    string ImageName = System.IO.Path.GetFileName(fileupload.FileName);
                    string physicalPath = Server.MapPath("~/Imgs/" + ImageName);
                    fileupload.SaveAs(physicalPath);

                    post.Image = ImageName;
                }

                foreach (var tag in TagId)
                {
                    PostTag pt = new PostTag
                    {
                        ID = Guid.NewGuid(),
                        PostID = post.ID,
                        TagId = new Guid(tag)
                    };
                    db.PostTags.Add(pt);
                }



                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", post.CategoryID);
            ViewBag.TagID = new SelectList(db.Tags, "ID", "TagName");
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("PostEditDelete");
        }

        // POST: Posts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    Post post = db.Posts.Find(id);
        //    db.Posts.Remove(post);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
