namespace Shop.Api.Business
{
    public static class BusinessErrors
    {
        public static BusinessError B001BasketNotFound = new BusinessError.NotFound { Code = "B001", Message = "The requested basket was not found" };
        public static BusinessError P001ProductNotFound = new BusinessError.NotFound { Code = "P001", Message = "The requested product was not found" };
        public static BusinessError I001BasketItemNotFound = new BusinessError.NotFound { Code = "I001", Message = "The requested basket item was not found" };
        public static BusinessError S001InsufficientStock = new BusinessError { Code = "S001", Message = "Not enough products in stock" };
        public static BusinessError S002TooManyReserved = new BusinessError { Code = "S002", Message = "Reserved quantity cannot exceed stock" };
    }
}
