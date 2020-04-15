using Newtonsoft.Json;
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
        [JsonIgnore]
        public virtual ICollection<_BottleRequest> BottleRequests { get; set; }
    }
}