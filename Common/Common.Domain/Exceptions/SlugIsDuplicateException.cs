namespace Common.Domain.Exceptions
{
    public class SlugIsDuplicateException : BaseDomainException
    {
        public SlugIsDuplicateException() : base("Slug is duplicate")
        {
        }

        public SlugIsDuplicateException(string message) : base(message)
        {
        }
    }
}
