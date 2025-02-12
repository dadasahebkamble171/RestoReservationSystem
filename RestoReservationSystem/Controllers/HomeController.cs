using RestoReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestoReservationSystem.Controllers
{
    public class HomeController : Controller
    {
        RestoResDbContext db = new RestoResDbContext();

        public ActionResult Index() 
        {
            if(Session["validUserId"] == null)
            {
                return View();
            }
            else
            {
                var uid = Convert.ToInt32(Session["validUserId"]);
                var username = db.Users.Where(u => u.UserId == uid).FirstOrDefault();
                string userfirstname = username.Name.ToString().Split(' ')[0];
                Session["Userfirstname"] = userfirstname;
                return View();
            }
            
        }

        public ActionResult Restaurants()
        {
            var restoInfo = db.Admins.ToList();
            ViewBag.restoInfo = restoInfo;
            return View(ViewBag.restoInfo);
        }


        public ActionResult restaurant_page(int id)
        {

            ViewBag.tablesInf = db.Tables.Where(m => m.RestoId == id).ToList();
            return View(ViewBag.tablesInf);
        }


        public ActionResult tbl_booking(int id)
        {
            chkavil_TblViewmodel obj = new chkavil_TblViewmodel()
            {
                chk = new CheckAvailable(),
                tablesInfo = db.Tables.Where(m => m.TableId == id).ToList()
            };
            return View(obj);
        }

        [HttpPost]
        public ActionResult tbl_booking(CheckAvailable chk, int id, Reservation res)
        {
            if (Session["validUserId"] != null)
            {
                chkavil_TblViewmodel obj = new chkavil_TblViewmodel()
                {
                    chk = new CheckAvailable(),
                    tablesInfo = db.Tables.Where(m => m.TableId == id).ToList()
                };

                try
                {
                    var tblInfo = db.Tables.Where(t => t.TableId == id).FirstOrDefault();
                    var restInfo = db.Admins.Where(a => a.AdminId == tblInfo.RestoId).FirstOrDefault();
                    var fetchedReservation = db.Reservations.Where(m => m.TableId == id && m.Catagory == tblInfo.Catagory && m.Capacity == tblInfo.Capacity && m.Time.ToString() == chk.time && m.Date.ToString() == chk.date).FirstOrDefault();
                    if (fetchedReservation != null)
                    {
                        TempData["bookingMsg"] = "Sorry for inconvinience, table was already booked on your selected time, please try another day, date or time";
                    }
                    else
                    {
                        int uid = Convert.ToInt16(Session["validUserId"]);
                        var userInfo = db.Users.Where(u => u.UserId == uid).FirstOrDefault();

                        res.RestoId = Convert.ToInt32(tblInfo.RestoId);
                        res.UserId = Convert.ToInt16(Session["validUserId"]);
                        res.TableId = Convert.ToInt16(tblInfo.TableId);
                        res.UserName = userInfo.Name;
                        res.UserEmail = userInfo.Email;
                        res.Contact = userInfo.Phone.ToString();
                        res.Capacity = tblInfo.Capacity;
                        res.Catagory = tblInfo.Catagory;
                        res.Day = chk.date.ToString().Split(' ')[0];
                        res.Date = chk.date.ToString();
                        res.Time = chk.time.ToString();
                        res.Status = "Reserved";
                        res.restoName = restInfo.RestaurantName;

                        db.Reservations.Add(res);
                        var rosAff = db.SaveChanges();
                        if (rosAff > 0)
                        {
                            TempData["bookingMsg"] = "Table reserved successfully !";
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["bookingMsg"] = "Error :" + ex.Message;
                }
                return View(obj);

            }
            else
            {
                return RedirectToAction("Login", "UserAccount");
            }
        }

        public ActionResult User_profile()
        {
            int uid = Convert.ToInt16(Session["validUserId"]);
            UserProfileViewModel profView = new UserProfileViewModel()
            {
                UserInfo = db.Users.Where(u => u.UserId == uid).ToList(),
                ResInfo = db.Reservations.Where(r => r.UserId == uid && r.Status == "Reserved").ToList(),
                HResInfo = db.Reservations.Where(r => r.UserId == uid && r.Status != "Reserved").ToList(),
            };

            return View(profView);
        }

        [HttpPost]
        public ActionResult CancelReserv(int id)
        {
                var reservation = db.Reservations.Find(id);
                if (reservation == null)
                {
                    return HttpNotFound();
                }

                reservation.Status = "Canceled"; // Update status column
                db.SaveChanges();

                return RedirectToAction("User_profile"); // Redirect to refresh the table
        }
    }
}