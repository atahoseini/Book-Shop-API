namespace Common.Application
{
    public class ValodationMessage
    {
        public const string RecaptchError  = "Don't valid recaptcha";
        public const string Required = "This field is required";

        public static string required(string field)
        {
            return $"{field} {Required}";
        }

        public static string maxKenght(string filed, int maxLenght)
        {
            return $"{filed} field must be less than {maxLenght} characters";
        }

        public static string minKenght(string filed, int minLenght)
        {
            return $"{filed} field must be more than {minLenght} characters";
        }
    }

}
