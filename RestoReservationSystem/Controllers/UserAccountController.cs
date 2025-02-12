using RestoReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace RestoReservationSystem.Controllers
{
    public class UserAccountController : Controller
    {
        RestoResDbContext db = new RestoResDbContext();
        // GET: UserAccount
        public ActionResult UserRegister()
        {
            return View();
        }


        public string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(1000, 10000).ToString(); // Generates a 4-digit OTP
        }

        // Function to send a verification OTP email
        public void SendVerificationEmail(string Uemail)
        {
            // SMTP configuration
            string smtpHost = "smtp.gmail.com";
            int smtpPort = 587;
            string senderEmail = "emartshopforyou@gmail.com"; // Your email
            string senderPassword = "tuqgriwxqekhkywv"; // Your email password or app-specific password

            try
            {
                // Generate OTP
                string rotp = GenerateOTP();
                Session["recivedOTP"] = rotp;

                // Create email content
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = "Your Email Verification OTP",
                    Body = $"Your OTP for email verification is: {rotp}\n\nPlease use this code to complete your registration.",
                    IsBodyHtml = false // Set to true if you want to use HTML
                };
                var userEmail = Uemail;
                mailMessage.To.Add(userEmail);

                // Configure SMTP client
                SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort)
                {
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true // Ensure secure connection
                };


                // Send the email
                smtpClient.Send(mailMessage);
                Session["emlmsg"] = "Verification email sent successfully on " + Uemail;
            }
            catch (Exception ex)
            {
                Session["emlmsg"] = "Error sending email: " + ex.Message;
            }
        }

        public ActionResult emailVerification()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendOTP(User eml, Admin adml)
        {
            var chkemlAdm = db.Admins.Where(ae => ae.Email == eml.Email).FirstOrDefault();
            var checkEml = db.Users.Where(e => e.Email == eml.Email).FirstOrDefault();
            if (checkEml != null || chkemlAdm != null)
            {
                Session["emlmsg"] = "Email already exists";
            }
            else
            {
                var Usereml = eml.Email;
                SendVerificationEmail(Usereml);
                TempData["verifiedEmailAU"] = eml.Email;
                return View("emailVerification");
            }
            return View("emailVerification");
        }

        [HttpPost]
        public ActionResult VerifyOTP(string otpcode, User eml)
        {
            var generatedOTP = Session["recivedOTP"].ToString();
            if(generatedOTP == otpcode)
            {
                TempData["otpmsg"] = "Verified Successfully";
                Session["verifiedEmailAU"] = TempData["verifiedEmailAU"];
                return View("emailVerification");
            }
            else
            {
                TempData["otpmsg"] = "Wrong OTP, provide valid OTP";
            }
            return View("emailVerification");
        }



        [HttpPost]
        public ActionResult UserRegister(User u)
        {

                //var generatedOTP = Convert.ToInt32(Session["recivedOTP"]);
                try
                {
                u.Email = Session["verifiedEmailAU"].ToString();
                    var userdt = db.Users.Where(chk => chk.Username == u.Username).FirstOrDefault();
                    var usreml = db.Users.Where(chk => chk.Email == u.Email).FirstOrDefault();

                    if (userdt != null)
                    {
                        TempData["insertMessage"] = "Username already exists!";
                    }
                    else if (usreml != null)
                    {
                        TempData["insertMessage"] = "Email already exists!";
                    }
                    else if (ModelState.IsValid)
                    {
                        db.Users.Add(u);
                        int a = db.SaveChanges();

                        if (a > 0)
                        {
                            ViewBag.insertMessage = "Registered successfully!";
                            ModelState.Clear();
                        }
                        else
                        {
                            TempData["insertMessage"] = "Data not inserted.";
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

        public ActionResult Login()
        {
            return View();
        }
         
        [HttpPost]
        public ActionResult Login(login log, User u, Admin ad)
        {
            try
            {
                var role = log.Role;
                
                    if (role == "User")
                    {
                        var validu = db.Users.Where(a => (a.Email == log.EmailOrUsername || a.Username == log.EmailOrUsername) && (a.Password == log.Password)).FirstOrDefault();
                        if (validu != null)
                        {
                            Session["validUserId"] = validu.UserId;
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["insertMessage"] = "Invalid credentials!";
                        }
                    }
                    else if (role == "Admin")
                    {
                        var valiAdm = db.Admins.Where(b => b.Email == log.EmailOrUsername && b.Password == log.Password).FirstOrDefault();
                        if (valiAdm != null)
                        {
                        Session["validAdminName"] = valiAdm.RestaurantName;
                        Session["validAdminId"] = valiAdm.AdminId;
                            return RedirectToAction("AdminIndex", "Admin");
                        }
                        else
                        {
                            TempData["insertMessage"] = "Invalid credentials!";
                        }
                    }

            }
            catch (Exception ex)
            {
                TempData["insertMessage"] = $"Error: {ex.Message}";
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("validUserId");
            return RedirectToAction("Index", "Home");
        }
    }
}