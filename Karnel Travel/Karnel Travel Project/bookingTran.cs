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
    
    public partial class bookingTran
    {
        public string btran_id { get; set; }
        public string btran_cust_id { get; set; }
        public string btran_name { get; set; }
        public System.DateTime btran_departure { get; set; }
        public System.DateTime btran_arrival { get; set; }
        public int btran_guests { get; set; }
        public int btran_seats { get; set; }
        public string btran_contactNo { get; set; }
    
        public virtual tranport tranport { get; set; }
        public virtual userDetail userDetail { get; set; }
    }
}
