//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BeautyProds
{
    using System;
    using System.Collections.Generic;
    
    public partial class BottleRequest
    {
        public int ID { get; set; }
        public int VendorID { get; set; }
        public long ReqQuantity { get; set; }
        public System.DateTime DueDate { get; set; }
        public byte SendNotification { get; set; }
        public int BottleID { get; set; }
    
        public virtual Bottle Bottle { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
