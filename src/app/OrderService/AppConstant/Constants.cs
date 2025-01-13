namespace OrderService.AppConstant
{
    public class Constants
    {
        public static string ANDROID_PUSHNOTIFICATIONTOKEN_RESET_VERSION => "ANDROID_PUSHNOTIFICATIONTOKEN_RESET_VERSION";

        public static string APP_NAME = "OrderServiceApp";
        public static string KEY_STORAGE_USER_PICTURE = $"{APP_NAME}_User_picture";
        public static string KEY_STORAGE_USER_ADD_PICTURE_ORDER = $"{APP_NAME}_User_add_picture_order";
        public static string KEY_STORAGE_USER_EMAIL = $"{APP_NAME}_User_email";
        public static string KEY_STORAGE_USER_TOKEN = $"{APP_NAME}_Token";
        public static string KEY_STORAGE_PRIVACY_POLICY_ACCEPTED = $"{APP_NAME}_Privacy_policy_accepted";

        public static string FIREBASE_KEY_URLS = "Urls";
        public static string FIREBASE_KEY_OPENId = "OpenId";
        
        public static bool DEBUG_API_IN_PRODUCTION => false;
        public static string DEBUG_API_URL => "http://192.168.0.101:5000";

        public static int SECONDS_CACHE_FIREBASE_REMOTE_CONFIG => 600;
    }
}