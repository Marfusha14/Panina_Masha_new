namespace BackendApi.Contracts.Filterr
{
    public class CreateFilterrRequest
    {
       
        public bool ProductAvailability { get; set; }
        public string ReleaseForm { get; set; } = null!;
        public bool AvailabilityOfDiscounts { get; set; }
        public int? Discounts { get; set; }
        public string VacationFromThePharmacy { get; set; } = null!;
        public string Indications { get; set; } = null!;
        public string Producer { get; set; } = null!;
        public string ExpirationDate { get; set; } = null!;
        public string BrandName { get; set; } = null!;
        public int QuantityInPack { get; set; }
        public decimal Price { get; set; }
    }
}
