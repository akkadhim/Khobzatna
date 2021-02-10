using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Khobzatna.Models
{
    public class RequestType
    {
        public RequestType()
        {
            Requests = new HashSet<Request>();
        }
        // Request is anything could be requested from other like food 
        public int RequestTypeId { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}