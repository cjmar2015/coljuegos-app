namespace APP.Helpers
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;

    public static class Settings
    {
        static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }


        const string urlAPI = "http://104.209.159.158:8080/";

        const string email = "Email";
        const string password = "Password";
        const string userID = "userID";
        private const string _isLogin = "isLogin";


        private static readonly bool _boolDefault = false;
        const string token = "Token";
        const string tokenType = "TokenType";

        static readonly string stringDefault = string.Empty;
        static readonly int intDefault = 0;

        public static string UrlAPI
        {
            get
            {
                return AppSettings.GetValueOrDefault(urlAPI, "http://104.209.159.158:8080/");
            }
            set
            {
                AppSettings.AddOrUpdateValue(urlAPI, value);
            }
        }

        public static string Email
        {
            get
            {
                return AppSettings.GetValueOrDefault(email, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(email, value);
            }
        }

        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault(password, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(password, value);
            }
        }

        public static string UserID
        {
            get
            {
                return AppSettings.GetValueOrDefault(userID, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(userID, value);
            }
        }

        public static bool IsLogin
        {
            get => AppSettings.GetValueOrDefault(_isLogin, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isLogin, value);
        }

        public static string Token
        {
            get
            {
                return AppSettings.GetValueOrDefault(token, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(token, value);
            }
        }

        public static string TokenType
        {
            get
            {
                return AppSettings.GetValueOrDefault(tokenType, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(tokenType, value);
            }
        }
    }
}
