using Data;
using Model.UsersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.UsersModels.LoginModel;

namespace Repository.User
{
    public interface IUserRepository 
    {
        public Task<LoginModelResponse> UserLogin(LoginModel userLogin);
    }
}
