namespace ProductManagement.Models
{
    public class ProductNotFoundException : Exception
    {
        public override string Message
        {
            get
            {
                return Constants.PRODUCTNOTFOUNDEXCEPTIONMESSAGE;
            }
        }
    }
}
