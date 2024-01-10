namespace ProductManagement.Models
{
    public class InvalidCategoryIdentifierException : Exception
    {
        public override string Message
        {
            get
            {
                return Constants.INVALIDCATEGORYEXCEPTIONMESSAGE;
            }
        }
    }
}
