using AutomobileInsuranceManagement_AIM.Models;
using AutomobileInsuranceManagement_AIM.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AutomobileInsuranceManagement_AIM.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        readonly AIMEntities dataconnection = new AIMEntities();
        public ActionResult Register()
        {
            //for admin
            List<Role> existing_Roles = dataconnection.Roles.ToList();
            ViewBag.roleId = new SelectList(existing_Roles, "roleId", "roleName");
            //---------
            return View();
        }

        [HttpPost]

        public ActionResult Register([Bind(Include = "userName,email,mobile,DOB")] user newUser,string password, HttpPostedFileBase profilePic)
        {

            bool uniqueEmail = true;
            List<string> existingemails = (from s in dataconnection.users select s.email).ToList();
            foreach (string email in existingemails)
            {
                if (newUser.email == email)
                {
                    uniqueEmail = false;
                    break;
                }
            }
            if (uniqueEmail)
            {
                if (ModelState.IsValid && profilePic != null)
                {
                    byte[] photo;
                    using (BinaryReader br = new BinaryReader(profilePic.InputStream))
                    {
                        photo = br.ReadBytes(profilePic.ContentLength);
                    }
                    //EncryptionandDecryption encryptPassword=new EncryptionandDecryption();
                    //var v=typeof(EncryptionandDecryption).GetMethod("Encrypt", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(new EncryptionandDecryption(), newUser);
                    //string encryptedPassword=encryptPassword.Encrypt(newUser.password, 0);
                    //newUser.password = encryptedPassword;
                    newUser.password =dataconnection.SP_PasswordEncrypt(password).First();
                    newUser.policyActive = 0;
                    newUser.isDeleted = false;
                    newUser.createdDate = DateTime.Now;
                    newUser.modifiedDate = DateTime.Now;
                    newUser.roleId = 2;
                    newUser.profilePic = photo;
                    dataconnection.users.Add(newUser);
                    dataconnection.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            else
            {
                ViewBag.ExistingEmail = "Email Already Exists";
            }
            
            return View();

        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(user loginCredentials,string decryptPassword)
        {
            RoleDecider_Result validUserCredents = dataconnection.RoleDecider(loginCredentials.email, decryptPassword).FirstOrDefault();
            switch (validUserCredents.userID.Value)
            {

                case -1:
                    break;
                default:
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, loginCredentials.email, DateTime.Now, DateTime.Now.AddMinutes(30), false, validUserCredents.Roles, FormsAuthentication.FormsCookiePath);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                    if (ticket.IsPersistent)
                    {
                        cookie.Expires = ticket.Expiration;
                    }
                    Response.Cookies.Add(cookie);
                    user liveUser = dataconnection.users.FirstOrDefault(x => x.email == loginCredentials.email);
                    Session["username"] = liveUser.userName;
                    Session["userId"] = liveUser.userId;
                    Session["policyActive"] = liveUser.policyActive;
                    Session["profilePic"] = liveUser.profilePic;
                    return RedirectToAction("DashBoard", "Home");
            }

            ViewBag.InvalidCredentials = "Invalid UserName or Password";
            return View();

        }

        public ActionResult CreateUser()
        {
            return View();
        }

        public ActionResult EditProfile()
        {
            int activeUserId = (int)Session["userId"];
            user EditUser = dataconnection.users.Find(activeUserId);
            return View(EditUser);
        }

        [HttpPost]
        public ActionResult EditProfile(user editedUser)
        {

            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}