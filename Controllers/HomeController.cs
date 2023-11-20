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
            int ActiveUserID = 0;
            ActiveUserID = (int)Session["userId"];
            if (ActiveUserID != 0)
            {
                Automobile vehicle = dataconnection.Automobiles.FirstOrDefault(s => s.userId == ActiveUserID);
                if (vehicle == null)
                {
                    TempData["vehicleAdded"] = "no";
                }
                else
                {

                    TempData["vehicleAdded"] = "yes";
                }
            }

            int isPolicyActive = 0;
            isPolicyActive = Convert.ToInt32(Session["policyActive"]);
            if (isPolicyActive == 0)
            {
                TempData["policyApplied"] = "no";
                List<RETURNPOLICY_Result> eligiblePolicies = new List<RETURNPOLICY_Result>();
                eligiblePolicies = dataconnection.RETURNPOLICY(ActiveUserID).ToList();
                ViewBag.availablePolices = eligiblePolicies;
            }
            else
            {
                #region UserPolicies
                TempData["policyApplied"] = "Yes";
                List<SP_Return_ActivePolicies_Result> activepolicies = dataconnection.SP_Return_ActivePolicies(ActiveUserID).ToList();
                ViewBag.userPolicies = activepolicies;
                #endregion

                #region UserAutoMobiles
                List<Automobile> userAutomobiles = dataconnection.Automobiles.Where(x => x.userId == ActiveUserID).ToList();
                ViewBag.activeUserVehicles = userAutomobiles;
                #endregion
            }
            return View();
        }
    }
}