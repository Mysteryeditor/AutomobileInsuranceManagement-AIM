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
    
    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            this.ActivePolicies = new HashSet<ActivePolicy>();
            this.Automobiles = new HashSet<Automobile>();
        }
    
        public int userId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<System.DateTime> modifiedDate { get; set; }
        public long mobile { get; set; }
        public byte[] profilePic { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        public Nullable<int> roleId { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string password { get; set; }
        public Nullable<int> policyActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActivePolicy> ActivePolicies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Automobile> Automobiles { get; set; }
        public virtual Role Role { get; set; }
        public virtual user users1 { get; set; }
        public virtual user user1 { get; set; }
    }
}
