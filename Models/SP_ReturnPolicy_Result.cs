//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutomobileInsuranceManagement_AIM.Models
{
    using System;
    
    public partial class SP_ReturnPolicy_Result
    {
        public int policyId { get; set; }
        public string PolicyName { get; set; }
        public string description { get; set; }
        public Nullable<decimal> claimAmount { get; set; }
        public Nullable<decimal> Premium { get; set; }
        public Nullable<int> years { get; set; }
        public Nullable<int> automobileTypeId { get; set; }
        public Nullable<int> insuranceTypeId { get; set; }
        public int automobileId { get; set; }
        public Nullable<int> userId { get; set; }
        public Nullable<bool> isInsured { get; set; }
    }
}
