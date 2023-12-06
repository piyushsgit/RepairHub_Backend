namespace Common.Helper
{
    public class StoreProcedures
    {
        #region Login
        public const string UserLogin = "[SP_Login]";
        public const string UserRegister = "sp_SMSUserRegister";
        public const string CheckUser = "sp_CheckUser_SMSUser";
        public const string GetUserDetailsByEmail = "sp_Get_User_Details_By_Email";
        #endregion

    }
}