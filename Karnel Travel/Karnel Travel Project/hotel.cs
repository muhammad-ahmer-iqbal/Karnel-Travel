//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Karnel_Travel_Project
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    
    public partial class hotel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hotel()
        {
            this.bookingHot = new HashSet<bookingHot>();
        }
    
        public string hot_id { get; set; }
        public string hot_name { get; set; }
        public string hot_country { get; set; }
        public decimal hot_charges { get; set; }
        public int hot_roomAvailable { get; set; }
        public Nullable<int> hot_rating { get; set; }
        public string hot_description { get; set; }
        public string hot_img { get; set; }
        public HttpPostedFileBase imageFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bookingHot> bookingHot { get; set; }
    }
}