using AutomobileInsuranceManagement_AIM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomobileInsuranceManagement_AIM.ViewModels
{
    public class WholePolicyData
    {
        public Policy AllPolicies{ get; set; }
        public ActivePolicy ActivePolicies { get; set; }
        public Automobile AutomobileData { get; set; }


    }
}