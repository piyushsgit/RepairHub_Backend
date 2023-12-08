using Data;
using Model.UsersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Repository.User
{
    public interface IUserRepository 
    {
        public Task<LoginModelResponse> UserLogin(LoginWithContact userLogin);
        public Task<LoginModelResponse> AdminLogin(LoginWithEmail userLogin);
        public Task<OtpVerificationResponse> Generateopt(string ContactNo);
    }
}   
