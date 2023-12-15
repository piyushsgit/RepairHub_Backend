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
        public class EmailSettings
        {
            public const string SmtpServer = "EmailSettings:SmtpServer";
            public const string Port = "EmailSettings:Port";
            public const string UseSsl = "EmailSettings:UseSsl";
            public const string UserName = "EmailSettings:UserName";
            public const string Password = "EmailSettings:Password";

        }
    }
}