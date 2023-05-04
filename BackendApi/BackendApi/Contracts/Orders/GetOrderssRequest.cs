namespace BackendApi.Contracts.Orders
{
    public class GetOrderssRequest
    {
        public int OrderNumber { get; set; }
        public int UserIdd { get; set; }
        public int NumberProduct { get; set; }
        public DateTime DateReferences { get; set; }
        public string Statuss { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
