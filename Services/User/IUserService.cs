using Common.Helper; 
using Model.UsersModels; 
namespace Services.User
{
    public interface IUserService
    {
        public Task<ApiPostResponse<LoginModelResponse>> Loginuser(LoginWithContact model);
        public Task<ApiPostResponse<LoginModelResponse>> AdminLogin(LoginWithEmail model);
        public Task<OtpVerificationResponse> Generateopt(string? ContactNo, Email? req);
        public Task<Message> ForgotPassword(ForgotPassword forgot);
        public TokenModel GetUserTokenData(string jwtToken = null);

    }
}
