namespace Common.Helper
{
    public class StoreProcedures
    {
        #region Login
        public const string UserLogin = "[SP_Login]";
        public const string UserRegister = "sp_SMSUserRegister";
        public const string GenerateOtp = "[sp_GenerateOtp]";
        public const string ForgotPassword = "[Sp_ForgotPassword]"; 
        #endregion
        #region Shop
        public const string GetShopDetails = "[SP_ShopList]"; 
        #endregion
    }
}