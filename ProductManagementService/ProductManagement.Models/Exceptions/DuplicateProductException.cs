namespace ProductManagement.Models
{
    public class DuplicateProductException : Exception
    {
        public override string Message
        {
            get
            {
                return Constants.DUPLICATEPRODUCTEXCEPTIONMESSAGE;
            }
        }
    }
}
