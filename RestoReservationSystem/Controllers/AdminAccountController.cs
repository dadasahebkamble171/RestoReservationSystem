using RestoReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestoReservationSystem.Controllers
{
    public class AdminAccountController : Controller
    {
        RestoResDbContext db = new RestoResDbContext();

        // GET: AdminAccount
        public ActionResult AdminRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminRegister(Admin ad)
        {
            try
            {
                ad.Email = Session["verifiedEmailAU"].ToString();
                var admindt = db.Admins.Where(chk => chk.RestaurantName == ad.RestaurantName).FirstOrDefault();
                var admineml = db.Admins.Where(chk => chk.Email == ad.Email).FirstOrDefault();

                if (admindt != null)
                {
                    TempData["insertMessage"] = "Restaurant name already exists!";
                }
                else if (admineml != null)
                {
                    TempData["insertMessage"] = "Email already exists!";
                }
                else if (ModelState.IsValid)
                {
                    db.Admins.Add(ad);
                    int a = db.SaveChanges();

                    if (a > 0)
                    {
                        ViewBag.insertMessage = "Registered successfully!";
                        ModelState.Clear();
                    }
                    else
                    {
                        TempData["insertMessage"] = "Registration faile!.";
                    }

                }
                else
                {
                    TempData["insertMessage"] = "Invalid data!";
                }
            }
            catch (Exception ex)
            {
                TempData["insertMessage"] = $"Error: {ex.Message}";
            }
            return View();
        }
    }
}