namespace ProductManagement.Models
{
    public class InvalidSubCategoryIdentifierException : Exception
    {
        public override string Message
        {
            get
            {
                return Constants.INVALIDSUBCATEGORYEXCEPTIONMESSAGE;
            }
        }
    }
}
