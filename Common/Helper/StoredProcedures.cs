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
        public const string GetShopDetailsById = "[Sp_GetShopDetailById]";
        public const string FilterShop = "GetTopShops";
        public const string ShopType = "SelectIdAndFieldName";
        public const string GetImages = "sp_selectImageById";
        public const string GetBrands = "SelectTopBrands";
        public const string GetSearchData = "SearchShops1";
        public const string GetShopAllRequest = "GetShopAllRequest";
        public const string GetCaseInfo = "GetCaseInfo";
        


        #endregion

    }
}