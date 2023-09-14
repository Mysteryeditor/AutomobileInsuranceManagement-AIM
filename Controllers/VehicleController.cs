using AutomobileInsuranceManagement_AIM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomobileInsuranceManagement_AIM.Controllers
{
    [Authorize]
    public class VehicleController : Controller
    {
        AIMEntities dataconnection=new AIMEntities();
        // GET: Vehicle
   public ActionResult AddVehicle()
        {
            List<AutomobileType> vehicle_Type = new List<AutomobileType>();
            vehicle_Type=dataconnection.AutomobileTypes.ToList();
            ViewBag.vehicle_Type = new SelectList(vehicle_Type, "autoTypeId","autoType");
            return View();
        }
        [HttpPost]
        public ActionResult AddVehicle([Bind (Include = "model,registration_number,chassis_number,brand,weight,manufacture_year,vehicle_Type") ]Automobile newVehicle)
        {
            int ActiveUserID = 0;
            ActiveUserID = (int)Session["userId"];
            newVehicle.userId=ActiveUserID;
            newVehicle.isInsured=false;
            dataconnection.Automobiles.Add(newVehicle);
            dataconnection.SaveChanges();
            return RedirectToAction("DashBoard", "Home");
        }

    }
}