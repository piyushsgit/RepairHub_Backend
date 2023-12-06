using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UsersModels
{
    public class LoginModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ContactNo { get; set; }
        public string? Otp { get; set; }

    }
    public class LoginModelResponse
    {
        public int UserID { get; set; }
        public string EmailId { get; set; }
        public string JwdToken { get; set; }
        public string IsVarified { get; set; }
        public string message { get; set; }
        
    }
}
