using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using RestoReservationSystem.Models;
using System.Data.Entity.Infrastructure;
using RestoReservationSystem.Migrations;

namespace RestoReservationSystem.Controllers
{
    public class AdminController : Controller
    {
        RestoResDbContext db = new RestoResDbContext();
        // GET: Admin
        public ActionResult AdminIndex()
        {
            int restoid = Convert.ToInt32(Session["validAdminId"]);
            TempData["reservList"] = db.Reservations.Where(r => r.RestoId == restoid).ToList();
            return View(TempData["reservList"]);
        }

        public ActionResult ManageTables()
        { 
            var logedinAdmin = Convert.ToInt32(Session["validAdminId"]);
            ViewBag.tablesInfo = db.Tables.Where(ri => (ri.RestoId == logedinAdmin)).ToList();
            return View(ViewBag.tablesInfo);
        }

        public ActionResult AddTable()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult AddTable(Table t)
        {
            try
            {
                t.Capacity = Convert.ToInt32(t.Capacity);
                t.RestoId = Convert.ToInt32(Session["validAdminId"]);
                //var daystring = t.Date.ToString();
                //t.Day = daystring.Split(' ')[0];
                //t.Status = "Available";
                if (t != null) 
                {
                    
                    db.Tables.Add(t);
                    var rowAff = db.SaveChanges();
                    if(rowAff > 0)
                    {
                        ViewBag.successmsg = "Table added successfully !";
                    }
                    else
                    {
                        ViewBag.successmsg = "Failed to add table !";
                    }
                }
                else
                {
                    ViewBag.successmsg = "somthing went wrong !";
                }
            }
            catch (Exception ex) 
            {
                ViewBag.Exception = ex;
            }
            return View();
        }

        public ActionResult updateTblInfo(int id)
        {
            var row = db.Tables.Where(model => model.TableId == id).FirstOrDefault();
            return View(row);
        }



        [HttpPost]
        public ActionResult updateTblInfo(Table tbl, int id)
        {
            if (tbl == null)
            {
                ViewBag.successmsg = "Failed to update!";
                return View(tbl);
            }

            var existingEntity = db.Tables.Find(tbl.TableId); // Ensure record exists

            if (existingEntity == null)
            {
                ViewBag.successmsg = "Record not found or already deleted!";
                return View(tbl);
            }

            try
            {
                db.Entry(existingEntity).CurrentValues.SetValues(tbl); // Update only modified fields
                var m = db.SaveChanges();

                if (m > 0)
                {
                    ViewBag.successmsg = "Data updated successfully!";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.successmsg = "No changes detected!";
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                ViewBag.successmsg = "Concurrency conflict! The record has been modified by another user.";
            }

            return View(tbl);
        }

        public ActionResult deleteTbl(int id)
        {
            var tbl = db.Tables.Where(model => model.TableId ==id).FirstOrDefault();
                db.Entry(tbl).State = EntityState.Deleted;
                var m = db.SaveChanges();
                if (m > 0)
                {
                    ViewBag.successmsg = "Data deleted successfully !";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.successmsg = "Failed to delete !";
                }
            
            return View();
        }

        public ActionResult Manage_Reservations() 
        {
            int restoid = Convert.ToInt32(Session["validAdminId"]);
            ManageResViewModel obj = new ManageResViewModel()
            {
                crs = new ChangeReservationStatus(),
                reserv = db.Reservations.Where(m => m.RestoId == restoid).ToList(),
            };

            return View(obj);
        }

        [HttpPost]
        public ActionResult Manage_Reservations(int id)
        {
            try
            {
                int restoid = Convert.ToInt32(Session["validAdminId"]);
                ManageResViewModel obj = new ManageResViewModel()
                {
                    reserv = db.Reservations.Where(m => m.RestoId == restoid).ToList(),
                };
                var reservation = db.Reservations.Find(id);
                if (reservation == null)
                {
                    return HttpNotFound();
                }

                reservation.Status = "Completed"; // Update status column
                var ra = db.SaveChanges();
                if (ra > 0)
                {
                    ViewBag.stmsg = "Reservation status changed successfully !";
                    return View(obj);
                }
                else
                {
                    ViewBag.stmsg = "Failed to change reservation status !";
                    return View(obj);
                }
            }
            catch (Exception ex) 
            {
                int restoid = Convert.ToInt32(Session["validAdminId"]);
                ManageResViewModel obj = new ManageResViewModel()
                {
                    reserv = db.Reservations.Where(m => m.RestoId == restoid).ToList(),
                };
                ViewBag.stmsg = ex.Message;
                return View(obj);
            }
        }
    }
}