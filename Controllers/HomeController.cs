using AutomobileInsuranceManagement_AIM.Models;
using AutomobileInsuranceManagement_AIM.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomobileInsuranceManagement_AIM.Controllers
{
    public class HomeController : Controller
    {
        AIMEntities dataconnection = new AIMEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
     public ActionResult DashBoard()
        {
            int ActiveUserID=0;
            ActiveUserID = (int)Session["userId"];
            if (ActiveUserID != 0)
            {
                Automobile vehicle = dataconnection.Automobiles.FirstOrDefault(s=>s.userId==ActiveUserID);
                if (vehicle == null)
                {
                    TempData["vehicleAdded"] = "no";
                }
                else
                {

                    TempData["vehicleAdded"] = "yes";
                }
            }

            int isPolicyActive =0;
            isPolicyActive = Convert.ToInt32(Session["policyActive"]);
            if (isPolicyActive == 0)
            {
                TempData["policyApplied"] = "no";
                List<RETURNPOLICY_Result> eligiblePolicies= new List<RETURNPOLICY_Result>();
                eligiblePolicies= dataconnection.RETURNPOLICY(ActiveUserID).ToList();
                ViewBag.availablePolices = eligiblePolicies;
            }
            else
            {
                #region UserPolicies
                TempData["policyApplied"] = "Yes";
                List<ActivePolicy> activeUserPolicies= dataconnection.ActivePolicies.Where(s=>s.userId==ActiveUserID).OrderBy(s=>s.policyId).ToList() ;
                ViewBag.chosenPolicies = activeUserPolicies;//for the render partial
                List<Policy> activePolicyInfo=new List<Policy>();//for the info of the selected policy
                List<Automobile> automobileInfo=new List<Automobile>();//for the info of the selected automobile

                foreach (var policy in activeUserPolicies)
                {
                    activePolicyInfo.Add(dataconnection.Policies.FirstOrDefault(x => x.policyId == policy.policyId));
                    automobileInfo.Add(dataconnection.Automobiles.FirstOrDefault(x => x.automobile_Id == policy.automobileId));
                }
                ViewBag.chosenPolicyInfo=activePolicyInfo;
                ViewBag.automobileData=automobileInfo;
                #endregion
                #region UserAutoMobiles
                List<Automobile> userAutomobiles = dataconnection.Automobiles.Where(x => x.userId == ActiveUserID).ToList();
                ViewBag.activeUserVehicles=userAutomobiles;
                #endregion
            }
            return View();
        }
    }
}