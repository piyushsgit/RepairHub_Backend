using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ConnectionInfo
{
    public  class ConnectionModel
    {
        public long UserId { get; set; }
        public long? ShopId { get; set; }
        public string? LoginConnectionId { get; set; }
        public string? ChatConnectionId { get; set; }
        public string? RequestConnectionId { get; set; }
        public bool? IsActive { get; set; }
    }
}
