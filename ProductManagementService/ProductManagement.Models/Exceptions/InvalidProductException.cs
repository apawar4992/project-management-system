namespace ProductManagement.Models
{
    public class InvalidProductException : Exception
    {
        public override string Message
        {
            get
            {
                return Constants.INVALIDPRODUCTEXCEPTIONMESSAGE;
            }
        }
    }
}
