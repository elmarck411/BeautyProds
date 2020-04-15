using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyProds.Models
{
    public class _BottleRequest
    {
        public int ID { get; set; }
        public int VendorID { get; set; }
        public long ReqQuantity { get; set; }
        public System.DateTime DueDate { get; set; }
        public byte SendNotification { get; set; }
        public int BottleID { get; set; }
        public virtual _Bottle Bottle { get; set; }
        public virtual _Vendor Vendor { get; set; }
    }
}