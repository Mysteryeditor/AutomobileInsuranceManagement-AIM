using AutomobileInsuranceManagement_AIM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomobileInsuranceManagement_AIM.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        AIMEntities dataconnection = new AIMEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        //[Authorize]
        public ActionResult UserList()
        {
            List<user> allUsers = dataconnection.users.ToList();
            List<Role> existing_Roles = dataconnection.Roles.ToList();
            ViewBag.roleId = new SelectList(existing_Roles, "roleId", "roleName");
            return View(allUsers);
        }

        public ActionResult AddUser()
        {
            return RedirectToAction("Register", "User");
        }

        [HttpPost]
        public ActionResult AddUser([Bind(Include = "userName,password,email,mobile,DOB,roleId")] user newUser, HttpPostedFileBase profilePic)
        {
            List<Role> existing_Roles = dataconnection.Roles.ToList();
            ViewBag.roleId = new SelectList(existing_Roles, "roleId", "roleName");
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
                    newUser.policyActive = 0;
                    newUser.isDeleted = false;
                    newUser.createdDate = DateTime.Now;
                    newUser.modifiedDate = DateTime.Now;
                    newUser.profilePic = photo;
                    dataconnection.users.Add(newUser);

                    dataconnection.SaveChanges();
                    return RedirectToAction("UserList");

                }
            }
            else
            {
                Response.Write("Email Already Exists");
            }

            return View();
        }

        public ActionResult EditUser(int? editId)
        {
            List<Role> existing_Roles = dataconnection.Roles.ToList();
            ViewBag.roles = new SelectList(existing_Roles, "roleId", "roleName");
            List<user> allUsers = dataconnection.users.ToList();
            user modifyUser = dataconnection.users.FirstOrDefault(s => s.userId == editId);
            List<user> modifiableUser = (from users in dataconnection.users where users.userId == editId select users).ToList();
            return View(modifiableUser.First());
        }
        [HttpPost]
        public ActionResult EditUser([Bind(Include = "userId,userName,roleId,email,password,DOB,policyActive,mobile")] user t, HttpPostedFileBase ProfilePic)//add id in action and model
        {
            if (ModelState.IsValid)
            {

                user freshData = dataconnection.users.Find(t.userId);
                if (freshData != null)
                {
                    freshData.userName = t.userName;
                    if (User.IsInRole("Admin"))
                    {
                        freshData.roleId = t.roleId;
                    }

                    else
                    {
                        freshData.roleId = 2;
                    }

                    freshData.modifiedDate = DateTime.Now;
                    freshData.email = t.email;
                    freshData.password = t.password;
                    freshData.DOB = t.DOB;
                    freshData.policyActive = t.policyActive;
                    if (ProfilePic != null)
                    {
                        byte[] newPic;
                        using (BinaryReader br = new BinaryReader(ProfilePic.InputStream))
                        {
                            newPic = br.ReadBytes(ProfilePic.ContentLength);
                        }
                        freshData.profilePic = newPic;
                    }

                    dataconnection.Entry(freshData).State = EntityState.Modified;
                    dataconnection.SaveChanges();
                    user liveUser = dataconnection.users.Find(t.userId);
                    Session["username"] = liveUser.userName;
                    Session["profilePic"] = liveUser.profilePic;

                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction("UserList", "Admin");
                    }

                    else
                    {
                        return RedirectToAction("DashBoard", "Home");
                    }


                }


            }
            return View();
        }

        public ActionResult Details(int userId)
        {
            user selectedUser = dataconnection.users.Find(userId);
            if (selectedUser != null)
            {
                return View(selectedUser);
            }
            return View();
        }

        [HttpGet]
        public ActionResult DeleteUser(int userId)
        {
            user deletedUser = dataconnection.users.Find(userId);
            if (deletedUser != null)
            {
                return View(deletedUser);
            }
            return View();
        }

        [HttpPost]
        [ActionName("DeleteUser")]
        public ActionResult ConfirmDeleteUser(int userId)
        {

            user deletedUser = dataconnection.users.Find(userId);
            if (deletedUser != null)
            {
                dataconnection.users.Remove(deletedUser);
                dataconnection.SaveChanges();
                return RedirectToAction("UserList");
            }
            return View();
        }


    }
}