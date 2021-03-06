﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCAppEF.Models;

namespace MVCAppEF.Controllers
{
    public class CustomersController : Controller
    {
        private AdventureWorksEntities db = new AdventureWorksEntities();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.Person).Include(c => c.SalesTerritory).Include(c => c.Store).Take(10);
            var t = customers.ToList();
            return View(t);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.PersonID = new SelectList(db.People, "BusinessEntityID", "PersonType");
            ViewBag.TerritoryID = new SelectList(db.SalesTerritories, "TerritoryID", "Name");
            ViewBag.StoreID = new SelectList(db.Stores, "BusinessEntityID", "Name");
            return View();
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,PersonID,StoreID,TerritoryID,AccountNumber,rowguid,ModifiedDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonID = new SelectList(db.People, "BusinessEntityID", "PersonType", customer.PersonID);
            ViewBag.TerritoryID = new SelectList(db.SalesTerritories, "TerritoryID", "Name", customer.TerritoryID);
            ViewBag.StoreID = new SelectList(db.Stores, "BusinessEntityID", "Name", customer.StoreID);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonID = new SelectList(db.People, "BusinessEntityID", "PersonType", customer.PersonID);
            ViewBag.TerritoryID = new SelectList(db.SalesTerritories, "TerritoryID", "Name", customer.TerritoryID);
            ViewBag.StoreID = new SelectList(db.Stores, "BusinessEntityID", "Name", customer.StoreID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,PersonID,StoreID,TerritoryID,AccountNumber,rowguid,ModifiedDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonID = new SelectList(db.People, "BusinessEntityID", "PersonType", customer.PersonID);
            ViewBag.TerritoryID = new SelectList(db.SalesTerritories, "TerritoryID", "Name", customer.TerritoryID);
            ViewBag.StoreID = new SelectList(db.Stores, "BusinessEntityID", "Name", customer.StoreID);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
