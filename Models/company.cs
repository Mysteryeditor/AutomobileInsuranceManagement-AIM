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
    
    public partial class company
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public company()
        {
            this.Automobiles = new HashSet<Automobile>();
            this.carmodels = new HashSet<carmodel>();
        }
    
        public int companyId { get; set; }
        public string companyName { get; set; }
        public Nullable<int> automobileType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Automobile> Automobiles { get; set; }
        public virtual AutomobileType AutomobileType1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<carmodel> carmodels { get; set; }
    }
}
