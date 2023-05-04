namespace BackendApi.Contracts.SavedAdress
{
    public class CreateSavedAdressRequest
    {
        public int AddressId { get; set; }
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public int House { get; set; }
        public int Construction { get; set; }
        public int Flat { get; set; }
        public string AddressName { get; set; } = null!;
    }
}
