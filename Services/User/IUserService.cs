using Common.Helper;
using Model.UsersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace Services.User
{
    public interface IUserService
    {
        public Task<ApiPostResponse<LoginModelResponse>> Loginuser(LoginWithContact model);
        public Task<ApiPostResponse<LoginModelResponse>> AdminLogin(LoginWithEmail model);
        public Task<OtpVerificationResponse> Generateopt(string ContactNo);
    }
}
