namespace BackendApi.Contracts.Basket
{
    public class CreateBasketRequest
    {
       
        public int ProductId { get; set; }
        public int QuantityOfGoods { get; set; }
    }
}
