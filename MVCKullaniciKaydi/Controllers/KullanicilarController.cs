using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCKullaniciKaydi.Models;

namespace MVCKullaniciKaydi.Controllers
{
    public class KullanicilarController : Controller
    {
        private KullaniciGirisiEntities db = new KullaniciGirisiEntities();


        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Giris(Kullanicilar Model)
        {

            var istifadeci = db.Kullanicilar.FirstOrDefault(x => x.Kullanici_Adi == Model.Kullanici_Adi && x.Kullanici_Parolasi == Model.Kullanici_Parolasi);

            if (istifadeci != null)
            {
                Session["Kullanici_Adi"] = istifadeci;
                return RedirectToAction("Index", "Home");

            }

            ViewBag.Hata("Kullanici Adi Ve ya Parola Yanlis");

            return View();
        }

        // GET: Kullanicilar
        public ActionResult Index()
        {
            return View(db.Kullanicilar.ToList());
        }

        // GET: Kullanicilar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanicilar kullanicilar = db.Kullanicilar.Find(id);
            if (kullanicilar == null)
            {
                return HttpNotFound();
            }
            return View(kullanicilar);
        }

        // GET: Kullanicilar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kullanicilar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Kullanici_Adi,Kullanici_Parolasi,Kullanici_Emaili,Tarih")] Kullanicilar kullanicilar)
        {
            if (ModelState.IsValid)
            {
                db.Kullanicilar.Add(kullanicilar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kullanicilar);
        }

        // GET: Kullanicilar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanicilar kullanicilar = db.Kullanicilar.Find(id);
            if (kullanicilar == null)
            {
                return HttpNotFound();
            }
            return View(kullanicilar);
        }

        // POST: Kullanicilar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Kullanici_Adi,Kullanici_Parolasi,Kullanici_Emaili,Tarih")] Kullanicilar kullanicilar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kullanicilar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kullanicilar);
        }

        // GET: Kullanicilar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanicilar kullanicilar = db.Kullanicilar.Find(id);
            if (kullanicilar == null)
            {
                return HttpNotFound();
            }
            return View(kullanicilar);
        }

        // POST: Kullanicilar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kullanicilar kullanicilar = db.Kullanicilar.Find(id);
            db.Kullanicilar.Remove(kullanicilar);
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
