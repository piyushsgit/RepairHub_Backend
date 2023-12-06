using Common.Helper;
using Model.UsersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.UsersModels.LoginModel;

namespace Services.User
{
    public interface IUserService
    {
        public Task<ApiResponse<LoginModelResponse>> Loginuser(LoginModel model);
    }
}
