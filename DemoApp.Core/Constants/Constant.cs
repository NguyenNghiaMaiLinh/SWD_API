namespace DemoApp.Core.Constants
{
    public  class Constant

    {
        public const string USER_ANONYMOUS = "anonymous";
        public const string CLAIM_USERNAME = "username";
        public const int MAX_PAGE_SIZE = 50;
        public const int DEFAULT_PAGE_SIZE = 20;
        public const int DEFAULT_PAGE_INDEX = 1;

    }

    public static class Role
    {
        public const string Admin = "admin";
        public const string User = "user";
    }
    public class ErrMessageConstants
    {

        public const string INVALID_ACCOUNT = "Password incorrect";
        public const string NOTFOUND = "Data not found";
        public const string ACCOUNT_NOTFOUND = "Account not found";
        public const string ACCOUNT_EXISTED = "Account already exists";
        public const string INVALID_PERMISSION = "You don't have permission for this action";
    }
    public class MessageConstants
    {
        public const string SUCCESS = "Success";
        public const string NO_RECORD = "No_Record";
        public const string FAILURE = "Failure";
        public const string NOTFOUND = "Not_Found";
    }

}
