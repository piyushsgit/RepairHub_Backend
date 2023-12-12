namespace Common.Helper
{
    public class StoreProcedures
    {
        #region Login
        public const string UserLogin = "[SP_Login]";
        public const string UserRegister = "sp_SMSUserRegister";
        public const string GenerateOtp = "[sp_GenerateOtp]";
        public const string GetUserDetailsByEmail = "sp_Get_User_Details_By_Email";
        
        #endregion

    }
}