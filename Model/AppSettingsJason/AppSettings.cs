namespace Model.AppSettingsJason
{
    public static class AppSettingsJason
    { 
        public class AppSettings
        {
            public const string ConnectionString = "AppSettings:ConnectionString";

        }
        public class JwtToken
        {
            public const string SecreatKey = "JwtToken:SecreatKey";
            public const string TimeOutMin = "JwtToken:TimeOutMin";
        }
         
    }
}