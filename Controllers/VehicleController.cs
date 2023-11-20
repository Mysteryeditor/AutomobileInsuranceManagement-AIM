using AutomobileInsuranceManagement_AIM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomobileInsuranceManagement_AIM.Controllers
{
    //[Authorize]
    public class VehicleController : Controller
    {
        AIMEntities dataconnection=new AIMEntities();
        // GET: Vehicle
   public ActionResult AddVehicle()
        {
            List<AutomobileType> vehicle_Type = new List<AutomobileType>();
            vehicle_Type = dataconnection.AutomobileTypes.ToList();

            ViewBag.vehicle_Type = new SelectList(vehicle_Type, "autoTypeId","autoType");

            List<company> companyList = new List<company>();
            //companyList = dataconnection.companies.ToList();
            ViewBag.companyList = new SelectList(companyList, "companyId", "companyName");

            List<carmodel> modelList = new List<carmodel>();
            //modelList = dataconnection.carmodels.ToList();
            ViewBag.modelList = new SelectList(modelList, "modelId", "modelName");



            return View();
        }
        [HttpPost]
        public ActionResult AddVehicle([Bind(Include = "modelId,companyId,registrationNumber,chassisNumber,brandId,weight,manufactureYear,vehicleTypeId")] Automobile newVehicle)
        {
            if (newVehicle.companyId == null)
            {
                List<AutomobileType> vehicle_Type = new List<AutomobileType>();
                vehicle_Type = dataconnection.AutomobileTypes.ToList();
                ViewBag.vehicle_Type = new SelectList(vehicle_Type, "autoTypeId", "autoType");

                List<company> companyList = dataconnection.companies.Where(x => x.automobileType == newVehicle.vehicleTypeId).ToList();
                ViewBag.companyList = new SelectList(companyList, "companyId", "companyName"); ;


                List<carmodel> modelList = new List<carmodel>();
                modelList = dataconnection.carmodels.ToList();
                ViewBag.modelList = new SelectList(modelList, "modelId", "modelName");
                return View();
            }

            else if (newVehicle.modelId == null)
            {

                List<AutomobileType> vehicle_Type = new List<AutomobileType>();
                vehicle_Type = dataconnection.AutomobileTypes.ToList();
                ViewBag.vehicle_Type = new SelectList(vehicle_Type, "autoTypeId", "autoType");

                List<company> companyList = dataconnection.companies.Where(x => x.automobileType == newVehicle.vehicleTypeId).ToList();
                ViewBag.companyList = new SelectList(companyList, "companyId", "companyName"); ;




                List<carmodel> modelList = dataconnection.carmodels.Where(x => x.companyId == newVehicle.companyId).ToList();
                ViewBag.modelList = new SelectList(modelList, "modelId", "modelName"); ;
                return View();
            }

            else if (ModelState.IsValid)
            {
                int ActiveUserID = 0;
                ActiveUserID = (int)Session["userId"];
                newVehicle.userId = ActiveUserID;
                newVehicle.isInsured = false;
                dataconnection.Automobiles.Add(newVehicle);
                dataconnection.SaveChanges();
                return RedirectToAction("DashBoard", "Home");
            }

            else
            {
                return RedirectToAction("NotFound","Error");
            }

           
        }
    
        public ActionResult AutomobileDetails(int automobileId)
        {
            Automobile chosenauto = dataconnection.Automobiles.Find(automobileId);
            return View(chosenauto);
        }
    }
}