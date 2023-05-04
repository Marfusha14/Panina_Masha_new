namespace BackendApi.Contracts.Basket
{
    public class GetBasketRequest
    {
        public int UserIdd { get; set; }
        public int ProductId { get; set; }
        public int QuantityOfGoods { get; set; }
    }
}
