using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyProds.Models
{
    public class _Vendor
    {

        public int ID { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string ContactEmail { get; set; }
        [JsonIgnore]
        public virtual ICollection<_BottleRequest> BottleRequests { get; set; }
    }
}