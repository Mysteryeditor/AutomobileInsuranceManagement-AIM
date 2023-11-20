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
    using System.Collections.Generic;
    
    public partial class transaction
    {
        public int Id { get; set; }
        public Nullable<int> policyId { get; set; }
        public string razorKey { get; set; }
        public Nullable<long> mobileNumber { get; set; }
        public Nullable<int> userId { get; set; }
        public string name { get; set; }
        public Nullable<int> automobileId { get; set; }
        public Nullable<double> premium { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<System.DateTime> renewalDate { get; set; }
        public string currency { get; set; }
        public string generatedID { get; set; }
        public Nullable<bool> IsSuccessful { get; set; }
    
        public virtual Automobile Automobile { get; set; }
        public virtual Policy Policy { get; set; }
        public virtual user user { get; set; }
    }
}