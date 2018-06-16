namespace Shop.Api.Business
{
    public class BusinessError
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public class NotFound : BusinessError
        {
        }
    }
}