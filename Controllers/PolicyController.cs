using AutomobileInsuranceManagement_AIM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomobileInsuranceManagement_AIM.Controllers
{
    public class PolicyController : Controller
    {
        AIMEntities dataconnection = new AIMEntities();
        // GET: Policy
        public ActionResult PolicyList()
        {
            List<Policy> policies = new List<Policy>();
            policies = dataconnection.Policies.ToList();
            return View(policies);
        }
        public ActionResult CreateNewPolicy()
        {
            List<AutomobileType> automobileTypes = new List<AutomobileType>();
            automobileTypes = dataconnection.AutomobileTypes.ToList();
            //ViewBag.roleId = new SelectList(existing_Roles, "roleId", "roleName");
            ViewBag.automobileTypeId = new SelectList(automobileTypes, "autoTypeId", "autoType");
            List<InsuranceType> insuranceTypes = new List<InsuranceType>();
            insuranceTypes = dataconnection.InsuranceTypes.ToList();
            ViewBag.insuranceTypeId = new SelectList(insuranceTypes, "TypeId", "TypeName");

            return View();
        }

        [HttpPost]
        public ActionResult CreateNewPolicy([Bind(Include = "policyName,claimAmount,premium,years,description,insuranceTypeId,automobileTypeId")] Policy newpolicy)
        {

            if (ModelState.IsValid)
            {
                newpolicy.isDeleted = false;
                dataconnection.Policies.Add(newpolicy);
                dataconnection.SaveChanges();
                return RedirectToAction("PolicyList");
            }
            return View();

        }

        public ActionResult EditPolicy(int policyId)
        {
            List<AutomobileType> automobileTypes = new List<AutomobileType>();
            automobileTypes = dataconnection.AutomobileTypes.ToList();
            ViewBag.automobileTypeId = new SelectList(automobileTypes, "autoTypeId", "autoType");
            List<InsuranceType> insuranceTypes = new List<InsuranceType>();
            insuranceTypes = dataconnection.InsuranceTypes.ToList();
            ViewBag.insuranceTypeId = new SelectList(insuranceTypes, "TypeId", "TypeName");
            Policy editPolicy = dataconnection.Policies.Find(policyId);
            if (editPolicy != null)
            {
                return View(editPolicy);
            }
            return View();

        }
        [HttpPost]
        public ActionResult EditPolicy(Policy policy)
        {
            if (ModelState.IsValid)
            {
                dataconnection.Entry(policy).State = EntityState.Modified;
                dataconnection.SaveChanges();
                return RedirectToAction("PolicyList");
            }
            return View();

        }

        public ActionResult ViewPolicy(int policyId)
        {
            Policy viewPolicy = dataconnection.Policies.Find(policyId);
            if (viewPolicy != null)
            {
                return View(viewPolicy);
            }
            return View();
        }

        public ActionResult DeleteResult(int policyId)
        {
            Policy deletePolicy = dataconnection.Policies.Find(policyId);
            if (deletePolicy != null)
            {
                return View(deletePolicy);
            }

            return View();
        }
        [HttpPost]
        [ActionName("DeleteResult")]
        public ActionResult ConfirmDelete(int policyId)
        {
            dataconnection.Policies.Remove(dataconnection.Policies.Find(policyId)); 
            dataconnection.SaveChanges();
            return RedirectToAction("PolicyList");
        }

        //------------------UserPolicies------------
        public ActionResult ViewUserPolicy()
        {
            return View(dataconnection.ActivePolicies.ToList());
        }
        public ActionResult AddUserPolicy(int policyId)
        {
            Policy chosenPolicy=dataconnection.Policies.Find(policyId);
            if (chosenPolicy != null)
            {
                int activeUserID = (int)Session["userId"];
                Automobile activeUserAuto=dataconnection.Automobiles.FirstOrDefault(s=>s.userId == activeUserID);
                ActivePolicy newActivePolicy = new ActivePolicy();
                newActivePolicy.userId = activeUserID;
                newActivePolicy.automobileId = activeUserAuto.automobile_Id;
                newActivePolicy.policyId = policyId;
                newActivePolicy.createdDate = DateTime.Now;
                newActivePolicy.renewalDate = DateTime.Now.AddYears(Convert.ToInt32(chosenPolicy.years));
                newActivePolicy.IsActive = true;
                dataconnection.ActivePolicies.Add(newActivePolicy);
                dataconnection.SaveChanges();
                user ActiveUser = dataconnection.users.Find(activeUserID);
                if (ActiveUser != null)
                {
                    Session["policyActive"] = ActiveUser.policyActive;

                }
                return RedirectToAction("DashBoard", "Home");
            }
        
            return View();
        }

        public ActionResult ExpandUserPolicy()
        {
            return View();
        }


    }
}