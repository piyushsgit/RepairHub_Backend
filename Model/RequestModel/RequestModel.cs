using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.RequestModel
{
    public class statusModel
    {
        public DateTime CreatedOn { get; set; }
        public DateTime RequestDate { get; set; }
        public string? Description { get; set; } 
        public string? StautsDescrption { get; set; }
        public string? DisplayName { get; set; }
        public string StatusName { get; set; }
        public string First_Name { get; set; }
        public string EmailId { get; set; }
        public string FullAddress { get; set; }
        public string? Images { get; set; }
        public long? Id { get; set; }
    }



}
