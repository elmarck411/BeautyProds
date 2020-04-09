using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyProds.Models
{
    public class _Bottle
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int VendorID { get; set; }

        public virtual _Vendor Vendor { get; set; }
    }
}