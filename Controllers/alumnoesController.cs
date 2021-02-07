using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRUDescuelaMVC.Models;

namespace CRUDescuelaMVC.Controllers
{
    public class alumnoesController : Controller
    {
        private escuelaEntities db = new escuelaEntities();

        // GET: alumnoes
        public ActionResult Index()
        {
            var alumno = db.alumno.Include(a => a.carrera);
            return View(alumno.ToList());
        }

        // GET: alumnoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            alumno alumno = db.alumno.Find(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            return View(alumno);
        }

        // GET: alumnoes/Create
        public ActionResult Create()
        {
            ViewBag.clave_c1 = new SelectList(db.carrera, "Clave_c", "nom_c");
            return View();
        }

        // POST: alumnoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mat_alu,nom_alu,edad_alu,sem_alu,gen_alu,clave_c1")] alumno alumno)
        {
            if (ModelState.IsValid)
            {
                db.alumno.Add(alumno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.clave_c1 = new SelectList(db.carrera, "Clave_c", "nom_c", alumno.clave_c1);
            return View(alumno);
        }

        // GET: alumnoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            alumno alumno = db.alumno.Find(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            ViewBag.clave_c1 = new SelectList(db.carrera, "Clave_c", "nom_c", alumno.clave_c1);
            return View(alumno);
        }

        // POST: alumnoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mat_alu,nom_alu,edad_alu,sem_alu,gen_alu,clave_c1")] alumno alumno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alumno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clave_c1 = new SelectList(db.carrera, "Clave_c", "nom_c", alumno.clave_c1);
            return View(alumno);
        }

        // GET: alumnoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            alumno alumno = db.alumno.Find(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            return View(alumno);
        }

        // POST: alumnoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            alumno alumno = db.alumno.Find(id);
            db.alumno.Remove(alumno);
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
